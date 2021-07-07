using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public sealed class User : IdentityUser
    {
        public string FullName { get; set; }

        public string NickName { get; set; }

        public DateTime BirthDay { get; set; }

        public bool IsLock { get; set; }

        public List<Rating> Rating { get; set; } = new List<Rating>();
    }
}
