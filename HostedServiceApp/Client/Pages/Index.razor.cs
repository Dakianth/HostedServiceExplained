using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using HostedServiceApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace HostedServiceApp.Client.Pages
{
    public partial class Index
    {
        private HubConnection _hubConnection;
        private readonly List<string> _messages = new List<string>();
        private readonly ConcurrentDictionary<string, ServicesStatus> _servicesStatus = new ConcurrentDictionary<string, ServicesStatus>();

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/statusReport"))
                .Build();

            _hubConnection.On<string, string, int>("UpdateStatus", (service, message, status) =>
            {
                var encodedMsg = $"[{service}] {message}";
                _messages.Add(encodedMsg);

                _servicesStatus.AddOrUpdate(service, (k) => new ServicesStatus(k, status), (k, o) => new ServicesStatus(k, status));
                StateHasChanged();
            });

            await _hubConnection.StartAsync();
        }

        public bool IsConnected => _hubConnection.State == HubConnectionState.Connected;
    }
}