using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Web.Mvc;
using NorthWindDemo.Models;

namespace NorthWindDemo.ViewModels
{
    /// <summary>
    /// Product 所有頁面搜尋條件
    /// </summary>
    public class ProductSearchCriteriaModel
    {
        public string Category { get; set; }
        public string Supplier { get; set; }
    }
    public class ProductIndexViewModel : ProductSearchCriteriaModel
    {
        public List<SelectListItem> CategorySelectList {  get; set; }
        public Products Products { get; set; }
        public List<Products> ProductsData { get; set; }
    }
    public class ProductDetailsViewModel : ProductSearchCriteriaModel
    {
        public Products Products { get; set; }
    }
    //public class ProductCreateViewModel : ProductSearchCriteriaModel
    //{
    //    public List<SelectListItem> CategorySelectList { get; set; }
    //    public List<SelectListItem> SupplierSelectList { get; set; }
    //    public Products Products { get; set; }
    //}
    public class ProductEditViewModel : ProductSearchCriteriaModel
    {
        public List<SelectListItem> CategorySelectList { get; set; }
        public List<SelectListItem> SupplierSelectList { get; set; }
        public Products Products { get; set; }
    }
    public class ProductDeleteViewModel : ProductSearchCriteriaModel
    {
        public Products Products { get; set; }
    }
}