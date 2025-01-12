using System;
using System.Collections.Generic;
using System.Linq;
using SecondTaskProject.Models;

namespace SecondTaskProject.Services
{
    public class UserService
    {
        private List<User> _users = new List<User>();

        public User Register(string login, string password, DateTime dateOfBirth)
        {
            if (_users.Any(u => u.Login == login)) throw new Exception("Этот логин уже занят.");

            var user = new User { Login = login, Password = password, DateOfBirth = dateOfBirth };
            _users.Add(user);
            return user;
        }

        public User Login(string login, string password)
        {
            var user = _users.FirstOrDefault(u => u.Login == login && u.Password == password);
            if (user == null) throw new Exception("Неверный логин или пароль.");
            return user;
        }
    }
}
