using System;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class AppUser
    {
        public int AppUserId { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
    }
}