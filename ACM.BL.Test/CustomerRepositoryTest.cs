using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ACM;

namespace ACM.BL.Test
{
    [TestClass]
    public class CustomerRepositoryTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void FindTestExistingCustomer()
        {
            //Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            //Act
            var result = repository.Find(customerList,2);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.CustomerID);
            Assert.AreEqual("Baggins", result.LastName);
            Assert.AreEqual("Bilbo", result.FirstName);
        }

        [TestMethod]
        public void FindTestNotFound()
        {
            //Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            //Act
            var result = repository.Find(customerList, 42);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void SortByNameTest()
        {
            //Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            //Act
            var result = repository.SortByName(customerList);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.First().CustomerID);
            Assert.AreEqual("Baggins", result.First().LastName);
            Assert.AreEqual("Bilbo", result.First().FirstName);
        }

        [TestMethod]
        public void SortByNameInReverseTest()
        {
            //Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            //Act
            var result = repository.SortByNameInReverse(customerList);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Last().CustomerID);
            Assert.AreEqual("Baggins", result.Last().LastName);
            Assert.AreEqual("Bilbo", result.Last().FirstName);
        }

        [TestMethod]
        public void SortByTypeTest()
        {
            //Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            //Act
            var result = repository.SortByNameInReverse(customerList);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(null, result.Last().CustomerTypeID);
        }

        [TestMethod]
        public void GetNamesTest()
        {
            //Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            //Act
            var query = repository.GetNames(customerList);

            //Analyze
            foreach (var item in query)
            {
                TestContext.WriteLine(item);
            }

            //Assert
            Assert.IsNotNull(query);
        }

        [TestMethod]
        public void GetNamesAndEmailTest()
        {
            //Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            //Act
            var query = repository.GetNamesAndEmail(customerList);

            //NOT REALLY A TEST
        }

        [TestMethod]
        public void GetNamesAndTypeTest()
        {
            //Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            CustomerTypeRepository typeRepository = new CustomerTypeRepository();
            var customerTypeList = typeRepository.Retrieve();

            //Act
            var query = repository.GetNamesAndType(customerList, customerTypeList);

            //NOT REALLY A TEST
        }

        [TestMethod]
        public void GetOverdueCustomersTest()
        {
            //Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            //Act
            var query = repository.GetOverdueCustomer(customerList);

            //Analyze
            foreach (var item in query)
            {
                TestContext.WriteLine(item.LastName + ", " + item.FirstName);
                //foreach (var childItem in item)
                //{
                //    TestContext.WriteLine(childItem.DueDate.ToString());
                //}
            }

            //Assert
            Assert.IsNotNull(query);
        }

        [TestMethod]
        public void GetInvoiceTotalByCustomerTest()
        {
            //Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            CustomerTypeRepository typeRepository = new CustomerTypeRepository();
            var customerTypeList = typeRepository.Retrieve();

            //Act
            var query = repository.GetInvoiceTotalByCustomerType(customerList, customerTypeList);

            //NOT REALLY A TEST
        }

        [TestMethod]
        public void CalculateMeanTest()
        {
            //Arrange
            InvoiceRespository repository = new InvoiceRespository();
            var invoiceList = repository.Retrieve();

            //Act
            var actual = repository.CalculateMean(invoiceList);

            //Assert
            Assert.AreEqual(6.875M, actual);
        }
    }
}
