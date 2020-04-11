using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BroadCastServiceWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly UdpClient _udpClient = new UdpClient(15000);

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                UdpReceiveResult result = await _udpClient.ReceiveAsync();
                var message = Encoding.ASCII.GetString(result.Buffer);
                Console.WriteLine($"{result.RemoteEndPoint} - {message}");
            }
        }
    }
}
