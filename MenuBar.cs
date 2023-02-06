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
        public MenuBar()
        {
            InitializeComponent();
            this.MouseClick += mouseClick;
        }
        private void MenuBar_Load(object sender, EventArgs e)
        {
            //Loads the default panel (Homepage) displays all products
            PF.NewPanel(this);
            PF.HomePage();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //refreshes the page/ returns user to homepage
            PF.CloseChildForm(this);
            PF.HomePage();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //calls the user profile page into the main panel
            PF.CloseChildForm(this);
            PF.UserProfile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //loads the shopping cart into the main panel
            PF.CloseChildForm(this);
            PF.shopCart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PF.catButtons(this);
        }

        private void mouseClick(object sender, MouseEventArgs e)
        {
            PF.buttonclear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string textbox = textBox1.Text;
            PF.CloseChildForm(this);
            PF.HomePage();
            PF.prodSearch(this,textbox);
        }
    }
}