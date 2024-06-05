using Dapper;
using Npgsql;
using System.Data;

namespace CodeGenerator
{
    public class DatabaseHelper
    {
        public static string? ConnectionString = "";
        public static int CommandTimeout { get; set; } = 600;



        public static async Task<string> ExecuteSqlFunction<T>(string query, T obj)
        {
            string? _result;
            IDbConnection DBConnection = new NpgsqlConnection(ConnectionString);

            using IDbConnection _DbConnection = DBConnection;
            if (_DbConnection.State != ConnectionState.Open)
                _DbConnection.Open();
            IDbTransaction _transaction = DBConnection.BeginTransaction();

            try
            {
                _result = await _DbConnection.QueryFirstOrDefaultAsync<string>(query, obj, _transaction, CommandTimeout, CommandType.Text);
                _transaction.Commit();
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                _result = ex.Message;
            }

            _DbConnection.Close();

            return _result!;
        }



        public static async Task<object> ExecuteSql<T>(string query)
        {
            IDbConnection DBConnection = new NpgsqlConnection(ConnectionString);

            using IDbConnection _DbConnection = DBConnection;
            if (_DbConnection.State != ConnectionState.Open)
                _DbConnection.Open();

            object _result = await _DbConnection.QueryAsync<T>(query);

            _DbConnection.Close();

            return _result!;
        }





    }
}
