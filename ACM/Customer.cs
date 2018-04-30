using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM
{
    public class Customer
    {
        #region Propiedades
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? CustomerTypeID { get; set; }
        public string EmailAddress { get; set; }
        public List<Invoice> InvoiceList { get; set; }
        #endregion
    }
}
