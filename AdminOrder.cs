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
    public partial class AdminOrder : Form
    {
        public int x = 1;
        List<string> orderRes = AdminFactory.order;
        List<string> orderPro = AdminFactory.orderP;
        public string orderID;
        public string discA;
        public string orderT;
        public string orderCre;
        public string orderCom;
        public string orderActi;

        public string prodOrderID;
        public string prodDes;
        public string prodPrice;
        public string prodID;
        public string prodQty;
        

        public AdminOrder()
        {
            InitializeComponent();
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
        }

        private void AdminOrder_Load(object sender, EventArgs e)
        {
            string query = "select * from orders;";
            AdminFactory AF = new AdminFactory();
            AF.search(query, x);
            for (int i = 0; i < orderRes.Count; i++)
            {
                comboBox1.Items.Add(orderRes[i]);
            }
            orderRes.Clear();
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
            AdminBar ab = new AdminBar();
            ab.back();
        }

        private void button1_Click(object sender, EventArgs e)
        {   // remove an order from the database
            DialogResult d = MessageBox.Show("Are you sure you wish to remove this item from the order?", "Warning!", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes)
            {
                double prodP = Convert.ToDouble(prodPrice);
                int qty = Convert.ToInt32(prodQty);
                double orderCost = Convert.ToDouble(orderT);
                double cost = orderCost - (prodP * qty);
                string query = "Delete from order_item where order_item_id = '" + prodOrderID + "'";
                string query2 = "update customer_management.orders set total = '" + cost + "' where orders.order_id = '"+orderID+"';";
                AdminFactory AF = new AdminFactory();
                AF.deleteRec(query,query2);
            }
            else
            {
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {   // update existing order details in database
            string discID = textBox5.Text;
            string price = textBox10.Text;
            string completedDate = dateTimePicker1.Text;
            string Active = comboBox3.Text;
            string query = "update customer_management.orders set total = '" + price + "' , discount_id = '"+discID+"', completed_date = '"+completedDate+"', active = '"+Active+"' where orders.order_id = '" + orderID + "';";
            AdminFactory AF = new AdminFactory();
            AF.updateRec(query);
        }

        private void button5_Click(object sender, EventArgs e)
        {   // populate form text boxes and labels with order details
            string orderinfo = comboBox1.Text;
            string[] orderstring = orderinfo.Split(','); //separate order ID from order information in search box
            orderID = orderstring[0]; // store orderID in variable 
            discA = orderstring[1];
            orderT = orderstring[2];
            orderCre = orderstring[3];
            orderActi = orderstring[4];
            label11.Visible = true; label11.Text = orderID;
            textBox5.Text = discA;
            textBox10.Text = orderT;
            textBox8.Text = orderCre;
            comboBox3.Text = orderActi;

            string query1 = "SELECT orders.order_id, discount.discount_amount, orders.total, orders.created_date, orders.completed_date, active " +
                "FROM orders,discount where order_id = '" + orderID + "' and orders.discount_id = discount.discount_id;";
            string query2 = "SELECT order_item.order_item_id, order_item.description, order_item.price, order_item.product_id, order_item.quantity " +
                      "FROM order_item, order_items, orders where orders.order_id = '" + orderID + "' and order_item.order_item_id = order_items.order_item_id and order_items.order_id = orders.order_id;";
            AdminFactory AF = new AdminFactory();
            AF.orderRes(this, query1, query2);
            AF.search(query2, x = 4);
            for (int i = 0; i < orderPro.Count; i++)
            {
                comboBox2.Items.Add(orderPro[i]);
            }
            orderPro.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {   //Entering order product details into form text boxes for editing
            string product = comboBox2.Text;
            string[] prodstring = product.Split(',');
            prodOrderID = prodstring[0];
            prodDes = prodstring[1];
            prodPrice = prodstring[2];
            prodID = prodstring[3];
            prodQty = prodstring[4];
            textBox1.Text = prodOrderID;
            textBox4.Text = prodID;
            textBox6.Text = prodDes;
            textBox2.Text = prodPrice;
            textBox3.Text = prodQty;
        }

        private void button4_Click(object sender, EventArgs e)
        {   //refresh page
            this.Controls.Clear();
            InitializeComponent();
            string query = "select * from orders;";
            AdminFactory AF = new AdminFactory();
            AF.search(query, x = 1);
            for (int i = 0; i < orderRes.Count; i++)
            {
                comboBox1.Items.Add(orderRes[i]);
            }
            orderRes.Clear();
        }
    }
}
