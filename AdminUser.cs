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
    public partial class AdminUser : Form
    {
        public int x = 2;
        public string userID;
        
        List<string> userRes = AdminFactory.user;
        public AdminUser()
        {
            InitializeComponent();
        }

        private void AdminUser_Load(object sender, EventArgs e)
        {   
            string query = "select * from customer";
            AdminFactory AF = new AdminFactory();
            AF.search(query, x);
            for (int i = 0; i < userRes.Count; i++)
            {
                comboBox5.Items.Add(userRes[i]);
            }
            userRes.Clear();
        }
        private void button3_Click(object sender, EventArgs e)
        {   // created a new user record in the database
            string query = "Insert into customer (firstname, lastname, email, dob, phonenumber, street, suburb, city, postcode, country, password, admin) " +
                "values ('" + textBox10.Text + "','" + textBox9.Text + "','" + textBox8.Text + "','" + dateTimePicker1.Text + "'," +
                "'" + textBox1.Text + "','" + textBox6.Text + "','" + textBox5.Text + "','" + textBox4.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "'," +
                "'" + textBox7.Text + "','" + comboBox2.Text + "')";
            AdminFactory AF = new AdminFactory();
            AF.updateRec(query);
        }
        private void back_Click(object sender, EventArgs e)
        {   // returns user to the home page
            this.Close();
            AdminBar ab = new AdminBar();
            ab.back();
        }
        private void button4_Click(object sender, EventArgs e)
        {   //refresh the page clearing textboxes and labels
            this.Controls.Clear();
            InitializeComponent();
            string query = "select * from customer";
            AdminFactory AF = new AdminFactory();
            AF.search(query, x);
            for (int i = 0; i < userRes.Count; i++)
            {
                comboBox5.Items.Add(userRes[i]);
            }
            userRes.Clear();
        }
        private void button1_Click(object sender, EventArgs e)
        {   //removes user from Database
            string query = "Delete from customer where customer_id = '" + userID + "'";
            DialogResult d = MessageBox.Show("Are you sure you wish to remove this item from the order?", "Warning!", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes)
            {
                AdminFactory AF = new AdminFactory();
                AF.RemUser(query);
            }
            else return;    
        }
        private void button2_Click(object sender, EventArgs e)
        {   // updates existing user details
            int UserID = Convert.ToInt32(userID);
            string query = "update customer_management.customer set firstname = '" + textBox10.Text + "' , lastname = '" + textBox9.Text + "', email = '" + textBox8.Text + "', dob = '" + dateTimePicker1.Text + "', " +
                "phonenumber = '" + textBox1.Text + "',street = '" + textBox6.Text + "',suburb = '" + textBox5.Text + "',city = '" + textBox4.Text + "'," +
                "postcode = '" + textBox2.Text + "',country = '" + comboBox1.Text + "',password = '" + textBox7.Text + "',admin = '" + comboBox2.Text + "' where customer.customer_id = '"+UserID+"';";
            AdminFactory AF = new AdminFactory();
            AF.updateRec(query);
        }
        private void button5_Click(object sender, EventArgs e)
        {   //populates form with chosen user details
            string orderinfo = comboBox5.Text;
            string[] orderstring = orderinfo.Split(','); //separate order ID from order information in search box
            userID = orderstring[0]; // store orderID in variable 
            string query = "select * from customer_management.customer where customer_id = '"+userID+"';";
            AdminFactory AF = new AdminFactory();
            AF.populate(query);
            label16.Text = AF.custid;
            textBox10.Text = AF.fname;
            textBox9.Text = AF.lname;
            textBox8.Text = AF.email;
            dateTimePicker1.Text = AF.dob;
            textBox1.Text = AF.ph;
            textBox6.Text = AF.street;
            textBox5.Text = AF.suburb;
            textBox4.Text = AF.city;
            textBox2.Text = AF.postcode;
            comboBox1.Text = AF.country;
            textBox7.Text = AF.pass;
            comboBox2.Text = AF.adminYN;
        }
        private void button6_Click(object sender, EventArgs e)
        {   //show password 
            textBox7.UseSystemPasswordChar = false;
        }
    }
}
