using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using TBEIWeb.Models;

namespace TBEIWeb.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        public virtual ActionResult Index()
        {
            return View();
        }

        public class LoginViewModel
        {
                [Required]
                [Display(Name = "Username")]
                public string UserName { get; set; }

                [Required]
                [DataType(DataType.Password)]
                [Display(Name = "Password")]
                public string Password { get; set; }

                [Display(Name = "Remember me?")]
                public bool RememberMe { get; set; }
            }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Index(LoginViewModel model,string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            

            // usually this will be injected via DI. but creating this manually now for brevity
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            var authService = new AdAuthenticationService(authenticationManager);

            var authenticationResult = authService.SignIn(model.UserName, model.Password);

            if (authenticationResult.IsSuccess)
            {
                // we are in!
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", authenticationResult.ErrorMessage);
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            
            return RedirectToAction("ShopSite", "ShopSite", Session["ShopName"].ToString());
        }


    }
}