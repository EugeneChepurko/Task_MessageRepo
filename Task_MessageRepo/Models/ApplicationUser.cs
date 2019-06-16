using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_MessageRepo.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string LastMessage { get; set; }
        public int Year { get; set; }               
        public virtual ICollection<Message> UserMessages { get; set; }                   
    }
}
