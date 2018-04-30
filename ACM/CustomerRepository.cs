using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM
{
    public class CustomerRepository
    {
        public Customer Find(List<Customer> customerList, int customerId)
        {
            Customer foundCustomer = null;

            //foreach (var c in customerList)
            //{
            //    if (c.CustomerID == customerId)
            //    {
            //        foundCustomer = c;
            //        break;
            //    }
            //}

            //var query = from c in customerList
            //            where c.CustomerID == customerId
            //            select c;

            //foundCustomer = query.First();

            foundCustomer = customerList.FirstOrDefault(c =>
                                               c.CustomerID == customerId);

            //foundCustomer = customerList.FirstOrDefault(c =>
            //                                            {
            //                                                Debug.WriteLine(c.LastName);
            //                                                return c.CustomerID == customerId;
            //                                            });

            //foundCustomer = customerList.Where(c => 
            //                                    c.CustomerID == customerId)
            //                                    .Skip(1)
            //                                    .FirstOrDefault();

            return foundCustomer;
        }

        public IEnumerable<Customer> SortByName(IEnumerable<Customer> customerList)
        {
            return customerList.OrderBy(c => c.LastName)
                                        .ThenBy(c => c.FirstName);
        }

        public IEnumerable<Customer> SortByNameInReverse(List<Customer> customerList)
        {
            //return customerList.OrderByDescending(c => c.LastName)
            //                                    .ThenByDescending(c => c.FirstName);
            return SortByName(customerList).Reverse();
        }

        public IEnumerable<Customer> SortByType(List<Customer> customerList)
        {
            //return customerList.OrderBy(c => c.CustomerTypeID);
            return customerList.OrderByDescending(c => c.CustomerTypeID.HasValue)
                                                    .ThenBy(c => c.CustomerTypeID);
        }

        public IEnumerable<Customer> RetrieveEmptyList()
        {
            return Enumerable.Repeat(new Customer(),5);
        }

        public List<Customer> Retrieve()
        {
            InvoiceRespository invoiceRepository = new InvoiceRespository();

            List<Customer> custList = new List<Customer>
            {
                new Customer()
                {
                    CustomerID = 1,
                    FirstName = "Frodo",
                    LastName = "Baggins",
                    EmailAddress = "fb@hob.me",
                    CustomerTypeID = 1,
                    InvoiceList = invoiceRepository.Retrieve(1)
                },
                new Customer()
                {
                    CustomerID = 2,
                    FirstName = "Bilbo",
                    LastName = "Baggins",
                    EmailAddress = "bb@hob.me",
                    CustomerTypeID = null,
                    InvoiceList = invoiceRepository.Retrieve(2)
                },
                new Customer()
                {
                    CustomerID = 3,
                    FirstName = "Samwise",
                    LastName = "Gamgee",
                    EmailAddress = "sg@hob.me",
                    CustomerTypeID = 4,
                    InvoiceList = invoiceRepository.Retrieve(3)
                },
                new Customer()
                {
                    CustomerID = 4,
                    FirstName = "Rosie",
                    LastName = "Cotton",
                    EmailAddress = "rc@hob.me",
                    CustomerTypeID = 2,
                    InvoiceList = invoiceRepository.Retrieve(4)
                }
            };
            return custList;
        }

        public IEnumerable<Customer> GetOverdueCustomer(List<Customer> customerList)
        {
            //var query = customerList.Select(c => c.InvoiceList
            //                        .Where(i => (i.IsPaid ?? false) == false));

            var query = customerList
                                .SelectMany( c => c.InvoiceList
                                    .Where(i => (i.IsPaid ?? false) == false),
                                    (c,i)=> c).Distinct();

            return query;
        }

        public IEnumerable<string> GetNames(List<Customer> customerList)
        {
            var query = customerList.Select(c => c.LastName + ", " + c.FirstName);
            return query;
        }

        public dynamic GetNamesAndEmail(List<Customer> customerList)
        {
            var query = customerList.Select(c => new
                            {
                                Name = c.LastName + ", " + c.FirstName,
                                c.EmailAddress
                            });

            foreach (var item in query)
            {
                Console.WriteLine(item.Name + ":" + item.EmailAddress);
            }

            return query;
        }

        public dynamic GetNamesAndType(List<Customer> customerList, List<CustomerType> customerTypeList)
        {
            var query = customerList.Join(customerTypeList,
                                            c => c.CustomerTypeID,
                                            ct => ct.CustomerTypeId,
                                            (c, ct) => new
                                            {
                                                Name = c.LastName + ", " + c.FirstName,
                                                CustomerTypeName = ct.TypeName
                                            });
            foreach (var item in query)
            {
                Console.WriteLine(item.Name + ": " + item.CustomerTypeName);
            }

            return query.ToList();
        }

        public dynamic GetNamesAndId(List<Customer> customerList)
        {
            var query = customerList.OrderBy(c => c.LastName)
                                        .ThenBy(c => c.FirstName)
                                        .Select(c => new {
                                            Name = c.LastName + ", " + c.FirstName,
                                            c.CustomerID
                                        });
            return query.ToList();
        }

        public IEnumerable<KeyValuePair<string, decimal>> GetInvoiceTotalByCustomerType(List<Customer> customerList, List<CustomerType> customerTypeList)
        {
            var customerTypeQuery = customerList.Join(customerTypeList, 
                                                    c => c.CustomerTypeID,
                                                    ct => ct.CustomerTypeId,
                                                    (c,ct) => new {
                                                        CustomerInstance = c,
                                                        CustomerTypeName = ct.TypeName
                                                    });

            var query = customerTypeQuery.GroupBy(c => c.CustomerTypeName,
                                            c => c.CustomerInstance.InvoiceList.Sum(inv => inv.TotalAmount),
                                            (groupKey, invTotal) => new KeyValuePair<string, decimal>(groupKey, invTotal.Sum()));

            foreach (var item in query)
            {
                Console.WriteLine(item.Key + ": " + item.Value);
            }

            return query;
        }
    }
}
