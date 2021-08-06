using System;

namespace BasketballTournament.Views
{
    public class TournamentView : IComparable<TournamentView>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Game { get; set; }
        public int Scores { get; set; }
        public int Place { get; set; }

        public int CompareTo(TournamentView other)
        {
            if (this.Scores > other.Scores)
            {
                return 1;
            }
            else if (this.Scores < other.Scores)
            {
                return -1;
            }
            else      
            {
                return 0;
            }
        }
    }
}
