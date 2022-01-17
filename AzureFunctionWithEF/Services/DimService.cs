using AzureFunctionWithEF.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionWithEF.Services
{
    public class DimService: IDimService
    {
        private readonly IDimAccountRepository _dimAccountRepo;
        public DimService(IDimAccountRepository dimAccountRepo)
        {
            _dimAccountRepo = dimAccountRepo;
        }

        public async Task<List<string>> ReturnListOfAccounts()
        {
            string cnxString = string.Empty;
            var listOfCountries = _dimAccountRepo.ReturnMissingListFromSql(cnxString);

            return listOfCountries;
        }

        //public async Task<List<DimAccount>> ReturnListOfAccounts()
        //{
        //    var myAccountList = await _dimAccountRepo.ReturnMissingFields

        //    return myAccountList;
        //}
    }
}
