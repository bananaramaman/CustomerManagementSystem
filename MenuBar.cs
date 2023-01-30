using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CustomerManagementSystem
{
    public partial class MenuBar : Form
    {
        public MenuBar()
        {
            InitializeComponent();
            this.MinimumSize = new Size(816, 639);
            this.MaximumSize = new Size(816, 639);
        }
        public string PassCustId;
        private Form currentChildForm;
        private object iconCurrentChildForm;
        private void MenuBar_Load(object sender, EventArgs e)
        {
            HomePage HM = new HomePage() { TopLevel = false, TopMost = true };
            panel1.Controls.Add(HM);
            OpenChildForm(HM);
        }
        public void OpenChildForm(Form childform)          // I took this part from Tati's Code in Admin Forms.
        {
            if (iconCurrentChildForm != null)
            {
                //open a specific fomr
                currentChildForm.Close();
            }
            currentChildForm = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            childform.BringToFront();
            childform.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }
    }
}