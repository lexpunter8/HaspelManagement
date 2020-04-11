using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace AllinqManagementApi
{
    public class BroadCastServiceWorker : BaseBackGroundService
    {
        private readonly UdpClient _udpClient = new UdpClient(15000);
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                UdpReceiveResult result = await _udpClient.ReceiveAsync();
                var message = Encoding.ASCII.GetString(result.Buffer);
                Console.WriteLine($"{result.RemoteEndPoint} - {message}");
                var returnMessage = Encoding.ASCII.GetBytes("5003");
                _udpClient.Send(returnMessage, returnMessage.Length, result.RemoteEndPoint);
            }
        }

    }
}
