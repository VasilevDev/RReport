using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RReport.Models
{
	public class ReportRequestDto
	{
		public string Report { get; set; }
		public object Data { get; set; }
		public string Output { get; set; }
	}
}
