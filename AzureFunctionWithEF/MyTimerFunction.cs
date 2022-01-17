using AzureFunctionWithEF.Common.Data;
using AzureFunctionWithEF.Common.Models;
using AzureFunctionWithEF.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureFunctionWithEF
{
    public class MyTimerFunction
    {
        private readonly MyDbContext _dbContext;
        // private readonly IDimAccountService _dimService;
        private readonly IDimService _dimService;

        public MyTimerFunction(
            MyDbContext dbContext,
            IDimService dimService)
        {
            _dbContext = dbContext;
            _dimService = dimService;
        }

        [FunctionName("MyTimerFunction")]
        public async Task Run([TimerTrigger("0 */1 * * * *"
            #if DEBUG
            ,RunOnStartup=true
            #endif
            )] TimerInfo myTimer, [DurableClient] IDurableOrchestrationClient starter,
            ILogger log, ExecutionContext context)
        {
            var countryList = await _dimService.ReturnListOfAccounts();

            foreach (var country in countryList)
            {
                log.LogInformation($"Dim account type: {country}");
            }
        }
    }
}
