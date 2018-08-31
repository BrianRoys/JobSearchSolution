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
	// [Authorize(Roles = "Administrator")]
	public class OppStatusController : Controller
    {
        private JSSEntities3 db = new JSSEntities3();

        // GET: OppStatus
        public ActionResult Index()
        {
            return View(db.OppStatus.ToList().OrderByDescending(s => s.SortOrder));
        }

        // GET: OppStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OppStatus oppStatus = db.OppStatus.Find(id);
            if (oppStatus == null)
            {
                return HttpNotFound();
            }
            return View(oppStatus);
        }

        // GET: OppStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OppStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IsActive,SortOrder,Status")] OppStatus oppStatus)
        {
            if (ModelState.IsValid)
            {
                db.OppStatus.Add(oppStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oppStatus);
        }

        // GET: OppStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OppStatus oppStatus = db.OppStatus.Find(id);
            if (oppStatus == null)
            {
                return HttpNotFound();
            }
            return View(oppStatus);
        }

        // POST: OppStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IsActive,SortOrder,Status")] OppStatus oppStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oppStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oppStatus);
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
