# WMTool

Ferramenta desktop em **Windows Forms (.NET Framework 4.8)** para apoiar operações de conferência e gestão de CECs, com integração em **SQL Server**, **AWS S3** e chamadas HTTP para sincronização.

> Projeto focado em uso interno/operacional, com telas para configuração, consulta, comparação de notas e download de documentos.

---

## 📌 Visão geral

O WMTool centraliza tarefas que normalmente seriam manuais e distribuídas em várias ferramentas:

- consulta de dados no banco de dados;
- comparação de registros para identificar notas sem CEC;
- geração de CSV com resultados;
- busca e download de CECs no bucket S3;
- disparo de requisições de sincronização via API;
- verificação automática de novas versões no GitHub Releases.

---

## ✅ Funcionalidades principais

### 1) Configurações gerais
Na aba **Settings** é possível salvar:

- credenciais e nome do bucket S3;
- dados de conexão com banco SQL Server;
- diretórios locais para salvar:
  - CSV de saída,
  - CSV de exceção de viagens,
  - CECs baixadas.

### 2) CEC not exists in the bucket
Fluxo para identificar e exportar documentos com inconsistência de CEC:

- executa uma query SQL (somente `SELECT`);
- compara dados retornados com os arquivos disponíveis no bucket;
- mostra progresso da validação;
- exibe resultados na grade;
- exporta relatório em CSV;
- opção de considerar arquivo de **exceção de viagens** para ignorar trips específicas.

### 3) Search CEC
Fluxo para pesquisa e download de CECs:

- executa uma query SQL para listar CECs;
- permite selecionar/desmarcar itens em lote;
- baixa itens selecionados para pasta local;
- permite abrir imagem, salvar imagem e abrir pasta de destino.

### 4) Request
Tela para integração HTTP:

- autentica via endpoint de token;
- executa endpoint de sync com payload configurado;
- permite salvar usuário, senha, domínio, ambiente, URL de token e URL de sync;
- mostra status e timestamp da última execução.

### 5) Atualização automática
Ao abrir o app, o WMTool consulta a release mais recente no GitHub (`thalisonss/WMTool`) e informa quando há versão nova disponível.

---

## 🧱 Stack e dependências

- **.NET Framework 4.8**
- **Windows Forms**
- **AWS SDK S3** (`AWSSDK.S3`)
- **Newtonsoft.Json**
- **SQL Server (System.Data.SqlClient)**
- **Microsoft Office Interop Excel** (referenciado no projeto)

---

## 🚀 Como executar localmente

## 1. Pré-requisitos

- Windows com .NET Framework 4.8
- Visual Studio 2019+ (com suporte a .NET Framework)
- Acesso à rede/corporativo para:
  - SQL Server
  - AWS S3
  - endpoints HTTP (token/sync)

## 2. Clonar o repositório

```bash
git clone <url-do-repositorio>
cd WMTool
```

## 3. Restaurar pacotes NuGet

No Visual Studio:

- botão direito na solução `WMTool.sln` → **Restore NuGet Packages**.

Ou via console (se disponível no ambiente):

```bash
nuget restore WMTool.sln
```

## 4. Configurar o aplicativo

No primeiro uso, abra a aba **Settings** e salve os campos necessários.

### Campos de configuração

| Grupo | Campo | Descrição |
|---|---|---|
| Banco de dados | Server | Nome/endereço do SQL Server |
| Banco de dados | Database | Nome do banco |
| Bucket | Access Key | Chave de acesso AWS |
| Bucket | Secret Key | Chave secreta AWS |
| Bucket | Bucket Name | Nome do bucket S3 |
| Diretórios | Pasta CSV | Destino dos relatórios de comparação |
| Diretórios | Pasta Trip Exception CSV | Arquivo de exceções de viagens |
| Diretórios | Pasta CECs | Destino de CECs baixadas |
| Request | Domain | Domínio da integração |
| Request | Environment | Ambiente (ex.: prod/hml) |
| Request | URL Token | Endpoint de autenticação |
| Request | URL Sync | Endpoint de sincronização |
| Request | Usuário/Senha | Credenciais da API |

> As configurações são persistidas em `userSettings`.

## 5. Executar

- Defina o projeto `WMTool` como startup;
- execute com **F5** (Debug) ou **Ctrl+F5**.

---

## 🔒 Regras de segurança da query SQL

A camada de negócio valida a query antes de executar:

- aceita apenas consultas que começam com `SELECT`;
- bloqueia palavras consideradas perigosas (`DROP`, `DELETE`, `--`, `INSERT`).

Se a query violar as regras, a execução é interrompida com erro.

---

## 📁 Estrutura do projeto

```text
WMTool/
├── Program.cs
├── WMTool.csproj
├── Screens/
│   ├── frmHomeScreen.cs
│   └── frmHomeScreen.Designer.cs
├── Business/
│   └── WMBusiness.cs
├── Databases/
│   └── Implements/
│       └── WMDatabase.cs
├── Integrations/
│   ├── WMApiClient.cs
│   ├── WMApiService.cs
│   └── Models/
│       └── TokenResponse.cs
└── Utils/
    ├── GitHubRelease.cs
    └── LogError.cs
```

---

## 🛠️ Troubleshooting rápido

- **Não conecta no banco:** valide `Server`, `Database`, usuário da máquina e conectividade de rede/VPN.
- **Não encontra arquivo no bucket:** confira `Bucket Name`, região, credenciais AWS e chave (`cPathCEC`).
- **Exportação CSV falha:** confirme permissão de escrita na pasta configurada.
- **Request falha:** verifique URL de token/sync, payload esperado e credenciais.
- **Erro de versão:** confirme se `Application.ProductVersion` está alinhada com o padrão das tags de release (`vX.Y.Z`).

---

## 🤝 Contribuição

Sugestões de melhoria:

- separar regras de negócio por serviço (S3, DB, Request);
- adicionar testes automatizados para validação de query e geração de CSV;
- padronizar tratamento de erros e logging;
- remover classes duplicadas de `GitHubRelease` e consolidar em `Utils`.

---

## 📄 Licença

Defina aqui a licença do projeto (MIT, Proprietária, etc.) conforme política da sua organização.
