using EntityFramework1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EntityFramework1.Controllers
{
    public class CreateNewQuiz : Controller
    {

        public QuizExamenContext db = new QuizExamenContext();

        public IActionResult Index(string userName, string userEmail, int easy, int medium, int hard)
        {
            //Insert user in database
            Quiz user = new Quiz();

            user.UserName = userName;
            user.Email = userEmail;
            db.Quiz.Add(user);
            db.SaveChanges();

            //insert questions in database
            List<int> questions = new List<int>();
            List<int> easyCatList = db.Question.Where(c => c.CategoryId == 1).Select(c => c.QuestionId).ToList();
            List<int> mediumCatList = db.Question.Where(c => c.CategoryId == 2).Select(c => c.QuestionId).ToList();
            List<int> hardCatList = db.Question.Where(c => c.CategoryId == 3).Select(c => c.QuestionId).ToList();

            if (easy != 0)
            {
                for (int i = 0; i < easy; i++)
                {
                    //int number = easyrandom.Next(easy-1);
                    questions.Add(easyCatList[i]);
                }
            }

            if (medium != 0)
            {
                for (int i = 0; i < medium; i++)
                {
                    //int number = mediumrandom.Next(medium);
                    questions.Add(mediumCatList[i]);
                }
            }

            if (hard != 0)
            {
                for (int i = 0; i < hard; i++)
                {
                    //int number = hardrandom.Next(hard);
                    questions.Add(hardCatList[i]);
                }
            }

            List<int> lastElementQuiz = db.Quiz.Select(q => q.QuizId).ToList();

            QuestionQuiz q = new QuestionQuiz();

            foreach (var item in questions)
            {
                q.QuestionId = item;
                q.QuizId = lastElementQuiz.Last();
                db.QuestionQuiz.Add(q);
                db.SaveChanges();
                Debug.WriteLine("Question number:" + item);
            }

            return View("Index");
        }
    }
}
