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

namespace CustomerManagementSystem.Admin
{
    class AdminFactory
    {
        public string MySqlConnectionString;
        Connection con = new Connection();
        public static Form currentChildForm;
        public static Panel panel;
        public int counter = 0;
        public static List<string> order = new List<string>();
        public static List<string> orderP = new List<string>();
        public static List<string> user = new List<string>();
        public static List<string> product = new List<string>();

        public string custid;
        public string fname;
        public string lname;
        public string email;
        public string dob;
        public string ph;
        public string street;
        public string suburb;
        public string city;
        public string postcode;
        public string country;
        public string pass;
        public string adminYN;

        public string prodid;
        public string prodname;
        public string prodcat;
        public string prodescription;
        public string imageurl;
        public string prodprice;
        public string prodstock;
       
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

        // admin product code 
        public void adminproduct()
        {
            AdminProduct AP = new AdminProduct() { TopLevel = false, TopMost = true };
            panel.Controls.Add(AP);
            OpenChildForm(AP);
        }
        public void ProdPop(string query)
        {
            // entering product information into form textboxes.
            int counter = 0;
            MySqlConnectionString = con.connectionString();
            MySqlConnection mySqlConnection = new MySqlConnection(MySqlConnectionString);
            MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
            mySqlConnection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            foreach (DataRow dr in dt.Rows)
            {
                counter += 1;
                prodid = dr["product_id"].ToString();
                prodname = dr["prod_name"].ToString();
                prodcat = dr["prod_category"].ToString();
                prodescription = dr["prod_description"].ToString();
                imageurl =   dr["image_url"].ToString();
                prodprice = dr["price"].ToString();
                prodstock = dr["stock_qty"].ToString();
            }
            mySqlConnection.Close();
        }
        // admin User code
        public void adminuser()
        {
            AdminUser AU = new AdminUser() { TopLevel = false, TopMost = true };
            panel.Controls.Add(AU);
            OpenChildForm(AU);
        }
        public void populate(string query)
        {
            // entering customer information into form textboxes.
            int counter = 0;
            MySqlConnectionString = con.connectionString();
            MySqlConnection mySqlConnection = new MySqlConnection(MySqlConnectionString);
            MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
            mySqlConnection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            foreach (DataRow dr in dt.Rows)
            {
                counter += 1;
                custid = dr["customer_id"].ToString();
                fname = dr["firstname"].ToString();
                lname = dr["lastname"].ToString();
                email = dr["email"].ToString();
                dob = dr["dob"].ToString();
                ph = dr["phonenumber"].ToString();
                street = dr["street"].ToString();
                suburb = dr["suburb"].ToString();
                city = dr["city"].ToString();
                postcode = dr["postcode"].ToString();
                country = dr["country"].ToString();
                pass = dr["password"].ToString();
                adminYN = dr["admin"].ToString();
            }
            mySqlConnection.Close();
        }
        public void RemUser(string query)
        {
            int result;
            MySqlConnectionString = con.connectionString();
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlCommand cmd;
            databaseConnection.Open();
            cmd = new MySqlCommand();
            cmd.Connection = databaseConnection;
            cmd.CommandText = query;
            result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                MessageBox.Show("The record has been removed", "Confirmation");
                databaseConnection.Close();
                return;
            }
        }

