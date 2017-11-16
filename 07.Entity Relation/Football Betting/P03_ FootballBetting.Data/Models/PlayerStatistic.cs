namespace P03_FootballBetting.Data.Models
{
    public class PlayerStatistic
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public string Assist { get; set; }
        public double MinutesPlayed { get; set; }
        public int ScoredGoals { get; set; }
    }
}
