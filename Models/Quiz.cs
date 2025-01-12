using System.Collections.Generic;

namespace SecondTaskProject.Models
{
    public class Quiz
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public List<Question> Questions { get; set; }
    }
}
