using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarsStorage.DAL.Config
{
	/// <summary>
	/// Класс определяет конфигурацию для UserEntity сущности. 
	/// </summary>
	public class UsersConfig : IEntityTypeConfiguration<UserEntity>
    {
		private readonly string hash1 = "JRxt+pwxtgCpgGS7TLXEjsPxV1ll/IZhLZSvN6QDzdc=";
		private readonly string hash2 = "adA8MaXGA/uwLaAtQ+ikggavMypELr5NK+V+KyB4l4U=";
		private readonly string hash3 = "Y0M0qRE2drIzyrcznkG1DsEhkrCDmI1GzkzXEpsv9yU=";
		private readonly string hash4 = "DQaXnrG7s5V9HUHoQiWAwhMpe661ZJlJdQ661J+n8gQ=";
		private readonly string hash5 = "eZw/yjr7gGpoaLUABw8Fgz2rNIMwLnqAN6o0S8tLS+8=";

		private readonly string salt1 = "+9jY4swyva9aJKzTI/5mHQ==";
		private readonly string salt2 = "Dxid1tYUAovqRAvao2lBtQ==";
		private readonly string salt3 = "LolXd97/5wfAEqROD/8bMg==";
		private readonly string salt4 = "BgqW+f6XxuldT0UltY6vMg==";
		private readonly string salt5 = "tQwRgbrUoOK519vJt2X10Q==";


		public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
			var user1 = new UserEntity { UserEntityId = 1, UserName = "admin", Email = "admin@mail.ru", Hash = hash1, Salt = salt1 };
			var user2 = new UserEntity { UserEntityId = 2, UserName = "manager", Email = "manager@mail.ru", Hash = hash2, Salt = salt2 };
			var user3 = new UserEntity { UserEntityId = 3, UserName = "user3", Email = "user3@mail.ru", Hash = hash3, Salt = salt3 };
			var user4 = new UserEntity { UserEntityId = 4, UserName = "user4", Email = "user4@mail.ru", Hash = hash4, Salt = salt4 };
			var user5 = new UserEntity { UserEntityId = 5, UserName = "user5", Email = "user5@mail.ru", Hash = hash5, Salt = salt5 };

			var usersList = new List<UserEntity>();
			usersList.AddRange([user1, user2, user3, user4, user5]);

			builder.HasData(usersList);
			builder.HasKey(u => u.UserEntityId);
			builder.Property(u => u.UserName).ValueGeneratedOnAdd();
			builder.ToTable("Users");
			builder.Property(r => r.UserName).IsRequired();
			builder.Property(r => r.Email).IsRequired();
		}
    }
}
