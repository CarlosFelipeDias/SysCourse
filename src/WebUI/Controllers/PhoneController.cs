using AutoMapper;
using DAO.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using WebUI.Helpers;
using WebUI.Models;

namespace WebUI.Controllers
{
    //[Route("[controller]")]
    public class PhoneController : Controller
    {
        // private readonly ILogger<PhoneController> _logger;

        // public PhoneController(ILogger<PhoneController> logger)
        // {
        //     _logger = logger;
        // }

        private readonly IPhoneDAO _phoneDAO;
        private readonly IMapper _mapper;

        public PhoneController(IPhoneDAO phoneDAO, IMapper mapper)
        {
            _phoneDAO = phoneDAO;
            _mapper = mapper;
        }

        public IActionResult Create(int contactId)
        {
            var phoneViewModel = new PhoneViewModel { ContactId = contactId };
            return View(phoneViewModel);

        }

        [HttpPost]
        public IActionResult Create(PhoneViewModel phoneViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(phoneViewModel);
            }

            phoneViewModel.PhoneNumber = PhoneNumberHelper.Normalize(phoneViewModel.PhoneNumber);
            var phoneDTO = _mapper.Map<PhoneDTO>(phoneViewModel);
            _phoneDAO.CreatePhone(phoneDTO);
            return RedirectToAction("Details", "Contact", new { id = phoneViewModel.ContactId });
        }

        public IActionResult Edit(int id, int contactId)
        {
            var phoneDTO = _phoneDAO.GetPhoneById(id);
            if (phoneDTO is null || phoneDTO.ContactId != contactId)
            {
                return NotFound();
            }

            var phoneViewModel = _mapper.Map<PhoneViewModel>(phoneDTO);
            phoneViewModel.PhoneNumber = PhoneNumberHelper.Format(phoneViewModel.PhoneNumber);
            return View(phoneViewModel);
        }

        [HttpPost]
        public IActionResult Edit(PhoneViewModel phoneViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(phoneViewModel);
            }

            phoneViewModel.PhoneNumber = PhoneNumberHelper.Normalize(phoneViewModel.PhoneNumber);
            var phoneDTO = _mapper.Map<PhoneDTO>(phoneViewModel);
            _phoneDAO.UpdatePhone(phoneDTO);
            return RedirectToAction("Details", "Contact", new { id = phoneViewModel.ContactId });
        }

        public IActionResult Delete(int id, int contactId)
        {
            _phoneDAO.DeletePhone(id);
            return RedirectToAction("Details", "Contact", new { id = contactId });
        }
        // public IActionResult Index()
        // {
        //     return View();
        // }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}
