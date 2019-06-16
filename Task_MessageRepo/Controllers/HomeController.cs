using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task_MessageRepo.Models;

namespace Task_MessageRepo.Controllers
{
    
    public class HomeController : Controller
    {
        ApplicationContext db = new ApplicationContext();
        Message message = new Message();
        //Message Message = new Message();
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<ApplicationUser> users = db.Users;
            ViewBag.Customers = users;
            //var listMessages = dbMessage.Messages;
            return View();
        }

        [HttpPost]
        public ActionResult Index(ApplicationUser user)
        {
            IEnumerable<ApplicationUser> users = db.Users /*UserManager.Users*/;
            
            ApplicationUser foundUser = UserManager.FindByEmail(User.Identity.Name);
            foundUser.LastMessage = user.LastMessage;
           
            if(foundUser.UserMessages == null)
            {
                
                message.ApplicationUserId = foundUser.Id;
                message.Mess = foundUser.LastMessage;

                foundUser.UserMessages = new List<Message>();
                foundUser.UserMessages.Add(message);
            }
            else
            {
                message.ApplicationUserId = foundUser.Id;
                message.Mess = foundUser.LastMessage;

                foundUser.UserMessages.Add(message);
            }
            
            //db.Entry(foundUser).State = System.Data.Entity.EntityState.Modified;
            IdentityResult result = UserManager.Update(foundUser);
            
            //db.Users.Add(foundUser); // ??
            //db.Entry(foundUser).State = System.Data.Entity.EntityState.Modified;
            
            db.SaveChanges();
            ViewBag.Customers = users;
            ViewBag.list = foundUser.UserMessages;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}