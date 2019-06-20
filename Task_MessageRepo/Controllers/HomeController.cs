using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Task_MessageRepo.Models;

namespace Task_MessageRepo.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db = new ApplicationContext();
        Message message = new Message();

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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Index(ApplicationUser user)
        {
            IEnumerable<ApplicationUser> users = db.Users /*UserManager.Users*/;

            ApplicationUser foundUser = await UserManager.FindByEmailAsync(User.Identity.Name);
            foundUser.LastMessage = user.LastMessage;
            message.DateTime = DateTime.Now;
            message.UserName = User.Identity.Name;
            if (foundUser.UserMessages == null)
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

            IdentityResult result = await UserManager.UpdateAsync(foundUser);
            if(result.Succeeded)
            {
                await db.SaveChangesAsync();
            }

            //db.Users.Add(foundUser); // ??
            //db.Entry(foundUser).State = System.Data.Entity.EntityState.Modified;
         
            ViewBag.Customers = users;
            ViewBag.list = foundUser.UserMessages;
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult ViewMyMessages(string id, string sortOrder)
        {
            ApplicationUser user = db.Users.FirstOrDefault(i => i.Id == id);

            ViewBag.IdSortParam = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.DateSortParam = sortOrder == "Date" ? "date_desc" : "Date";
            var messages = from s in user.UserMessages
                           select s;
            switch (sortOrder)
            {
                case "id_desc":
                    messages = messages.OrderByDescending(s => s.Id);
                    break;
                case "Date":
                    messages = messages.OrderBy(s => s.DateTime);
                    break;
                case "date_desc":
                    messages = messages.OrderByDescending(s => s.DateTime);
                    break;
                default:
                    messages = messages.OrderBy(s => s.Id);
                    break;
            }
            return View(messages.ToList());
        }

        [Authorize]
        [HttpGet]
        public ActionResult ViewAllMessages(string sortOrder)
        {
            ViewBag.IdSortParam = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.DateSortParam = sortOrder == "Date" ? "date_desc" : "Date";
            var messages = from mess in db.Messages
                           select mess;
            switch (sortOrder)
            {
                case "id_desc":
                    messages = messages.OrderByDescending(s => s.Id);
                    break;
                case "Date":
                    messages = messages.OrderBy(s => s.DateTime);
                    break;
                case "date_desc":
                    messages = messages.OrderByDescending(s => s.DateTime);
                    break;
                default:
                    messages = messages.OrderBy(s => s.Id);
                    break;
            }
            return View(messages.ToList());
        }

        [Authorize]
        public async Task<RedirectToRouteResult> DeleteMessage(int id)
        {    
            Message message = await db.Messages.FindAsync(id);
            if(message != null)
            {
                db.Messages.Remove(message);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("ViewAllMessages");
        }      

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "My contacts.";
            return View();
        }
    }
}