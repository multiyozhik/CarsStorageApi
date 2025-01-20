using CarsStorage.BLL.Abstractions;
using Riok.Mapperly.Abstractions;

namespace CarsStorageApi.Mappers
{
	[Mapper]
	public partial class UserMapper
	{
		public partial AppUser UserDtoToAppUser(UserDTO userDTO);
		public partial UserDTO AppUserToUserDto(AppUser appUser);

		public partial RegisterAppUser RegUserDtoToRegAppUser(RegisterUserDTO regUserDTO);
	}
}
