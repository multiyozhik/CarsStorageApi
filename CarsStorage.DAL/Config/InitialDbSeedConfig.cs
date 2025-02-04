namespace CarsStorage.DAL.Config
{
    /// <summary>
    /// Класс конфигурирования начальных настроек БД (имя роли пользователя по умолчанию и начальное количество пользователей в БД). 
    /// </summary>
    public class InitialDbSeedConfig
    {
        public string? DefaultRoleName { get; }

        public int InitialUsersCount { get; }
    }
}
