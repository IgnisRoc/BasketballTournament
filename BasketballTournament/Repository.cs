using BasketballTournament.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballTournament
{
    public static class Repository
    {
        public static UserEntity User;

        public static Dictionary<int, TournamentEntity> Tournaments = new Dictionary<int, TournamentEntity>();
        public static Dictionary<int, TeamEntity> Teams = new Dictionary<int, TeamEntity>();
        public static Dictionary<int, GameEntity> Games = new Dictionary<int, GameEntity>();
    }
}
