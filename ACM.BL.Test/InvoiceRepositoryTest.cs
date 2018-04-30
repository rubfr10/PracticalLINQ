using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ACM;

namespace ACM.BL.Test
{
    [TestClass]
    public class InvoiceRepositoryTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void CalculateTotalAmountInvoiceTest()
        {
            //Arrange
            InvoiceRespository repository = new InvoiceRespository();
            var invoiceList = repository.Retrieve();

            //Act
            var actual = repository.CalculateTotalAmountInvoiced(invoiceList);

            //Assert
            Assert.AreEqual(1333.14M,actual);
        }

        [TestMethod]
        public void CalculateTotalUnitSold()
        {
            //Arrange
            InvoiceRespository repository = new InvoiceRespository();
            var invoiceList = repository.Retrieve();

            //Act
            var actual = repository.CalculateTotalUnitSold(invoiceList);

            //Assert
            Assert.AreEqual(136, actual);
        }

        [TestMethod]
        public void GetInvoiceTotalByIsPaidTest()
        {
            //Arrange
            InvoiceRespository repository = new InvoiceRespository();
            var invoiceList = repository.Retrieve();

            //Act
            var query = repository.GetInvoiceTotalByIsPaid(invoiceList);

            //NOT REALLY A TEST
        }

        [TestMethod]
        public void GetInvoiceTotalByIsPaidAndMonthTest()
        {
            //Arrange
            InvoiceRespository repository = new InvoiceRespository();
            var invoiceList = repository.Retrieve();

            //Act
            var query = repository.GetInvoiceTotalByIsPaidAndMonth(invoiceList);

            //NOT REALLY A TEST
        }

        [TestMethod]
        public void CalculateMedianTest()
        {
            //Arrange
            InvoiceRespository repository = new InvoiceRespository();
            var invoiceList = repository.Retrieve();

            //Act
            var actual = repository.CalculateMedian(invoiceList);

            //Assert
            Assert.AreEqual(10M, actual);
        }

        [TestMethod]
        public void CalculateModeTest()
        {
            //Arrange
            InvoiceRespository repository = new InvoiceRespository();
            var invoiceList = repository.Retrieve();

            //Act
            var actual = repository.CalculateMode(invoiceList);

            //Assert
            Assert.AreEqual(10M, actual);
        }
    }
}
