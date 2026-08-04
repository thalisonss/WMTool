# Gerador de Script INSERT (JSON → SQL)

Caminho inverso do "Reprocessar JSON": em vez de pegar dados do banco e montar um JSON, essa tela
pega um **JSON** de entrada e gera o **texto** de um script SQL de `INSERT` para popular tabelas —
o mesmo trabalho que antes era feito manualmente escrevendo `JSON_VALUE(...)` à mão.

A ferramenta **nunca executa o INSERT**. Ela só gera o texto do script; rodar fica por conta do
usuário, em outra ferramenta (SSMS etc.). Isso não é só uma escolha de design — a camada de acesso a
dados do projeto bloqueia qualquer query com `INSERT`, `DELETE` ou `DROP`, então mesmo as queries de
resolução (Query Geral/Customizada) só podem ser `SELECT`.

> Os exemplos desta documentação usam um esquema **fictício** (tabelas/colunas com prefixo `ACME_`)
> só para ilustrar o mecanismo — não correspondem a nenhuma tabela real de um cliente específico.

## Onde fica cada coisa

| Peça | Arquivo |
|---|---|
| Modelos (regras) | `InsertScript/Models/*.cs` |
| Motor de geração | `InsertScript/InsertScriptGenerationEngine.cs` |
| Resolução de SQL parametrizado | `InsertScript/GenericSqlParameterResolver.cs` |
| Save/Load do arquivo de regras | `InsertScript/InsertScriptRuleSetStore.cs` |
| Tela principal | `Screens/ucInsertScriptGenerator.cs` (+ `.Designer.cs`) |
| Editor de colunas de uma tabela | `Screens/frmTableColumnsEditor.cs` (+ `.Designer.cs`) |
| Aba no app | `Screens/frmHomeScreen.*` → aba "Gerar Script INSERT" |
| Arquivo de regras em uso | caminho configurável na aba Settings, campo "Gerador de Script INSERT — Regras" |

---

## Conceitos

### 1. Parâmetros globais — "Localizar/Substituir"

Grid no topo da tela (`dgvParameters`). Cada linha é um par `Nome` → `Valor`, editável a qualquer
momento antes de gerar o script. Servem como ponto de partida do **pool de parâmetros** de cada
linha do INSERT (ver seção 5). Não são obrigatórios — uma tabela pode não usar nenhum parâmetro
global, se conseguir tudo que precisa a partir do próprio JSON (ver "JSON Path" abaixo).

Na coluna "Localizar" você pode digitar com ou sem chaves (`{NewOrderId}` ou `NewOrderId`) — a
ferramenta normaliza removendo as chaves ao sincronizar.

### 2. Regra de tabela (`TableInsertRule`)

Cada linha da grid de tabelas (`dgvTables`) descreve **um** `INSERT INTO <tabela>`:

| Campo | Significado |
|---|---|
| `TableName` | nome da tabela de destino |
| `Enabled` | se desmarcado, a tabela é ignorada na geração (sem gerar nem um comentário) |
| `RowSourceType` | `SingleRow` ou `JsonArray` (ver seção 4) |
| `ArrayJsonPath` | só usado em `JsonArray`: caminho do array no JSON (ex.: `$.order_batch.order_batch.Document[0].items`) |
| `GeneralSqlTemplate` | a "Query Geral" da tabela — opcional, ver seção 9 |
| `Columns` | lista de `ColumnRule`, uma por coluna do INSERT (editada em janela separada, botão "Editar Colunas") |

### 3. Coluna (`ColumnRule`) e as 5 fontes de valor

Cada coluna tem uma **Fonte** (`ColumnValueSourceType`):

