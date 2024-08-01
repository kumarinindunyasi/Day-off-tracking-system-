namespace API.Models
{
    public class AuthResultModel
    {
        public bool Succes { get; set; }
        public string Message { get; set; } = string.Empty;
        public int Id { get; set; }
        public int RoleId { get; set; }

    }
}
