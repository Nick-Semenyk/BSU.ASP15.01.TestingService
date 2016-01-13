using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TestingService.BLL.Interface.Entities;
using TestingService.BLL.Interface.Services;
using TestingService.BLL.Services;
using TestingService.WebApplication.Models;
using TestingService.WebApplication.Security;

namespace TestingService.WebApplication.Controllers
{
    [CustomAuthorize(Roles = "user")]
    public class TestsManagerController : Controller
    {
        private ITestService testService;

        private IUserService userService =
            System.Web.Mvc.DependencyResolver.Current.GetService(typeof (UserService)) as UserService;

        private IQuestionService questionService =
            System.Web.Mvc.DependencyResolver.Current.GetService(typeof (QuestionService)) as QuestionService;

        private IAnswerService answerService =
            System.Web.Mvc.DependencyResolver.Current.GetService(typeof(AnswerService)) as AnswerService;


        public TestsManagerController(ITestService testService)
        {
            this.testService = testService;
        }

        // GET: TestsManager
        public ActionResult Index()
        {
            IEnumerable<TestEntity> tests = testService.GetByAuthor(userService.GetByLogin(SessionPersister.Username));
            ViewBag.Tests = tests;
            return View();
        }

        public ActionResult NewTest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewTest(TestViewModel test)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid data");
                return View(test);
            }
            TestEntity e = new TestEntity()
            {
                Name = test.Name,
                Description = test.Description,
                AllowedTime = test.AllowedTime,
                Anonymous = test.Anonymous,
                AuthorId = userService.GetByLogin(SessionPersister.Username).Id,
                CreationDate = DateTime.Now,
                GlobalAvailability = test.GlobalAvailability,
                Interview = test.Interview,
                QuestionCount = 0
            };
            testService.CreateTest(e);
            return RedirectToAction("Index");
        }

        public ActionResult EditTest(int? testId)
        {
            if (testId == null)
                return RedirectToAction("Index");
            TestEntity e = testService.GetById(testId.Value);
            IEnumerable<QuestionEntity> questions = questionService.GetAllTestQuestions(e);
            ViewBag.Questions = questions;
            ViewBag.Test = e;
            if (e != null)
                return View(e);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditTest(TestEntity e)
        {
            IEnumerable<QuestionEntity> questions = questionService.GetAllTestQuestions(e);
            ViewBag.Questions = questions;
            ViewBag.Test = e;
            return View();
        }

        [HttpGet]
        public ActionResult NewQuestion(int? testId)
        {
            if (testId == null)
                return RedirectToAction("Index");
            TestEntity e = testService.GetById(testId.Value);
            ViewBag.Test = e;
            if (e == null)
                return RedirectToAction("Index");
            return View(new QuestionViewModel() {TestId = e.Id});
        }

        [HttpPost]
        public ActionResult NewQuestion(QuestionViewModel qvm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid data");
                return View(qvm);
            }
            IEnumerable<QuestionEntity> questions = questionService.GetAllTestQuestions(testService.GetById(qvm.TestId));
            if (questions.Any(entity => entity.QuestionNumberInTest == qvm.QuestionNumber))
            {
                ModelState.AddModelError("", "Question with such number is occupied");
                return View(qvm);
            }
            if (qvm.QuestionCount<=0)
            {
                ModelState.AddModelError("", "Question count should be positive value");
                return View(qvm);
            }
            string structure = "<question><text>Text</text>";
            ViewBag.Checkbox = qvm.Checkbox;
            ViewBag.Radiobutton = qvm.Radiobutton;
            ViewBag.Textbox = qvm.Textbox;
            ViewBag.Interview = testService.GetById(qvm.TestId).Interview;
            for (int i = 0; i<qvm.QuestionCount; i++)
            {
                if (qvm.QuestionType == 1)
                {
                    structure += $"<cb>{i}</cb>";
                    continue;
                }
                if (qvm.QuestionType == 2)
                {
                    structure += $"<rb>{i}</rb>";
                }
                else
                {
                    structure += $"<tb>{i}</tb>";
                }
            }
            structure += "</question>";
            QuestionEntity question = new QuestionEntity()
            {
                QuestionNumberInTest = qvm.QuestionNumber,
                QuestionStructure = structure,
                TestId = qvm.TestId
            };
            questionService.CreateQuestion(question);
            AnswerViewModel avm = new AnswerViewModel()
            {
                QuestionId = question.Id,
                QuestionType = qvm.QuestionType,
                AnswerCount = qvm.QuestionCount,
                Answers = new string[qvm.QuestionCount],
                CorrectAnswers = new string[qvm.QuestionCount],
                CorrectBoolAnswers = new bool[qvm.QuestionCount],
                AnswerValue = 0,
                Question = ""
            };
            return View("EditQuestion", avm);
        }

        [HttpGet]
        public ActionResult EditQuestion(int? questionId)
        {
            if (questionId == null)
                return RedirectToAction("Index");
            QuestionEntity e = questionService.GetById(questionId.Value);
            ViewBag.Test = e;
            if (e == null)
                return RedirectToAction("Index");
            int count = Regex.Matches(e.QuestionStructure, "<rb>").Count
                + Regex.Matches(e.QuestionStructure, "<cb>").Count
                + Regex.Matches(e.QuestionStructure, "<tb>").Count;
            int questionType = 1;
            if (Regex.Matches(e.QuestionStructure, "<cb>").Count > 0)
                questionType = 1;
            if (Regex.Matches(e.QuestionStructure, "<rb>").Count > 0)
                questionType = 2;
            if (Regex.Matches(e.QuestionStructure, "<tb>").Count > 0)
                questionType = 3;
            AnswerViewModel avm = new AnswerViewModel()
            {
                QuestionId = questionId.Value,
                QuestionType = questionType,
                AnswerCount = count,
                Answers = new string[count],
                CorrectAnswers = new string[count],
                CorrectBoolAnswers = new bool[count],
                AnswerValue = 0,
                Question = ""
            };
            return View(avm);
        }

        [HttpPost]
        public ActionResult EditQuestion(AnswerViewModel e)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid data");
                return View(e);
            }
            QuestionEntity question = questionService.GetById(e.QuestionId);
            string matchCodeTag = @"\<text\>(.*?)\</text\>";
            string textToReplace = question.QuestionStructure;
            string replaceWith = $"<text>{e.Question}</text>";
            string output = Regex.Replace(textToReplace, matchCodeTag, replaceWith);
            question.QuestionStructure = output;
            string answerStructure = "<answer>";
            if (e.QuestionType == 2)
                answerStructure += $"<rb>{e.RadioButtonNumber}</rb>";
            for (int i = 0; i<e.AnswerCount; i++)
            {
                if (e.QuestionType == 1)
                    if (e.CorrectBoolAnswers[i])
                        answerStructure += $"<cb>{e.Answers[i]}</cb>";
                if (e.QuestionType == 3)
                    answerStructure += $"<tb>{e.CorrectAnswers[i]}</tb>";
            }
            answerStructure += "</answer>";
            AnswerEntity answer = new AnswerEntity()
            {
                AnswerValue = e.AnswerValue,
                QuestionId = e.QuestionId,
                AnswerStructure = answerStructure
            };
            answerService.CreateAnswer(answer);
            return RedirectToAction("EditTest", question.TestId);
        }
        
        [HttpGet]
        public ActionResult QuestionInfo(int? questionId)
        {
            if (questionId == null)
                return RedirectToAction("Index");
            QuestionEntity e = questionService.GetById(questionId.Value);
            if (e == null)
                return RedirectToAction("Index");
            int count = Regex.Matches(e.QuestionStructure, "<rb>").Count
                + Regex.Matches(e.QuestionStructure, "<cb>").Count
                + Regex.Matches(e.QuestionStructure, "<tb>").Count;
            int questionType = 1;
            if (Regex.Matches(e.QuestionStructure, "<cb>").Count > 0)
                questionType = 1;
            if (Regex.Matches(e.QuestionStructure, "<rb>").Count > 0)
                questionType = 2;
            if (Regex.Matches(e.QuestionStructure, "<tb>").Count > 0)
                questionType = 3;
            
            string match = @"<text>(.*?)</text>";
            string match1 = @"<cb>(.*?)</cb>";
            string match2 = @"<rb>(.*?)</rb>";
            string match3 = @"<tb>(.*?)</tb>";
            QuestionTestViewModel tvm = new QuestionTestViewModel();
            tvm.Variants = new string[count];
            tvm.Question = Regex.Match(e.QuestionStructure, match).Groups[0].Value;
            tvm.QuestionType = questionType;
            for (int i = 0; i<count; i++)
            {
                if (questionType == 1)
                    tvm.Variants[i] = Regex.Match(e.QuestionStructure, match1).Groups[i].Value;
                if (questionType == 2)
                    tvm.Variants[i] = Regex.Match(e.QuestionStructure, match2).Groups[i].Value;
                if (questionType == 3)
                    tvm.Variants[i] = Regex.Match(e.QuestionStructure, match3).Groups[i].Value;
            }
            return View(tvm);
        }
    }
}