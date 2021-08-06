namespace BasketballTournament.Entities
{
    public class TeamEntity : BaseEntity
    {
        public string Name { get; set; }

        public TeamEntity(int id, string name) : base(id)
        {
            Name = name;
        }
    }
}