| Fonte | Quando usar | Toca o banco? | Alimenta o pool? |
|---|---|---|---|
| **Literal** | expressão SQL crua: `NULL`, `GETDATE()`, `1`, `'0'` — vai pro INSERT **sem aspas/escape automático** | não | não |
| **JsonPath** | lê um valor do JSON de entrada | não | sim |
| **Parameter** | lê um valor já existente no pool (parâmetro global, `RowIndex`/`RowNumber`, ou outra coluna já resolvida) | não | sim |
| **GeneralResult** | lê uma coluna do resultado da *Query Geral* da tabela (1 SELECT, várias colunas — roda 1x por linha) | sim | sim |
| **CustomSql** | roda uma query dedicada só pra essa coluna, com `{placeholders}` do pool | sim | sim |

Campos específicos de cada fonte, todos na mesma `ColumnRule` (só o relevante pro tipo escolhido é
usado):

- `JsonPath` → caminho (JsonPath) usado quando `SourceType = JsonPath`
- `ParameterName` → nome do parâmetro usado quando `SourceType = Parameter`
- `SqlTemplate` → SQL usado quando `SourceType = CustomSql`
- `ResultColumn` → nome da coluna a ler no resultado da Query Geral, usado quando `SourceType = GeneralResult` (vazio = usa o próprio nome da coluna)
- `LiteralValue` → expressão usada quando `SourceType = Literal`
- `IncludeInInsert` → `true` (padrão) inclui a coluna no INSERT final; `false` faz a coluna resolver e
  alimentar o pool **sem aparecer** na lista de colunas/valores do INSERT (ver seção 8)

**Exemplo** (tabela fictícia `ACME_InvoiceItem`, uma linha por item de um array):

```json
{ "ColumnName": "cIDProduct", "SourceType": "JsonPath", "JsonPath": "productCode" },
{ "ColumnName": "nSeq",       "SourceType": "CustomSql", "SqlTemplate": "SELECT {RowNumber} * 10" },
{ "ColumnName": "xUnitType",  "SourceType": "CustomSql",
  "SqlTemplate": "SELECT TOP 1 xUnitTypeDefault FROM ACME_Product WHERE cIDProduct = {cIDProduct} AND cIDCompany = {cIDCompany}" }
```

### 4. `SingleRow` vs `JsonArray`

- **SingleRow**: gera **1** linha de INSERT, usando o JSON raiz inteiro como "a linha". Uso típico:
  tabelas de cabeçalho (ex.: `ACME_Invoice`, `ACME_Order`).
- **JsonArray**: gera **N** linhas, uma por item de um array do JSON (`ArrayJsonPath`). Uso típico:
  tabelas de item/detalhe (ex.: `ACME_InvoiceItem`, `ACME_OrderItem`).

Em `JsonArray`, um `JsonPath` de coluna é resolvido **relativo ao item** (ex.: `"productCode"`, não
`"$.items[0].productCode"`). Isso significa que, de dentro de um item, você **não alcança** campos do
topo do documento (identificadores do documento, totais gerais) com um JsonPath comum — é exatamente
pra isso que existe o prefixo `root:` (seção 6).

### 5. O pool de parâmetros da linha (mecanismo central)

Cada linha do INSERT (a única linha de uma tabela `SingleRow`, ou cada item de uma `JsonArray`) tem
seu próprio dicionário de parâmetros, resolvido em **duas passadas**
(`InsertScriptGenerationEngine.BuildInsertStatementAsync`):

1. **Passada 1** — `Literal`, `JsonPath` e `Parameter` (não tocam banco). Toda coluna `JsonPath`/
   `Parameter` que resolve grava seu valor no pool **sob o nome da própria coluna** — é isso que
   deixa `{NomeDaColuna}` disponível pras queries da passada 2. `Literal` **não** entra no pool (é
   uma expressão crua, não um valor).
2. **Passada 2** — `GeneralResult` e `CustomSql`, já com o pool cheio da passada 1. Toda query dessas
   é montada trocando `{nome}` por `@nome` (parâmetro real do ADO.NET, nunca concatenação de texto)
   e rodada contra o banco.

