using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using Newtonsoft.Json;

namespace AllinqApp.Managers
{
    public struct UdpState
    {
        public UdpClient u;
        public IPEndPoint e;
    }
    public class ApiManager
    {
        public EventHandler<EventArgs> Initialized;
        private int myPortNumber;
        private IPEndPoint myApiEndPoint;
        private bool myIsConnected;
        private string myEndPoint => $"http://{myApiEndPoint.Address}:{myApiEndPoint.Port}/api/haspel";

        public ApiManager()
        {
        }

        public async Task<Haspel> GetHaspelByBarcode(string barcode)
        {
            if (!myIsConnected)
            {
                return new Haspel
                {
                    Barcode = "Not found",
                    UsedBy = "Not Found"
                };
            }
            string getBarcodeUrl = $"{myEndPoint}/{barcode}";
            Uri url = new Uri(getBarcodeUrl);
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            Haspel result = JsonConvert.DeserializeObject<Haspel>(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<Haspel[]> GetData()
        {
            var httpClient = new HttpClient();
            try
            {

                string url = $"http://{myApiEndPoint.Address}:{myApiEndPoint.Port}/api/haspel";
                Uri uri = new Uri(url);
                var response = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
                string responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Haspel[]>(responseString);
                Console.WriteLine(response.Content);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        public async Task PostData(Haspel[] haspels)
        {
            var json = JsonConvert.SerializeObject(haspels);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            string url = $"http://{myApiEndPoint.Address}:{myApiEndPoint.Port}/api/haspel";
            var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            client.Dispose();
        }

        public Task Initialize()
        {
            return DoBroadCast();
        }
        private void ReicevedCalBack(IAsyncResult result)
        {
            try
            {

                UdpClient u = ((UdpState)result.AsyncState).u;
                IPEndPoint e = ((UdpState)result.AsyncState).e;

                byte[] receiveBytes = u.EndReceive(result, ref e);
                string receiveString = Encoding.ASCII.GetString(receiveBytes);

                bool parsed = int.TryParse(receiveString, out int portNumber);

                if (parsed)
                {
                    myPortNumber = portNumber;
                    myApiEndPoint = e;
                    myApiEndPoint.Port = myPortNumber;
                    myIsConnected = true;
                    Initialized?.Invoke(this, new EventArgs());
                }
            } catch (Exception e)
            {
                //
            }
        }
        private async Task DoBroadCast()
        {
            using (var client = new UdpClient())
            {
                client.EnableBroadcast = true;
                var endpoint = new IPEndPoint(IPAddress.Broadcast, 15000);
                var message = Encoding.ASCII.GetBytes("Hello Api - " + DateTime.Now.ToString());
                UdpState state = new UdpState
                {
                    e = endpoint,
                    u = client
                };
                client.BeginReceive(new AsyncCallback(ReicevedCalBack), state);

                client.Client.ReceiveTimeout = 1000;
                while (!myIsConnected)
                {
                    await client.SendAsync(message, message.Length, endpoint);
                    await Task.Delay(1000);
                }
                
                client.Close();
            }
        }

        public async Task PostData(Haspel haspel)
        {
            var json = JsonConvert.SerializeObject(haspel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();

            var response = await client.PostAsync(myEndPoint, data);

            string result = response.Content.ReadAsStringAsync().Result;

            client.Dispose();
        }
    }
}
