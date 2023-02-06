using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Forms;
using CustomerManagementSystem.Products;

namespace CustomerManagementSystem
{
    public partial class HomePage : Form
    {
        
        public HomePage()
        {
            InitializeComponent();
        }
        public void HomePage_Load(object sender, EventArgs e)
        {
            ProductFactory PF = new ProductFactory();
            PF.ProductDisp(this);
        }
    }
}