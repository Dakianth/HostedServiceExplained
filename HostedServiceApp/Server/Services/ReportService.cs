using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
using Grpc.Core;
using HostedService;
using HostedServiceApp.Server.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace HostedServiceApp.Server.Services
{
    public class ReportService : Report.ReportBase
    {
        private readonly IHubContext<StatusReport> _hubContext;
        private readonly ILogger<ReportService> _logger;

        public ReportService(IHubContext<StatusReport> hubContext, ILogger<ReportService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public override async Task<UpdateResponse> Update(UpdateRequest request, ServerCallContext context)
        {
            await _hubContext.Clients.All.SendAsync("UpdateStatus", request.Id, request.Message, (int)request.Status);

            var status = $"[{request.Id}] {request.Message}";
            _logger.LogInformation(status);

            return new UpdateResponse { Status = status };
        }
    }
}