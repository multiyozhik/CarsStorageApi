namespace CarsStorage.DAL.Entities
{
    /// <summary>
    /// Класс сущности для промежуточной таблицы отношений между пользователем и его ролью.
    /// </summary>    
    public class UsersRolesEntity
    {
        /// <summary>
        /// Идентификатор сущности пользователя.
        /// </summary>
		public int UserEntityId { get; set; }


        /// <summary>
        /// Идентификатор сущности роли.
        /// </summary>
        public int RoleEntityId { get; set; }


        /// <summary>
        /// Сущность пользователя.
        /// </summary>
        public UserEntity? UserEntity { get; set; }


        /// <summary>
        /// Сущность роли пользователя.
        /// </summary>
        public RoleEntity? RoleEntity { get; set; }
    }
}