﻿using NorthWindDemo.Models;
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
    public class SuppliersService : ISuppliersService
    {
        private IRepository<Suppliers> repository = new DataRepository<Suppliers>();

        public IResult Create(Suppliers instance)
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

        public IResult Delete(int supplierID)
        {
            IResult result = new Result(false);

            if (!this.IsExists(supplierID))
            {
                result.Message = "找不到資料";
            }

            try
            {
                var instance = this.GetByID(supplierID);
                this.repository.Delete(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public IEnumerable<Suppliers> GetAll()
        {
            return this.repository.GetAll();
        }

        public Suppliers GetByID(int supplierID)
        {
            return this.repository.Get(x => x.SupplierID == supplierID);
        }

        public bool IsExists(int supplierID)
        {
            return this.repository.GetAll().Any(x => x.SupplierID == supplierID);
        }

        public IResult Update(Suppliers instance)
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