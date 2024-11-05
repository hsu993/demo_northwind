using NorthWindDemo.Models;
using NorthWindDemo.Models.Interface;
using NorthWindDemo.Models.Repository;
using NorthWindDemo.Service.Interface;
using NorthWindDemo.Service.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWindDemo.Service
{
    public class ProductService : IProductService
    {
        private IRepository<Products> repository = new DataRepository<Products>();

        public IResult Create(Products instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException();
            }

            IResult result = new Result(false);
            try
            {
                this.repository.Create(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public IResult Delete(int productID)
        {
            IResult result = new Result(false);

            if (!this.IsExists(productID))
            {
                result.Message = "找不到資料";
            }

            try
            {
                var instance = this.GetByID(productID);
                this.repository.Delete(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public IEnumerable<Products> GetAll()
        {
            return this.repository.GetAll();
        }

        public IEnumerable<Products> GetByCategory(int categoryID)
        {
            return this.repository.GetAll().Where(x => x.CategoryID == categoryID);
        }

        public Products GetByID(int productID)
        {
            return this.repository.Get(x => x.ProductID == productID);
        }

        public bool IsExists(int productID)
        {
            return this.repository.GetAll().Any(x => x.ProductID == productID);
        }

        public IResult Update(Products instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException();
            }

            IResult result = new Result(false);
            try
            {
                this.repository.Update(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }
    }
}
