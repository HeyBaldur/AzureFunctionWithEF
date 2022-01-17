using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureFunctionWithEF.Services
{
    public interface IDimService
    {
        Task<List<string>> ReturnListOfAccounts();
    }
}