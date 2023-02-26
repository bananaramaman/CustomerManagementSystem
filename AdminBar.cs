using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using loginsec;
using System.Net;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using CustomerManagementSystem.Admin;
using CustomerManagementSystem.UserValidation;

namespace CustomerManagementSystem
{
    public partial class AdminBar : Form
    {
        AdminFactory AF = new AdminFactory();
        public static string userAdmin = UserFactory.admin;

        public AdminBar()
        {
            InitializeComponent();

            AF.NewPanel(this);

            string fname = UserFactory.Fname;
            string lname = UserFactory.Lname;
            string userID = UserFactory.userId;
            string username = fname + " " + lname;
            string userNUM = " - user #" + userID;
            label3.Text = username;
            label4.Text = userNUM;
        }

        private void AdminBar_Load(object sender, EventArgs e)
        {
            //Loads the default panel for admin
            AF.adminproduct();
        }
        public void back()
        {
            this.Close();
            MenuBar MB = new MenuBar();
            MB.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AF.adminproduct();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AF.adminorder();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AF.adminuser();
        }
    }
}