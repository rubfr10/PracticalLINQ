using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACM;

namespace ACMWIN
{
    public partial class CustomerWin : Form
    {
        CustomerRepository customerRepository = new CustomerRepository();
        public CustomerWin()
        {
            InitializeComponent();
        }

        private void GetCustomersButton_Click(object sender, EventArgs e)
        {
            //CustomerGridView.DataSource = customerRepository.Retrieve();

            var customerList = customerRepository.Retrieve();
            //CustomerGridView.DataSource = customerRepository.SortByName(customerList).ToList();

            //CustomerGridView.DataSource = customerRepository.GetOverdueCustomer(customerList).ToList();

            //var unpaidCustomerList = customerRepository.GetOverdueCustomer(customerList);
            //CustomerGridView.DataSource = customerRepository.SortByName(unpaidCustomerList).ToList();

            CustomerTypeRepository customerTypeRepository = new CustomerTypeRepository();
            var customerTypeList = customerTypeRepository.Retrieve();

            CustomerGridView.DataSource = customerRepository.GetNamesAndType(customerList, customerTypeList);
        }

        private void CustomerWin_Load_1(object sender, EventArgs e)
        {
            var customerList = customerRepository.Retrieve();

            CustomersComboBox.DisplayMember = "Name";
            CustomersComboBox.ValueMember = "CustomerID";
            CustomersComboBox.DataSource = customerRepository.GetNamesAndId(customerList);
        }

        private void CustomersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CustomersComboBox.SelectedValue != null)
            {
                int customerId;
                if (int.TryParse(CustomersComboBox.SelectedValue.ToString(), out customerId))
                {
                    var customerList = customerRepository.Retrieve();

                    CustomerGridView.DataSource = new List<Customer>() {
                        customerRepository.Find(customerList, customerId)
                    };
                }
            }
        }
    }
}
