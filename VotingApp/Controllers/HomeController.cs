using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using VotingApp.Hubs;
using VotingApp.Models;

namespace VotingApp.Controllers
{
    public class HomeController : Controller
    {
        private static List<Vote> _votes = new List<Vote>
        {
            new Vote { Option = "Option 1", Count = 0 },
            new Vote { Option = "Option 2", Count = 0 },
            new Vote { Option = "Option 3", Count = 0 }
        };

        private readonly IHubContext<VoteHub> _hubContext;

        public HomeController(IHubContext<VoteHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View(_votes);
        }

        [HttpPost]
        public IActionResult Vote(string option)
        {
            var vote = _votes.FirstOrDefault(v => v.Option == option);
            if (vote != null)
            {
                vote.Count++;
                _hubContext.Clients.All.SendAsync("ReceiveVote", option);
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult Unvote(string option)
        {
            var vote = _votes.FirstOrDefault(v => v.Option == option);
            if (vote != null && vote.Count > 0)
            {
                vote.Count--;
                _hubContext.Clients.All.SendAsync("ReceiveUnvote", option);
            }
            return Ok();
        }
    }
}