        // admin Order Code
        public void adminorder()
        {
            AdminOrder AO = new AdminOrder() { TopLevel = false, TopMost = true };
            panel.Controls.Add(AO);
            OpenChildForm(AO);
        }
        public void updateRec(string query)
        {
            int result;
            MySqlConnectionString = con.connectionString();
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlCommand cmd;
            databaseConnection.Open();
            cmd = new MySqlCommand();
            cmd.Connection = databaseConnection;
            cmd.CommandText = query;
            result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                MessageBox.Show("The record has been updated", "Confirmation");
                databaseConnection.Close();
                return;
            }
        }
        public void deleteRec(string query, string query2)
        {
            int result;
            MySqlConnectionString = con.connectionString();
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlCommand cmd;
            databaseConnection.Open();
            cmd = new MySqlCommand();
            cmd.Connection = databaseConnection;
            cmd.CommandText = query;
            result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                cmd.CommandText = query2;
                result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("The record has been removed", "Confirmation");
                    databaseConnection.Close();
                    return;
                }
            }
        }
        public void search(string query, int x)    // enable the function to search for an existing orders, users, products and update details
        {
            MySqlConnectionString = con.connectionString();
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            databaseConnection.Open();
            MySqlDataReader myReader = commandDatabase.ExecuteReader();
            while (myReader.Read()) //displaying details on page 
            {
                if (x == 1)
                {   //order list 
                    order.Add(myReader.GetString(0) + ", " + myReader.GetString(1) + ", " + myReader.GetString(2)+", "+ myReader.GetString(3) + ", " + myReader.GetString(5));
                }
                if (x == 2)
                {   //user list
                    user.Add(myReader.GetString(0) + ", " + myReader.GetString(1) + ", " + myReader.GetString(2) + ", " + myReader.GetString(3));
                }
                if (x == 3)
                {   //product list
                    product.Add(myReader.GetString(0) + ", " + myReader.GetString(1) + ", " + myReader.GetString(2) + ", " + myReader.GetString(5) + ", " + myReader.GetString(6));
                }
                if (x == 4)
                {   //Order item list
                    orderP.Add(myReader.GetString(0) + ", " + myReader.GetString(1) + ", " + myReader.GetString(2) + ", " + myReader.GetString(3) + ", " + myReader.GetString(4));
                }
            }
            databaseConnection.Close();
        }
        public void orderRes(Form UP, string query1,string query2)
        {
            int x = 50;
            int y = 30;
            int counter = 0;
            string orderitemid;
            string desc;
            string price;
            string prodid;
            string qty;
            string orderN;
            string discAm;
            string total;
            string created;
            string active;

            MySqlConnectionString = con.connectionString();
            MySqlConnection mySqlConnection = new MySqlConnection(MySqlConnectionString);
            MySqlCommand cmd = new MySqlCommand(query1, mySqlConnection);
            DataTable dt = new DataTable();
            try
            {
                mySqlConnection.Open();
                dt.Load(cmd.ExecuteReader());
                foreach (DataRow dr in dt.Rows)
                {
                    counter += 1;
                    orderN = dr["order_id"].ToString();
                    discAm = dr["discount_amount"].ToString();
                    total = dr["total"].ToString();
                    created = dr["created_date"].ToString();
                    active = dr["active"].ToString();
                    var ID = new Label()
                    {
                        Name = "id",
                        Text = "Order# : " + orderN,
                        Location = new Point(y, x),
                        Height = 20,
                        Width = 75,
                        BackColor = Color.RoyalBlue,
                        ForeColor = Color.White,
                    };
                    UP.Controls.Add(ID);
                    var create = new Label()
                    {
                        Name = "createddate",
                        Text = "Order Date: " + created,
                        Location = new Point(y + 75, x),
                        Height = 20,
                        Width = 350,
                        BackColor = Color.RoyalBlue,
                        ForeColor = Color.White,
                    };
                    UP.Controls.Add(create);
                    var discount = new Label()
                    {
                        Name = "discount",
                        Text = "Discount %" + discAm,
                        Location = new Point(y + 425, x),
                        Height = 20,
                        Width = 200,
                        BackColor = Color.RoyalBlue,
                        ForeColor = Color.White,
                    };
                    UP.Controls.Add(discount);
                    var totalprice = new Label()
                    {
                        Name = "total",
                        Text = "Total $" + total,
                        Location = new Point(y + 625, x),
                        Height = 20,
                        Width = 100,
                        BackColor = Color.RoyalBlue,
                        ForeColor = Color.White,
                    };
                    UP.Controls.Add(totalprice);
                    x += 22;
                    y = 30;
                   
                    MySqlCommand CMD = new MySqlCommand(query2, mySqlConnection);
                    DataTable DT = new DataTable();
                    try
                    {
                        DT.Load(CMD.ExecuteReader());
                        foreach (DataRow row in DT.Rows)
                        {
                            orderitemid = row["order_item_id"].ToString();
                            desc = row["description"].ToString();
                            price = row["price"].ToString();
                            prodid = row["product_id"].ToString();
                            qty = row["quantity"].ToString();

                            var itemord = new Label()
                            {
                                Name = "orderitem",
                                Text = orderitemid,
                                Location = new Point(y + 0, x),
                                Height = 20,
                                Width = 50,
                                Visible = false,
                            };
                            UP.Controls.Add(itemord);
                            var productid = new Label()
                            {
                                Name = "productid",
                                Text = "SKU# : " + prodid,
                                Location = new Point(y + 10, x),
                                Height = 20,
                                Width = 65,
                            };
                            UP.Controls.Add(productid);
                            var description = new Label()
                            {
                                Name = "description",
                                Text = desc,
                                Location = new Point(y + 80, x),
                                Height = 20,
                                Width = 350,
                            };
                            UP.Controls.Add(description);
                            var prodprice = new Label()
                            {
                                Name = "price",
                                Text = "price $" + price,
                                Location = new Point(y + 435, x),
                                Height = 20,
                                Width = 100,
                            };
                            UP.Controls.Add(prodprice);
                            var prodqty = new Label()
                            {
                                Name = "quantity",
                                Text = "QTY: " + qty,
                                Location = new Point(y + 540, x),
                                Height = 20,
                                Width = 75,
                            };
                            UP.Controls.Add(prodqty);
                            x += 22;
                            y = 30;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Query error " + ex.Message);
                    }
                    x += 20;
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
            return;
        }
    }
}
