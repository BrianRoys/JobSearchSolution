using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobSearchSolution;
using System.Collections;
using JobSearchSolution.ViewModel;

namespace JobSearchSolution.Controllers
{
    public class ContactsController : Controller
    {
        private JSSEntities2 db = new JSSEntities2();

        // GET: Contacts
        public ActionResult Index()
        {
			var contact = db.Contact.Where(c => c.UserId == SessionValues.CurrentUserId); // .Include(c => c.User);
            return View(contact.ToList());
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contact.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
			ViewBag.Opps = new SelectList(db.Opp.Where(o => o.IsActive), "Id", "Name");
			ViewBag.Events = new SelectList(db.Event, "Id", "Name");
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Phone,EMailAddress,AgencyName,PhysicalAddress,URL,Notes")] Contact contact)
        {
            if (ModelState.IsValid)
            {
				contact.UserId = SessionValues.CurrentUserId;
				contact.IsActive = true;				

				string str = Request["Opp"]; // e.g. "1,2,5"
				if (str != null)
				{
					foreach (string oId in str.Split(','))
					{
						Opp o = db.Opp.Find(Int32.Parse(oId));
						contact.Opp.Add(o);
					}
				}
				str = Request["Event"]; // e.g. "1,2,5"
				if (str != null)
				{
					foreach (string eId in str.Split(','))
					{
						Event e = db.Event.Find(Int32.Parse(eId));
						contact.Event.Add(e);
					}
				}
				db.Contact.Add(contact);
				db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			var contactViewModel = new ContactViewModel
			{
				Contact = db.Contact.Include(i => i.Opp).Include(i => i.Event).First(c => c.Id == (int)id)
			};

			if (contactViewModel.Contact == null)
			{
				return HttpNotFound();
			}

			contactViewModel.AllEvents = new SelectList(db.Event, "Id", "Name");
			contactViewModel.AllOpps = new SelectList(db.Opp.Where(o => o.IsActive), "Id", "Name");
			return View(contactViewModel);
		}

        // POST: Contacts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContactViewModel contactView)
        {
			if (contactView == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

            if (ModelState.IsValid)
            {
				var oldContact = db.Contact
					.Include(i => i.Opp)
					.Include(i => i.Event)
					.First(c => c.Id == contactView.Contact.Id);

				if (TryUpdateModel(oldContact, "Contact"))
				{
					foreach (Event ev in db.Event)
					{
						if (contactView.SelectedEvents.Contains(ev.Id))
						{
							oldContact.Event.Add(ev);
						}
						else
						{
							oldContact.Event.Remove(ev);
						}
					}
					foreach (Opp op in db.Opp.Where(e => e.IsActive))
					{
						if (contactView.SelectedOpps.Contains(op.Id))
						{
							oldContact.Opp.Add(op);
						}
						else
						{
							oldContact.Opp.Remove(op);
						}
					}
					contactView.Contact.UserId = SessionValues.CurrentUserId;
					db.Entry(oldContact).State = EntityState.Modified;
					db.SaveChanges();
				}
				return RedirectToAction("Index");
            }
            return View(contactView);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
			// Never delete Contacts, just set them as not active.
			throw new NotImplementedException(); 

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contact.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contact.Find(id);
            db.Contact.Remove(contact);
            db.SaveChanges();
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
    }
}
