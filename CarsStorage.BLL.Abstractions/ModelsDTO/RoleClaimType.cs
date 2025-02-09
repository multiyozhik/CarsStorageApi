﻿namespace CarsStorage.BLL.Abstractions.ModelsDTO
{
	/// <summary>
	/// Возможные типы клаймов для описания роли.
	/// </summary>
	public enum RoleClaimType
	{
		CanBrowseCars = 1,
		CanManageUsers = 2,
		CanManageRoles = 3,
		CanManageCars = 4
	}
}
