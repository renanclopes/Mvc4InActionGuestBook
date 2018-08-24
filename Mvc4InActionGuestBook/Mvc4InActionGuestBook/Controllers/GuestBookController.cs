using Mvc4InActionGuestBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc4InActionGuestBook.Controllers
{
    public class GuestBookController : Controller
    {
        private GuestbookContext _db = new GuestbookContext();
        
        public ActionResult Index()
        {
            var mostRecentEntries =
                                    (from entry in _db.Entries
                                     orderby entry.DateAdded descending
                                     select entry).Take(20);

            var model = mostRecentEntries.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(GuestbookEntry entry)
        {
            entry.DateAdded = DateTime.Now;

            _db.Entries.Add(entry);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ViewResult Show(int id)
        {
            var entry = _db.Entries.Find(id);

            bool hasPermission = User.Identity.Name == entry.Name;
            ViewData["hasPermission"] = hasPermission;

            return View(entry);
        }

    }
}