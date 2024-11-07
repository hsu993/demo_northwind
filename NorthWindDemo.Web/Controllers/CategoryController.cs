using NorthWindDemo.Models;
using NorthWindDemo.Service;
using NorthWindDemo.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWindDemo.Web.Controllers
{
    public class CategoryController : Controller
    {
        //private ICategoryService categoryService;
        //public CategoryController()
        //{
        //    this.categoryService = new CategoryService();
        //}

        private ICategoryService _categoryService;
        public CategoryController(ICategoryService service)
        {
            this._categoryService = service;
        }


        // GET: Category
        public ActionResult Index()
        {
            var categories = this._categoryService.GetAll()
                .OrderByDescending(x => x.CategoryID)
                .ToList();

            return View(categories);
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("index");
            }
            else
            {
                var category = this._categoryService.GetByID(id.Value);
                return View(category);
            }
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Categories category)
        {
            try
            {
                if (category != null && ModelState.IsValid)
                {
                    if (Request.Files.Count > 0)
                    {
                        HttpPostedFileBase objFiles = Request.Files["Photo"];

                        using (var binaryReader = new BinaryReader(objFiles.InputStream))
                        {
                            category.Picture = binaryReader.ReadBytes(objFiles.ContentLength);
                        }
                    }
                    this._categoryService.Create(category);
                    return RedirectToAction("index");
                }
                else
                {
                    return View(category);
                }
            }
            catch
            {
                return View(category);
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("index");
            }
            else
            {
                var category = this._categoryService.GetByID(id.Value);
                return View(category);
            }
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Categories category)
        {
            try
            {
                // TODO: Add update logic here
                if (category != null && ModelState.IsValid)
                {
                    if (Request.Files.Count > 0)
                    {
                        HttpPostedFileBase objFiles = Request.Files["Photo"];

                        using (var binaryReader = new BinaryReader(objFiles.InputStream))
                        {
                            category.Picture = binaryReader.ReadBytes(objFiles.ContentLength);
                        }
                    }
                    this._categoryService.Update(category);
                    return View(category);
                }
                else
                {
                    return RedirectToAction("index");
                }
            }
            catch
            {
                return View(category);
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("index");
            }
            else
            {
                var category = this._categoryService.GetByID(id.Value);
                return View(category);
            }
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Categories category)
        {
            try
            {
                // TODO: Add delete logic here
                this._categoryService.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Delete", new { id = id });
            }
        }
    }
}
