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
    public class OppsController : Controller
    {
        private JSSEntities2 db = new JSSEntities2();

        // GET: Opps
        public ActionResult Index()
        {
            var opps = db.Opp.Include(o => o.OppStatus).Include(o => o.User);
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
            ViewBag.Status = new SelectList(db.OppStatus, "Id", "Status");
            ViewBag.UserId = new SelectList(db.User, "Id", "UserName");
            return View();
        }

        // POST: Opps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Name,Description,DateOpened,Status,Location,IsActive,Rate,HasBeenReported")] Opp opp)
        {
            if (ModelState.IsValid)
            {
                db.Opp.Add(opp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Status = new SelectList(db.OppStatus, "Id", "Status", opp.Status);
            ViewBag.UserId = new SelectList(db.User, "Id", "UserName", opp.UserId);
            return View(opp);
        }

        // GET: Opps/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.Status = new SelectList(db.OppStatus, "Id", "Status", opp.Status);
            ViewBag.UserId = new SelectList(db.User, "Id", "UserName", opp.UserId);
            return View(opp);
        }

        // POST: Opps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Name,Description,DateOpened,Status,Location,IsActive,Rate,HasBeenReported")] Opp opp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(opp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Status = new SelectList(db.OppStatus, "Id", "Status", opp.Status);
            ViewBag.UserId = new SelectList(db.User, "Id", "UserName", opp.UserId);
            return View(opp);
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