Importante: **o pool é por tabela e por linha** — não atravessa pra outra tabela da mesma regra, nem
de um item do array pro próximo. Se duas tabelas precisam do mesmo dado (ex.: `cIDCompany`), cada
uma precisa da sua própria coluna/parâmetro.

Um `{nome}` que nenhuma coluna/parâmetro dessa tabela produziu não trava a geração: vira aviso
(`Tabela X, coluna Y: ...`) e a coluna cai pra `NULL`.

### 6. `root:` — JSON Path a partir da raiz do documento

Prefixo especial: `"root:$.doc_params.cIDCompany"`. Só faz diferença em tabelas `JsonArray` — troca o
contexto de resolução do **item atual** para o **documento inteiro**. Necessário sempre que uma
coluna de uma tabela de item precisa de um valor de cabeçalho (identificadores do documento, peso
total do envio, etc.), porque um JsonPath comum (`"$.foo"`) dentro de um item do array não alcança o
resto do documento (a biblioteca de JSON trata `$` como "o próprio token em que a busca foi chamada",
não a raiz absoluta).

Em tabela `SingleRow` o prefixo é opcional/inofensivo — a "linha" já É o documento raiz.

**Exemplo**: dentro de `ACME_InvoiceItem` (JsonArray), pra ler o peso total do envio (que fica no
cabeçalho do documento, não em cada item):

```json
{ "ColumnName": "nWeight", "SourceType": "JsonPath",
  "JsonPath": "root:$.order_batch.order_batch.Document[0].shipping.grossWeight" }
```

### 7. `{RowIndex}` / `{RowNumber}`

Dois parâmetros automáticos, sempre presentes no pool, **sem precisar declarar nenhuma coluna**:

- `RowIndex` — posição da linha no array, começando em 0 (sempre `0` em tabela `SingleRow`)
- `RowNumber` — o mesmo, começando em 1

Uso típico: montar um `nSeq` sequencial por item — `SELECT {RowNumber} * 10` gera 10, 20, 30...

### 8. `IncludeInInsert` — coluna auxiliar

Quando uma tabela precisa de um valor do JSON só pra **alimentar outra query** (não pra ser inserido
como coluna real, porque a tabela nem tem essa coluna), declare uma `ColumnRule` normal (JsonPath,
por exemplo) e marque **`IncludeInInsert = false`** (checkbox "Inserir?" desmarcado na grid). Ela
resolve e entra no pool normalmente, mas some da lista de colunas/valores do INSERT final.

**Exemplo** (`ACME_InvoiceExt` não tem coluna `cIDTripCode`, mas precisa dela só pra achar o
motorista/entregador numa tabela auxiliar):

```json
{ "ColumnName": "cIDTripCode", "SourceType": "JsonPath", "JsonPath": "$.doc_params.cIDTripCode", "IncludeInInsert": false },
{ "ColumnName": "cIDDriverCode", "SourceType": "CustomSql",
  "SqlTemplate": "SELECT TOP 1 cDriverCode FROM ACME_DriverTrip WHERE cIDTripCode = {cIDTripCode} AND cIDCompany = {cIDCompany}" }
```

### 9. Query Geral (`GeneralSqlTemplate`) — "bundle" de várias colunas

Uma tabela pode ter **uma única** Query Geral, rodada **uma vez por linha** (cacheada — mesmo que
várias colunas usem `GeneralResult`, a query só roda uma vez). Ideal quando várias colunas vêm da
mesma origem (evita repetir a mesma consulta várias vezes como `CustomSql`).

**Exemplo** (`ACME_Order`, uma query alimentando 4 colunas de uma vez, combinando duas tabelas
auxiliares com `CROSS JOIN` porque cada lado já é garantidamente 1 linha):

