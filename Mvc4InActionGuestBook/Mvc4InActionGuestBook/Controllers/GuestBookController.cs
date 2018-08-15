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

            ViewBag.Entries = mostRecentEntries.ToList();
            return View();
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
    }
}