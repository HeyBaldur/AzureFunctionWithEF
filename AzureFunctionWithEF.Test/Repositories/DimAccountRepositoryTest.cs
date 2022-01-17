using AzureFunctionWithEF.Repositories;
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

        [SetUp]
        public void SetUp()
        {
            _dimAccountMock = new Mock<IDimAccountRepository>();
        }

        [Test]
        public void ReturnMissingListFromSql_Test()
        {
            //List<string> myList = new()
            //{
            //    "Germany"
            //};

            var myRepo = new DimAccountRepository();

            string connectionString = string.Empty;
            
            var mySection = ConfigureEnvironmentVariablesFromLocalSettings();

            for (int i = 0; i < mySection.Count(); i++)
            {
                var str1 = mySection.ElementAt(i);

                foreach (JProperty attributeProperty in str1)
                {
                    if (attributeProperty.Name == "SqlConnectionString")
                    {
                        var attribute = str1[attributeProperty.Name];
                        connectionString = attribute.ToString();
                    }
                }
            }

            var result = myRepo.ReturnMissingListFromSql(connectionString);
        }

        public IEnumerable<JToken> ConfigureEnvironmentVariablesFromLocalSettings()
        {
            var path = Path.GetDirectoryName(typeof(DimAccountRepository)
                .Assembly.Location); var json = 
                    File.ReadAllText(Path.Join(path, "local.settings.json"));
            var parsed = Newtonsoft.Json.Linq.JObject.Parse(json).Values();

            return parsed;
        }
    }
}