```sql
SELECT TOP 1
  CR.xRegion AS xRegion,
  CR.xDistributionChannel AS xDistributionChannel,
  CR.xPaymentTerm AS xPaymentTerm,
  DT.cDriverCode AS cDriverCode
FROM ACME_CustomerRegion CR
CROSS JOIN ACME_DriverTrip DT
WHERE CR.cIDCompany = {cIDCompany} AND CR.cIDCustomer = {cIDCustomer}
  AND CR.cIDBranch = {cIDBranch} AND CR.mc1Enabled = 1
  AND DT.cIDTripCode = {cIDTripCode} AND DT.cIDCompany = {cIDCompany}
```

Cada coluna então só declara `SourceType = GeneralResult` + `ResultColumn` (o nome da coluna no
resultado dessa query).

### 10. Como o valor final é escrito no SQL (quoting)

- `Literal`: escrito **exatamente como digitado**, sem aspas nem escape — por isso serve pra `NULL`,
  `GETDATE()`, números (`1`, `0`) ou um texto já entre aspas (`'0'`, `'ABC123'`).
- `JsonPath` / `Parameter` / `GeneralResult` / `CustomSql`: o valor resolvido é sempre escrito como
  **literal de texto** entre aspas simples, com `'` interno duplicado (`O'Brien` → `'O''Brien'`).
  `NULL` (de verdade, não a string `"NULL"`) vira a palavra `NULL` sem aspas. O SQL Server converte
  implicitamente esse texto pro tipo real da coluna (número, data, etc.) na maioria dos casos.

---

## Passo a passo: criando uma regra do zero

1. Cole/carregue o JSON de entrada (`txtJson` ou "Carregar arquivo...").
2. (Opcional) cadastre parâmetros globais na grid "Localizar/Substituir", se precisar de valores que
   não estão no JSON (ex.: um ID gerado manualmente).
3. Clique "Adicionar Tabela", digite o nome real da tabela, escolha o Modo (`SingleRow`/`JsonArray`)
   e, se for `JsonArray`, o caminho do array.
4. Clique "Colunas" → "Editar Colunas": adicione uma linha por coluna da tabela de destino, na ordem
   que quiser que apareçam no INSERT. Pra cada uma, escolha a Fonte e preencha só o campo relevante.
5. Se alguma coluna depende de um valor de cabeçalho dentro de uma tabela `JsonArray`, use
   `JsonPath` com prefixo `root:`.
6. Se um valor do JSON não corresponde a nenhuma coluna real da tabela (só serve de apoio pra outra
   query), declare-o mesmo assim e desmarque "Inserir?".
7. Se várias colunas dependem da mesma consulta no banco, configure a "Query Geral" da tabela
   (botão "Configurar" na grid de tabelas) em vez de repetir `CustomSql` em cada uma.
8. Clique "Gerar Script INSERT". Confira o SQL gerado e a caixa de avisos — todo `NULL` inesperado ou
   aviso de "parâmetro/caminho não encontrado" é sinal de alguma coluna ainda sem regra certa.
9. "Salvar Regras..." grava tudo (parâmetros + tabelas + colunas) num `.json` portátil. "Salvar
   Script (.sql)..." grava só o SQL gerado.

O caminho configurado em Settings ("Gerador de Script INSERT — Regras") é carregado automaticamente
toda vez que a aba abre — não precisa clicar em "Carregar Regras..." se as regras já estão nesse
arquivo.

---

## O que a ferramenta permite

- Gerar `INSERT` pra quantas tabelas quiser numa mesma regra, cada uma com seu próprio modo (linha
  única ou array).
- Combinar livremente as 5 fontes de valor dentro da mesma tabela, inclusive fazendo uma coluna
  depender do valor já resolvido de outra (na mesma linha).
- Ler valores tanto do item de um array quanto da raiz do documento (`root:`), mesmo dentro de uma
  tabela `JsonArray`.
- Rodar consultas reais no banco (somente leitura) pra resolver valores que não estão no JSON —
  lookups, contagens (`COUNT`/`OPENJSON`), concatenação de texto, cálculo de data, etc. — qualquer
  `SELECT` válido.
