using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using RReport.Database;
using RReport.Models;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RReport.Controllers
{
	[Produces("application/json")]
	[Route("api/Report")]
	public class ReportController : Controller
	{
		public ReportController()
		{

		}

		[HttpPost]
		public IActionResult GetReport([FromBody]ReportRequestDto reportRequest)
		{
			try
			{
				StiReport report = new StiReport();
				StiDatabase database = new StiPostgreSQLDatabase("ownRadioRdev", "Server=localhost;Port=5432;User Id = postgres;Password=postgres;Database=ownRadioRdev;");
				StiDataSource datasource = new StiPostgreSQLSource("ownRadioRdev", "tracks", "tracks", "select * from tracks", true, false);

				report.Dictionary.DataSources.Add(datasource);
				report.Dictionary.Databases.Add(database);

				using(var connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id = postgres;Password=postgres;Database=ownRadioRdev;"))
				{
					NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("select * from tracks", connection);

					DataTable dataTableCategories = new DataTable();
					adapter.Fill(dataTableCategories);

					foreach (DataColumn col in dataTableCategories.Columns)
					{
						datasource.Columns.Add(col.ColumnName, col.DataType);
					}
				}

				report.Dictionary.Synchronize();

				return Ok(JObject.Parse(report.SaveToJsonString()));

				/*
				report.RegData("PostgreSQL", new SqlConnection());
				report.LoadDocument(StiNetCoreHelper.MapPath(this, $"Reports/{reportRequest.Report}"));

				StiNetCoreActionResult result = null;

				switch (reportRequest.Output.ToLower())
				{
					case "pdf":
						result = StiNetCoreReportResponse.ResponseAsPdf(report);
					break;
					case "html":
						result = StiNetCoreReportResponse.PrintAsHtml(report);
					break;
					case "xls":
						result = StiNetCoreReportResponse.ResponseAsXls(report);
						break;

					default:
						throw new Exception("Недопустимый формат файла");
				}

				var reportJson = JObject.Parse(report.SaveToJsonString());
				var dataJson = JObject.FromObject(reportRequest.Data);

				foreach (var item in dataJson)
				{
					reportJson[item.Key] = item.Value;
				}

				report.LoadDocumentFromJson(JsonConvert.SerializeObject(reportJson));

				return StiNetCoreReportResponse.PrintAsHtml(report);*/
			}
			catch(Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}