using EntityFramework1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EntityFramework1.Controllers
{
    public class ChooseQuiz : Controller
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
            Debug.WriteLine("StartQuiz");
            Debug.WriteLine(quizId);
            TempData["quizId"] = quizId;
            ViewData["answers"] = db.ItemOption;
            List<int> questionsNumber = db.QuestionQuiz.Where(q => q.QuizId == quizId).Select(q=>q.QuestionId).ToList();
            foreach(int i in questionsNumber) {
                Debug.WriteLine(i);
            }
            return View(db.Question.Where(c => questionsNumber.Contains(c.QuestionId)).ToList());
        }


        public IActionResult Answers(IFormCollection form)
        {
            Debug.WriteLine("Answer: " + form["question"]);
            string value = form["question"];
            string[] result = value.Split(',');
            

            foreach (string res in result)
            {
                //Debug.WriteLine("Raspuns: " + res);
                Answer a = new Answer();
                a.OptionId = Int32.Parse(res);
                a.QuizId = TempData["quizId"] as int?;
                db.Answer.Add(a);
                db.SaveChanges();
                Debug.WriteLine("Option ID: " + res);
            }


            return View();
        }



    }
}
