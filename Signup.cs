using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using loginsec;
using CustomerManagementSystem.UserValidation;

namespace CustomerManagementSystem
{
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }
        
        // Takes the user back to login page
        private void button1_Click(object sender, EventArgs e)
        {
            back();
        }

        // submits the user details to the UserFactory class 
        private void button2_Click(object sender, EventArgs e)
        {
            string fName = textBox1.Text;
            string lName = textBox2.Text;
            string email = textBox4.Text;
            string password = textBox5.Text;
            string password2 = textBox6.Text;
            // create new customer record in database from information filled out in the form
            UserFactory signup = new UserFactory();
            signup.NewUser(fName, lName, email, password, password2);
        }

        // method to return to login screen
        private void back()
        {
            LoginForm LF = new LoginForm();
            LF.Show();
            this.Hide();
        }
    }
}