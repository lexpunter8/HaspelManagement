using AllinqApp.Managers;
using DataModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Managers
{
    public class HaspelApiManager
    {
        private ApiManager myParent;
        private string myApiEndPoint => $"{myParent.BaseApiUrl}/api/haspel";
        public HaspelApiManager(ApiManager apiManager)
        {
            myParent = apiManager;
        }

        public async Task<Haspel> GetHaspelByBarcode(string barcode)
        {
            try
            {
                if (!myParent.IsConnected)
                {
                    return new Haspel
                    {
                        Barcode = "Not found",
                        UsedBy = "-",
                        Status = Enums.EHaspelStatus.Unkown
                    };
                }
                string getBarcodeUrl = $"{myApiEndPoint}/{barcode}";
                Uri url = new Uri(getBarcodeUrl);
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                if (!response.IsSuccessStatusCode)
                {
                    return new Haspel
                    {
                        Barcode = "Not found",
                        UsedBy = "-",
                        Status = Enums.EHaspelStatus.Unkown
                    };
                }
                Haspel result = JsonConvert.DeserializeObject<Haspel>(await response.Content.ReadAsStringAsync());

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new Haspel
                {
                    Barcode = "Not found",
                    UsedBy = "-",
                    Status = Enums.EHaspelStatus.Unkown
                };
            }
        }

        public async Task<Haspel[]> GetData()
        {

            if (!myParent.IsConnected)
            {
                return new Haspel[]
                {
                    new Haspel
                    {
                        Barcode = "Kan niet met de server verbinden!"
                    }
                };
            }
            var httpClient = new HttpClient();
            try
            {
                Uri uri = new Uri(myApiEndPoint);
                var response = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
                string responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Haspel[]>(responseString);
                Console.WriteLine(response.Content);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                myParent.LosedConnection?.Invoke(this, new EventArgs());
            }
            return null;
        }

        public async Task PostData(Haspel[] haspels)
        {
            var json = JsonConvert.SerializeObject(haspels);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();

            var response = await client.PostAsync(myApiEndPoint, data);

            string result = response.Content.ReadAsStringAsync().Result;

            client.Dispose();
        }
        public async Task PostData(Haspel haspel)
        {
            if (!myParent.IsConnected)
            {
                return;
            }

            var json = JsonConvert.SerializeObject(haspel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();

            var response = await client.PostAsync(myApiEndPoint, data);

            string result = response.Content.ReadAsStringAsync().Result;

            client.Dispose();
        }
    }
}
