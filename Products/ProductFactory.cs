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

namespace CustomerManagementSystem.Products
{
    class ProductFactory
    {
        public string MySqlConnectionString;
        Connection con = new Connection();
        public Form currentChildForm;
        public object iconCurrentChildForm;
        public Panel panel;
        public Panel Catbox;
        public Button button;
        public int counter = 0;

        public void OpenChildForm(Form childform)
        {
            if (iconCurrentChildForm != null)
            {
                //open a specific form
                currentChildForm.Dispose();
                currentChildForm = null;
            }
            currentChildForm = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            childform.Show();
        }

        public void CloseChildForm(Form childform)
        {
            currentChildForm.Close();
        }

        public void NewPanel(Form MB)
        {
            panel = new Panel()
            {
                AutoSize = true,
                AutoScroll = true, Visible = true,
                Location = new System.Drawing.Point(0, 125),
                Name = "panel",
                Size = new System.Drawing.Size(800, 475),
                BackgroundImage = Properties.Resources.LoginBackground,
                BackgroundImageLayout = ImageLayout.Stretch,
            };
            MB.Controls.Add(panel);
            return;
        }

        public void shopCart()
        {
            CartPage CP = new CartPage() { TopLevel = false, TopMost = true };
            panel.Controls.Add(CP);
            OpenChildForm(CP);
        }

        public void UserProfile()
        {
            UserProfile UP = new UserProfile() { TopLevel = false, TopMost = true };
            panel.Controls.Add(UP);
            OpenChildForm(UP);
        }

        public void HomePage()
        {
            HomePage HP = new HomePage() { TopLevel = false, TopMost = true };
            panel.Controls.Add(HP);
            OpenChildForm(HP);
        }

        public void catButtons(Form MB)
        {
            //generate category options
            string query = "Select prod_category from customer_management.products group by prod_category";
            string cat;
            int x = 0;
            if (Catbox != null)
            {
                return;
            }
            else
            {
                Catbox = new Panel()
                {
                    AutoSize = true,
                    Visible = true,
                    Location = new System.Drawing.Point(601, 98),
                    Name = "panel1",
                    Width = 186,
                };
                MB.Controls.Add(Catbox);
                Catbox.BringToFront();

                MySqlConnectionString = con.connectionString();
                MySqlConnection mySqlConnection = new MySqlConnection(MySqlConnectionString);
                MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
                DataTable dt = new DataTable();
                mySqlConnection.Open();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    counter += 1;
                    cat = dr["prod_category"].ToString();
                    button = new Button()
                    {
                        Name = "button" + counter,
                        Text = cat,
                        Font = new Font("Nirmala UI", 16, FontStyle.Bold),
                        BackColor = Color.Firebrick,
                        Location = new Point(0, x),
                        Width = 186,
                        Height = 40,
                    };
                    Catbox.Controls.Add(button);
                    button.BringToFront();
                    x += 40;
                }
            }
        }
        public void buttonclear()
        {
            if (Catbox != null)
            {
                Catbox.Dispose();
                Catbox = null;
            }
            else
                return;
        }
        
        public void prodSearch(Form HP, string textbox)
        {
            string query = "select * from customer_management.products where prod_name like '%'" + textbox + "'%'";
            ProductDisp(HP, query);
        }
        public void prodall(Form HP)
        {
            string query = "select product_id, prod_name, prod_category, prod_description, image_url, price, stock_qty from customer_management.products";
            ProductDisp( HP, query );
        }

        public void ProductDisp(Form HP)
        {

            // populating form with products
            int counter = 0;
            int x = 25;
            int y = 60;
            string id;
            string name;
            string category;
            string description;
            string url;
            string price;
            string stock;
            
            MySqlConnectionString = con.connectionString();
            MySqlConnection mySqlConnection = new MySqlConnection(MySqlConnectionString);
            MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
            DataTable dt = new DataTable();
            mySqlConnection.Open();
            dt.Load(cmd.ExecuteReader());

            foreach (DataRow dr in dt.Rows)
            {
                counter += 1;
                id = dr["product_id"].ToString();
                name = dr["prod_name"].ToString();
                category = dr["prod_category"].ToString();
                description = dr["prod_description"].ToString();
                url = dr["image_url"].ToString();
                price = dr["price"].ToString();
                stock = dr["stock_qty"].ToString();
               
                var title = new Label()
                {
                    Name = "title" + counter,
                    Text = name,
                    Location = new Point(y, x),
                    Width = 125,
                    Height = 30,
                };
                HP.Controls.Add(title);
                var label = new Label()
                {
                    Name = "desc" + counter,
                    Text = description,
                    Location = new Point(y, x + 155),
                    Width = 125,
                    Height = 100,
                };
                HP.Controls.Add(label);
                var picture = new PictureBox
                {
                    Name = "pictureBox" + counter,
                    Size = new Size(125, 125),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Location = new Point(y, x + 30),
                    ImageLocation = url,
                    WaitOnLoad = true,
                };
                HP.Controls.Add(picture);
                y += 185;
                if (y > 615)
                {
                    y = 60;
                    x += 280;
                }
            }
            mySqlConnection.Close();
        }
    }
}
