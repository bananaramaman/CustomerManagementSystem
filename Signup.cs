using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using loginsec;
using System.Timers;

namespace CustomerManagementSystem
{
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }
        Connection con = new Connection();
        public string MySqlConnectionString;
        
        private void button1_Click(object sender, EventArgs e)
        {
            back();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            newCustomer();
        }

        // method to return to login screen
        private void back()
        {
            LoginForm LF = new LoginForm();
            LF.Show();
            this.Hide();
        }

        // create new customer record in database from information filled out in the form
        private void newCustomer()
        {
            int result;
            string fName = textBox1.Text;
            string lName = textBox2.Text;
            string email = textBox4.Text;
            string password = textBox5.Text;
            string password2 = textBox6.Text;

            string query = "Insert into customer (firstname, lastname, email, password) values ('" + fName + "','" + lName + "','" + email + "','" + password + "')";
            MySqlConnectionString = con.connectionString();
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlCommand commandDatabase;

            if (fName == "" || lName == "" || password == "" || email == "" || password != password2)
            {
                MessageBox.Show("Please check that all information is entered correctly, and that both passwords match", "Sign up error");
            }
            else
            {
                try
                {
                    databaseConnection.Open();
                    commandDatabase = new MySqlCommand();
                    commandDatabase.Connection = databaseConnection;
                    commandDatabase.CommandText = query;
                    result = commandDatabase.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Your account has been sucessfully created!" , "Welcome!");
                        databaseConnection.Close();
                        back();
                        this.Close();
                    }
                }
                catch (Exception a)
                {
                    MessageBox.Show("Query error " + a.Message);
                }
            }
        }

    }
}