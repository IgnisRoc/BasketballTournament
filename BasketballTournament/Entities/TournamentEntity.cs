using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballTournament.Entities
{
    public class TournamentEntity : BaseEntity
    {
        public string Name { get; set; }

        public List<TeamEntity> Teams = new List<TeamEntity>();

        public List<GameEntity> Games = new List<GameEntity>();



        public TournamentEntity(int id, string name) : base(id)
        {
            Name = name;
        }
    }
}
