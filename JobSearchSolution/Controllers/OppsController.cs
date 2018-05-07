using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using JobSearchSolution.ViewModel;

namespace JobSearchSolution.Controllers
{
    public class OppsController : Controller
    {
        private JSSEntities2 db = new JSSEntities2();

        // GET: Opps
        public ActionResult Index()
        {
            var opps = db.Opp.Where(o => o.UserId == SessionValues.CurrentUserId).Include(o => o.OppStatus);
            return View(opps.ToList());
        }

        // GET: Opps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opp opp = db.Opp.Find(id);
            if (opp == null)
            {
                return HttpNotFound();
            }
            return View(opp);
        }

        // GET: Opps/Create
        public ActionResult Create()
        {
			OppViewModel ovm = new OppViewModel
			{
				Opp = new Opp()
			};
			LoadLists(ref ovm);
			return View(ovm);
        }

        // POST: Opps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OppViewModel ovm)
        {
            if (!ModelState.IsValid)
			{
				LoadLists(ref ovm);
				return View(ovm);
			}
			else
            {
				ovm.Opp.UserId = SessionValues.CurrentUserId;
				ovm.Opp.DateOpened = DateTime.Now;
				ovm.Opp.IsActive = true;
				db.Opp.Add(ovm.Opp);
				foreach (var ev in db.Event)
				{
					if (ovm.SelectedEvents.Contains(ev.Id))
					{
						ovm.Opp.Event.Add(ev);
					}
				}
				foreach (var c in db.Contact)
				{
					if (ovm.SelectedContacts.Contains(c.Id))
					{
						ovm.Opp.Contact.Add(c);
					}
				}
				db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        // GET: Opps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			OppViewModel ovm = new OppViewModel
			{
				Opp = db.Opp.Include(i => i.Contact).Include(i => i.Event).First(c => c.Id == (int)id)
			};
            if (ovm.Opp == null)
            {
                return HttpNotFound();
            }
			LoadLists(ref ovm);
			return View(ovm);
        }

        // POST: Opps/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OppViewModel ovm)
        {
            if (!ModelState.IsValid)
			{
				LoadLists(ref ovm);
				return View(ovm);
			}
			{
				Opp oldOpp = db.Opp
					.Include(i => i.Contact)
					.Include(i => i.Event)
					.First(c => c.Id == ovm.Opp.Id);

				if (TryUpdateModel(oldOpp, "Opp"))
				{
					foreach (Event ev in db.Event)
					{
						if (ovm.SelectedEvents.Contains(ev.Id))
						{
							oldOpp.Event.Add(ev);
						}
						else
						{
							oldOpp.Event.Remove(ev);
						}
					}
					foreach (Contact c in db.Contact.Where(e => e.IsActive))
					{
						if (ovm.SelectedContacts.Contains(c.Id))
						{
							oldOpp.Contact.Add(c);
						}
						else
						{
							oldOpp.Contact.Remove(c);
						}
					}
					db.Entry(oldOpp).State = EntityState.Modified;
					db.SaveChanges();
				}
				return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

		private void LoadLists(ref OppViewModel ovm)
		{
			ovm.AllEvents = new SelectList(db.Event.Where(e => e.UserId == SessionValues.CurrentUserId), "Id", "Name");
			ovm.AllContacts = new SelectList(db.Contact.Where(o => o.IsActive && o.UserId == SessionValues.CurrentUserId), "Id", "Name");
			ovm.AllStatuses = new SelectList(db.OppStatus.Where(o => o.IsActive).OrderBy(o => o.SortOrder), "Id", "Status");
		}
	}
}
