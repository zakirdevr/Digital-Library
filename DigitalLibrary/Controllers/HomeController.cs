using DigitalLibrary.Models;
using DigitalLibrary.Repository;
using DigitalLibrary.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLibrary.Controllers
{
    //[Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly NewBookAlertConfig _newBookAlertConfig = null;
        private readonly NewBookAlertConfig _ThirdBookAlertConfig = null;
        private readonly IMessageRepository _messageRepository = null;
        private readonly IUserService _userService = null;
        private readonly IEmailService _emailService = null;
        public HomeController(IOptionsSnapshot<NewBookAlertConfig>
            newBookAlertConfig, IMessageRepository messageRepository,
            IUserService userService, IEmailService emailService)
        {
            _newBookAlertConfig = newBookAlertConfig.Get("InternalBook");
            _ThirdBookAlertConfig = newBookAlertConfig.Get("ThirdPartyBook");
            _messageRepository = messageRepository;
            _userService = userService;
            _emailService = emailService;
        }
        //public HomeController(IOptions<NewBookAlertConfig> newBookAlertConfig)
        //{
        //    _newBookAlertConfig = newBookAlertConfig.Value;
        //}

        //private readonly IConfiguration _configuration = null;
        //public HomeController(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        //[Route("~/")]
        [Route("")]
        public async Task<ViewResult> Index()
        {
            //---------For Email Start--------
            //UserEmailOptions options = new UserEmailOptions()
            //{
            //    ToEmails = new List<string>() { "test6@gmail.com" },
            //    PlaceHolders = new List<KeyValuePair<string, string>>()
            //    {
            //        new KeyValuePair<string, string>("{{UserName}}", "Planet Of Zakir")
            //    }
            //};
            //await _emailService.SendTestEmail(options);
            //-------- For Email End ---------

            var userId = _userService.GetUserId();
            var isLoggedIn = _userService.IsAuthenticated();

            var newBook = _newBookAlertConfig.IsActive;
            var newBook2 = _ThirdBookAlertConfig.IsActive;
            var result = _messageRepository.GetName();

            //var newBook = new NewBookAlertConfig();
            //_configuration.Bind("NewBookAlert",newBook);
            //var result = newBook.IsActive;
            //var result2 = newBook.AlertMessage;

            //var newBook = _configuration.GetSection("NewBookAlert");
            //var result5 = newBook.GetValue<bool>("IsActive");
            //var result4 = _configuration.GetValue<bool>("NewBookAlert:IsActive");
            //var result = _configuration["AppName"];
            //var result2 = _configuration["MyObject:Key1"];
            //var result3 = _configuration["MyObject:Key3:Name"];
            return View();
        }
        //[Route("~/about-us", Name ="about-us")]
        [Route("about-us", Name = "about-us")]
        public ViewResult About()
        {
            return View();
        }

        //[Route("portfolio")]
        //[HttpGet]

        //[Route("portfolio")][HttpGet]
    
        //[HttpGet("portfolio/{id:int:min(1)}")]
        public ViewResult Portfolio(int id)
        {
            return View();
        }

        //[Route("contact-us/{id:int}/{name:alpha:minlength(3)}")]
        [Authorize(Roles ="Admin,User")]
        public ViewResult Contact()
        {
            return View();
        }
    }
}


