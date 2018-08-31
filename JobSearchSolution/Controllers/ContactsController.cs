using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using JobSearchSolution.ViewModel;
using System;
using Microsoft.AspNet.Identity;

namespace JobSearchSolution.Controllers
{
	[Authorize]
	public class ContactsController : Controller
    {
        private JSSEntities3 db = new JSSEntities3();

		// GET: Contacts
		public ActionResult Index(bool ShowInactive = false)
        {
			var userId = new Guid(this.HttpContext.User.Identity.GetUserId());
			if (ShowInactive)
			{
				var contacts = db.Contact.Where(c => c.UserId == userId && !c.IsActive);
				return View(contacts.ToList());
			}
			else
			{
				var contacts = db.Contact.Where(c => c.UserId == userId && c.IsActive);
				return View(contacts.ToList());
			}
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
			var userId = new Guid(this.HttpContext.User.Identity.GetUserId());
			ContactViewModel cvm = new ContactViewModel
			{
				Contact = new Contact(),
				AllOpps = new SelectList(db.Opp.Where(o => o.IsActive && o.UserId == userId), "Id", "Name"),
				AllEvents = new SelectList(db.Event.Where(e =>  e.UserId == userId), "Id", "Name")
			};
            return View(cvm);
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactViewModel cvm)
        {
            if (ModelState.IsValid)
            {
				var userId = new Guid(this.HttpContext.User.Identity.GetUserId());
				cvm.Contact.UserId = userId;
				cvm.Contact.IsActive = true;				
				db.Contact.Add(cvm.Contact);
				foreach (var ev in db.Event)
				{
					if (cvm.SelectedEvents.Contains(ev.Id))
					{
						cvm.Contact.Event.Add(ev);
					}
				}
				foreach (var op in db.Opp)
				{
					if (cvm.SelectedOpps.Contains(op.Id))
					{
						cvm.Contact.Opp.Add(op);
					}
				}
				db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cvm);
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
			var userId = new Guid(this.HttpContext.User.Identity.GetUserId());
			contactViewModel.AllEvents = new SelectList(db.Event.Where(e => e.UserId == userId), "Id", "Name");
			contactViewModel.AllOpps = new SelectList(db.Opp.Where(o => o.IsActive && o.UserId == userId), "Id", "Name");
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
					// contactView.Contact.UserId = SessionValues.CurrentUserId;
					db.Entry(oldContact).State = EntityState.Modified;
					db.SaveChanges();
				}
				return RedirectToAction("Index");
            }
            return View(contactView);
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
