using System.Threading.Tasks;

namespace HostedService.Services
{
    public interface IReportService
    {
        Task UpdateAsync(string service, string message, int status);
    }
}