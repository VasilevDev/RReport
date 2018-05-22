using Newtonsoft.Json.Linq;

namespace RReport.Models
{
	public class ReportRequestDto
	{
		public string Report { get; set; }
		public JToken Data { get; set; }
		public string Output { get; set; }
		public string DataProvider { get; set; }
	}
}
