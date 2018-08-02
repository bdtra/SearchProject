using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwitterSearcher.Models;
using TwitterSearcher.ViewModels;

namespace TwitterSearcher.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Add()
        {
            AddUserViewModel newAddUserViewModel = new AddUserViewModel();
            return View(newAddUserViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddUserViewModel newAddUserViewModel)
        {
            if (ModelState.IsValid)
            {
                if (newAddUserViewModel.Password == newAddUserViewModel.Verify)
                {
                    return Redirect("/");
                }
                else
                {
                    return View(newAddUserViewModel);
                }
            }
            else return View(newAddUserViewModel);
        }
    }
}