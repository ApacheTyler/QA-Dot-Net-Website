using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SNPData;

namespace WoofQAWebsite.Controllers
{
    public class AnswersController : Controller
    {
        private SNPDB db = new SNPDB();

        // GET: /Answers/
        [Authorize (Roles=("Admin"))]
        public ActionResult Index()
        {
            var answers = db.Answers.Include(a => a.Question);
            return View(answers.ToList());
        }

        // GET: /Answers/Details/5
        [Authorize(Roles = ("Admin"))]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // GET: /Answers/Create
        [Authorize(Roles = ("Admin"))]
        public ActionResult Create()
        {
            ViewBag.QuestionAnsID = new SelectList(db.Questions, "QuestionID", "QuestionText");
            return View();
        }

        // POST: /Answers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = ("Admin"))]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AnswerID,CreatedByID,QuestionAnsID,AnswerText,DateModified,IsActive,Like")] Answer answer)
        {

            var userID = User.Identity.Name;
            var currentDate = DateTime.Now;

            answer.CreatedByID = userID;
            answer.DateModified = currentDate;

            if (ModelState.IsValid)
            {
                db.Answers.Add(answer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionAnsID = new SelectList(db.Questions, "QuestionID", "QuestionText", answer.QuestionAnsID);
            return View(answer);
        }

        // GET: /Answers/Edit/5
        [Authorize(Roles = ("Admin"))]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionAnsID = new SelectList(db.Questions, "QuestionID", "QuestionText", answer.QuestionAnsID);
            return View(answer);
        }

        // POST: /Answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = ("Admin"))]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AnswerID,CreatedByID,QuestionAnsID,AnswerText,DateModified,IsActive,Like")] Answer answer)
        {

            var id = User.Identity.Name;
            answer.CreatedByID = id;

            if (ModelState.IsValid)
            {
                db.Entry(answer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionAnsID = new SelectList(db.Questions, "QuestionID", "QuestionText", answer.QuestionAnsID);
            return View(answer);
        }

        // GET: /Answers/Delete/5
        [Authorize(Roles = ("Admin"))]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // POST: /Answers/Delete/5
        [Authorize(Roles = ("Admin"))]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Answer answer = db.Answers.Find(id);
            db.Answers.Remove(answer);
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
