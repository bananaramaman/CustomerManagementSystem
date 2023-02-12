using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using loginsec;
using System.Net;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using CustomerManagementSystem.UserValidation;
using CustomerManagementSystem.Products;

namespace CustomerManagementSystem
{
    public partial class UserProfile : Form
    {
        ProductFactory PF = new ProductFactory();
        public int x = 0;
        public string y;

        public UserProfile()
        {
            InitializeComponent();
        }

        private void UserProfile_Load(object sender, EventArgs e)
        {
            string fname = UserFactory.Fname;
            string lname = UserFactory.Lname;
            string username = fname +" "+ lname;
            label1.Text = username;
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
            PF.HomePage(x, y);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            UserFactory UF = new UserFactory();
            UF.UserProfile(this);
        }
    }
}
