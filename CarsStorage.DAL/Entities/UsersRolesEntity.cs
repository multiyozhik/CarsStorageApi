namespace CarsStorage.DAL.Entities
{
    /// <summary>
    /// Класс сущности для связи отношений между пользователем и его ролью.
    /// </summary>
    
    public class UsersRolesEntity
    {
        public string UserEntityId { get; set; }

        public int RoleEntityId { get; set; }

        public UserEntity? UserEntity { get; set; }

        public RoleEntity? RoleEntity { get; set; }
    }
}