using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DailyPlannerList.Models;
using Microsoft.AspNet.Identity;

namespace DailyPlannerList.Controllers
{
    public class DailyPlannersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DailyPlanners
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault
                (x => x.Id == currentUserId);
            return View(db.DailyPlanners.ToList().Where(x=> x.User == currentUser));
        }
        
        // GET: DailyPlanners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyPlanner dailyPlanner = db.DailyPlanners.Find(id);
            if (dailyPlanner == null)
            {
                return HttpNotFound();
            }
            return View(dailyPlanner);
        }

        // GET: DailyPlanners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DailyPlanners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,IsDone")] DailyPlanner dailyPlanner)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault
                    (x => x.Id == currentUserId);
                dailyPlanner.User = currentUser;
                db.DailyPlanners.Add(dailyPlanner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dailyPlanner);
        }

        // GET: DailyPlanners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyPlanner dailyPlanner = db.DailyPlanners.Find(id);
            if (dailyPlanner == null)
            {
                return HttpNotFound();
            }
            return View(dailyPlanner);
        }

        // POST: DailyPlanners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,IsDone")] DailyPlanner dailyPlanner)
        {
            if (ModelState.IsValid)
            {            
                db.Entry(dailyPlanner).State = EntityState.Modified;
                db.DailyPlanners.Add(dailyPlanner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dailyPlanner);
        }

        // GET: DailyPlanners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyPlanner dailyPlanner = db.DailyPlanners.Find(id);
            if (dailyPlanner == null)
            {
                return HttpNotFound();
            }
            return View(dailyPlanner);
        }

        // POST: DailyPlanners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DailyPlanner dailyPlanner = db.DailyPlanners.Find(id);
            db.DailyPlanners.Remove(dailyPlanner);
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
