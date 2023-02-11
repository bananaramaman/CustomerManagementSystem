﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomerManagementSystem.Products;
using CustomerManagementSystem.Orders;
using CustomerManagementSystem.UserValidation;


namespace CustomerManagementSystem
{
    public partial class PaymentPage : Form
    {
        ProductFactory PF = new ProductFactory();
        public int x = 0;
        public string y;
        public PaymentPage()
        {
            InitializeComponent();
            dateTimePicker1.CustomFormat = "yyyy-MM-dd"; 
        }

        private void PaymentPage_Load(object sender, EventArgs e)
        {
            OrderFactory OF = new OrderFactory();
            OF.Orderlist(this);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                addressLabel.Visible = true;
                label3.Visible = true;
                textBox1.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label9.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                comboBox1.Visible = true;
            }
            else if (radioButton2.Checked)
            {
                addressLabel.Visible = false;
                label3.Visible = false;
                textBox1.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label9.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                comboBox1.Visible = false;
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
            PF.HomePage(x, y);
        }
        private void ConfirmOrder_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dateTimePicker1.Text);
            string street = textBox1.Text;
            string suburb = textBox2.Text;
            string city = textBox3.Text;
            string postcode = textBox5.Text;
            string Phone = textBox4.Text;
            string country = comboBox1.Text;
            string DOB = dateTimePicker1.Text;
            OrderFactory OF = new OrderFactory();
            OF.UserPurchase(street,suburb,city,postcode,Phone,country,DOB);
        }
    }
}
