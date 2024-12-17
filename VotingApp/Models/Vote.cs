namespace VotingApp.Models
{
    public class Vote
    {
        public required string Option { get; set; }
        public int Count { get; set; }
    }
}