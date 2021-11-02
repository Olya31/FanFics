using DAL.Models;
using System;

namespace FanFics.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string NickName { get; set; }

        public DateTime BirthDay { get; set; }

        public bool IsComment { get; set; }

        public UserViewModel ToUserViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                NickName = user.NickName,
                BirthDay = user.BirthDay,
                IsComment = false
            };
        }
    }
}

