using AllinqApp.Managers;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Managers
{
    public class TeamApiManager
    {
        private ApiManager myParent;
        private string myApiEndPoint => $"{myParent.BaseApiUrl}/api/teams";

        public TeamApiManager(ApiManager parent)
        {
            myParent = parent;
        }
        
        public async Task<string[]> GetData()
        {

            if (!myParent.IsConnected)
            {
                return new string[0];
            }

            var httpClient = new HttpClient();
            try
            {
                Uri uri = new Uri(myApiEndPoint);
                var response = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
                string responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<string[]>(responseString);
                Console.WriteLine(response.Content);
                return result ?? new string[0];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new string[0];
        }

        public async Task PostData(string team)
        {
            if (!myParent.IsConnected)
            {
                return;
            }

            var data = new StringContent(team, Encoding.UTF8, "application/json");

            var client = new HttpClient();

            var response = await client.PostAsync(myApiEndPoint, data);

            string result = response.Content.ReadAsStringAsync().Result;

            client.Dispose();
        }
    }
}
