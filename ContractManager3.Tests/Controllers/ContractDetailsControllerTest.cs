using ContractManager3.Models;
using ContractManager3.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Mvc;

namespace ContractManager3.Controllers.Tests
{
    [TestClass()]
    public class ContractDetailsControllerTest
    {

        [TestMethod()]
        public void IndexTest()
        {
            TestDeploymentContext tdc = new TestDeploymentContext();
            var controller = new ContractDetailsController(tdc);
            var result = controller.Index();
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            TestDeploymentContext tdc = new TestDeploymentContext();

            //adding one contract, as the contract requires a supplier id
            ContractDetail x = new ContractDetail() { Contract_ID = 0,  Supplier_ID = 0, ContractStartDate = new DateTime(10, 05, 2019),
            ContractFinishDate = new DateTime(10, 05, 2020),Servicetype = Service.Waste, PriceDescription = "Hourly Price", Price = 22.00,
            VatRate = 0.135, PriceUpdatedate = new DateTime(01 / 01 / 2019) };
            tdc.ContractDetails.Add(x);
            Assert.IsNotNull(x);


            tdc.Supplier.Add(new Supplier () { Supplier_ID = 0, SupplierNumber = "SN000", SupplierName = "Tipperary Water", SupplierAddress = "5 Cherry Orchard Industrial Estate, Ballyfermot, Dublin 10.", SupplierCounty = "Dublin", SupplierContact = "Lana kelly", SupplierEMail = "info@tipperarywater.ie" });
            var controller = new SuppliersController(tdc);
            var result = controller.Details(0);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Supplier y = new Supplier();
            //(Supplier)((ViewResult)result).Model;
            Assert.AreEqual(y.SupplierName, "Tipperary Water");
        }



        [TestMethod()]
        public void CreateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteConfirmedTest()
        {
            Assert.Fail();
        }
    }
}