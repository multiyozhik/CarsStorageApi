namespace CarsStorage.DAL.Models
{
	/// <summary>
	/// Возможные типы утверждений для описания роли.
	/// </summary>
	public enum RoleClaimType
    {
		/// <summary>
		/// Возможность просмотра автомобиля.
		/// </summary>
		CanBrowseCars = 1,

		/// <summary>
		/// Возможность администрирования пользователями и изменения их данных. 
		/// </summary>
		CanManageUsers = 2,

		/// <summary>
		/// Возможность изменения ролей пользователя.
		/// </summary>
		CanManageRoles = 3,

		/// <summary>
		/// Возможность изменения данных по автомобилю.
		/// </summary>
		CanManageCars = 4
	}
}
