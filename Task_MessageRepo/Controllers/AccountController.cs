using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Task_MessageRepo.Models;

namespace Task_MessageRepo.Controllers
{
    public class AccountController : Controller
    {
        public static List<ApplicationUser> applicationUsers = GetDataFromJson();

        public static List<ApplicationUser> GetDataFromJson()
        {
            if (applicationUsers == null)
            {
                applicationUsers = new List<ApplicationUser>();
            }

            using (StreamReader file = System.IO.File.OpenText(@"E:\STEP\myhomework2017\Task_MessageRepo\Task_MessageRepo\UsersDatabase.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                var usersList_Deserialize = (List<ApplicationUser>)serializer.Deserialize(file, typeof(List<ApplicationUser>));
                return usersList_Deserialize;
            }
        }

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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, Year = model.Year };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    applicationUsers.Add(user);
                    using (StreamWriter file = System.IO.File.CreateText(@"E:\STEP\myhomework2017\Task_MessageRepo\Task_MessageRepo\UsersDatabase.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        //serialize object directly into file stream
                        serializer.Serialize(file, applicationUsers);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Login or password is incorrect.");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (string.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        public async Task<ActionResult> EditCurrentUser()
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                EditModel editModel = new EditModel { Year = user.Year };
                return View(editModel);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<ActionResult> EditCurrentUser(EditModel editModel)
        {
            // FOR adding to db EF
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            string output = "";
            int userYear = user.Year;

            if (user != null)
            {
                user.Year = editModel.Year;
                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    // FOR adding to JSON without db
                    ApplicationUser tempUser = applicationUsers.FirstOrDefault(i => i.Year == userYear);
                    using (StreamReader file = System.IO.File.OpenText(@"E:\STEP\myhomework2017\Task_MessageRepo\Task_MessageRepo\UsersDatabase.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();

                        List<ApplicationUser> jsonObj = (List<ApplicationUser>)serializer.Deserialize(file, typeof(List<ApplicationUser>));
                        foreach (var year in jsonObj)
                        {
                            if (year.Year == tempUser?.Year)
                            {
                                year.Year = editModel.Year;
                            }
                        }
                        output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                    }
                    System.IO.File.WriteAllText(@"E:\STEP\myhomework2017\Task_MessageRepo\Task_MessageRepo\UsersDatabase.json", output);
                    // END adding to JSON without db
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong");
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found!");
            }
            return View(editModel);
        }

        public async Task<ActionResult> Edit(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);

            if (user != null)
            {
                EditModel editModel = new EditModel { Year = user.Year };
                return View(editModel);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditModel editModel)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(editModel.Id);
            if (user != null)
            {
                user.Year = editModel.Year;
                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Что-то пошло не так");
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found!");
            }
            return View(editModel);
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string output = "";
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    using (StreamReader file = System.IO.File.OpenText(@"E:\STEP\myhomework2017\Task_MessageRepo\Task_MessageRepo\UsersDatabase.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();

                        List<ApplicationUser> jsonObj = (List<ApplicationUser>)serializer.Deserialize(file, typeof(List<ApplicationUser>));
                        foreach (var userr in jsonObj)
                        {
                            if (userr.Id == id)
                            {
                                jsonObj.Remove(userr);
                                break;
                            }
                        }
                        output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                    }
                    System.IO.File.WriteAllText(@"E:\STEP\myhomework2017\Task_MessageRepo\Task_MessageRepo\UsersDatabase.json", output);
                    return RedirectToAction("Index", "Home");
                }                              
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login");
        }
    }
}