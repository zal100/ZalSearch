using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZalSearch.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using System.Web.Script.Serialization;

namespace ZalSearch.Controllers
{
    public class ParameterKeyController : Controller
    {
        private ZalSearch2Context db = new ZalSearch2Context();

        //
        // GET: /ParameterKey/

        public ActionResult Index()
        {
            return View(db.ParameterKeys.ToList());
        }

        //
        // GET: /ParameterKey/Details/5

        public ActionResult Details(int id = 0)
        {
            ParameterKeys parameterkeys = db.ParameterKeys.Find(id);
            if (parameterkeys == null)
            {
                return HttpNotFound();
            }
            return View(parameterkeys);
        }

        //
        // GET: /ParameterKey/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ParameterKey/Create

        [HttpPost]
        public ActionResult Create(ParameterKeys parameterkeys)
        {
            if (ModelState.IsValid)
            {
                db.ParameterKeys.Add(parameterkeys);
                db.SaveChanges();

                Lookup(parameterkeys.TermValue);

                return RedirectToAction("Index", "Home");
            }

            return View(parameterkeys);
        }

        //
        // GET: /ParameterKey/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ParameterKeys parameterkeys = db.ParameterKeys.Find(id);
            if (parameterkeys == null)
            {
                return HttpNotFound();
            }
            return View(parameterkeys);
        }

        //
        // POST: /ParameterKey/Edit/5

        [HttpPost]
        public ActionResult Edit(ParameterKeys parameterkeys)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parameterkeys).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parameterkeys);
        }

        //
        // GET: /ParameterKey/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ParameterKeys parameterkeys = db.ParameterKeys.Find(id);
            if (parameterkeys == null)
            {
                return HttpNotFound();
            }
            return View(parameterkeys);
        }

        //
        // POST: /ParameterKey/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ParameterKeys parameterkeys = db.ParameterKeys.Find(id);
            db.ParameterKeys.Remove(parameterkeys);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public async Task<string> Lookup(string lookupString)
        {
            string results = await LookupItunes(lookupString);

            return results.ToString();
        }

        private static async Task<string> LookupItunes(string lookupString)
        {
            using (var client = new HttpClient())
            {
                string requestURI = "http://itunes.apple.com/search?term=" + lookupString + "&media=all";
                string response = await client.GetStringAsync(
                 requestURI
                ).ConfigureAwait(false);

                JavaScriptSerializer js = new JavaScriptSerializer();

                dynamic resultsObject = js.Deserialize<dynamic>(response);
                int resultCount = resultsObject["resultCount"];
                object[] results = resultsObject["results"];
                foreach (Dictionary<string,object> result in results)
                {
                    string wrapperType = result["wrapperType"].ToString();

                    string viewURL = string.Empty;
                    switch (wrapperType)
                    {
                        case "artist":
                            viewURL = result["artistViewUrl"].ToString();
                            break;
                        case "collection":
                            viewURL = result["collectionViewUrl"].ToString();
                            break;
                        case "track":
                            viewURL = result["trackViewUrl"].ToString();
                            break;
                    }

                    PageController page = new PageController();
                    PageModels pageData= new PageModels();

                    pageData.ITunesPageURL = viewURL;
                    pageData.Clicks = 0;

                    page.Create(pageData);
               }

                return response;
            }
        }
    }
}