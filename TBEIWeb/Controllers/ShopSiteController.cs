using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TBEIWeb.Models;

namespace TBEIWeb.Controllers
{
    [Authorize]
    public class ShopSiteController : Controller
    {
        // GET: ShopSite
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShopSite(string siteName)
        {
            ViewData["ShopName"] = siteName;
            return View();
        }

        public ActionResult ProNest(string siteName)
        {
            // Pass the name of the TBEI Site to be displayed at the top of the screen.
            ViewData["ShopName"] = siteName;

            var DataSource = new PronestEntities().Nests_Get(null, null, null, null, false, null).ToList();
            ViewBag.datasource = DataSource;
            return View("ProNest","_LayoutFullScreen");
        }

    }
}