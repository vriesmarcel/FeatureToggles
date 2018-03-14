using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class FeatureTogglesController : Controller
    {
        private MusicStoreEntities db = new MusicStoreEntities();

        // GET: FeatureTogles
        public async Task<ActionResult> Index()
        {
            return View(await db.FeatureToggles.ToListAsync());
        }

        // GET: FeatureTogles/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.FeatureToggle featureTogle = await db.FeatureToggles.FindAsync(id);
            if (featureTogle == null)
            {
                return HttpNotFound();
            }
            return View(featureTogle);
        }

        // GET: FeatureTogles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FeatureTogles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description, Enabled")] Models.FeatureToggle featureToggle)
        {
            if (ModelState.IsValid)
            {
                featureToggle.Id = Guid.NewGuid();
                db.FeatureToggles.Add(featureToggle);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(featureToggle);
        }

        // GET: FeatureTogles/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.FeatureToggle featureTogle = await db.FeatureToggles.FindAsync(id);
            if (featureTogle == null)
            {
                return HttpNotFound();
            }
            return View(featureTogle);
        }

        // POST: FeatureTogles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description, Enabled")] Models.FeatureToggle featureToggle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(featureToggle).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(featureToggle);
        }

        // GET: FeatureTogles/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.FeatureToggle featureTogle = await db.FeatureToggles.FindAsync(id);
            if (featureTogle == null)
            {
                return HttpNotFound();
            }
            return View(featureTogle);
        }

        // POST: FeatureTogles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Models.FeatureToggle featureTogle = await db.FeatureToggles.FindAsync(id);
            db.FeatureToggles.Remove(featureTogle);
            await db.SaveChangesAsync();
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
