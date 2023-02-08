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
using CustomerManagementSystem.Products;

namespace CustomerManagementSystem
{
    public partial class MenuBar : Form
    {
        ProductFactory PF = new ProductFactory();
        public int x = 0;
        public string y;

        public MenuBar()
        {
            InitializeComponent();
            PF.NewPanel(this);
            this.MouseClick += mouseClick;
        }

        private void MenuBar_Load(object sender, EventArgs e)
        {
            //Loads the default panel (Homepage) displays all products
            PF.HomePage(x, y);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //refreshes the page/ returns user to homepage
            x = 0;
            PF.HomePage(x, y);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //calls the user profile page into the main panel
            PF.UserProfile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //loads the shopping cart into the main panel
            PF.shopCart();
        }

        private void button1_Click(object sender, EventArgs e)
        {   //displays category options
            PF.catButtons(this);
        }
        private void mouseClick(object sender, MouseEventArgs e)
        {   //collapses the category drop menu on mouse click
            PF.buttonclear();
        }

        private void button4_Click(object sender, EventArgs e)
        {   //product search function
            string y = textBox1.Text;
            string text = "%" + y + "%";
            x = 1;
            textBox1.Clear();
            PF.HomePage(x, text);
        }
    }
}