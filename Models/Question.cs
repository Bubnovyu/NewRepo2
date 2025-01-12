using System.Collections.Generic;

namespace SecondTaskProject.Models
{
    public class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public List<int> CorrectAnswers { get; set; }
    }
}
