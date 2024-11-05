using NorthWindDemo.Models;
using NorthWindDemo.Service.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWindDemo.Service.Interface
{
    public interface ISuppliersService
    {
        IResult Create(Suppliers instance);

        IResult Update(Suppliers instance);

        IResult Delete(int supplierID);

        bool IsExists(int supplierID);

        Suppliers GetByID(int supplierID);

        IEnumerable<Suppliers> GetAll();

    }
}
