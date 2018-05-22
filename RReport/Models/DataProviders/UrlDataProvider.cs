using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net.Http;
using System.Text;

namespace RReport.Models.DataProviders
{
	public class UrlDataProvider : IDataProvider
	{
		private readonly string _url;

		public UrlDataProvider(string url)
		{
			_url = url;
		}

		public DataSet GetData()
		{
			DataSet data = new DataSet();
			JObject request = new JObject();

			request.Add("testName", "testValue");
			request.Add("testName1", "testValue1");

			var content = new StringContent(request.ToString(), Encoding.UTF8, "application/json");

			using (HttpClient client = new HttpClient())
			using(var response = client.PostAsync(_url, content))
			{
				var result = response.Result.Content.ReadAsStringAsync();
				data = JsonConvert.DeserializeObject<DataSet>(result.Result);
			}

			return data;
		}
	}
}
