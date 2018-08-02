using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwitterSearcher.Data;
using TwitterSearcher.ViewModels;

namespace TwitterSearcher.Controllers
{
    public class SearchController : Controller
    {
        //db init
        private UserDbContext context;
        public SearchController(UserDbContext dbContext)
        {
            context = dbContext;
        }

        //Index
        public IActionResult Index()
        {
            return View();
        }

        //New search
        public IActionResult Add()
        {
            SearchViewModel newSearchViewModel = new SearchViewModel();
            return View(newSearchViewModel);
        }
    }
}