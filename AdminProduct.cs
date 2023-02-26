using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomerManagementSystem.UserValidation;
using CustomerManagementSystem.Admin;
using CustomerManagementSystem.Products;

namespace CustomerManagementSystem
{
    public partial class AdminProduct : Form
    {
        public int x = 3;
        public string prodID;
        List<string> prodRes = AdminFactory.product;
        public AdminProduct()
        {
            InitializeComponent();
        }

        private void AdminProduct_Load(object sender, EventArgs e)
        {
            string query = "select * from products";
            AdminFactory AF = new AdminFactory();
            AF.search(query, x);
            for (int i = 0; i < prodRes.Count; i++)
            {
                comboBox1.Items.Add(prodRes[i]);
            }
            prodRes.Clear();
        }

        private void back_Click(object sender, EventArgs e)
        {   //returns user to home page
            this.Close();
            AdminBar ab = new AdminBar();
            ab.back();
        }

        private void button4_Click(object sender, EventArgs e)
        {   // refresh the form page clearing all text boxes / lables
            this.Controls.Clear();
            InitializeComponent();
            string query = "select * from products";
            AdminFactory AF = new AdminFactory();
            AF.search(query, x);
            for (int i = 0; i < prodRes.Count; i++)
            {
                comboBox1.Items.Add(prodRes[i]);
            }
            prodRes.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {   //add product to database
            string query = "Insert into products (prod_name, prod_category, prod_description, image_url, price, stock_qty) " +
               "values ('" + textBox10.Text + "','" + comboBox2.Text + "','" + textBox9.Text + "','" + textBox3.Text + "'," +
               "'" + textBox8.Text + "','" + textBox7.Text + "')";
            AdminFactory AF = new AdminFactory();
            AF.updateRec(query);
        }

        private void button1_Click(object sender, EventArgs e)
        {   // delete product from database
            DialogResult d = MessageBox.Show("Are you sure you wish to remove this item from the order?", "Warning!", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes)
            {
                string query = "Delete from products where product_id = '" + prodID + "'";
                AdminFactory AF = new AdminFactory();
                AF.RemUser(query);
            }
            else
            {
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {   //update existing product
            string query = "update customer_management.products set prod_name = '" + textBox10.Text + "' , prod_category = '" + comboBox2.Text + "', prod_description = '" + textBox9.Text + "'," +
                " image_url = '" + textBox3.Text + "',price = '" + textBox8.Text + "',stock_qty = '" + textBox7.Text + "' where products.product_id = '" + prodID + "';";
            AdminFactory AF = new AdminFactory();
            AF.updateRec(query);
        }

        private void button5_Click(object sender, EventArgs e)
        {   //populate form with chosen product details
            string orderinfo = comboBox1.Text;
            string[] orderstring = orderinfo.Split(','); //separate order ID from order information in search box
            prodID = orderstring[0]; // store orderID in variable 
            string query = "select * from customer_management.products where product_id = '" + prodID + "';";
            AdminFactory AF = new AdminFactory();
            AF.ProdPop(query);

            label3.Text = AF.prodid;
            textBox10.Text = AF.prodname;
            comboBox2.Text = AF.prodcat;
            textBox9.Text = AF.prodescription;
            textBox3.Text = AF.imageurl;
            textBox8.Text = AF.prodprice;
            textBox7.Text = AF.prodstock;

        }
    }
}
