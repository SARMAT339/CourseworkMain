﻿namespace Coursework.Models
{
    public class User
    {
        Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone {  get; set; }
    }
}
