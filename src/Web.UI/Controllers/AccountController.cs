﻿using System;
using System.Web.Mvc;
using CommunitySite.Core.Data;
using CommunitySite.Core.Domain;
using CommunitySite.Core.Services.Authentication;
using CommunitySite.Web.UI.Models;

namespace CommunitySite.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        readonly MemberRepository _memberRepository;
        readonly AuthenticationService _authenticationService;

        public AccountController(MemberRepository memberRepository, AuthenticationService authenticationService)
        {
            _memberRepository = memberRepository;
            _authenticationService = authenticationService;
        }

        public ActionResult Register()
        {
            return View("Register", new RegistrationModel());
        }

        [HttpPost]
        public ActionResult Register(RegistrationModel registrationModel)
        {
            if (!ModelState.IsValid)
                return View("Register");

            var member = new Member
                             {
                                 FirstName = registrationModel.FirstName,
                                 LastName = registrationModel.LastName,
                                 Username = registrationModel.Username,
                                 Password = registrationModel.Password,
                                 Email = registrationModel.Email
                             };
            
            _memberRepository.Save(member);
            _authenticationService.SignIn(member.Username);
            return RedirectToAction("Profile");
        }

        public ActionResult RecoverPassword()
        {
            return View("PasswordRecovery", new PasswordRecoveryModel());
        }

        [HttpPost]
        public ActionResult RecoverPassword(PasswordRecoveryModel recoveryModel)
        {
            if (!ModelState.IsValid)
                return View("PasswordRecover");

            var member = new Member
            {
                Email = recoveryModel.Email
            };

            _memberRepository.Save(member);
            _authenticationService.SignIn(member.Username);
            return RedirectToAction("Profile");
        }
    }
}