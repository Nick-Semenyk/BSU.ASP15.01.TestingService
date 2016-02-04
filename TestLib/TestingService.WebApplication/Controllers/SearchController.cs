using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingService.BLL.Interface.Services;
using TestingService.BLL.Mappers;
using TestingService.WebApplication.Infrastructure.Mappers;
using TestingService.WebApplication.Models;
using TestingService.WebApplication.Models.Pages;
using TestingService.WebApplication.Security;

namespace TestingService.WebApplication.Controllers
{
    [TestPassing]
    public class SearchController : Controller
    {
        private ITestService testService;

        public  SearchController(ITestService testService)
        {
            this.testService = testService;
        }

        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Search(string query, int? page)
        {
            if (string.IsNullOrEmpty(query))
            {
                return View("Index");
            }
            if (page == null)
                page = 1;
            int pageSize = 1;
            ViewBag.Query = query;
            List<TestViewModel> tests = testService.SearchByString(query).
                Select(entity => entity.ToMvcTest()).ToList();
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page.Value,
                PageSize = pageSize,
                TotalItems = tests.Count()
            };
            tests = tests.Skip((page.Value - 1)*pageSize).Take(pageSize).ToList();
            TestPageViewModel result = new TestPageViewModel() { PageInfo = pageInfo, Tests = tests};
            return View(result);
        }

        public ActionResult AdvancedSearch()
        {
            return View();
        }
    }
}