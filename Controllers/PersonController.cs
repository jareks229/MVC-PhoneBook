using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warsztaty_3.Models;

namespace Warsztaty_3.Controllers
{
    public class PersonController : Controller
    {
        [HttpGet]
        public IActionResult Index(int page = 1, string filterLastName = "")
        {
            var repo = new SourceManager();
            List<PersonModel> list = repo.Get();

            if (!String.IsNullOrWhiteSpace(filterLastName))
            {
                list = list.Where(q => q.LastName.StartsWith(filterLastName)).ToList();
            }

            //Stackoverflow rulez !
            var pageElements = 5;
            var pages = Math.Ceiling((decimal)list.Count() / pageElements);
            list = list.Skip(((page - 1) * pageElements)).Take(pageElements).ToList();

            ViewBag.Page = page;
            ViewBag.Pages = pages;
            ViewBag.FilterLastName = filterLastName;

            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Create(PersonModel model)
        {
            var manager = new SourceManager();

            manager.Add(model);

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var manager = new SourceManager();
            PersonModel model = manager.GetById(id);
            
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var repo = new SourceManager();
            var model = repo.GetById(id);
                

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(PersonModel model)
        {
            var repo = new SourceManager();
            repo.Update(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            var repo = new SourceManager();
            repo.Remove(id);

            return RedirectToAction("Index");
        }
    }
}