using CarsStorage.BLL.Abstractions.Models;
using CarsStorageApi.AuthModels;
using CarsStorageApi.Models;
using Riok.Mapperly.Abstractions;

namespace CarsStorageApi.Mappers
{
    [Mapper]
	public partial class UserMapper
	{
		public partial AppUserDTO UserRequestResponseToAppUserDto(UserRequestResponse userRequestResponse);
		public partial UserRequestResponse AppUserDtoToUserRequestResponse(AppUserDTO appUserDTO);
		public partial AppUserRegisterDTO RegisterUserDataRequestToAppUserRegisterDTO(RegisterUserDataRequest registerUserDataRequest);
		public partial AppUserLoginDTO LoginDataRequestToAppUserLoginDTO(LoginDataRequest loginDataRequest);
		public partial AppUserCreaterDTO UserRequestToAppUserCreaterDto(UserRequest userRequest);
	}
}
