namespace CarsStorage.BLL.Services.Config
{
	/// <summary>
	/// Класс конфигураций токена для его генерации и валидации.
	/// </summary>
	public class JWTConfig
	{
		/// <summary>
		/// Секретный ключ токена.
		/// </summary>
		public string Key { get; set; } = string.Empty;


		/// <summary>
		/// Издатель токена.
		/// </summary>
		public string Issuer { get; set; } = string.Empty;


		/// <summary>
		/// Получатель токена.
		/// </summary>
		public string Audience { get; set; } = string.Empty;


		/// <summary>
		/// Время жизни токена.
		/// </summary>
		public int ExpireMinutes { get; set; }


		/// <summary>
		/// Булево значение, валидировать ли издателя токена.
		/// </summary>
		public bool ValidateIssuer { get; set; } = true;


		/// <summary>
		/// Булево значение, валидировать ли получателя токена.
		/// </summary>
		public bool ValidateAudience { get; set; } = true;


		/// <summary>
		/// Булево значение, валидировать ли подпись.
		/// </summary>
		public bool ValidateIssuerSigningKey { get; set; } = true;


		/// <summary>
		/// Булево значение, валидировать ли время жизни токена.
		/// </summary>
		public bool ValidateLifetime { get; set; } = true;


		/// <summary>
		/// Булево значение, требуется ли значение времени жизни токена.
		/// </summary>
		public bool RequireExpirationTime { get; set; } = true;
	}
}
