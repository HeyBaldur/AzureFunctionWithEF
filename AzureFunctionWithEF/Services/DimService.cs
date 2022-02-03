using AzureFunctionWithEF.Common.Exceptions;
using AzureFunctionWithEF.Repositories;
using AzureFunctionWithEF.Services.Interfaces;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<DimService> _logger;
        public DimService(
            IDimAccountRepository dimAccountRepo,
            IGetConnections connections,
            ILogger<DimService> logger)
        {
            _dimAccountRepo = dimAccountRepo;
            _connections = connections;
            _logger = logger;
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
                    var listOfCountries = _dimAccountRepo.ReturnMissingListFromSql(ref cnxString, out int sqlValue);

                    switch (sqlValue)
                    {
                        case 1:
                            _logger.LogInformation("There available values in the request");
                            break;
                        case 0:
                            _logger.LogWarning("No values to return");
                            break;
                        default:
                            break;
                    }

                    if (listOfCountries == null && sqlValue == 0)
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
