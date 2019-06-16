using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Task_MessageRepo.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Message> Messages { get; set; }

        public ApplicationContext() : base("MessageRepo", throwIfV1Schema: false) { }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
}