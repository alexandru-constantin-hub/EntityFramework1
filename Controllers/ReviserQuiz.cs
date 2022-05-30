using EntityFramework1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EntityFramework1.Controllers
{
    public class ReviserQuiz : Controller
    {
        public QuizExamenContext db = new QuizExamenContext();
        public IActionResult Index()
        {
            
            return View();
        }


        public IActionResult CheckUser(string userName, string userEmail)
        {

            return View("ChooseQuiz", db.Quiz.Where(q => q.UserName == userName && q.Email == userEmail));
        }


        public IActionResult StartQuiz(int quizId)
        {
            var answerTableInterogation = db.Answer.Where(c => c.QuizId == quizId).Select(c => c.OptionId).ToList();

            return View(db.ItemOption.Include(q => q.Question).Where(c => answerTableInterogation.Contains(c.OptionId)).ToList());
        }
    }
}
