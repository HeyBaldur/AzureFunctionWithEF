using AzureFunctionWithEF.Common.Models;
using AzureFunctionWithEF.Services;
using AzureFunctionWithEF.Test.Data;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionWithEF.Test.Services.Test
{
    [TestFixture]
    public class DimServicesTest
    {
        private Mock<IDimAccountService> _dimServiceMock;
        [SetUp]
        public void SetUp()
        {
            _dimServiceMock = new Mock<IDimAccountService>();

            #region Initializers
            var mockData = MockData.ReturnDimAccountMockData();
            _dimServiceMock
                .Setup(dm => dm.Add(It.IsAny<DimAccount>()))
                .Returns(mockData);
            #endregion
        }

        [Test]
        public async Task Add_TestAsync_ReturnADimAccount()
        {

            var accnt = new DimAccount
            {
                AccountKey = 1,
                ParentAccountKey = 1,
                AccountCodeAlternateKey = 1140,
                ParentAccountCodeAlternateKey = 1120,
                AccountDescription = "Other Receivables [Rodolfo]",
                AccountType = "StoreProcedure"
            };

            var opObject = _dimServiceMock.Object;
            var result = await opObject.Add(accnt);
            Assert.AreEqual(accnt.AccountKey, result.AccountKey);
        }

        //[Test]
        //public void Add_ThrowArgumenNullException()
        //{
        //    var opObject = _dimServiceMock.Object;
        //    var arrange = opObject.Add(null).Result;
        //    Assert.That(() => opObject.Add(null), Throws.Exception);
        //}


    }
}
