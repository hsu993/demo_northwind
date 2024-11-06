using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthWindDemo.Models;
using NorthWindDemo.Service;
using NorthWindDemo.Service.Interface;

namespace NorthWindDemo.Web.Controllers
{
    public class ProductController : Controller
    {
        //private northwindDBEntities db = new northwindDBEntities();

        private IProductService productService;
        private ICategoryService categoryService;
        private ISuppliersService suppliersService;

        /// <summary>
        /// 取得所有類別資料
        /// </summary>
        public IEnumerable<Categories> Categories
        {
            get
            {
                return categoryService.GetAll();
            }
        }
        /// <summary>
        /// 取得所有供應商資料
        /// </summary>
        public IEnumerable<Suppliers> Suppliers
        {
            get
            {
                return suppliersService.GetAll();
            }
        }

        public ProductController()
        {
            this.productService = new ProductService();
            this.categoryService = new CategoryService();
            this.suppliersService = new SuppliersService();
        }

        // GET: Product
        public ActionResult Index(string category = "")
        {
            //var products = db.Products.Include(p => p.Categories).Include(p => p.Suppliers);
            //return View(products.ToList());
            int categoryID = 1;

            ViewBag.CategorySelectList = int.TryParse(category, out categoryID)
                ? this.CategorySelectList(categoryID.ToString())
                : this.CategorySelectList("");

            var result = category.Equals("", StringComparison.OrdinalIgnoreCase)
                ? productService.GetAll()
                : productService.GetByCategory(categoryID);

            var products = result.OrderByDescending(x => x.ProductID).ToList();

            ViewBag.Category = category;

            return View(products);
        }

        [HttpPost]
        public ActionResult ProductsOfCategory(string category)
        {
            return RedirectToAction("Index", new { category = category });
        }

        /// <summary>
        /// CategorySelectList
        /// </summary>
        /// <param name="selectedValue">The selected value.</param>
        /// <returns></returns>
        public List<SelectListItem> CategorySelectList(string selectedValue = "")
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem()
            {
                Text = "All Category",
                Value = "",
                Selected = selectedValue.Equals("", StringComparison.OrdinalIgnoreCase)
            });

            var categories = categoryService.GetAll().OrderBy(x => x.CategoryID);

            foreach (var c in categories)
            {
                items.Add(new SelectListItem()
                {
                    Text = c.CategoryName,
                    Value = c.CategoryID.ToString(),
                    Selected = selectedValue.Equals(c.CategoryID.ToString())
                });
            }
            return items;
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id, string category, string supplier)
        {
            if (!id.HasValue) return RedirectToAction("index");

            Products product = productService.GetByID(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.Category = string.IsNullOrWhiteSpace(category) ? "" : category;
            ViewBag.Supplier = string.IsNullOrWhiteSpace(supplier) ? "" : supplier;

            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create(string category, string supplier)
        {
            //ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            //ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            //return View();
            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName");
            ViewBag.Category = string.IsNullOrWhiteSpace(category) ? "" : category;

            ViewBag.SupplierID = new SelectList(this.Suppliers, "SupplierID", "CompanyName");
            ViewBag.Supplier = string.IsNullOrWhiteSpace(supplier) ? "" : supplier;
            return View();
        }

        // POST: Product/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Products products, string category)
        {
            if (ModelState.IsValid)
            {
                this.productService.Create(products);
                return RedirectToAction("Index", new { category = category });
            }

            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName", products.CategoryID);
            ViewBag.SupplierID = new SelectList(this.Suppliers, "SupplierID", "CompanyName", products.SupplierID);

            return View(products);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id, string category, string supplier)
        {
            if (!id.HasValue) return RedirectToAction("index");

            Products product = this.productService.GetByID(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.Category = string.IsNullOrWhiteSpace(category) ? "all" : category;

            ViewBag.SupplierID = new SelectList(this.Suppliers, "SupplierID", "CompanyName", product.SupplierID);
            ViewBag.Supplier = string.IsNullOrWhiteSpace(supplier) ? "all" : supplier;

            return View(product);
        }

        // POST: Product/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Products products, string category)
        {
            if (ModelState.IsValid)
            {
                this.productService.Update(products);
                return RedirectToAction("Index", new { category = category });
            }

            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName", products.CategoryID);
            ViewBag.SupplierID = new SelectList(this.Suppliers, "SupplierID", "CompanyName", products.SupplierID);

            return View(products);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id, string category, string supplier)
        {
            if (!id.HasValue) return RedirectToAction("index");

            Products product = this.productService.GetByID(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.Category = string.IsNullOrWhiteSpace(category) ? "all" : category;
            ViewBag.Supplier = string.IsNullOrWhiteSpace(supplier) ? "all" : supplier;

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string category)
        {
            //Products product = this.productService.GetByID(id);
            this.productService.Delete(id);

            return RedirectToAction("Index", new { category = category });
        }
    }
}
