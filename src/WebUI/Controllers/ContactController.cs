using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebUI.Models;

namespace WebUI.Controllers
{
    //[Route("[controller]")]
    public class ContactController : Controller
    {

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Contact contact)
        {

            return View();
        }
        // private readonly ILogger<ContactController> _logger;

        // public ContactController(ILogger<ContactController> logger)
        // {
        //     _logger = logger;
        // }
        List<Contact> lstContact = new List<Contact>();

        public ContactController()
        {
            lstContact.Add(new Contact() { Id = 1, Name = "Maria", Surname = "Joaquina", Email = "mariajoaquina@hotmail.com" });
            lstContact.Add(new Contact() { Id = 2, Name = "Lucio", Surname = "Flavio", Email = "lucioflavio@gmail.com" });
        }

        public IActionResult Index()
        {

            return View(lstContact);
        }

        public IActionResult Details(int id)
        {

            //var contact = lstContact.FirstOrDefault(c => c.Id == id);
            var contact = lstContact.Find(c => c.Id == id);
            return View(contact);
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}