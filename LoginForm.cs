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

namespace CustomerManagementSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        // Login in button - Passes user entered email & password to UserFactory for verification
        private void button2_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string password = maskedTextBox1.Text;
            this.Hide();
            UserFactory user = new UserFactory();
            user.UserLogin(email, password);
        }

        // Sign up button - takes User to the sign up page where they can create an account
        private void button1_Click(object sender, EventArgs e)
        {
            Signup SU = new Signup();
            SU.StartPosition = FormStartPosition.WindowsDefaultLocation;
            SU.Show();
            this.Hide();
        }
    }
}