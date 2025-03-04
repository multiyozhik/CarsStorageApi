using CarsStorage.Abstractions.BLL.Services;
using CarsStorage.Abstractions.ModelsDTO;

namespace CarsStorageApi.Middlewares
{
	/// <summary>
	/// Класс middleware для обработки запроса при проведении технических работ.
	/// </summary>
	/// <param name="technicalWorksService">Сервис по проведению технических работ.</param>
	public class TechnicalWorksMiddleware(ITechnicalWorksService technicalWorksService): IMiddleware
	{
		/// <summary>
		/// Метод обработки запроса.
		/// </summary>
		/// <param name="context">Контекст запроса.</param>
		/// <param name="next">Следующий делегат конвейера обработки запроса.</param>
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			if (context.User.Identity.IsAuthenticated)
			{
				var userClaims = context.User.Claims.ToList();
				var userClaimsIsAdmin = userClaims.Any(c => c.Value == RoleClaimTypeBLL.CanManageUsers.ToString());
				var isUnderMaintenance = await technicalWorksService.IsUnderMaintenance();
				if (isUnderMaintenance.Result && !userClaimsIsAdmin)
				{
					context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
					await context.Response.WriteAsync("Сервис временно недоступен, ведутся технические работы по обслуживанию.");
					return;
				}
			}		
			await next(context);
		}
	}
}
