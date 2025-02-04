namespace CarsStorage.BLL.Abstractions.ModelsDTO.AuthModels
{
	public class JWTTokenDTO
	{
		public string? AccessToken { get; set; }
		public string? RefreshToken { get; set; }
		public DateTime TokenExpires { get; set; }
	}
}
