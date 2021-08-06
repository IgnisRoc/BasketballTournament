namespace BasketballTournament.Entities
{
    public class UserEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SaltedPass { get; set; }

        public UserEntity(int id, string first, string last) : base(id) 
        {
            FirstName = first;
            LastName = last;
            SaltedPass = "";
        }
    }
}
