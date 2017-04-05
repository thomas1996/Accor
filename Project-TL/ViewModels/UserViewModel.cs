using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_TL.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel()
        {

        }
        public UserViewModel(User u)
        {
            Username = u.Username;
            Location = u.Location;
            Admin = u.Admin;
        }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name ="Email")]
        [DisplayFormat(DataFormatString = "^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$")]
        [MaxLength(length: 150, ErrorMessage = "{0} has a max lenght of 150 chars")]
        public string Username { get; set; }


        [Required(ErrorMessage = "{0} is required")]
        public string Location { get; set; }

        
        public bool Admin { get; set; }
    }
}