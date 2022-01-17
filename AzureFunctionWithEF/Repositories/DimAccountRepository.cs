using AzureFunctionWithEF.Common.Data;
using AzureFunctionWithEF.Common.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionWithEF.Repositories
{
    public class DimAccountRepository: IDimAccountRepository
    {
        //private readonly MyDbContext _dbContext;
        //public DimAccountRepository(MyDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        //public DimAccountRepository()
        //{

        //}

        ///// <summary>
        ///// Returns all missing fields
        ///// </summary>
        ///// <returns></returns>
        //public async Task<List<DimAccount>> ReturnMissingFields()
        //{
        //    string param = "Expenditures";
        //    var accounts = await _dbContext.DimAccount
        //                .FromSqlInterpolated($"EXEC [dbo].[ReturnMissingFields] {param}")
        //                .ToListAsync();

        //    return accounts;
        //}

        /// <summary>
        /// Return a list of countries
        /// </summary>
        /// <returns></returns>
        public List<string> ReturnMissingListFromSql(string cnxString)
        {
            List<string> myList = new List<string>();
            string connetionString = null;

            SqlConnection connection;
            SqlDataAdapter adapter;

            SqlCommand command = new SqlCommand();
            SqlParameter param;

            DataSet ds = new DataSet();

            int i = 0;

            //connetionString = Environment.GetEnvironmentVariable("SqlConnectionString");
            connetionString = cnxString;
            connection = new SqlConnection(connetionString);

            connection.Open();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetCities";

            param = new SqlParameter("@EnglishCountryRegionName", "Canada");
            param.Direction = ParameterDirection.Input;
            param.DbType = DbType.String;
            command.Parameters.Add(param);

            adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);

            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                myList.Add(ds.Tables[0].Rows[i][0].ToString());
            }

            connection.Close();
            return myList;
        }
    }
}
