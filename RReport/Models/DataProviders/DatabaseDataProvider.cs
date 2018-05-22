using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Data;

namespace RReport.Models.DataProviders
{
	public class DatabaseDataProvider : IDataProvider
	{
		private readonly string _tableName;
		private readonly string _connectionString;
		private readonly JToken _parameters;

		public DatabaseDataProvider(string tableName, string connectionString, JToken parameters)
		{
			_tableName = tableName;
			_connectionString = connectionString;
			_parameters = parameters;
		}

		public DataSet GetData()
		{
			DataSet data = new DataSet();
			string sqlCmd = $"select * from {_tableName} ";
			string cmdParams = string.Empty;

			using (var connection = new NpgsqlConnection(_connectionString))
			{
				if (_parameters.HasValues)
				{
					foreach (JProperty elem in _parameters)
					{
						cmdParams = String.Join(',', $"{elem.Name}='{elem.Value.ToString()}'");
					}

					sqlCmd = $"select * from {_tableName} where {cmdParams}";
				}

				NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sqlCmd, connection);
				adapter.Fill(data);
			}

			return data;
		}
	}
}
