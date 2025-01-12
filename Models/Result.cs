using System;

namespace SecondTaskProject.Models
{
    public class Result
    {
        public string QuizName { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalQuestions { get; set; }
        public DateTime Date { get; set; }

        public double GetScorePercentage() => (double)CorrectAnswers / TotalQuestions * 100;
    }
}
