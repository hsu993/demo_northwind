using Newtonsoft.Json.Linq;
using NorthWindDemo.Models;
using NorthWindDemo.Service;
using NorthWindDemo.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace NorthWindDemo.Web.Controllers
{
    public class ProductApiController : Controller
    {
        private IProductService productService;
        public ProductApiController()
        {
            this.productService = new ProductService();
        }
        // GET api/values
        public IEnumerable<Products> Get()
        {
            var categories = this.productService.GetAll()
                .OrderByDescending(x => x.ProductID)
                .ToList();

            return categories;
        }

        // GET api/values/5
        public Products Get(int? id)
        {
            if (!id.HasValue) return null;

            Products product = productService.GetByID(id.Value);
            if (product == null)
            {
                return null;
            }
            return product;
        }

        //// POST api/values
        //public void Post([FromBody] string value)
        //{
        //    values.Add(value);
        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody] string value)
        //{
        //    values[id] = value;
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //    values.RemoveAt(id);
        //}
    }
}