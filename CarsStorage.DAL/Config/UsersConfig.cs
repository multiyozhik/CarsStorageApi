using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;

namespace CarsStorage.DAL.Config
{
    public class UsersConfig(IEnumerable<RoleEntity> rolesList, IPasswordHasher passwordHasher, 
        IOptions<InitialDbSeedConfig> initialDbSeedOptions, IOptions<AdminConfig> adminOptions) : IEntityTypeConfiguration<UserEntity>
    {
		private readonly Random random = new();
		private readonly List<RoleEntity> rolesList = rolesList.ToList();
        private readonly AdminConfig adminConfig = adminOptions.Value;
        private readonly InitialDbSeedConfig initialDbSeedConfig = initialDbSeedOptions.Value;


		public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            var usersList = Enumerable.Range(1, initialDbSeedConfig.InitialUsersCount)
                .Select(index =>
                {
                    var user = new UserEntity
                    {
                        UserName = $"user{index}",
                        Email = $"user{index}@mail.ru",
                        RolesList = [rolesList[random.Next(rolesList.Count)]],
                        PasswordHash = passwordHasher.HashPassword($"user{index}")
                    };
                    return user;
                });

			if (string.IsNullOrEmpty(adminConfig.UserName)
                || string.IsNullOrEmpty(adminConfig.Password)
                || string.IsNullOrEmpty(adminConfig.Role))
                throw new Exception("Не заданы конфигурации для администратора");

			usersList = usersList.Append(new UserEntity
            {
                UserName = adminConfig.UserName,
                Email = adminConfig.Email,
                RolesList = [new RoleEntity(adminConfig.Role)],
                PasswordHash = passwordHasher.HashPassword(adminConfig.Password)
            });

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
    }
}
