using System.Web.Mvc;
using Microsoft.Practices.Unity;
using NorthWindDemo.Models.Interface;
using NorthWindDemo.Models;
using NorthWindDemo.Service.Interface;
using NorthWindDemo.Service;
using Unity.Mvc4;
using NorthWindDemo.Models.Repository;
using System.Data.Entity;

namespace NorthWindDemo.Web
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new UnityDependencyResolver(container));

      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();

      // register all your components with the container here
      // it is NOT necessary to register your controllers

      // e.g. container.RegisterType<ITestService, TestService>();    
      RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
            var dbContext = new northwindDBEntities();
            // Repository
            container.RegisterType<IRepository<Categories>, DataRepository<Categories>>(
                new InjectionConstructor(dbContext));
            container.RegisterType<IRepository<Products>, DataRepository<Products>>(
                new InjectionConstructor(dbContext));
            container.RegisterType<IRepository<Suppliers>, DataRepository<Suppliers>>(
                new InjectionConstructor(dbContext));

            // Service
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<ISuppliersService, SuppliersService>();
        }
  }
}