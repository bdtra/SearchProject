using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterSearcher.Data;
using TwitterSearcher.Models;
using TwitterSearcher.ViewModels;

namespace TwitterSearcher.Controllers
{
    public class UserController : Controller
    {
        //db init
        private UserDbContext context;
        public UserController(UserDbContext dbContext)
        {
            context = dbContext;
        }
        
        //Index
        public IActionResult Index()
        {
            return View();
        }
        
        //Add New User
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
                    User newUser = new User
                    {
                        Username = newAddUserViewModel.Username,
                        Email = newAddUserViewModel.Email,
                        Password = newAddUserViewModel.Password
                    };
                    context.Users.Add(newUser);
                    context.SaveChanges();
                    HttpContext.Session.SetString("UserID", newUser.UserID.ToString());
                    return Redirect("/User/" + newUser.UserID.ToString());
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