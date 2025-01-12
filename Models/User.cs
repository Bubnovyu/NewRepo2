using System;
using System.Collections.Generic;

namespace SecondTaskProject.Models
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Result> Results { get; set; } = new List<Result>();
    }
}
