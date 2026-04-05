using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebUI.Models;
using DAO;
using DTO;

namespace WebUI.Controllers
{
    //[Route("[controller]")]
    public class ContactController : Controller
    {
        private readonly IContactDAO _contactDAO;

        private static ContactViewModel ToModel(ContactDTO contactDTO)
        {
            return new ContactViewModel
            {
                Id = contactDTO.Id,
                Name = contactDTO.Name,
                Surname = contactDTO.Surname,
                Email = contactDTO.Email
            };
        }

        private static ContactDTO ToDto(ContactViewModel contact)
        {
            return new ContactDTO
            {
                Id = contact.Id,
                Name = contact.Name,
                Surname = contact.Surname,
                Email = contact.Email
            };
        }

        public IActionResult Create()
        {
                      return View();
        }

        [HttpPost]
        public IActionResult Create(ContactViewModel contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            _contactDAO.CreateContact(ToDto(contact));

            return RedirectToAction(nameof(Index));
        }
        // private readonly ILogger<ContactController> _logger;

        // public ContactController(ILogger<ContactController> logger)
        // {
        //     _logger = logger;
        // }
       
        
        public ContactController(IContactDAO contactDAO)
        {
            _contactDAO = contactDAO;
        }
    
        public IActionResult Index()
        {
            var lstContactDTO = _contactDAO.GetAllContacts();
            var lstContact = new List<ContactViewModel>();
            foreach (var contactDTO in lstContactDTO)
            {
                lstContact.Add(ToModel(contactDTO));
            }

            return View(lstContact);
        }

        public IActionResult Details(int id)
        {
            var contactDTO = _contactDAO.GetContactById(id);
            if (contactDTO is null)
            {
                return NotFound();
            }

            var contact = ToModel(contactDTO);

            return View(contact);
        }

        public IActionResult Edit(int id)
        {
            
            var contactDTO = _contactDAO.GetContactById(id);
            if (contactDTO is null)
            {
                return NotFound();
            }

            return View(ToModel(contactDTO));
        }

        [HttpPost]
        public IActionResult Edit(ContactViewModel contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            _contactDAO.UpdateContact(ToDto(contact));

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var contactDTO = _contactDAO.GetContactById(id);
            if (contactDTO is null)
            {
                return NotFound();
            }

            return View(ToModel(contactDTO));
        }

        [HttpPost]
        public IActionResult Delete(ContactViewModel contact)
        {
            _contactDAO.DeleteContact(contact.Id);

            return RedirectToAction(nameof(Index));
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}
