using AzureFunctionWithEF.Common.Data;
using AzureFunctionWithEF.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionWithEF.Test.Repositories
{
    public class DimAccountRepositoryTest
    {
        private Mock<IDimAccountRepository> _dimAccountMock;
        private Mock<DbContext> _dbContextMock;
        private Mock<MyDbContext> _dbContext;

        [SetUp]
        public void SetUp()
        {
            _dimAccountMock = new Mock<IDimAccountRepository>();
            _dbContextMock = new Mock<DbContext>();
            _dbContext = new Mock<MyDbContext>();
        }

        [Test]
        [Ignore("This is a repository, it should not be tested")]
        public void ReturnMissingListFromSql_Test()
        {
            var myRepo = new DimAccountRepository(_dbContext.Object);
            
            var configToken = ConfigureEnvironmentVariablesFromLocalSettings();

            string connectionString = ReturnConnectionString(configToken);

            var result = myRepo.ReturnMissingListFromSql(ref connectionString);

            Assert.IsNotNull(result);
        }

        #region Private methods
        private static string ReturnConnectionString(IEnumerable<JToken> jTokenValues)
        {
            var cnxString = string.Empty;
            for (int i = 0; i < jTokenValues.Count(); i++)
            {
                var str1 = jTokenValues.ElementAt(i);

                foreach (JProperty attributeProperty in str1)
                {
                    if (attributeProperty.Name == "SqlConnectionString")
                    {
                        var attribute = str1[attributeProperty.Name];
                        cnxString = attribute.ToString();
                    }
                }
            }

            return cnxString;
        }

        private static IEnumerable<JToken> ConfigureEnvironmentVariablesFromLocalSettings()
        {
            var path = Path.GetDirectoryName(typeof(DimAccountRepository)
                .Assembly.Location); 
            var json = File.ReadAllText(Path.Join(path, "local.settings.json"));
            var parsed = Newtonsoft.Json.Linq.JObject.Parse(json).Values();

            return parsed;
        }
        #endregion
    }
}
