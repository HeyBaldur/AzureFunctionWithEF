using AzureFunctionWithEF.Services.Interfaces;
using System;

namespace AzureFunctionWithEF.Services
{
    public class GetConnections : IGetConnections
    {
        /// <summary>
        /// Return connection string from database
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            return Environment.GetEnvironmentVariable("SqlConnectionString");
        }
    }
}
