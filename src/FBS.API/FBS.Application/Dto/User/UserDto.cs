namespace FBS.Application.Dto.User
{
    public record UserDto
    {
        public UserDto()
        {
            
        }
        public Guid id { get; set; }
        public string email { get; set; } = string.Empty;
        public string userName { get; set; } = string.Empty;
        public string role { get; set; } = string.Empty;
    }
}
