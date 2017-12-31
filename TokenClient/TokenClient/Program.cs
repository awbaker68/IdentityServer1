using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace TokenClient
{
    class Program
    {

		static void Main()
        {
			Task t = ExecuteAsync();
			t.Wait();

			Console.WriteLine("Execution Complete");
			Console.Read();
		}

		private static async Task ExecuteAsync()
		{
			// discover endpoints from metadata
			var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
			if (disco.IsError)
			{
				Console.WriteLine(disco.Error);
				return;
			}

			// request token
			Console.WriteLine("Requesting Token");
			var tokenClient = new IdentityModel.Client.TokenClient(disco.TokenEndpoint, "apiclient", "secret");
			var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

			if (tokenResponse.IsError)
			{
				Console.WriteLine(tokenResponse.Error);
				return;
			}

			Console.WriteLine(tokenResponse.Json);

			// call api
			Console.WriteLine("Calling API");
			var client = new HttpClient();
			client.SetBearerToken(tokenResponse.AccessToken);

			var response = await client.GetAsync("http://localhost:5002/identity");
			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine(response.StatusCode);
			}
			else
			{
				var content = await response.Content.ReadAsStringAsync();
				Console.WriteLine(JArray.Parse(content));
			}
			return;
		}

	}
}
