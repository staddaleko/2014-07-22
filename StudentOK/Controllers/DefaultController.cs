using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentOK.Models;

namespace StudentOK.Controllers
{
    public class DefaultController : Controller
    {
        public StudentDataContextLINQ dba = new StudentDataContextLINQ();
        //public StudentTabela dbt = new StudentTabela();

        public ActionResult Index()
        {
            var list = from s in dba.StudentTabelas select s;
            return View("Index", list.ToList());
        }

        //
        // GET: /Default/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Default/Create

        public ActionResult Create()
        {
            return View("Create");
        }

        //
        // POST: /Default/Create

        [HttpPost]
        public ActionResult Create(StudentTabela S)
        {
            dba.StudentTabelas.InsertOnSubmit(S);
            //dba.StudentTabelas.InsertOnSubmit(S);
            dba.SubmitChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Default/Edit/5

        public ActionResult Edit(int id)
        {
            var S = (from s in dba.StudentTabelas where s.ID == id select s).First();

            return View("Edit", S);
        }

        //
        // POST: /Default/Edit/5

        [HttpPost]
        public ActionResult Edit(StudentTabela S)
        {
            var SU = (from s in dba.StudentTabelas where s.ID == S.ID select s).First();
            SU.Imię = S.Imię;
            SU.Nazwisko = S.Nazwisko;
            SU.Wiek = S.Wiek;
            dba.SubmitChanges();

                return RedirectToAction("Index");
            
        }

        //
        // GET: /Default/Delete/5

        public ActionResult Delete(int id)
        {
            var S = (from s in dba.StudentTabelas where s.ID == id select s).ToList();
            dba.StudentTabelas.DeleteOnSubmit(S[0]);
            dba.SubmitChanges();
            return RedirectToAction("Index");
        }

        //
        // POST: /Default/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
