using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWindDemo.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NorthWindDemo.Web.Controllers.Tests
{
    [TestClass()]
    public class CategoryControllerTests
    {
        //[TestMethod()]
        //public void CategoryControllerTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void IndexTest()
        //{
        //    // Arrange
        //    CategoryController controller = new CategoryController();

        //    // Act
        //    ViewResult result = controller.Index() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);

        //    //Assert.Fail();
        //}

        //[TestMethod()]
        //public void DetailsTest()
        //{
        //    // Arrange
        //    CategoryController controller = new CategoryController();

        //    // Act
        //    ViewResult result = controller.Details(81) as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);

        //    //Assert.Fail();
        //}

        [TestMethod()]
        public void CreateTest()
        {
            // Arrange
            CategoryController controller = new CategoryController();

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);

            //Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest1()
        {
            // Arrange
            CategoryController controller = new CategoryController();

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);

            //Assert.Fail();
        }

        //[TestMethod()]
        //public void EditTest()
        //{
        //    // Arrange
        //    CategoryController controller = new CategoryController();

        //    // Act
        //    ViewResult result = controller.Details(81) as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);

        //    //Assert.Fail();
        //}

        //[TestMethod()]
        //public void EditTest1()
        //{
        //    // Arrange
        //    CategoryController controller = new CategoryController();

        //    // Act
        //    ViewResult result = controller.Details(81) as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);

        //    //Assert.Fail();
        //}

        //[TestMethod()]
        //public void DeleteTest()
        //{
        //    // Arrange
        //    CategoryController controller = new CategoryController();

        //    // Act
        //    ViewResult result = controller.Details(81) as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);

        //    //Assert.Fail();
        //}

        //[TestMethod()]
        //public void DeleteTest1()
        //{
        //    // Arrange
        //    CategoryController controller = new CategoryController();

        //    // Act
        //    ViewResult result = controller.Delete(81) as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);

        //    //Assert.Fail();
        //}
    }
}