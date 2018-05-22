using System.Collections.Generic;

namespace RReport.Models
{
	public class ReportSettings
	{
		public string ReportName { get; set; }
		public string Database { get; set; }
		public string ConnectionString { get; set; }
		public List<string> TableNames { get; set; }
	}
}
