using System.Data;

namespace RReport.Models
{
	public class XmlFileDataProvider : IDataProvider
	{
		private readonly string _fileName;

		public XmlFileDataProvider(string fileName)
		{
			_fileName = fileName;
		}

		public DataSet GetData()
		{
			DataSet data = new DataSet();
			data.ReadXml($"Reports/Data/{_fileName}");

			return data;
		}
	}
}
