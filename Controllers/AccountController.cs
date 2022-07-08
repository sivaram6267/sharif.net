﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OnlineShoppingStore.DAL;
using OnlineShoppingStore.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using System.Configuration;

namespace OnlineShoppingStore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;
        public dbMyOnlineShoppingEntities _DBEntity;
        public DbSet<Tbl_Registration> tblRegristation;

        public AccountController()
        {
        }

        //public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        //{
        //    UserManager = userManager;
        //    SignInManager = signInManager;
        //}

        //public ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        //    }
        //    private set 
        //    { 
        //        _signInManager = value; 
        //    }
        //}

        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}

        ////
        ////// GET: /Account/Login
        ////[AllowAnonymous]
        ////public ActionResult Login(string returnUrl)
        ////{
        ////    ViewBag.ReturnUrl = returnUrl;
        ////    return View();
        ////}

        ////
        //// POST: /Account/Login
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // This doesn't count login failures towards account lockout
        //    // To enable password failures to trigger account lockout, change to shouldLockout: true
        //    var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(returnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.RequiresVerification:
        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
        //        case SignInStatus.Failure:
        //        default:
        //            ModelState.AddModelError("", "Invalid login attempt.");
        //            return View(model);
        //    }
        //}

        ////
        //// GET: /Account/VerifyCode
        //[AllowAnonymous]
        //public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        //{
        //    // Require that the user has already logged in via username/password or external login
        //    if (!await SignInManager.HasBeenVerifiedAsync())
        //    {
        //        return View("Error");
        //    }
        //    return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        ////
        //// POST: /Account/VerifyCode
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // The following code protects for brute force attacks against the two factor codes. 
        //    // If a user enters incorrect codes for a specified amount of time then the user account 
        //    // will be locked out for a specified amount of time. 
        //    // You can configure the account lockout settings in IdentityConfig
        //    var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(model.ReturnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.Failure:
        //        default:
        //            ModelState.AddModelError("", "Invalid code.");
        //            return View(model);
        //    }
        //}

        ////
        //// GET: /Account/Register
        //[AllowAnonymous]
        //public ActionResult Register()
        //{
        //    return View();
        //}

        ////
        //// POST: /Account/Register
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await UserManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

        //            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
        //            // Send an email with this link
        //            // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
        //            // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //            // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

        //            return RedirectToAction("Index", "Home");
        //        }
        //        AddErrors(result);
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        ////
        //// GET: /Account/ConfirmEmail
        //[AllowAnonymous]
        //public async Task<ActionResult> ConfirmEmail(string userId, string code)
        //{
        //    if (userId == null || code == null)
        //    {
        //        return View("Error");
        //    }
        //    var result = await UserManager.ConfirmEmailAsync(userId, code);
        //    return View(result.Succeeded ? "ConfirmEmail" : "Error");
        //}

        ////
        //// GET: /Account/ForgotPassword
        //[AllowAnonymous]
        //public ActionResult ForgotPassword()
        //{
        //    return View();
        //}

        ////
        //// POST: /Account/ForgotPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await UserManager.FindByNameAsync(model.Email);
        //        if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
        //        {
        //            // Don't reveal that the user does not exist or is not confirmed
        //            return View("ForgotPasswordConfirmation");
        //        }

        //        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
        //        // Send an email with this link
        //        // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
        //        // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
        //        // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
        //        // return RedirectToAction("ForgotPasswordConfirmation", "Account");
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        ////
        //// GET: /Account/ForgotPasswordConfirmation
        //[AllowAnonymous]
        //public ActionResult ForgotPasswordConfirmation()
        //{
        //    return View();
        //}

        ////
        //// GET: /Account/ResetPassword
        //[AllowAnonymous]
        //public ActionResult ResetPassword(string code)
        //{
        //    return code == null ? View("Error") : View();
        //}

        ////
        //// POST: /Account/ResetPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var user = await UserManager.FindByNameAsync(model.Email);
        //    if (user == null)
        //    {
        //        // Don't reveal that the user does not exist
        //        return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    }
        //    var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    }
        //    AddErrors(result);
        //    return View();
        //}

        ////
        //// GET: /Account/ResetPasswordConfirmation
        //[AllowAnonymous]
        //public ActionResult ResetPasswordConfirmation()
        //{
        //    return View();
        //}

        ////
        //// POST: /Account/ExternalLogin
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    // Request a redirect to the external login provider
        //    return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        //}

        ////
        //// GET: /Account/SendCode
        //[AllowAnonymous]
        //public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        //{
        //    var userId = await SignInManager.GetVerifiedUserIdAsync();
        //    if (userId == null)
        //    {
        //        return View("Error");
        //    }
        //    var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
        //    var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
        //    return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        ////
        //// POST: /Account/SendCode
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> SendCode(SendCodeViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    // Generate the token and send it
        //    if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
        //    {
        //        return View("Error");
        //    }
        //    return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        //}

        ////
        //// GET: /Account/ExternalLoginCallback
        //[AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    // Sign in the user with this external login provider if the user already has a login
        //    var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(returnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.RequiresVerification:
        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
        //        case SignInStatus.Failure:
        //        default:
        //            // If the user does not have an account, then prompt the user to create an account
        //            ViewBag.ReturnUrl = returnUrl;
        //            ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
        //            return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
        //    }
        //}

        ////
        //// POST: /Account/ExternalLoginConfirmation
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Index", "Manage");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            return View("ExternalLoginFailure");
        //        }
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await UserManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await UserManager.AddLoginAsync(user.Id, info.Login);
        //            if (result.Succeeded)
        //            {
        //                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //        AddErrors(result);
        //    }

        //    ViewBag.ReturnUrl = returnUrl;
        //    return View(model);
        //}

        ////
        //// POST: /Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LogOff()
        //{
        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        //    return RedirectToAction("Index", "Home");
        //}

        ////
        //// GET: /Account/ExternalLoginFailure
        //[AllowAnonymous]
        //public ActionResult ExternalLoginFailure()
        //{
        //    return View();
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (_userManager != null)
        //        {
        //            _userManager.Dispose();
        //            _userManager = null;
        //        }

        //        if (_signInManager != null)
        //        {
        //            _signInManager.Dispose();
        //            _signInManager = null;
        //        }
        //    }

        //    base.Dispose(disposing);
        //}

        //#region Helpers
        //// Used for XSRF protection when adding external logins
        //private const string XsrfKey = "XsrfId";

        //private IAuthenticationManager AuthenticationManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().Authentication;
        //    }
        //}

        

        //private void AddErrors(IdentityResult result)
        //{
        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError("", error);
        //    }
        //}

        //private ActionResult RedirectToLocal(string returnUrl)
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    return RedirectToAction("Index", "Home");
        //}

        //internal class ChallengeResult : HttpUnauthorizedResult
        //{
        //    public ChallengeResult(string provider, string redirectUri)
        //        : this(provider, redirectUri, null)
        //    {
        //    }

        //    public ChallengeResult(string provider, string redirectUri, string userId)
        //    {
        //        LoginProvider = provider;
        //        RedirectUri = redirectUri;
        //        UserId = userId;
        //    }

        //    public string LoginProvider { get; set; }
        //    public string RedirectUri { get; set; }
        //    public string UserId { get; set; }

        //    public override void ExecuteResult(ControllerContext context)
        //    {
        //        var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
        //        if (UserId != null)
        //        {
        //            properties.Dictionary[XsrfKey] = UserId;
        //        }
        //        context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        //    }
        //}
        //#endregion

        //public ActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Login(Models.LoginViewModel model)
        //{
        //    using (var context = new dbMyOnlineShoppingEntities())
        //    {
        //        bool isValid = context.Tbl_MemberRol.Any(x => x.UserName == model.UserName && x.Password == model.Password);
        //        if (isValid)
        //        {
        //            FormsAuthentication.SetAuthCookie(model.UserName, false);
        //            return RedirectToAction("Index", "Employees");
        //        }

        //        ModelState.AddModelError("", "Invalid username and password");
        //        return View();
        //    }
        //}



        [AllowAnonymous]

        public ActionResult UserRegistration(UserRegration UserReg)
        {
            var result = false;
            try
            {
                using (var Context = new dbMyOnlineShoppingEntities())
                {
                    Tbl_Registration dbreg = new Tbl_Registration();
                    Random random = new Random();
                    int id = random.Next();

                    int otpValue = new Random().Next(100000, 999999);

                    UserReg.EmailVerification = false;
                    UserReg.ActivetionCode = Guid.NewGuid();
                    if (UserReg != null)
                    {
                        dbreg.Id = id;
                        dbreg.FristName = UserReg.FirstName;
                        dbreg.LastName = UserReg.LastName;
                        dbreg.PhoneNumber = Convert.ToInt64(UserReg.Phone);
                        dbreg.EmailId = UserReg.Email;
                        dbreg.Password = null;
                        dbreg.ConfirmPassowrd = null;
                        dbreg.CreatedOn = DateTime.Now;
                        dbreg.ModifiedOn = DateTime.Now;
                        dbreg.IsActive = true;
                        dbreg.IsDelete = false;
                        dbreg.EmailVerification = UserReg.EmailVerification;
                        dbreg.ActivetionCode = UserReg.ActivetionCode;
                        dbreg.RoleId = UserReg.IsAdmin;
                        dbreg.OTP = otpValue;
                        Context.Tbl_Registration.Add(dbreg);
                        Context.SaveChanges();

                        SendEmailToUser(UserReg.Email, otpValue);
                        SendOTPToUser(Convert.ToInt64(UserReg.Phone),otpValue);
                        result = true;
                    }

                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public void SendOTPToUser(long phonenumber,int otp)
        {
            int otpValue = otp;
            var status = "";
            try
            {
                // Find your Account Sid and Auth Token at twilio.com/console
                string OtpMessage = "Your OTP Number is " + otpValue + " ( Sent By : Online Super Market )";
                const string accountSid = "ACc36fc726a563c78ee25f940f3dc4190c";
                const string authToken = "145cfc0c791d05fd84e0cc1b696a4e91";

                TwilioClient.Init(accountSid, authToken);               
                // var to = new PhoneNumber("+91" + phonenumber);
                var message = MessageResource.Create(
                    to: new PhoneNumber("+91"+phonenumber.ToString()),                    
                    from: new PhoneNumber("+19402602938"), //  From number, must be an SMS-enabled Twilio number ( This will send sms from ur "To" numbers ). 
                    body: OtpMessage);


                //    ViewBag.Message = "Message Sent";
            }
            catch (Exception e)
            {

                throw (e);

            }
        }
        public void SendEmailToUser(string emailId, int OTP)
        {
            var GenarateUserVerificationLink = "/Home/UserVerification/" + OTP;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, GenarateUserVerificationLink);

            var fromMail = new MailAddress("mhemalathalance@gmail.com", "hemalatha"); // set your email    
            var fromEmailpassword = "mhema@123"; // Set your password     
            var toEmail = new MailAddress(emailId);

            var smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = true;
            smtp.UseDefaultCredentials = true;
            NetworkCredential NetworkCred = new NetworkCredential(fromMail.Address, fromEmailpassword);
            smtp.Credentials = NetworkCred;

            var Message = new MailMessage(fromMail, toEmail);
            Message.Subject = "Registration Completed";
            Message.Body = "<br/> Your registration completed succesfully." +
                           "<br/> please click on the below link for account verification" +
                           "<br/><br/><a href=" + link + ">" + link + "</a>" +
                           "<br/> please use this OTP "+OTP+" for User Login";
            Message.IsBodyHtml = true;

            smtp.Send(Message);
        }
       
        //public string GeneratePassword()
        //{
        //    string OTPLength = "4";
        //    string OTP = string.Empty;

        //    string Chars = string.Empty;
        //    Chars = "1,2,3,4,5,6,7,8,9,0";

        //    char[] seplitChar = { ',' };
        //    string[] arr = Chars.Split(seplitChar);
        //    string NewOTP = "";
        //    string temp = "";
        //    Random rand = new Random();
        //    for (int i = 0; i < Convert.ToInt32(OTPLength); i++)
        //    {
        //        temp = arr[rand.Next(0, arr.Length)];
        //        NewOTP += temp;
        //        OTP = NewOTP;
        //    }
        //    return OTP;
        //}
        [AllowAnonymous]
        public ActionResult UserLogin(UserloginModel UserLogin)
        {
            var result = false;
            try
            {
                Tbl_Registration reg = new Tbl_Registration();
                using (var Context = new dbMyOnlineShoppingEntities())
                {
                    var IsAdmin = Context.Tbl_Registration.Select(x => x.RoleId).FirstOrDefault();
                    foreach (var item in Context.Tbl_Registration)
                    {
                        if (item.EmailId == UserLogin.UserName && item.OTP.ToString() == UserLogin.OTP && item.RoleId==true)
                        {
                            result = true;
                            break;
                        }
                        else
                        {
                            return Json(result=false, JsonRequestBehavior.AllowGet);
                        }
                    }
                    //ViewData["ISADMIN"] = IsAdmin;
                }
               
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            
            
           
        }
        [AllowAnonymous]
        public void SendResetLink(string UserName)
        {
            Random random = new Random();           

            int newOtp = new Random().Next(100000, 999999);


            var GenarateUserVerificationLink = "/Home/ResetPassword/";
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, GenarateUserVerificationLink);

            var fromMail = new MailAddress("mhemalathalance@gmail.com", "hemalatha"); // set your email    
            var fromEmailpassword = "mhema@123"; // Set your password     
            var toEmail = new MailAddress(UserName);

            var smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = true;
            smtp.UseDefaultCredentials = true;
            NetworkCredential NetworkCred = new NetworkCredential(fromMail.Address, fromEmailpassword);
            smtp.Credentials = NetworkCred;

            var Message = new MailMessage(fromMail, toEmail);
            Message.Subject = "Reset Link Send Successfully";
            Message.Body = "<br/> Your Resent Password link succesfully." +
                           "<br/> please click on the below link for Login your account" +
                           "<br/><br/><a href=" + link + ">" + link + "</a>" +
                           "<br/> please use this OTP " + newOtp + " for User Login";
            Message.IsBodyHtml = true;

            smtp.Send(Message);

            //return View();
        }


       

        //public ActionResult Rolebasedpage()
        //{

        //    using(var Context =new dbMyOnlineShoppingEntities())
        //    {
        //        var UserType=Context.Tbl_Roles.Select(x=>x.Name.ToString()).FirstOrDefault();
        //        if (UserType.ToLower() == "Admin")
        //        {
        //            return PartialView("_AdminLayout");
        //        }
        //        else
        //        {
        //            return View();
        //        }
        //    }
        //}


    }


}