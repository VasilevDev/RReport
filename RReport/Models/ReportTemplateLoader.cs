using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RReport.Models
{
	public class ReportTemplateLoader
	{
		public void Load(StiReport report, string reportName)
		{
			string dirName = "Reports";
			DirectoryInfo dirInfo = new DirectoryInfo(dirName);

			var result = dirInfo.GetFiles()
				.Select(file => file.Name)
				.Where(m => Path.GetFileNameWithoutExtension(m) == reportName).ToList();

			if(result.Count == 0)
			{
				throw new Exception($"Шаблон отчета с именем {reportName} не найден");
			}

			report.Load($"{dirName}/{result[0]}");
		}
	}
}
