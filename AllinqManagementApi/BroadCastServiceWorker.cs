using System;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace AllinqManagementApi
{
    public class BroadCastServiceWorker : BaseBackGroundService
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly UdpClient _udpClient = new UdpClient(15001);
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _log.Info("start execute backgroudnworker");
            while (!stoppingToken.IsCancellationRequested)
            {
                _log.Info("waiting for connection");
                UdpReceiveResult result = await _udpClient.ReceiveAsync();
                var message = Encoding.ASCII.GetString(result.Buffer);
                _log.Info($"{result.RemoteEndPoint} - {message}");
                var returnMessage = Encoding.ASCII.GetBytes("5003");
                _udpClient.Send(returnMessage, returnMessage.Length, result.RemoteEndPoint);
            }
        }

    }
}
