using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using loginsec;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CustomerManagementSystem
{
    public partial class LoginForm : Form
    {
        Connection con = new Connection();
        string id;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuBar MB = new MenuBar();
            MB.StartPosition = FormStartPosition.WindowsDefaultLocation;
            MB.PassCustId = id;
            this.Hide();
            MB.ShowDialog();
            /*
            try
            {
                if (textBox1.Text != "" && maskedTextBox1.Text != "")
                {
                    con.Open();
                    // changing username to Emp_User_Name, password to Emp_password and human.resources.employee to human_resources.employee
                    string query = "select customer_id from customer_management.customer WHERE email ='" + textBox1.Text + "' AND password ='" + maskedTextBox1.Text + "'";
                    MySqlDataReader row;
                    row = con.ExecuteReader(query);
                    if (row.HasRows)
                    {
                        while (row.Read())
                        {
                            id = row["customer_id"].ToString();
                        }
                        MenuBar MB = new MenuBar();
                        MB.StartPosition = FormStartPosition.WindowsDefaultLocation;
                        MB.PassCustId = id;
                        this.Hide();
                        MB.ShowDialog();

                        //MessageBox.Show("Data found your name is " + firstname + " " + username + " " + " and your ID at " + id);
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Username or Password!", "Login Page");
                    }
                }
                else
                {
                    MessageBox.Show("Username or Password is empty!", "Login Page");
                }
            }
            catch
            {
                MessageBox.Show("Connection Error!", "Database Information");
            }
            */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Signup SU = new Signup();
            SU.StartPosition = FormStartPosition.WindowsDefaultLocation;
            SU.Show();
            this.Hide();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}