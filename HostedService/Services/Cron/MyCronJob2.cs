using System;
using System.Threading;
using System.Threading.Tasks;
using HostedService.Timers.Cron;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HostedService.Services.Cron
{
    public class MyCronJob2 : CronJobService
    {
        private readonly ILogger<MyCronJob2> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IReportService _reportService;

        public MyCronJob2(IScheduleConfig<MyCronJob2> config,
                          ILogger<MyCronJob2> logger,
                          IServiceProvider serviceProvider,
                          IReportService reportService)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _reportService = reportService;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await _reportService.UpdateAsync(nameof(MyCronJob2), StartMessage, 1);

            _logger.LogInformation(StartMessage);

            await base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            await _reportService.UpdateAsync(nameof(MyCronJob2), DoWorkMessage, 2);
            _logger.LogInformation(DoWorkMessage);

            using var scope = _serviceProvider.CreateScope();
            var scopedService = scope.ServiceProvider.GetRequiredService<IMyScopedService>();

            await scopedService.DoWork(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _reportService.UpdateAsync(nameof(MyCronJob2), StopMessage, 0);

            _logger.LogInformation(StopMessage);

            await base.StopAsync(cancellationToken);
        }
    }
}