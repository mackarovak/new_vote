using Microsoft.AspNetCore.SignalR;

namespace VotingApp.Hubs
{
    public class VoteHub : Hub
    {
        public async Task Vote(string option)
        {
            await Clients.All.SendAsync("ReceiveVote", option);
        }

        public async Task Unvote(string option)
        {
            await Clients.All.SendAsync("ReceiveUnvote", option);
        }
    }
}