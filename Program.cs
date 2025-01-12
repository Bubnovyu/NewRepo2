using System;
using System.Collections.Generic;
using System.Linq;
using SecondTaskProject.Models;
using SecondTaskProject.Services;

namespace SecondTaskProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var userService = new UserService();
            var quizService = new QuizService();

            quizService.AddQuiz(new Quiz
            {
                Name = "История",
                Category = "История",
                Questions = new List<Question>
                {
                    new Question
                    {
                        Text = "Кто был первым президентом США?",
                        Options = new List<string> { "Джордж Вашингтон", "Авраам Линкольн", "Томас Джефферсон", "Джеймс Мэдисон" },
                        CorrectAnswers = new List<int> { 0 }
                    }
                }
            });

            quizService.AddQuiz(new Quiz
            {
                Name = "География",
                Category = "География",
                Questions = new List<Question>
                {
                    new Question
                    {
                        Text = "Какой океан самый большой?",
                        Options = new List<string> { "Атлантический", "Тихий", "Индийский", "Северный Ледовитый" },
                        CorrectAnswers = new List<int> { 1 }
                    }
                }
            });

            quizService.AddQuiz(new Quiz
            {
                Name = "История",
                Category = "История",
                Questions = new List<Question>
                {
                    new Question
                    {
                        Text = "Когда начилась WW2?",
                        Options = new List<string> { "3 января 1931", "2 октября 1939", "1 сентября 1939", "12 июня 1939" },
                        CorrectAnswers = new List<int> { 2 }
                    }
                }
            });

            Console.WriteLine("Добро пожаловать в приложение Викторина!");
            User currentUser = null;

            while (currentUser == null)
            {
                Console.WriteLine("1. Вход\n2. Регистрация");
                var choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Введите логин: ");
                    var login = Console.ReadLine();
                    Console.Write("Введите пароль: ");
                    var password = Console.ReadLine();

                    try
                    {
                        currentUser = userService.Login(login, password);
                        Console.WriteLine("Вы успешно вошли!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (choice == "2")
                {
                    Console.Write("Введите логин: ");
                    var login = Console.ReadLine();
                    Console.Write("Введите пароль: ");
                    var password = Console.ReadLine();
                    Console.Write("Введите дату рождения (yyyy-MM-dd): ");
                    var dateOfBirth = DateTime.Parse(Console.ReadLine());

                    try
                    {
                        currentUser = userService.Register(login, password, dateOfBirth);
                        Console.WriteLine("Регистрация прошла успешно! Вы вошли в систему.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Начать новую викторину\n2. Посмотреть результаты\n3. Топ-20 игроков\n4. Настройки\n5. Выход");
                var menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        Console.WriteLine("Выберите категорию или смешанную викторину:");
                        var quizzes = quizService.GetQuizzes();
                        for (int i = 0; i < quizzes.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {quizzes[i].Name} ({quizzes[i].Category})");
                        }
                        Console.WriteLine("0. Смешанная викторина");

                        int quizChoice = int.Parse(Console.ReadLine());
                        Quiz selectedQuiz = quizChoice == 0
                            ? quizService.GetRandomMixedQuiz()
                            : quizzes[quizChoice - 1];

                        int correctAnswers = 0;
                        foreach (var question in selectedQuiz.Questions)
                        {
                            Console.WriteLine(question.Text);
                            for (int i = 0; i < question.Options.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {question.Options[i]}");
                            }

                            Console.WriteLine("Введите номера правильных ответов через запятую:");
                            var answers = Console.ReadLine()
                                .Split(',')
                                .Select(a => int.Parse(a.Trim()) - 1)
                                .ToList();

                            if (AreAnswersCorrect(answers, question.CorrectAnswers))
                            {
                                correctAnswers++;
                            }
                        }

                        var result = new Result
                        {
                            QuizName = selectedQuiz.Name,
                            CorrectAnswers = correctAnswers,
                            TotalQuestions = selectedQuiz.Questions.Count,
                            Date = DateTime.Now
                        };

                        currentUser.Results.Add(result);
                        Console.WriteLine($"Вы ответили правильно на {correctAnswers} из {selectedQuiz.Questions.Count} вопросов.");
                        break;

                    case "2":
                        foreach (var resultItem in currentUser.Results)
                        {
                            Console.WriteLine($"{resultItem.QuizName}: {resultItem.CorrectAnswers}/{resultItem.TotalQuestions} ({resultItem.GetScorePercentage():F2}%)");
                        }
                        break;

                    case "3":
                        Console.WriteLine("Функционал Топ-20 игроков пока не реализован.");
                        break;

                    case "4":
                        Console.WriteLine("1. Изменить пароль\n2. Изменить дату рождения");
                        var settingsChoice = Console.ReadLine();
                        if (settingsChoice == "1")
                        {
                            Console.Write("Введите новый пароль: ");
                            currentUser.Password = Console.ReadLine();
                        }
                        else if (settingsChoice == "2")
                        {
                            Console.Write("Введите новую дату рождения (yyyy-MM-dd): ");
                            currentUser.DateOfBirth = DateTime.Parse(Console.ReadLine());
                        }
                        break;

                    case "5":
                        exit = true;
                        break;
                }
            }
        }

        static bool AreAnswersCorrect(List<int> userAnswers, List<int> correctAnswers)
        {
            if (userAnswers.Count != correctAnswers.Count)
                return false;

            return !correctAnswers.Except(userAnswers).Any() && !userAnswers.Except(correctAnswers).Any();
        }
    }
}
