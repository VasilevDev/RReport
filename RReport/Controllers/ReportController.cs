using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RReport.Database;
using RReport.Models;
using RReport.Models.DataProviders;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using System;

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
			ReportTemplateLoader loader = new ReportTemplateLoader();
			StiReport report = new StiReport();
			IDataProvider provider = null;

			try
			{
				loader.Load(report, reportRequest.Report);

				if (reportRequest.DataProvider.ToLower() == "xml")
				{
					provider = new XmlFileDataProvider(reportRequest.Data["filename"].Value<string>());
					report.RegData(provider.GetData());
				} 
				else if (reportRequest.DataProvider.ToLower() == "database")
				{
					StimulsoftRepository repository = new StimulsoftRepository();
					ReportSettings reportSettings = repository.GetReportSettings(reportRequest.Report);

					foreach (var tableName in reportSettings.TableNames)
					{
						provider = new DatabaseDataProvider(tableName, reportSettings.ConnectionString, reportRequest.Data);
						report.RegData(provider.GetData());
					}
				}
				else if(reportRequest.DataProvider.ToLower() == "url")
				{
					provider = new UrlDataProvider("URL");
					report.RegData(provider.GetData());
				}

				ExportFormat exporter = new ExportFormat();
				return exporter.ExportReport(report, reportRequest.Output.ToLower());
			}
			catch(Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}