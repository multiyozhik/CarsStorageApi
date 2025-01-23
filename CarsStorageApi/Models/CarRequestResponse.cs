namespace CarsStorageApi.Models
{
    public class CarRequestResponse
    {
        public int Id { get; set; }
        public string? Model { get; set; }
        public string? Make { get; set; }
        public string? Color { get; set; }
        public int Count { get; set; }
    }
}
