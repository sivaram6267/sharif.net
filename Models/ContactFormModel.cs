using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace OnlineShoppingStore.Models
{
    public class ContactFormModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Subject { get; set; }
        public string Email { get; set; }

        public string Body { get; set; }
       
    }
}