using AzureFunctionWithEF.Common.Data;
using AzureFunctionWithEF.Common.Models;
using System;
using System.Threading.Tasks;

namespace AzureFunctionWithEF.Services
{
    public class DimAccountService: IDimAccountService
    {
        private readonly MyDbContext _dbContext;
        public DimAccountService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DimAccount> Add(DimAccount dim)
        {
            try
            {
                await _dbContext.AddAsync(dim);
                await _dbContext.SaveChangesAsync();

                return dim;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
