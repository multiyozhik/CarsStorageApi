using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;

namespace CarsStorage.DAL.Config
{
	/// <summary>
	/// Класс определяет конфигурацию для UserEntity сущности. 
	/// </summary>
	public class UsersConfig(IPasswordHasher passwordHasher, InitialDbSeedConfig initialDbSeedConfig, 
        IOptions<AdminConfig> adminOptions) : IEntityTypeConfiguration<UserEntity>
    {
        private readonly List<UserEntity> usersList = GetUserEntities(passwordHasher, initialDbSeedConfig, adminOptions.Value);

		public void Configure(EntityTypeBuilder<UserEntity> builder)
        {           
            builder.HasData(usersList);
            builder.HasKey(u => u.Id);
            builder.ToTable("Users");

            builder
                .HasMany(u => u.RolesList)
                .WithMany(r => r.UsersList)
                .UsingEntity<UsersRolesEntity>(
                    ur => ur
                        .HasOne(i => i.RoleEntity)
                        .WithMany(u => u.UserRolesList)
                        .HasForeignKey(i => i.RoleEntityId)
                        .OnDelete(DeleteBehavior.Cascade),
                    ur => ur
                        .HasOne(i => i.UserEntity)
                        .WithMany(r => r.UserRolesList)
                        .HasForeignKey(i => i.UserEntityId)
                        .OnDelete(DeleteBehavior.Cascade),
                    ur =>
                    {
                        ur.HasKey(i => new { i.UserEntityId, i.RoleEntityId });
                        ur.ToTable("UserRoles");
                    });
        }

        private static List<UserEntity> GetUserEntities(IPasswordHasher passwordHasher, InitialDbSeedConfig initialDbSeedConfig, 
            AdminConfig adminConfig)
        {
			var usersList = Enumerable.Range(2, initialDbSeedConfig.InitialUsersCount)
				.Select(index =>
				{
					var user = new UserEntity
					{
						Id = index,
						UserName = $"user{index}",
						Email = $"user{index}@mail.ru",
						PasswordHash = passwordHasher.HashPassword($"user{index}")
					};
					return user;
				}).ToList();

			if (string.IsNullOrEmpty(adminConfig.UserName)
				|| string.IsNullOrEmpty(adminConfig.Password)
				|| string.IsNullOrEmpty(adminConfig.Role))
				throw new Exception("Не заданы конфигурации для администратора");

			usersList.Add(new UserEntity
			{
				Id = 1,
				UserName = adminConfig.UserName,
				Email = adminConfig.Email,
				PasswordHash = passwordHasher.HashPassword(adminConfig.Password)
			});
            return usersList;
		}			
    }
}
