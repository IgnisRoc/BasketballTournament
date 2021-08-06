namespace BasketballTournament.Entities
{
    public class GameEntity : BaseEntity
    {
        public TournamentEntity Tournament { get; set; }
        public TeamEntity Team1 { get; set; }
        public TeamEntity Team2 { get; set; }
        public bool WinTeam { get; set; }
        public int Scores { get; set; }

        public GameEntity(int id, TournamentEntity tournament , TeamEntity team1, TeamEntity team2, bool winTeam, int scores) : base(id)
        {
            Team1 = team1;
            Team2 = team2;
            WinTeam = winTeam;
            Scores = scores;
            Tournament = tournament;
        }
    }
}
