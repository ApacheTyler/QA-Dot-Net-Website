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
    public class QuestionsController : Controller
    {
        private SNPDB db = new SNPDB();

        // GET: /Questions/
        public ActionResult Index()
        {
            return View(db.Questions.ToList());
        }

        // GET: /Questions/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
           // IQueryable tmpQuery;

            var answer1 = db.Answers.ToList();
            
            IEnumerable<Answer> answer = db.Answers.Where(a => a.QuestionAnsID == question.QuestionID);
                           
          // db.Answers.Where(i => answer.Question == question);

            if (question == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // GET: /Questions/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="QuestionID,DateModified,QuestionText,IsActive")] Question question)
        {
            var isActive = false;

            var createdBy = User.Identity.Name.ToString();

            var ansText = "seed";

            var like = false;

            Answer ans = new Answer();
            //Trying to seed database

            var currentDate = DateTime.Now;
            question.DateModified = currentDate;

            ans.IsActive = isActive;
            ans.CreatedByID = createdBy;
            ans.AnswerText = ansText;
            ans.Like = like;
            ans.DateModified = currentDate;
            ans.QuestionAnsID = question.QuestionID;


            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                db.Answers.Add(ans);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(question);
        }

        // GET: /Questions/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: /Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="QuestionID,DateModified,QuestionText,IsActive")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        // GET: /Questions/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: /Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Answers/Create
         [Authorize]
        public ActionResult CreateTest()
        {
            ViewBag.QuestionAnsID = new SelectList(db.Questions, "QuestionID", "QuestionText");
            return View();
        }

        // POST: /Answers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CreateTest([Bind(Include = "CreatedByID,QuestionAnsID,AnswerText,DateModified,IsActive,Like")] Answer answer, int id)
        {

            var userID = User.Identity.Name;
            var currentDate = DateTime.Now;
            var answerID = id;
            var isAct = true;

            answer.CreatedByID = userID;
            answer.DateModified = currentDate;
            answer.QuestionAnsID = id;
            answer.IsActive = isAct;
            

            if (ModelState.IsValid)
            {
                db.Answers.Add(answer);
                db.SaveChanges();
                return RedirectToAction("Index"); ;
            }

            ViewBag.QuestionAnsID = new SelectList(db.Questions, "QuestionID", "QuestionText", answer.QuestionAnsID);
            return View(answer);
        }

        // GET: /Answers/Edit/5
        [Authorize]
        public ActionResult EditTest(int? id)
        {
            if (id == null)//
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
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult EditTest([Bind(Include = "AnswerID,CreatedByID,QuestionAnsID,AnswerText,DateModified,IsActive,Like")] Answer answer, int id)
        {
            var userID = User.Identity.Name;
            var currentDate = DateTime.Now;
            var isAct = true;

            answer.CreatedByID = userID;
            answer.DateModified = currentDate;
            answer.IsActive = isAct;

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
        [Authorize]
        public ActionResult DeleteTest(int? id)
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
        [HttpPost, ActionName("DeleteTest")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteTestConfirmed(int id)
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
