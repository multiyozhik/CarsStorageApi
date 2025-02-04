namespace CarsStorage.BLL.Abstractions.ModelsDTO.AuthModels
{
	/// <summary>
	///  Класс данных пользователя при регистрации в приложении.
	/// </summary>
	public class UserRegisterDTO
	{
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
	}
}
