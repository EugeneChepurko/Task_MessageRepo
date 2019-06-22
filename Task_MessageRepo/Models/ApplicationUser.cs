using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_MessageRepo.Models
{
    [JsonObject(IsReference = true)]
    public class ApplicationUser : IdentityUser
    {
        public string LastMessage { get; set; }      
        public int Year { get; set; }               
        public virtual ICollection<Message> UserMessages { get; set; }                   
    }
}
