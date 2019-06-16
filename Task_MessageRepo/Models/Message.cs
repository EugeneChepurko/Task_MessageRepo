using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Task_MessageRepo.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string Mess { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}