using Infrastructure.Managers;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
namespace AllinqApp.Managers
{
    public struct UdpState
    {
        public UdpClient u;
        public IPEndPoint e;
    }
    public class ApiManager
    {
        private int myPortNumber;
        private IPEndPoint myApiEndPoint;

        public ApiManager()
        {
            HaspelApiManager = new HaspelApiManager(this);
            TeamApiManager = new TeamApiManager(this);
        }

        public Task Initialize()
        {
            return DoBroadCast();
        }

        public HaspelApiManager HaspelApiManager { get; private set; }
        public TeamApiManager TeamApiManager { get; private set; }

        public bool IsConnected;
        public string BaseApiUrl => $"http://{myApiEndPoint.Address}:{myApiEndPoint.Port}";

        public EventHandler<EventArgs> Connected;
        public EventHandler<EventArgs> LosedConnection;

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
                    IsConnected = true;
                    Connected?.Invoke(this, new EventArgs());
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
                while (!IsConnected)
                {
                    await client.SendAsync(message, message.Length, endpoint);
                    await Task.Delay(1000);
                }
                
                client.Close();
            }
        }

    }
}
