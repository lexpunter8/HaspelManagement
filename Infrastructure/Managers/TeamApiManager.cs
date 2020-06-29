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
                return new string[]
                    {
                        "Kan niet met de server verbinden!"
                    };
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
                myParent.LosedConnection?.Invoke(this, new EventArgs());
            }
            return new string[0];
        }

        public async Task PostData(string team)
        {
            if (!myParent.IsConnected)
            {
                return;
            }

            var teamModel = new DataModels.Team
            {
                Name = team
            };
            try
            {

                var json = JsonConvert.SerializeObject(teamModel);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var client = new HttpClient();

                var response = await client.PostAsync(myApiEndPoint, data);

                string result = response.Content.ReadAsStringAsync().Result;

                client.Dispose();
            }
            catch (Exception)
            {
                myParent.LosedConnection?.Invoke(this, new EventArgs());
                
            }
        }
    }
}
