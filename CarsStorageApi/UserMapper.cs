using CarsStorage.BLL.Abstractions;
using CarsStorage.BLL.Servises;
using Riok.Mapperly.Abstractions;

namespace CarsStorageApi
{
	[Mapper]
	public partial class UserMapper
	{
		public partial AppUser UserDtoToAppUser(UserDTO userDTO);
		public partial UserDTO AppUserToUserDto(AppUser appUser);

		public partial RegisterAppUser RegUserDtoToRegAppUser(RegisterUserDTO regUserDTO);
		public partial RegisterUserDTO RegAppUserToRegUserDto(RegisterAppUser regAppUser);
	}
}
