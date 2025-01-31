﻿namespace CarsStorage.DAL.Entities
{
    /// <summary>
    /// Класс сущности для связи отношений между пользователем и его ролью.
    /// </summary>
    
    public class UsersRolesEntity
    {
        public int IdentityAppUserId { get; set; }

        public int RoleEntityId { get; set; }

        public IdentityAppUser? IdentityAppUser { get; set; }

        public RoleEntity? RoleEntity { get; set; }
    }
}