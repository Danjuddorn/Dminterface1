using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dminterface1.Models;

namespace Dminterface1.Controllers
{
    public class ResponsesController : Controller
    {
        private dminterfaceEntities db = new dminterfaceEntities();

        // GET: Responses
        public ActionResult Index()
        {
            var responses = db.Responses.Include(r => r.Person).Include(r => r.Question).Include(r => r.Survey);
            return View(responses.ToList());
        }

        // GET: Responses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return View(response);
        }

        // GET: Responses/Create
        public ActionResult Create()
        {
            ViewBag.Person_idPerson = new SelectList(db.People, "idPerson", "Forename");
            ViewBag.Questions_idQuestions = new SelectList(db.Questions, "idQuestions", "Question1");
            ViewBag.Survey_idSurvey = new SelectList(db.Surveys, "idSurvey", "idSurvey");
            return View();
        }

        // POST: Responses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idResponse,Response1,Person_idPerson,Questions_idQuestions,Survey_idSurvey")] Response response)
        {
            if (ModelState.IsValid)
            {
                db.Responses.Add(response);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Person_idPerson = new SelectList(db.People, "idPerson", "Forename", response.Person_idPerson);
            ViewBag.Questions_idQuestions = new SelectList(db.Questions, "idQuestions", "Question1", response.Questions_idQuestions);
            ViewBag.Survey_idSurvey = new SelectList(db.Surveys, "idSurvey", "idSurvey", response.Survey_idSurvey);
            return View(response);
        }

        // GET: Responses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            ViewBag.Person_idPerson = new SelectList(db.People, "idPerson", "Forename", response.Person_idPerson);
            ViewBag.Questions_idQuestions = new SelectList(db.Questions, "idQuestions", "Question1", response.Questions_idQuestions);
            ViewBag.Survey_idSurvey = new SelectList(db.Surveys, "idSurvey", "idSurvey", response.Survey_idSurvey);
            return View(response);
        }

        // POST: Responses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idResponse,Response1,Person_idPerson,Questions_idQuestions,Survey_idSurvey")] Response response)
        {
            if (ModelState.IsValid)
            {
                db.Entry(response).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Person_idPerson = new SelectList(db.People, "idPerson", "Forename", response.Person_idPerson);
            ViewBag.Questions_idQuestions = new SelectList(db.Questions, "idQuestions", "Question1", response.Questions_idQuestions);
            ViewBag.Survey_idSurvey = new SelectList(db.Surveys, "idSurvey", "idSurvey", response.Survey_idSurvey);
            return View(response);
        }

        // GET: Responses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return View(response);
        }

        // POST: Responses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Response response = db.Responses.Find(id);
            db.Responses.Remove(response);
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
