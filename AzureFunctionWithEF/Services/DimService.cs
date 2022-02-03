using AzureFunctionWithEF.Common.Exceptions;
using AzureFunctionWithEF.Repositories;
using AzureFunctionWithEF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionWithEF.Services
{
    public class DimService: IDimService
    {
        private readonly IDimAccountRepository _dimAccountRepo;
        private readonly IGetConnections _connections;
        public DimService(
            IDimAccountRepository dimAccountRepo,
            IGetConnections connections)
        {
            _dimAccountRepo = dimAccountRepo;
            _connections = connections;
        }

        /// <summary>
        /// Return all list values
        /// </summary>
        /// <returns></returns>
        public Task<List<string>> ReturnListOfAccounts()
        {
            try
            {
                return Task.Factory.StartNew(() =>
                {
                    string cnxString = _connections.GetConnectionString();
                    var listOfCountries = _dimAccountRepo.ReturnMissingListFromSql(ref cnxString);

                    if (listOfCountries == null)
                    {
                        throw new NullObjectException("No list of objects were found");
                    }
                    else
                    {
                        return listOfCountries;
                    }
                });
            }
            catch (Exception ex)
            {
                throw new ArgumentException();
            }
        }
    }
}
