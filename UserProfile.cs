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
using CustomerManagementSystem.Products;
using System.Media;


namespace CustomerManagementSystem
{
    public partial class UserProfile : Form
    {
        
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
            string userID = UserFactory.userId;
            string username = fname +" "+ lname;
            string userNUM = " - user #" + userID;
            label1.Text = username;
            label3.Text = userNUM;
            UserFactory UF = new UserFactory();
            UF.UserProfile(this);
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
            ProductFactory PF = new ProductFactory();
            PF.HomePage(x, y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer(@"C:\Users\DJBur\Documents\CustomerManagement\CustomerManagementSystem\Audio\Pain.wav");
            simpleSound.Play();
        }
    }
}
