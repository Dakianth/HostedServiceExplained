using System;
using System.Threading;
using System.Threading.Tasks;
using HostedService.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HostedServiceWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IReportService _reportService;

        public Worker(ILogger<Worker> logger, IReportService reportService)
        {
            _logger = logger;
            _reportService = reportService;
        }

        protected string StartMessage => $"[{DateTime.Now:hh:mm:ss}] {GetType().Name}: starts.";
        protected string DoWorkMessage => $"[{DateTime.Now:hh:mm:ss}] {GetType().Name}: running.";
        protected string StopMessage => $"[{DateTime.Now:hh:mm:ss}] {GetType().Name}: starts.";

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await _reportService.UpdateAsync(nameof(Worker), StartMessage, 1);
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _reportService.UpdateAsync(nameof(Worker), DoWorkMessage, 2);
                _logger.LogInformation(DoWorkMessage);
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _reportService.UpdateAsync(nameof(Worker), StopMessage, 3);
            await base.StopAsync(cancellationToken);
        }
    }
}