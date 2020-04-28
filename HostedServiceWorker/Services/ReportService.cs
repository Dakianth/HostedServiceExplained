using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Net.Client;
using HostedService;
using HostedService.Services;
using Microsoft.Extensions.Logging;
using static HostedService.UpdateRequest.Types;

namespace HostedServiceWorker.Services
{
    public class ReportService : IReportService
    {
        const string REPORT_URL = "https://localhost:5001/";
        private readonly ILogger<ReportService> _logger;

        public ReportService(ILogger<ReportService> logger)
        {
            _logger = logger;
        }

        public async Task UpdateAsync(string service, string message, int status)
        {
            using var channel = GrpcChannel.ForAddress(REPORT_URL);
            var client = new Report.ReportClient(channel);

            var request = new UpdateRequest
            {
                Id = service,
                Message = message,
                Status = (Status)status
            };

            var response = await client.UpdateAsync(request);
            _logger.LogInformation(response.Status);
        }
    }
}
