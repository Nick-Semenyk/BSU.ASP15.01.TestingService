﻿namespace TestingService.BLL.Interface.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
    }
}