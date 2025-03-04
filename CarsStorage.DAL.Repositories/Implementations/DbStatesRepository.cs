using CarsStorage.Abstractions.DAL.Repositories;
using CarsStorage.DAL.DbContexts;
using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.DAL.Repositories.Implementations
{
	/// <summary>
	/// Класс репозитория состояний (конфигураций) БД.
	/// </summary>
	/// <param name="dbContext">Объект контекста данных.</param>
	public class DbStatesRepository(AppDbContext dbContext) : IDbStatesRepository
	{
		/// <summary>
		/// Метод для получения объекта состояния БД - проведения технических работ.
		/// </summary>
		/// <returns>Объект состояния БД - проведение технических работ.</returns>
		private async Task<DbStateEntity> GetMaintenanceState() 
			=> await dbContext.DbStates.FirstOrDefaultAsync(s => s.StateName == "Технические работы в настоящий момент")
			?? throw new Exception("Не установлено начальное состояние технических работ БД.");


		/// <summary>
		/// Метод возвращает булево значение, проводятся ли технические работы.
		/// </summary>
		/// <returns>Возвращает true, если проводятся технические работы.</returns
		public async Task<bool> IsUnderMaintenance()
		{
			var maintenanceStateValue = await GetMaintenanceState();
			return maintenanceStateValue.Value;
		}


		/// <summary>
		/// Метод для изменения состояния БД.
		/// </summary>
		/// <param name="dbStateEntity">Объект состояния БД.</param>
		public async Task Update(bool value)
		{
			var maintenanceState = await GetMaintenanceState();
			maintenanceState.Value = value;
			dbContext.Update(maintenanceState);
			await dbContext.SaveChangesAsync();
		}
	}
}
