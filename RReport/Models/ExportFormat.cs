using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using System;

namespace RReport.Models
{
	public class ExportFormat
	{
		public StiNetCoreActionResult ExportReport(StiReport report, string format)
		{
			StiNetCoreActionResult result;

			switch (format)
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
				case "xml":
					result = StiNetCoreReportResponse.ResponseAsXml(report);
					break;
				
					//todo
				default:
					throw new Exception("Недопустимый формат файла");
			}

			return result;
		}
	}
}
