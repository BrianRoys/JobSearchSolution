﻿using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using JobSearchSolution.ViewModel;
using Microsoft.AspNet.Identity;
using System;

namespace JobSearchSolution.Controllers
{
	[Authorize]
	public class EventsController : Controller
	{
		private JSSEntities3 db = new JSSEntities3();

		// GET: Events
		public ActionResult Index(bool ShowInactive = false)
		{
			var userId = new Guid(this.HttpContext.User.Identity.GetUserId());
			var events = db.Event
				.Where(c => c.UserId == userId)
				.OrderByDescending(c => c.Date)
				.Include(e => e.EventType);
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
			EventViewModel evm = new EventViewModel()
			{
				Event = new Event()
			};
			LoadLists(ref evm);
			return View(evm);
		}

		// POST: Events/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(EventViewModel evm)
		{
			if (!ModelState.IsValid)
			{
				LoadLists(ref evm);
				return View(evm);
			}
			var userId = new Guid(this.HttpContext.User.Identity.GetUserId());
			evm.Event.UserId = userId;
			db.Event.Add(evm.Event);
			foreach (var op in db.Opp)
			{
				if (evm.SelectedOpps.Contains(op.Id))
				{
					evm.Event.Opp.Add(op);
				}
			}
			foreach (var c in db.Contact)
			{
				if (evm.SelectedContacts.Contains(c.Id))
				{
					evm.Event.Contact.Add(c);
				}
			}
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		// GET: Events/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			EventViewModel evm = new EventViewModel()
			{
				Event = db.Event.Include(i => i.Contact).Include(i => i.Opp).First(c => c.Id == (int)id)
			};
			if (evm.Event == null)
			{
				return HttpNotFound();
			}
			LoadLists(ref evm);
			return View(evm);
		}

		// POST: Events/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(EventViewModel evm)
		{
			if (!ModelState.IsValid)
			{
				LoadLists(ref evm);
				return View(evm);
			}
			Event oldEvent = db.Event
				.Include(i => i.Contact)
				.Include(i => i.Opp)
				.First(c => c.Id == evm.Event.Id);

			if (TryUpdateModel(oldEvent, "Event"))
			{
				foreach (Opp op in db.Opp.Where(e => e.IsActive))
				{
					if (evm.SelectedOpps.Contains(op.Id))
					{
						oldEvent.Opp.Add(op);
					}
					else
					{
						oldEvent.Opp.Remove(op);
					}
				}
				foreach (Contact c in db.Contact.Where(e => e.IsActive))
				{
					if (evm.SelectedContacts.Contains(c.Id))
					{
						oldEvent.Contact.Add(c);
					}
					else
					{
						oldEvent.Contact.Remove(c);
					}
				}
				db.Entry(oldEvent).State = EntityState.Modified;
				db.SaveChanges();
			}
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private void LoadLists(ref EventViewModel evt)
		{
			var userId = new Guid(this.HttpContext.User.Identity.GetUserId());
			evt.AllEventTypes = new SelectList(db.EventType.Where(c => c.IsActive), "Id", "Type");
			evt.AllContacts = new SelectList(db.Contact.Where(c => c.IsActive && c.UserId == userId), "Id", "Name");
			evt.AllOpps = new SelectList(db.Opp.Where(o => o.IsActive && o.UserId == userId), "Id", "Name");
		}
	}
}
