using AzureFunctionWithEF.Common.Data;
using AzureFunctionWithEF.Common.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AzureFunctionWithEF.Repositories
{
    public class DimAccountRepository: IDimAccountRepository, IDisposable
    {
        private readonly MyDbContext _dbContext;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        bool disposed = false;

        public DimAccountRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Return a list from EF Core 
        /// </summary>
        /// <returns></returns>
        public async Task<List<DimAccount>> ReturnMissingFields()
        {
            string param = "Expenditures";
            var accounts = await _dbContext.DimAccount
                        .FromSqlInterpolated($"EXEC [dbo].[ReturnMissingFields] {param}")
                        .ToListAsync();

            return accounts;
        }

        /// <summary>
        /// Return a missing list from SQL using CNX
        /// </summary>
        /// <param name="cnxString"></param>
        /// <returns></returns>
        public List<string> ReturnMissingListFromSql(ref string cnxString)
        {
            List<string> myList = new List<string>();
            SqlConnection connection;
            SqlDataAdapter adapter;

            SqlCommand command = new SqlCommand();
            SqlParameter param;

            DataSet ds = new DataSet();
            string connetionString = cnxString;
            connection = new SqlConnection(connetionString);

            connection.Open();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetCities";

            param = new SqlParameter("@EnglishCountryRegionName", "Canada")
            {
                Direction = ParameterDirection.Input,
                DbType = DbType.String
            };
            command.Parameters.Add(param);

            adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);


            int i;
            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                myList.Add(ds.Tables[0].Rows[i][0].ToString());
            }

            connection.Close();
            return myList;
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
