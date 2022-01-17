using AzureFunctionWithEF.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionWithEF.Test.Data
{
    public static class MockData
    {
        public static Task<DimAccount> ReturnDimAccountMockData()
        {
            return Task.Factory.StartNew(() =>
            {
                return new DimAccount 
                { 
                    AccountKey = 1, 
                    AccountDescription = "Some description" 
                };
            });
        }
    }
}
