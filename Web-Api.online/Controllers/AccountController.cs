﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web_Api.online.Controllers
{
    public class AccountController : Controller
    {
        [Route("Account/Login")]
        public ActionResult Login(string ReturnUrl)
        {
            return Redirect("/Identity/Account/Login?ReturnUrl=" + ReturnUrl);
        }

        [Route("Account/Register")]
        public ActionResult Register(string ReturnUrl)
        {
            return Redirect("/Identity/Account/Register?ReturnUrl=" + ReturnUrl);
        }
    }
}
