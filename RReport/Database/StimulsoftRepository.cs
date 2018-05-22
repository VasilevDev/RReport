using Newtonsoft.Json.Linq;
using Npgsql;
using RReport.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace RReport.Database
{
	public class StimulsoftRepository
	{
		List<ReportSettings> _settings = new List<ReportSettings>();

		public StimulsoftRepository()
		{
			_settings.Add(
				new ReportSettings()
				{
					ReportName = "TwoSimpleLists.mrt",
					Database = "ownRadioRdev",
					ConnectionString = "Server=localhost;Port=5432;User Id = postgres; Password=postgres;Database=ownRadioRdev;",
					TableNames = new List<string>()
					{
						"tracks",
						"devices"
					}
				}
			);

			_settings.Add(
				new ReportSettings()
				{
					ReportName = "TestList2.mrt",
					Database = "palantir",
					ConnectionString = "Server=localhost;Port=5432;User Id = postgres; Password=postgres;Database=palantir;",
					TableNames = new List<string>()
					{
						"reports",
						"plans"
					}
				}
			);

			_settings.Add(
				new ReportSettings()
				{
					ReportName = "Test_report.mrt",
					Database = "ownRadioRdev",
					ConnectionString = "Server=localhost;Port=5432;User Id = postgres; Password=postgres;Database=ownRadioRdev;",
					TableNames = new List<string>()
					{
						"tracks"
					}
				}
			);
		}

		public ReportSettings GetReportSettings(string reportName)
		{
			return _settings.Find(m => m.ReportName == reportName);
		}

		public DataSet GetReportData(string table, string connectionString, JToken parameters)
		{
			DataSet data = new DataSet();
			string sqlCmd = $"select * from {table} ";
			string cmdParams = string.Empty;

			using (var connection = new NpgsqlConnection(connectionString))
			{
				
				if (parameters.HasValues)
				{
					foreach (JProperty elem in parameters)
					{
						cmdParams = String.Join(',', $"{elem.Name}='{elem.Value.ToString()}'");
					}

					sqlCmd = $"select * from {table} where {cmdParams}";
				}

				NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sqlCmd, connection);

				/*foreach (JProperty param in parameters)
				{
					adapter.SelectCommand.Parameters.Add(new NpgsqlParameter($"@{param.Name}", param.Value.ToString()));
				}*/

				adapter.Fill(data);
			}

			return data;
		}
	}
}
