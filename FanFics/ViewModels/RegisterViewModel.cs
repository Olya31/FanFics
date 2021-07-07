using DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace FanFics.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "NickName")]
        public string NickName { get; set; }

        [Required]
        [Display(Name = "FullName")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "BirthDay")]
        public DateTime BirthDay { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password mismatch")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]

        public string PasswordConfirm { get; set; }
        
        public bool IsLock { get; set; } = false;

        public User ToUser()
        {
            return new User
            {
                Email = this.Email,
                UserName = this.Email,               
                FullName = this.FullName,
                NickName = this.NickName,
                BirthDay = this.BirthDay
            };
        }
    }
}
