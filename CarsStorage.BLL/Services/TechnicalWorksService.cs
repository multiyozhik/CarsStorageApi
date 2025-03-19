using CarsStorage.Abstractions.BLL.Services;
using CarsStorage.Abstractions.DAL.Repositories;
using CarsStorage.Abstractions.Exceptions;
using CarsStorage.Abstractions.General;
using CarsStorage.BLL.Services.Kafka;
using Microsoft.Extensions.Logging;

namespace CarsStorage.BLL.Services.Services
{
	/// <summary>
	/// Класс сервиса по проведению технических работ.
	/// </summary>
	/// <param name="repository">Репозиторий технических работ.</param>
	public class TechnicalWorksService(IDbStatesRepository repository, ILogger<TechnicalWorksService> logger, IKafkaProducer<Message> kafkaProducer) : ITechnicalWorksService
	{
		/// <summary>
		/// Метод для запуска технических работ.
		/// </summary>
		/// <returns>Строка сообщения о запуске технических работ.</returns>
		public async Task<ServiceResult<string>> StartTechnicalWorks()
		{
			try
			{
				await repository.Update(true);
				await kafkaProducer.ProduceAsync(new Message() { Text = "В настоящее время проводятся технические работы." }, cancellationToken: default);
				return new ServiceResult<string>("В настоящее время проводятся технические работы.");
			}
			catch (Exception exception)
			{
				var innner = exception.InnerException;
				logger.LogError("Ошибка в {service} в {method} при запуске технических работ: {errorMessage}", this, nameof(this.StartTechnicalWorks), exception.Message);
				throw new ServerException(exception.Message);
			}
		}

		/// <summary>
		/// Метод для завершения технических работ.
		/// </summary>
		/// <returns>Строка сообщения о прекращении технических работ.</returns>
		public async Task<ServiceResult<string>> StopTechnicalWorks()
		{
			try
			{
				await repository.Update(false);
				return new ServiceResult<string>("Технические работы завершены.");
			}
			catch (Exception exception)
			{
				logger.LogError("Ошибка в {service} в {method} при остановке технических работ: {errorMessage}", this, nameof(this.StopTechnicalWorks), exception.Message);
				throw new ServerException(exception.Message);
			}
		}


		/// <summary>
		/// Метод возвращает булево значение, проводятся ли технические работы в настоящий момент.
		/// </summary>
		/// <returns>Возвращает true, если проводятся технические работы.</returns>
		public async Task<ServiceResult<bool>> IsUnderMaintenance()
		{
			var isUnderMaintenance = await repository.IsUnderMaintenance();
			return new ServiceResult<bool>(isUnderMaintenance);
		}
	}
}
