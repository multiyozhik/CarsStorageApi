﻿namespace CarsStorage.BLL.Abstractions
{
	public class AppUser
	{
		public Guid Id { get; set; }
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public IEnumerable<string>? Roles { get; set; }
	}
}
