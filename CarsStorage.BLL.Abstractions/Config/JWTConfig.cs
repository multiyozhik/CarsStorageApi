﻿namespace CarsStorage.BLL.Abstractions.Config
{
	/// <summary>
	/// Класс конфигураций токена для его генерации и валидации.
	/// </summary>
	public class JWTConfig
	{
		public string Key { get; } = string.Empty;
		public string Issuer { get; } = string.Empty;
		public string Audience { get; } = string.Empty;
		public int ExpireMinutes { get; }
		public bool ValidateIssuer { get; }
		public bool ValidateAudience { get; }
		public bool ValidateIssuerSigningKey { get; }
		public bool ValidateLifetime { get; }
	}
}
