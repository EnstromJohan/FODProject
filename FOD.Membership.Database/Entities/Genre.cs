namespace FOD.Membership.Database.Entities
{
    public class Genre : IEntity
    {
        public Genre()
        {
            Films = new HashSet<Film>();
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Film>? Films { get; set; }
    }
}
