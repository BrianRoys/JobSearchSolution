using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobSearchSolution;

namespace JobSearchSolution.Controllers
{
    public class EventsController : Controller
    {
        private JSSEntities2 db = new JSSEntities2();

        // GET: Events
        public ActionResult Index()
        {
			var events = db.Event.Where(c => c.UserId == SessionValues.CurrentUserId).Include(e => e.EventType).Include(e => e.User);
			return View(events.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			// The '@' changes a keyword 'event' to a variable name.
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.Type = new SelectList(db.EventType, "Id", "Type");
            ViewBag.Contact = new SelectList(db.Contact, "Id", "Name");
            ViewBag.Opp = new SelectList(db.Opp, "Id", "Name");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Type,Date,Description,Results")] Event @event)
        {
            if (ModelState.IsValid)
            {
				@event.UserId = SessionValues.CurrentUserId;
				string str = Request["Opp"]; // e.g. "1,2,5"
				if (str != null)
				{
					foreach (string oId in str.Split(','))
					{
						Opp o = db.Opp.Find(Int32.Parse(oId));
						@event.Opp.Add(o);
					}
				}
				str = Request["Contact"]; // e.g. "1,2,5"
				if (str != null)
				{
					foreach (string cId in str.Split(','))
					{
						Contact c = db.Contact.Find(Int32.Parse(cId));
						@event.Contact.Add(c);
					}
				}

				db.Event.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // ViewBag.Type = new SelectList(db.EventType, "Id", "Type", @event.Type);
            // ViewBag.UserId = new SelectList(db.User, "Id", "UserName", @event.UserId);
            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.Type = new SelectList(db.EventType, "Id", "Type", @event.Type);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Name,Type,Date,Description,Results")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Type = new SelectList(db.EventType, "Id", "Type", @event.Type);
            ViewBag.UserId = new SelectList(db.User, "Id", "UserName", @event.UserId);
            return View(@event);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
