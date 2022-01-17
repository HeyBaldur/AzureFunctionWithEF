using AzureFunctionWithEF.Common.Models;
using System.Threading.Tasks;

namespace AzureFunctionWithEF.Services
{
    public interface IDimAccountService
    {
        public Task<DimAccount> Add(DimAccount dim);
    }
}