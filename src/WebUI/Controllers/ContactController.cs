using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using DAO;
using DTO;
using DAO.Interfaces;
using WebUI.Helpers;

namespace WebUI.Controllers
{
    //[Route("[controller]")]
    public class ContactController : Controller
    {
        private readonly IContactDAO _contactDAO;
        private readonly IMapper _mapper;

        private readonly IPhoneDAO _phoneDAO;

        public ContactController(IContactDAO contactDAO, IMapper mapper, IPhoneDAO phoneDAO)
        {
            _contactDAO = contactDAO;
            _mapper = mapper;
            _phoneDAO = phoneDAO;
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
            try
            {
                var contactDTO = _mapper.Map<ContactDTO>(contact);
                _contactDAO.CreateContact(contactDTO);
                TempData[Constants.Messages.SuccessMessage] = "Contact successfully created!";
            }
            catch (Exception ex)
            {

                TempData[Constants.Messages.ErrorMessage] = $"An error occurred while trying to create the contact. Please try again later. {ex.Message}";
            }
            return RedirectToAction(nameof(Index));
        }
        // private readonly ILogger<ContactController> _logger;

        // public ContactController(ILogger<ContactController> logger)
        // {
        //     _logger = logger;
        // }


        public IActionResult Index()
        {
            var lstContactDTO = _contactDAO.GetAllContacts();
            var lstContact = _mapper.Map<List<ContactViewModel>>(lstContactDTO);

            return View(lstContact);
        }

        public IActionResult Details(int id)
        {
            var contactDTO = _contactDAO.GetContactById(id);
            if (contactDTO is null)
            {
                return NotFound();
            }

            var contact = _mapper.Map<ContactViewModel>(contactDTO);
            var phoneDTOList = _phoneDAO.GetPhonesByContactId(id);
            var phoneList = _mapper.Map<List<PhoneViewModel>>(phoneDTOList);
            foreach (var phone in phoneList)
            {
                phone.PhoneNumber = PhoneNumberHelper.Format(phone.PhoneNumber);
            }
            contact.PhonesList?.AddRange(phoneList);

            return View(contact);
        }

        public IActionResult Edit(int id)
        {

            var contactDTO = _contactDAO.GetContactById(id);
            if (contactDTO is null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ContactViewModel>(contactDTO));
        }

        [HttpPost]
        public IActionResult Edit(ContactViewModel contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }
            

            try
            {
                var contactDTO = _mapper.Map<ContactDTO>(contact);
                _contactDAO.UpdateContact(contactDTO);
                TempData[Constants.Messages.SuccessMessage] = "Contact successfully updated!";
            }
            catch (Exception ex)
            {

                TempData[Constants.Messages.ErrorMessage] = $"An error occurred while trying to update the contact. Please try again later. {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var contactDTO = _contactDAO.GetContactById(id);
            if (contactDTO is null)
            {
                return NotFound();
            }

            var contact = _mapper.Map<ContactViewModel>(contactDTO);
            var phoneDTOList = _phoneDAO.GetPhonesByContactId(id);
            var phoneList = _mapper.Map<List<PhoneViewModel>>(phoneDTOList);
            foreach (var phone in phoneList)
            {
                phone.PhoneNumber = PhoneNumberHelper.Format(phone.PhoneNumber);
            }

            contact.PhonesList.AddRange(phoneList);
            return View(contact);
        }

        [HttpPost]
        public IActionResult Delete(ContactViewModel contact)
        {

            try
            {
                _contactDAO.DeleteContact(contact.Id);
                TempData[Constants.Messages.SuccessMessage] = "Contact successfully deleted!";
            }
            catch (Exception ex)
            {

                TempData[Constants.Messages.ErrorMessage] = $"An error occurred while trying to delete the contact. Please try again later. {ex.Message}";
            }
            return RedirectToAction(nameof(Index));
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}
