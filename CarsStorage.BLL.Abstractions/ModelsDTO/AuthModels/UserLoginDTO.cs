namespace CarsStorage.BLL.Abstractions.ModelsDTO.AuthModels
{
	/// <summary>
	///  Класс данных пользователя при входе в приложение.
	/// </summary>
	public class UserLoginDTO
	{
		public string? UserName { get; set; }
		public string? Password { get; set; }
	}
}
