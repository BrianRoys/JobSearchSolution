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
    public class EventTypesController : Controller
    {
        private JSSEntities2 db = new JSSEntities2();

        // GET: EventTypes
        public ActionResult Index()
        {
            return View(db.EventType.ToList());
        }

        // GET: EventTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventType eventType = db.EventType.Find(id);
            if (eventType == null)
            {
                return HttpNotFound();
            }
            return View(eventType);
        }

        // GET: EventTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IsActive,SortOrder,Type")] EventType eventType)
        {
            if (ModelState.IsValid)
            {
                db.EventType.Add(eventType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eventType);
        }

        // GET: EventTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventType eventType = db.EventType.Find(id);
            if (eventType == null)
            {
                return HttpNotFound();
            }
            return View(eventType);
        }

        // POST: EventTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IsActive,SortOrder,Type")] EventType eventType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventType);
        }

        // GET: EventTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventType eventType = db.EventType.Find(id);
            if (eventType == null)
            {
                return HttpNotFound();
            }
            return View(eventType);
        }

        // POST: EventTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventType eventType = db.EventType.Find(id);
            db.EventType.Remove(eventType);
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
