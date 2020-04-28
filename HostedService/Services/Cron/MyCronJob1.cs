using System.Threading;
using System.Threading.Tasks;
using HostedService.Timers.Cron;
using Microsoft.Extensions.Logging;

namespace HostedService.Services.Cron
{
    public class MyCronJob1 : CronJobService
    {
        private readonly ILogger<MyCronJob1> _logger;
        private readonly IReportService _reportService;

        public MyCronJob1(IScheduleConfig<MyCronJob1> config, ILogger<MyCronJob1> logger, IReportService reportService)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _reportService = reportService;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await _reportService.UpdateAsync(nameof(MyCronJob1), StartMessage, 1);

            _logger.LogInformation(StartMessage);

            await base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            await _reportService.UpdateAsync(nameof(MyCronJob1), DoWorkMessage, 2);

            _logger.LogInformation(DoWorkMessage);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _reportService.UpdateAsync(nameof(MyCronJob1), StopMessage, 3);
            _logger.LogInformation(StopMessage);

            await base.StopAsync(cancellationToken);
        }
    }
}