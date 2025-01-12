using System;
using System.Collections.Generic;
using System.Linq;
using SecondTaskProject.Models;

namespace SecondTaskProject.Services
{
    public class QuizService
    {
        private List<Quiz> _quizzes = new List<Quiz>();

        public void AddQuiz(Quiz quiz) => _quizzes.Add(quiz);

        public List<Quiz> GetQuizzes() => _quizzes;

        public Quiz GetRandomMixedQuiz()
        {
            var allQuestions = _quizzes.SelectMany(q => q.Questions).ToList();
            var random = new Random();
            var mixedQuestions = allQuestions.OrderBy(_ => random.Next()).Take(20).ToList();

            return new Quiz { Name = "Смешанная викторина", Questions = mixedQuestions };
        }
    }
}
