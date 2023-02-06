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

namespace CustomerManagementSystem
{
    public partial class ProductView : Form
    {
        MenuBar MB = new MenuBar();
        ProductFactory PF = new ProductFactory();
        public int x = 0;
        public string y;
        public ProductView()
        {
            InitializeComponent();
        }
        private void ProductView_Load(object sender, EventArgs e)
        {
            
        }
        public void prod(string id,string name,string category,
            string description, string url, string price, string stock)
        {
            Title1.Text = name;
            cat1.Text = category;
            desc.Text = description;
            pictureBox1.ImageLocation = url;
            Price1.Text = "$"+price;
            Stock1.Text = stock;
        }
        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
            PF.NewPanel(MB);
            PF.HomePage(x,y);
        }

        private void buynow_Click(object sender, EventArgs e)
        {

        }

        private void addtocart_Click(object sender, EventArgs e)
        {
            CartPage CP = new CartPage();
            PF.CloseChildForm(this);
            PF.shopCart();
        }
    }
}
