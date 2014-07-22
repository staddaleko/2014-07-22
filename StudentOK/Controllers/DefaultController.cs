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
            var SD = (from s in dba.StudentTabelas where s.ID == id select s).First();
            return View("Details", SD);
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
            var S = (from s in dba.StudentTabelas where s.ID == id select s).First();
            var asoc = (from a in dba.Stu_Przeds where a.ID_Studenta == id select a).ToList();
            if (asoc.Count == 0)
            {
                dba.StudentTabelas.DeleteOnSubmit(S);
                dba.SubmitChanges();
            }
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

        public ActionResult PartialDetails(int id_s)
        {
            ViewBag.p_o = (from p in dba.Przedmioties
                           join asoc in dba.Stu_Przeds on p.ID_przedmiotu equals asoc.ID_Przedmiotu
                           join s in dba.StudentTabelas on asoc.ID_Studenta equals s.ID
                           where s.ID == id_s
                           select new { p.Nazwa_przedmiotu, asoc.Ocena }).ToList();

            return PartialView("PartialDetails");
        }
    }
}