- Reaproveitar uma mesma consulta pra várias colunas de uma vez (Query Geral).
- Expor um valor do JSON como parâmetro sem ele virar uma coluna do INSERT (`IncludeInInsert=false`).
- Usar um parâmetro digitado manualmente (grid Localizar/Substituir) direto como valor de uma coluna,
  sem tocar o banco (`Parameter`).
- Salvar/carregar o conjunto de regras inteiro num arquivo `.json`, e configurar um caminho padrão
  que carrega sozinho ao abrir a aba.

## O que NÃO permite / limitações conhecidas

- **Nunca executa o INSERT.** Só gera o texto — rodar é manual, em outra ferramenta. Não é possível
  fazer o app aplicar o script direto no banco (bloqueado deliberadamente na camada de acesso a
  dados do projeto).
- **Toda Query Geral/Customizada precisa ser `SELECT`.** `INSERT`/`DELETE`/`DROP` são bloqueados pelo
  mesmo motivo acima — nem funcionaria como query de resolução.
- **O pool de parâmetros não atravessa tabelas nem linhas.** Um valor resolvido numa coluna só existe
  pras outras colunas *da mesma tabela* e *da mesma linha* (mesmo item do array). Pra reaproveitar
  entre tabelas diferentes, cada uma precisa da sua própria coluna/parâmetro apontando pro mesmo
  lugar.
- **Toda `ColumnRule` declarada vira coluna do INSERT, a menos que `IncludeInInsert=false`.** Não dá
  pra ter uma coluna "só de leitura" sem marcar explicitamente essa flag.
- **String vazia (`""`) do JSON não vira `NULL` automaticamente.** Se o campo JSON existir mas
  estiver em branco, o valor gerado é uma string vazia (`''`), não `NULL` — o que **quebra a execução
  real do INSERT** se a coluna de destino for numérica/data (SQL Server não converte `''` pra
  número/data implicitamente). Onde isso importa, usar uma coluna auxiliar (`IncludeInInsert=false`)
  + `CustomSql` com `ISNULL(NULLIF({valor}, ''), 0)` (ou o literal apropriado). Exemplo:

  ```json
  { "ColumnName": "ShippingChargeRaw", "SourceType": "JsonPath", "JsonPath": "shippingCharge", "IncludeInInsert": false },
  { "ColumnName": "nValueShip", "SourceType": "CustomSql",
    "SqlTemplate": "SELECT ISNULL(NULLIF({ShippingChargeRaw}, ''), 0)" }
  ```

- **`JsonPath` de uma tabela `JsonArray` nunca alcança a raiz sozinho** — precisa do prefixo `root:`
  explicitamente; esquecer o prefixo resulta em "caminho não encontrado" (aviso) e `NULL`.
- **Tabela `JsonArray` gera 1 `INSERT` por item — sem agrupar em `VALUES` múltiplos.** Pra um array
  com 50 itens, o script tem 50 linhas de `INSERT` separadas (não é um único `INSERT ... VALUES
  (...), (...), ...`). Funcionalmente equivalente, só mais texto.
- **Nenhuma verificação de tipo/schema real da tabela.** A ferramenta não sabe os tipos das colunas
  do banco — quem garante que o valor gerado é compatível é quem escreveu a regra (por isso vale
  sempre validar o script gerado contra uma linha real já existente, comparando coluna a coluna,
  antes de confiar na regra).
- **Sem suporte a colunas com nomes repetidos numa mesma tabela** — cada `ColumnName` deve ser único
  dentro da mesma `TableInsertRule` (senão a última sobrescreve o pool da anterior).

---

## Exemplo completo de regra

Duas tabelas fictícias ilustrando os casos mais comuns: uma tabela de ligação simples (`SingleRow`,
só `JsonPath`) e uma tabela de item (`JsonArray`, combinando `root:`, `{RowNumber}` e `CustomSql`).

