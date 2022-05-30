using EntityFramework1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework1.Controllers
{
    public class HomeController : Controller
    {
        public QuizExamenContext db = new QuizExamenContext();

        public IActionResult GenerateDataNewQuiz()
        {

            ViewBag.howmanyQuestionsEasy = db.Question.Where(c => c.CategoryId == 1).Count();
            ViewBag.howmanyQuestionsMedium = db.Question.Where(c => c.CategoryId == 2).Count();
            ViewBag.howmanyQuestionsHard = db.Question.Where(c => c.CategoryId == 3).Count();

            return View("../CreateNewQuiz/CreateNewQuiz");
        }

        public IActionResult ChooseQuiz()
        {

            return View("../ChooseQuiz/Index");
        }


        public IActionResult ReviserQuiz()
        {
            return View("../ReviserQuiz/Index");
        }





        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
