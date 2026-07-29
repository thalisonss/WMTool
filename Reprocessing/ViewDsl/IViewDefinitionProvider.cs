using System.Threading.Tasks;
using WMTool.Reprocessing.ViewDsl.Models;

namespace WMTool.Reprocessing.ViewDsl
{
    // Abstração pra ViewSqlBuilder poder resolver views aninhadas (uma view usada como derived table
    // dentro de outra, ex.: Custom_WM_NF_ProductTot faz JOIN direto com Custom_WM_NF_ProductOrder) sem
    // criar uma dependência circular com WMTool.Reprocessing.Repositories (que já depende de ViewDsl).
    // Implementada por Repositories.ViewRepository.
    public interface IViewDefinitionProvider
    {
        Task<ViewDefinition> GetLatestEnabledAsync(string alias, string viewName, string connectionString);
    }
}
