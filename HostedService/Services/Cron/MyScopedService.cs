using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HostedService.Services.Cron
{
    public class MyScopedService : IMyScopedService
    {
        private readonly ILogger<MyScopedService> _logger;
        private readonly IReportService _reportService;

        public MyScopedService(ILogger<MyScopedService> logger, IReportService reportService)
        {
            _logger = logger;
            _reportService = reportService;
        }

        protected string StartMessage => $"[{DateTime.Now:hh:mm:ss}] {GetType().Name}: starts.";
        protected string DoWorkMessage => $"[{DateTime.Now:hh:mm:ss}] {GetType().Name}: Is working.";
        protected string StopMessage => $"[{DateTime.Now:hh:mm:ss}] {GetType().Name}: starts.";

        public async Task DoWork(CancellationToken cancellationToken)
        {
            await _reportService.UpdateAsync(nameof(MyScopedService), StartMessage, 1);
            _logger.LogInformation(StartMessage);

            await _reportService.UpdateAsync(nameof(MyScopedService), DoWorkMessage, 2);
            _logger.LogInformation(DoWorkMessage);
            await Task.Delay(1000 * 20, cancellationToken);

            await _reportService.UpdateAsync(nameof(MyScopedService), StopMessage, 0);
            _logger.LogInformation(StopMessage);
        }
    }
}