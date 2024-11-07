using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthWindDemo.Models;
using NorthWindDemo.Models.Interface;
using NorthWindDemo.Models.Repository;
using NorthWindDemo.Service.Interface;
using NorthWindDemo.Service.Misc;
 
namespace NorthWindDemo.Service
{
    public class CategoryService : ICategoryService
    {
        //private IRepository<Categories> repository = new DataRepository<Categories>();
        private IRepository<Categories> _repository;
        public CategoryService(IRepository<Categories> repository)
        {
            this._repository = repository;
        }
        
        public IResult Create(Categories instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException();
            }

            IResult result = new Result(false);
            try
            {
                this._repository.Create(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public IResult Update(Categories instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException();
            }

            IResult result = new Result(false);
            try
            {
                this._repository.Update(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public IResult Delete(int categoryID)
        {
            IResult result = new Result(false);

            if (!this.IsExists(categoryID))
            {
                result.Message = "找不到資料";
            }

            try
            {
                var instance = this.GetByID(categoryID);
                this._repository.Delete(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public bool IsExists(int categoryID)
        {
            return this._repository.GetAll().Any(x => x.CategoryID == categoryID);
        }

        public Categories GetByID(int categoryID)
        {
            return this._repository.Get(x => x.CategoryID == categoryID);
        }

        public IEnumerable<Categories> GetAll()
        {
            return this._repository.GetAll();
        }
    }
}