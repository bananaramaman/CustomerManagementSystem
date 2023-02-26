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
        public static Form currentChildForm;
        public static Panel panel;
        public Panel Catbox;
        public Button button;
        public int counter = 0;

        public void OpenChildForm(Form childform)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            childform.BringToFront();
            childform.Show();
        }
        public void NewPanel(Form MB)
        {
            if (panel != null)
            {
                panel.Dispose();
            }
            panel = new Panel()
            {
                AutoSize = true,
                AutoScroll = true,
                Visible = true,
                Location = new System.Drawing.Point(0, 125),
                Name = "panel",
                Size = new System.Drawing.Size(800, 475),
                BackgroundImage = Properties.Resources.LoginBackground,
                BackgroundImageLayout = ImageLayout.Stretch,
            };
            MB.Controls.Add(panel);
        }
        //Logout/quit app
        public void quit()
        {
            System.Windows.Forms.Application.Exit();
        }
        // shows Admin control window
        public void admin()
        {
            AdminBar AB = new AdminBar();
            AB.Show();
        }
        // displays the user shopping cart
        public void shopCart()
        {
            CartPage CP = new CartPage() { TopLevel = false, TopMost = true };
            panel.Controls.Add(CP);
            OpenChildForm(CP);
        }
        // displays the user shopping cart
        public void Payment()
        {
            PaymentPage PP = new PaymentPage() { TopLevel = false, TopMost = true };
            panel.Controls.Add(PP);
            OpenChildForm(PP);
        }
        // displays the user profile page
        public void UserProfile()
        {
            UserProfile UP = new UserProfile() { TopLevel = false, TopMost = true };
            panel.Controls.Add(UP);
            OpenChildForm(UP);
        }
        // picture click event handler. Displays product page
        void picture_Click(object sender, EventArgs e)
        {
            string name = (((System.Windows.Forms.PictureBox)sender).Name);
            ProductPage(name);
        }
        //Displays product page
        public void ProductPage(string name)
        {
            ProductView PV = new ProductView() { TopLevel = false, TopMost = true };
            string query = "select product_id, prod_name, prod_category, prod_description, image_url, price, stock_qty from customer_management.products where prod_name = '" + name + "';";
            try
            {
                con.Open();
                MySqlDataReader dr;
                dr = con.ExecuteReader(query);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PV.Title1.Text = dr["prod_name"].ToString();
                        PV.cat1.Text = dr["prod_category"].ToString();
                        PV.desc.Text = dr["prod_description"].ToString();
                        PV.pictureBox1.ImageLocation = dr["image_url"].ToString();
                        PV.Price1.Text = dr["price"].ToString();
                        PV.Stock1.Text = dr["stock_qty"].ToString();
                        PV.id.Text = dr["product_id"].ToString();
                        
                        panel.Controls.Add(PV);
                        OpenChildForm(PV);
                    }
                }
                else{
                    MessageBox.Show("Connection Error!", "Database Information");}
            }
            catch{
                MessageBox.Show("Connection Error!", "Database Information");}
        }

        // Displays product home page and determines product views (search, categories etc)
        public void HomePage(int x,string y)
        {
            HomePage HP = new HomePage() { TopLevel = false, TopMost = true };
            
            if (x > 0)
            {
                string query = "select product_id, prod_name, prod_category, prod_description, image_url, price, stock_qty from customer_management.products where prod_name like '" + y + "';";
                ProductDisp(HP, query);
            }
            else
            {
                string query = "select product_id, prod_name, prod_category, prod_description, image_url, price, stock_qty from customer_management.products";
                ProductDisp(HP, query);
            }
        }
        // Displays drop down Category menu 
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
        public void ProductDisp(Form HP, string query)
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
            try
            {
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
                        Name = name,
                        Size = new Size(125, 125),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Location = new Point(y, x + 30),
                        ImageLocation = url,
                        WaitOnLoad = true,
                    };
                    // register the event handler for this picture box
                    picture.Click += new EventHandler(picture_Click);
                    HP.Controls.Add(picture);
                    y += 185;
                    if (y > 615)
                    {
                        y = 60;
                        x += 280;
                    }
                }
                if (counter == 0)
                {
                    MessageBox.Show("No results found. Try again", "Search error");
                    return;
                }
                mySqlConnection.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show("Query error " + a.Message);
            }
            OpenChildForm(HP);
            panel.Controls.Add(HP);
            
        }
    }
}
