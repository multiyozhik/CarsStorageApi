using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarsStorage.DAL.Config
{
	/// <summary>
	/// Класс конфигурации для сущности состояния БД.
	/// </summary>
	public class DbStatesConfig : IEntityTypeConfiguration<DbStateEntity>
	{
		/// <summary>
		/// Метод конфигурирования сущности состояния БД.
		/// </summary>
		/// <param name="builder">Объект API для конфигурирования.</param>
		public void Configure(EntityTypeBuilder<DbStateEntity> builder)
		{
			builder.HasData(new DbStateEntity() { Id = 1, StateName = "Технические работы в настоящий момент", Value = false });
			builder.ToTable("DbStates");
			builder.HasKey(s => s.Id);
			builder.Property(s => s.Id).ValueGeneratedOnAdd();
			builder.Property(s => s.StateName).IsRequired();
			builder.Property(s => s.Value).IsRequired();
		}
	}
}