```json
{
  "Parameters": [],
  "Tables": [
    {
      "TableName": "ACME_OrderInvoice",
      "Enabled": true,
      "RowSourceType": "SingleRow",
      "ArrayJsonPath": null,
      "GeneralSqlTemplate": null,
      "Columns": [
        { "ColumnName": "cIDCompany",       "SourceType": "JsonPath", "JsonPath": "$.doc_params.cIDCompany" },
        { "ColumnName": "cIDInvoiceNumber", "SourceType": "JsonPath", "JsonPath": "$.doc_params.cIDInvoiceNumber" },
        { "ColumnName": "cIDOrderNumber",   "SourceType": "JsonPath", "JsonPath": "$.doc_params.cIDOrderNumber" },
        { "ColumnName": "mc1Enabled",       "SourceType": "Literal",  "LiteralValue": "1" },
        { "ColumnName": "mc1LastUpdate",    "SourceType": "Literal",  "LiteralValue": "GETDATE()" }
      ]
    },
    {
      "TableName": "ACME_InvoiceItem",
      "Enabled": true,
      "RowSourceType": "JsonArray",
      "ArrayJsonPath": "$.order_batch.order_batch.Document[0].items",
      "GeneralSqlTemplate": null,
      "Columns": [
        { "ColumnName": "cIDCompany", "SourceType": "JsonPath", "JsonPath": "root:$.doc_params.cIDCompany" },
        { "ColumnName": "cIDProduct", "SourceType": "JsonPath", "JsonPath": "productCode" },
        { "ColumnName": "nSeq",       "SourceType": "CustomSql", "SqlTemplate": "SELECT {RowNumber} * 10" },
        { "ColumnName": "nAmount",    "SourceType": "JsonPath", "JsonPath": "quantity" },
        { "ColumnName": "nUnitValue", "SourceType": "JsonPath", "JsonPath": "unitPrice" },
        { "ColumnName": "nWeight",    "SourceType": "JsonPath", "JsonPath": "root:$.order_batch.order_batch.Document[0].shipping.grossWeight" },
        { "ColumnName": "xUnitType",  "SourceType": "CustomSql",
          "SqlTemplate": "SELECT TOP 1 xUnitTypeDefault FROM ACME_Product WHERE cIDProduct = {cIDProduct} AND cIDCompany = {cIDCompany}" },
        { "ColumnName": "mc1Enabled",    "SourceType": "Literal", "LiteralValue": "1" },
        { "ColumnName": "mc1LastUpdate", "SourceType": "Literal", "LiteralValue": "GETDATE()" }
      ]
    }
  ]
}
```

Sobre o JSON de entrada correspondente:

```json
{
  "doc_params": {
    "cIDCompany": "1060",
    "cIDInvoiceNumber": "167",
    "cIDOrderNumber": "934320260724184413"
  },
  "order_batch": {
    "order_batch": {
      "Document": [
        {
          "shipping": { "grossWeight": "44.5" },
          "items": [
            { "productCode": "ABC123", "quantity": "1.0", "unitPrice": "2650.0" }
          ]
        }
      ]
    }
  }
}
```

... esse par gera:

```sql
-- ACME_OrderInvoice (1 registro(s))
INSERT INTO ACME_OrderInvoice (cIDCompany, cIDInvoiceNumber, cIDOrderNumber, mc1Enabled, mc1LastUpdate)
VALUES ('1060', '167', '934320260724184413', 1, GETDATE());

-- ACME_InvoiceItem (1 registro(s))
INSERT INTO ACME_InvoiceItem (cIDCompany, cIDProduct, nSeq, nAmount, nUnitValue, nWeight, xUnitType, mc1Enabled, mc1LastUpdate)
VALUES ('1060', 'ABC123', '10', '1.0', '2650.0', '44.5', 'EA', 1, GETDATE());
```
