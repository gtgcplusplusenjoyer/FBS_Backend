namespace FBS.Core.Entities.User
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
    }
}
