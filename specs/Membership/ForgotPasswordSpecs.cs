using System;
using System.Web.Mvc;
using System.Web.Routing;
using CommunitySite.Core.Data;
using CommunitySite.Core.Data.NHibernate;
using CommunitySite.Core.Domain;
using CommunitySite.Core.Services.Authentication;
using CommunitySite.Web.UI;
using CommunitySite.Web.UI.Controllers;
using CommunitySite.Web.UI.Models;
using FakeItEasy;
using FakeItEasy.Core;
using Machine.Specifications;
using MvcContrib.TestHelper;

namespace CommunitySite.Specifications.Registration
{
    public class When_a_user_wishes_to_recover_password
    {
        Establish context = () => MvcApplication.RegisterRoutes(RouteTable.Routes);

        It should_navigate_to_the_password_recovery_page = () =>
            "~/account/recoverpassword".ShouldMapTo<AccountController>(ctrl => ctrl.RecoverPassword());
    }

    public class When_navigating_to_the_password_recovery_page : With_a_member_registration_context
    {
        Because of = () => _results = _controller.RecoverPassword();

        It should_load_password_recovery_page = () =>
            _results.AssertViewRendered().ForView("PasswordRecovery");

        It should_load_an_password_recovery_form = () =>
           _results.AssertViewRendered().ViewData.Model.ShouldBe(typeof(PasswordRecoveryModel));

       
    }

    public class When_submitting_valid_and_complete_password_recovery_information : With_a_member_registration_context
    {
        Establish context = () =>
        {
            A.CallTo(() => _memberRepository.GetByEmail("test@test.com")).Returns(new Member { Email = "test@test.com", Password = "1234" });
            _recoveryModel = new PasswordRecoveryModel { Email = "test@test.com" };
            //_controller.ModelState.Clear();
        };

        Because of = () => _results = _controller.RecoverPassword(_recoveryModel);

        It should_get_a_member_by_email = () =>
            A.CallTo(() => _memberRepository.GetByEmail(_recoveryModel.Email)).MustHaveHappened();
        


        //It should_email_password_to_recoverer = () =>
        //    A.CallTo(() => _authenticationService.SignIn("username"));

        //It should_take_the_user_to_the_member_password_sent_page = () =>
        //    _results.AssertActionRedirect().ToAction("Profile");

        protected static PasswordRecoveryModel _recoveryModel;
    }



}