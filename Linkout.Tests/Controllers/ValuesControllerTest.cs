using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Linkout;
using Linkout.Controllers;
using Linkout.Services;
using Moq;

namespace Linkout.Tests
{
    [TestClass]
    public class ValuesControllerTest
    {
        private ITestService GetMockService()
        {
            var selSrvc = new Mock<ITestService>();
            return selSrvc.Object;
        }

        [TestMethod]
        public void Get()
        {
            // Arrange
            ValuesController controller = new ValuesController(GetMockService());

            // Act
            IEnumerable<string> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            ValuesController controller = new ValuesController(GetMockService());

            // Act
            string result = controller.Get(5);

            // Assert
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            ValuesController controller = new ValuesController(GetMockService());

            // Act
            controller.Post("value");

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            ValuesController controller = new ValuesController(GetMockService());

            // Act
            controller.Put(5, "value");

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            ValuesController controller = new ValuesController(GetMockService());

            // Act
            controller.Delete(5);

            // Assert
        }
    }
}
