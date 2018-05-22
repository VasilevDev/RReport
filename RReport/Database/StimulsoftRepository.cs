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
					ReportName = "TwoSimpleLists",
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
					ReportName = "Test_report",
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
	}
}
