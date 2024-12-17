using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace VotingApp.Hubs
{
    public class HubContext<T> : IHubContext<T> where T : Hub
    {
        private readonly IHubContext<T> _hubContext;

        public HubContext(IHubContext<T> hubContext)
        {
            _hubContext = hubContext;
        }

        public IHubClients Clients => _hubContext.Clients;

        public IGroupManager Groups => _hubContext.Groups;

        public Task SendAsync(string methodName, object[] args)
        {
            return _hubContext.Clients.All.SendAsync(methodName, args);
        }
    }
}