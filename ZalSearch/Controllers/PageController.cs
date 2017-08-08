using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZalSearch.Models;

namespace ZalSearch.Controllers
{
    public class PageController : Controller
    {
        private ZalSearch2Context db = new ZalSearch2Context();

        //
        // GET: /Page/

        public ActionResult Index()
        {
            return View(db.PageModels.ToList());
        }

        //
        // GET: /Page/Details/5

        public ActionResult Details(int id = 0)
        {
            PageModels pagemodels = db.PageModels.Find(id);
            if (pagemodels == null)
            {
                return HttpNotFound();
            }
            return View(pagemodels);
        }

        //
        // GET: /Page/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Page/Create

        [HttpPost]
        public ActionResult Create(PageModels pagemodels)
        {
            if (ModelState.IsValid)
            {
                db.PageModels.Add(pagemodels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pagemodels);
        }

        //
        // GET: /Page/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PageModels pagemodels = db.PageModels.Find(id);
            if (pagemodels == null)
            {
                return HttpNotFound();
            }
            return View(pagemodels);
        }

        //
        // POST: /Page/Edit/5

        [HttpPost]
        public ActionResult Edit(PageModels pagemodels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagemodels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pagemodels);
        }

        //
        // GET: /Page/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PageModels pagemodels = db.PageModels.Find(id);
            if (pagemodels == null)
            {
                return HttpNotFound();
            }
            return View(pagemodels);
        }

        //
        // POST: /Page/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PageModels pagemodels = db.PageModels.Find(id);
            db.PageModels.Remove(pagemodels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult StoreClick(int id = 0)
        {
            PageModels pagemodels = db.PageModels.Find(id);
            if (pagemodels == null)
            {
                return HttpNotFound();
            }
            string artistURL = pagemodels.ITunesPageURL;
            pagemodels.Clicks += 1;
            db.Entry(pagemodels).State = EntityState.Modified;
            db.SaveChanges();
            
            return Redirect(artistURL);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }



    }
}