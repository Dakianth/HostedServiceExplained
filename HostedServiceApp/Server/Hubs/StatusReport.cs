using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace HostedServiceApp.Server.Hubs
{
    public class StatusReport : Hub
    {
        public async Task UpdateStatus(string service, string message, int status)
        {
            await Clients.All.SendAsync("UpdateStatus", service, message, status);
        }
    }
}