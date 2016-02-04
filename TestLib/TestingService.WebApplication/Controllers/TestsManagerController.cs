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
using TestingService.WebApplication.Infrastructure.Mappers;
using TestingService.WebApplication.Models;
using TestingService.WebApplication.Security;
using TestingService.WebApplication.Models.Question;
using TestingService.WebApplication.Models.Answer;

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

        private IUsersAnswersService usersAnswerService =
            System.Web.Mvc.DependencyResolver.Current.GetService(typeof(UsersAnswersService)) as UsersAnswersService;


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

     /*   [HttpGet]
        public ActionResult TestInfo(int? testId)
        {
            if (testId == null)
            {
                return RedirectToAction("Index");
            }
            TestEntity e = testService.GetById(testId.Value);
            if (e == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Username = userService.GetUserEntity(e.AuthorId).ToMvcUser().Login;
            return View(e);
        }*/

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
                return View("Error");
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
            TestEntity oldTest = testService.GetById(e.Id);
            e.CreationDate = oldTest.CreationDate;
            e.AuthorId = oldTest.AuthorId;
            e.QuestionCount = oldTest.QuestionCount;
            ViewBag.Questions = questions;
            ViewBag.Test = e;
            testService.UpdateTest(e);
            return View(e);
        }

        [HttpGet]
        public ActionResult NewQuestion(int? testId)
        {
            if (testId == null)
                return RedirectToAction("Index");
            TestEntity e = testService.GetById(testId.Value);
            if (e == null)
                return RedirectToAction("Index");
            e.QuestionCount++;
            testService.UpdateTest(e);
            ViewBag.Test = e;
            QuestionViewModel qvm = new QuestionViewModel()
            {
                TestId = e.Id,
                QuestionNumber = 0
            };
            questionService.CreateQuestion(qvm.ToBllQuestion());
            qvm = questionService.GetAllTestQuestions(e).Select(entity => entity.ToMvcQuestion()).OrderByDescending(model => model.QuestionNumber).First();
            return RedirectToAction("EditQuestion", new {questionId = qvm.Id});
            return View(qvm);
        }

        [HttpPost]
        public ActionResult NewQuestion(QuestionViewModel qvm)
        {
            qvm = questionService.GetById(qvm.Id).ToMvcQuestion();
            var post = Request.Form;
            for (int i = 0; i<qvm.Items.Count; i++)
            {
                qvm.Items[i].Value = post[i.ToString()];
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid data");
                return View(qvm);
            }
            //!user, rights
            questionService.UpdateQuestion(qvm.ToBllQuestion());
            return View(qvm);
        }

        [HttpGet]
        public ActionResult EditQuestion(int? questionId)
        {
            if (questionId == null)
            {
                return RedirectToAction("Index");
            }
            QuestionViewModel e = questionService.GetById(questionId.Value).ToMvcQuestion();
            ViewBag.Answers = answerService.GetAllAnswers(e.ToBllQuestion()).Select(entity => entity.ToMvcAnswer());
            return View("EditQuestion", e);
        }

        [HttpGet]
        public ActionResult AddToQuestion(int? type, int? testId, int? questionId)
        {
            if (type == null || testId == null || questionId == null)
            {
                return View("Error");
            }
            QuestionViewModel e = questionService.GetById(questionId.Value).ToMvcQuestion();
            e.Items.Add(new QuestionItem() {ItemType = (DisplayType) type.Value, Value = " "});
            questionService.UpdateQuestion(e.ToBllQuestion());
            return RedirectToAction("EditQuestion", new {questionId = e.Id});
        }

        [HttpPost]
        public ActionResult EditQuestion(QuestionViewModel qvm)
        {
            qvm = questionService.GetById(qvm.Id).ToMvcQuestion();
            var post = Request.Form;
            for (int i = 0; i < qvm.Items.Count; i++)
            {
                qvm.Items[i].Value = post[i.ToString()];
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid data");
                return View(qvm);
            }
            //!user, rights
            ViewBag.Answers = answerService.GetAllAnswers(qvm.ToBllQuestion()).Select(entity => entity.ToMvcAnswer());
            questionService.UpdateQuestion(qvm.ToBllQuestion());
            return View(qvm);
        }

        [HttpGet]
        public ActionResult DeleteQuestion(int? questionId)
        {
            if (questionId == null)
                return View("Error");
            QuestionViewModel qvm = questionService.GetById(questionId.Value).ToMvcQuestion();
            IEnumerable<AnswerEntity> answers = answerService.GetAllAnswers(qvm.ToBllQuestion());
            foreach(AnswerEntity a in answers)
            {
                answerService.DeleteAnswer(a);
            }
            IEnumerable<UsersAnswersEntity> usersAnswers = usersAnswerService.GetByPredicate(entity => entity.QuestionId == qvm.Id);
            foreach(UsersAnswersEntity e in usersAnswers)
            {
                usersAnswerService.DeleteAnswer(e);
            }
            List<QuestionEntity> questions =
                questionService.GetAllTestQuestions(testService.GetById(qvm.TestId)).ToList();
            bool dec = false;
            for (int i = 0; i<questions.Count; i++)
            {
                if (dec && questions[i].QuestionNumberInTest!=null)
                {
                    questions[i].QuestionNumberInTest--;
                }
                if (qvm.Id == questions[i].Id)
                {
                    questionService.DeleteQuestion(qvm.ToBllQuestion());
                    dec = true;
                }
            }
            TestEntity test = testService.GetById(qvm.TestId);
            test.QuestionCount--;
            testService.UpdateTest(test);
            return RedirectToAction("EditTest", new {testId = qvm.TestId});
        }

        [HttpGet]
        public ActionResult DeleteQuestionItem(int? questionId, int? id)
        {
            if (questionId == null || id == null)
            {
                return View("Error");
            }
            QuestionViewModel qvm = questionService.GetById(questionId.Value).ToMvcQuestion();
            int j = id.Value;
            if (qvm.Items[id.Value].ItemType != DisplayType.Text)
            {
                IEnumerable<AnswerViewModel> answers =
                    answerService.GetAllAnswers(qvm.ToBllQuestion()).Select(entity => entity.ToMvcAnswer());
                if (answers.Any())
                {
                    for (int i=0; i<=id.Value; i++)
                    {
                        if (qvm.Items[i].ItemType == DisplayType.Text)
                            j--;
                    }
                    foreach(AnswerViewModel a in answers)
                    {
                        a.Items.RemoveAt(j);
                        answerService.UpdateAnswer(a.ToBllAnswer());
                    }
                }
            }
            qvm.Items.RemoveAt(id.Value);
            questionService.UpdateQuestion(qvm.ToBllQuestion());
            return RedirectToAction("EditQuestion", new {questionId = questionId.Value});
        }
        
        [HttpGet]
        public ActionResult QuestionInfo(int? questionId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult AddAnswer(int? testId, int? questionId)
        {
            if (testId == null || questionId == null)
            {
                return View("Error");
            }
            AnswerViewModel a = new AnswerViewModel()
            {
                AnswerValue = 0,
                QuestionId = questionId.Value
            };
            QuestionViewModel q = questionService.GetById(a.QuestionId).ToMvcQuestion();
            foreach (QuestionItem item in q.Items)
            {
                a.Items.Add(new AnswerItem() {ItemType = item.ItemType, BoolValue = false, Value = ""});
            }
            answerService.CreateAnswer(a.ToBllAnswer());
            a = answerService.GetByPredicate(entity => entity.QuestionId == questionId.Value).Last().ToMvcAnswer();
            return RedirectToAction("EditAnswer", new {answerId = a.Id});
        }

        [HttpGet]
        public ActionResult EditAnswer(int? answerId)
        {
            if (answerId == null)
                return View("Error");
            AnswerViewModel a = answerService.GetAnswerEntity(answerId.Value).ToMvcAnswer();
            if (a == null)
                return View("Error");
            ViewBag.Question = questionService.GetById(a.QuestionId).ToMvcQuestion();
            return View(a);
        }

        [HttpPost]
        public ActionResult EditAnswer(AnswerViewModel a)
        {
            AnswerViewModel answerFromBase = answerService.GetAnswerEntity(a.Id).ToMvcAnswer();
            answerFromBase.AnswerValue = a.AnswerValue;
            a = answerFromBase;
            QuestionViewModel q = questionService.GetById(a.QuestionId).ToMvcQuestion();
            var post = Request.Form;
            int j = 0;
            for (int i = 0; i < q.Items.Count; i++)
            {
                switch(q.Items[i].ItemType)
                {
                    case DisplayType.Text:
                        break;
                    case DisplayType.CheckBox:
                        if (post[i.ToString()] != null)
                            a.Items[j].Value = "true";
                        j++;
                        break;
                    case DisplayType.RadioButton:
                        if (post[i.ToString()] != null)
                            a.Items[j].Value = "true";
                        j++;
                        break;
                    case DisplayType.TextBox:
                        a.Items[j].Value = post[i.ToString()];
                        j++;
                        break;
                }
            }
            answerService.UpdateAnswer(a.ToBllAnswer());
            ViewBag.Question = questionService.GetById(a.QuestionId).ToMvcQuestion();
            return View(a);
        }

        private bool IsTestReady(TestViewModel test)
        {
            TestEntity t = testService.GetById(test.Id);
            if (t.QuestionCount == 0)
                return false;
            IEnumerable<QuestionEntity> questions = questionService.GetAllTestQuestions(test.ToBllTest());
            foreach(QuestionEntity q in questions)
            {
                if(!IsQuestionReady(q.ToMvcQuestion()))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsQuestionReady(QuestionViewModel q)
        {
            int questionItems = 0;
            foreach(QuestionItem item in q.Items)
            {
                if (item.ItemType != DisplayType.Text)
                    questionItems++;
            }
            if (questionItems == 0)
                return false;
            IEnumerable<AnswerViewModel> answers = answerService.GetAllAnswers(q.ToBllQuestion()).Select(entity => entity.ToMvcAnswer());
            foreach (AnswerViewModel a in answers)
                if (answers.Where(b => a!=b).Any(b => a.Structure == b.Structure))
                {
                    return false;
                }
            return true;
        }
    }
}