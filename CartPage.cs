using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomerManagementSystem.Products;
using CustomerManagementSystem.Payment;

namespace CustomerManagementSystem
{
    public partial class CartPage : Form
    {
        ProductFactory PF = new ProductFactory();
        public int x = 0;
        public string y;
        public CartPage()
        {
            InitializeComponent();
        }
        private void CartPage_Load(object sender, EventArgs e)
        {
            PaymentFactory PP = new PaymentFactory();
            PP.CartList(this);
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
            PF.HomePage(x, y);
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            this.Close();
            PF.shopCart();
        }

        private void Checkout_Click(object sender, EventArgs e)
        {
            this.Close();
            PF.Payment();
        }
    }
}
