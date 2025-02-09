namespace CarsStorage.BLL.Implementations.Config
{
	/// <summary>
	/// Класс конфигураций токена для его генерации и валидации.
	/// </summary>
	public class JWTConfig
	{
		public string? Key { get; set; }
		public string? Issuer { get; set; }
		public string? Audience { get; set; }
		public int ExpireMinutes { get; set; }
		public bool ValidateIssuer { get; set; } = true;
		public bool ValidateAudience { get; set; } = true;
		public bool ValidateIssuerSigningKey { get; set; } = true;
		public bool ValidateLifetime { get; set; } = true;
		public bool RequireExpirationTime { get; set; } = true;
	}
}
