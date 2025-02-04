namespace CarsStorage.DAL.Entities
{
    /// <summary>
    /// Класс сущности для промежуточной таблицы отношений между пользователем и его ролью.
    /// </summary>
    
    public class UsersRolesEntity
    {
        public int UserEntityId { get; set; }

        public int RoleEntityId { get; set; }

        public UserEntity? UserEntity { get; set; }

        public RoleEntity? RoleEntity { get; set; }
    }
}