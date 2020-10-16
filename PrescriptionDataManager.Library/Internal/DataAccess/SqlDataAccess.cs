
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PrescriptionDataManager.Library.Internal.DataAccess
{
    public class SqlDataAccess
    {
        /*      private readonly IConfiguration _config;
              private readonly ILogger _logger;

              public SqlDataAccess(IConfiguration config, ILogger<SqlDataAccess> logger)
              {
                  _config = config;
                  _logger = logger;
              }*/



        public string GetConnectionString(string name)
        {

            // return _config.GetConnectionString(name);
            string conn = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            Console.WriteLine();
            return conn;
        }

        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure).ToList();

                return rows;
            }


        }

        public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(storedProcedure, parameters,
                   commandType: CommandType.StoredProcedure);


            }


        }

        private IDbConnection _connection;
        private IDbTransaction _transaction;
        // a Open connection /Start Transaction
        public void StartTransaction(string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            _connection = new SqlConnection(connectionString);
            _connection.Open();

            _transaction = _connection.BeginTransaction();
            isClosed = false;
        }




        //Load using the transaction

        public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
        {

            List<T> rows = _connection.Query<T>(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();

            return rows;



        }
        //Save using the transaction

        public void SaveDataInTransaction<T>(string storedProcedure, T parameters)
        {

            _connection.Execute(storedProcedure, parameters,
                  commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        //Determine if sql connection is closed
        private bool isClosed = false;

        private readonly IConfiguration _config;
        private readonly ILogger<SqlDataAccess> _logger;

        // Close connection/ stop transaction method
        public void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();
            isClosed = true;
        }

        public void RollbackTransation()
        {
            _transaction?.Rollback();
            _connection?.Close();
            isClosed = true;
        }

        //Dispose

        public void Dispose()
        {
            if (isClosed == false)
            {
                try
                {
                    CommitTransaction();
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex, "Commit Transaction failed in the dispose method");
                }
            }

            _transaction = null;
            _connection = null;
        }

        /*    public void Dispose()
            {
                if (isClosed == false)
                {
                    try
                    {
                        CommitTransaction();
                    }
                    catch (Exception ex)
                    {

                        _logger.LogError(ex, "Commit Transaction failed in the dispose method");
                    }
                }

                _transaction = null;
                _connection = null;
            }*/
    }
}