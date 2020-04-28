using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HostedService.Timers.Cron;
using Microsoft.Extensions.Logging;

namespace HostedService.Services.Cron
{
    public class MyCronJob3 : CronJobService
    {
        private readonly ILogger<MyCronJob3> _logger;
        private readonly IReportService _reportService;

        public MyCronJob3(IScheduleConfig<MyCronJob3> config, ILogger<MyCronJob3> logger, IReportService reportService)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _reportService = reportService;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await _reportService.UpdateAsync(nameof(MyCronJob3), StartMessage, 1);

            _logger.LogInformation(StartMessage);

            await base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            await _reportService.UpdateAsync(nameof(MyCronJob3), DoWorkMessage, 2);

            _logger.LogInformation(DoWorkMessage);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _reportService.UpdateAsync(nameof(MyCronJob3), StopMessage, 0);

            _logger.LogInformation(StopMessage);

            await base.StopAsync(cancellationToken);
        }
    }
}
