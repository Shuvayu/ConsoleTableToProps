using ConsoleTableToProps.Models.DataModels;
using ConsoleTableToProps.Queries;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ConsoleTableToProps.Repositories
{
    public class DataRepository
    {
        private string _connectionString;
        public DataRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IEnumerable<QueryMapping> DbCallForSchema(string databaseName, string tableName)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("TableName", tableName, DbType.String, ParameterDirection.Input);

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = string.Format(TableSchema.columnNames, databaseName);
                var result = sqlConnection.Query<QueryMapping>(query, parameters);
                return result;
            }
        }

        public void OutputToTxtFile(string txtString)
        {
            StreamWriter file = new StreamWriter(@"Output.txt");
            file.WriteLine(txtString);
            file.WriteLine(Environment.NewLine);
            file.Close();
        }
    }
}
