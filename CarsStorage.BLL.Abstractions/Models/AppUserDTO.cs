namespace CarsStorage.BLL.Abstractions.Models
{
    public class AppUserDTO
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public IEnumerable<string>? Roles { get; set; }
    }
}
