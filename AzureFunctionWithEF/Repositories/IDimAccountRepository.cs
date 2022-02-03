using AzureFunctionWithEF.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureFunctionWithEF.Repositories
{
    public interface IDimAccountRepository
    {
        List<string> ReturnMissingListFromSql(ref string cnxString, out int sqlValue);
        Task<List<DimAccount>> ReturnMissingFields();
    }
}