using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RReport.Database;
using RReport.Models;
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
			try
			{
				StiReport report = new StiReport();
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

				return StiNetCoreReportResponse.PrintAsHtml(report);
			}
			catch(Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}