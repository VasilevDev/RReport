using System.Data;

namespace RReport.Models
{
	interface IDataProvider
	{
		DataSet GetData();
	}
}
