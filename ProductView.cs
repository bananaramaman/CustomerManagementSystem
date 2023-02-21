using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Forms;
using CustomerManagementSystem.Products;
using CustomerManagementSystem.Payment;


namespace CustomerManagementSystem
{
    public partial class ProductView : Form
    {
        ProductFactory PF = new ProductFactory();
        public int x = 0;
        public string y;
        public ProductView()
        {
            InitializeComponent();
            
        }
        private void ProductView_Load(object sender, EventArgs e)
        {
            int stock = Convert.ToInt32(Stock1.Text);
            QTY.Maximum = stock;
        }
        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
            PF.HomePage(x, y);
        }

        private void buynow_Click(object sender, EventArgs e)
        {
            string Desc = Title1.Text;
            string ID = id.Text;
            int quant = Convert.ToInt32(QTY.Value);
            double price = Convert.ToDouble(Price1.Text);
            PaymentFactory PP = new PaymentFactory();
            PP.CartContents(Desc, price, ID, quant);
            this.Close();
            PF.Payment();
        }

        private void addtocart_Click(object sender, EventArgs e)
        {
            string Desc = Title1.Text;
            string ID = id.Text;
            int quant = Convert.ToInt32(QTY.Value);
            double price = Convert.ToDouble(Price1.Text);
            PaymentFactory PP = new PaymentFactory();
            PP.CartContents(Desc, price, ID, quant);
        }
    }
}
