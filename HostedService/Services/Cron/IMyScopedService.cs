using System.Threading;
using System.Threading.Tasks;

namespace HostedService.Services.Cron
{
    public interface IMyScopedService
    {
        Task DoWork(CancellationToken cancellationToken);
    }
}