using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class Contact
    {

        public int Id { get; set; }
        
        [DisplayName("First Name")]
        public string Name{get;set;}

         [DisplayName("Surname")]
        public string Surname{get;set;}

         [DisplayName("Email")]
        public string Email{get;set;}
    }
}