using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using AutoMapper;
using WebUI.Models;
using DAO;
using DTO;
using DAO.Interfaces;

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

        public IActionResult create(int contactId)
        {
            var phoneViewModel = new PhoneViewModel { ContactId = contactId };
            return View(phoneViewModel);

        }

        [HttpPost]
        public IActionResult create(PhoneViewModel phoneViewModel)
        {
            var phoneDTO = _mapper.Map<PhoneDTO>(phoneViewModel);
            _phoneDAO.CreatePhone(phoneDTO);
            return RedirectToAction("Details", "Contact", new { id = phoneViewModel.ContactId });
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