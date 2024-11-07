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
using NorthWindDemo.ViewModels;

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
            ProductIndexViewModel viewModel = new ProductIndexViewModel();

            viewModel.CategorySelectList = int.TryParse(category, out int categoryID)
                ? this.CategorySelectList(categoryID.ToString())
                : this.CategorySelectList("");

            var result = category.Equals("", StringComparison.OrdinalIgnoreCase)
                ? productService.GetAll()
                : productService.GetByCategory(categoryID);

            var products = result.OrderByDescending(x => x.ProductID).ToList();
            viewModel.ProductsData = products;
            viewModel.Category = string.IsNullOrWhiteSpace(category) ? "" : category;

            return View(viewModel);
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
        private List<SelectListItem> CategorySelectList(string selectedValue = "")
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

        /// <summary>
        /// CategorySelectList
        /// </summary>
        /// <param name="selectedValue">The selected value.</param>
        /// <returns></returns>
        private List<SelectListItem> SupplierSelectList(string selectedValue = "")
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem()
            {
                Text = "All Supplier",
                Value = "",
                Selected = selectedValue.Equals("", StringComparison.OrdinalIgnoreCase)
            });

            var suppliers = suppliersService.GetAll().OrderBy(x => x.SupplierID);

            foreach (var s in suppliers)
            {
                items.Add(new SelectListItem()
                {
                    Text = s.CompanyName,
                    Value = s.SupplierID.ToString(),
                    Selected = selectedValue.Equals(s.SupplierID.ToString())
                });
            }
            return items;
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id, string category, string supplier)
        {
            ProductDetailsViewModel viewModel = new ProductDetailsViewModel();
            if (!id.HasValue) return RedirectToAction("index");

            Products product = productService.GetByID(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            viewModel.Products = product;

            viewModel.Category = string.IsNullOrWhiteSpace(category) ? "" : category;
            viewModel.Supplier = string.IsNullOrWhiteSpace(supplier) ? "" : supplier;

            return View(viewModel);
        }

        // GET: Product/Create
        public ActionResult Create(string category, string supplier)
        {
            ProductEditViewModel viewModel = new ProductEditViewModel();

            viewModel.CategorySelectList = int.TryParse(category, out int categoryID)
                ? this.CategorySelectList(categoryID.ToString())
                : this.CategorySelectList("");

            viewModel.SupplierSelectList = int.TryParse(supplier, out int supplierID)
                ? this.SupplierSelectList(supplierID.ToString())
                : this.SupplierSelectList("");

            viewModel.Category = string.IsNullOrWhiteSpace(category) ? "" : category;
            viewModel.Supplier = string.IsNullOrWhiteSpace(supplier) ? "" : supplier;
            return View(viewModel);
        }

        // POST: Product/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Products products, string category, ProductCreateViewModel viewModel)
        public ActionResult Create(ProductEditViewModel viewModel, string category)
        {
            if (ModelState.IsValid)
            {
                this.productService.Create(viewModel.Products);
                return RedirectToAction("Index", new { category = category });
            }

            viewModel.CategorySelectList = viewModel.Products.CategoryID != null
                ? this.CategorySelectList(viewModel.Products.CategoryID.ToString())
                : this.CategorySelectList("");

            viewModel.SupplierSelectList = viewModel.Products.SupplierID != null
                ? this.SupplierSelectList(viewModel.Products.SupplierID.ToString())
                : this.SupplierSelectList("");

            viewModel.Category = string.IsNullOrWhiteSpace(category) ? "" : category;
            return View(viewModel);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id, string category, string supplier)
        {
            ProductEditViewModel viewModel = new ProductEditViewModel();
            if (!id.HasValue) return RedirectToAction("index");

            Products product = this.productService.GetByID(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            viewModel.Products = product;
            viewModel.CategorySelectList = int.TryParse(category, out int categoryID)
                ? this.CategorySelectList(categoryID.ToString())
                : this.CategorySelectList("");

            viewModel.SupplierSelectList = int.TryParse(supplier, out int supplierID)
                ? this.SupplierSelectList(supplierID.ToString())
                : this.SupplierSelectList("");

            viewModel.Category = string.IsNullOrWhiteSpace(category) ? "" : category;

            return View(viewModel);
        }

        // POST: Product/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Products products, string category)
        public ActionResult Edit(ProductEditViewModel viewModel, string category)
        {
            if (ModelState.IsValid)
            {
                this.productService.Update(viewModel.Products);
                return RedirectToAction("Index", new { category = category });
            }

            viewModel.CategorySelectList = viewModel.Products.CategoryID != null
                ? this.CategorySelectList(viewModel.Products.CategoryID.ToString())
                : this.CategorySelectList("");

            viewModel.SupplierSelectList = viewModel.Products.SupplierID != null
                ? this.SupplierSelectList(viewModel.Products.SupplierID.ToString())
                : this.SupplierSelectList("");

            viewModel.Category = string.IsNullOrWhiteSpace(category) ? "" : category;
            return View(viewModel);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id, string category, string supplier)
        {
            ProductDeleteViewModel viewModel = new ProductDeleteViewModel();
            if (!id.HasValue) return RedirectToAction("index");

            Products product = this.productService.GetByID(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }

            viewModel.Products = product;
            viewModel.Category = string.IsNullOrWhiteSpace(category) ? "" : category;
            //ViewBag.Supplier = string.IsNullOrWhiteSpace(supplier) ? "" : supplier;

            return View(viewModel);
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
