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

namespace CustomerManagementSystem.Payment
{
    class PaymentFactory
    {
        public string MySqlConnectionString;
        Connection con = new Connection();

        public void CartContents(string desc, double price, string ID, int qty)
        {   //adds items to the order_item table database 
            int result;
            string query = "Insert into order_item (description, price, product_id, quantity) values ('" + desc + "','" + price + "','" + ID + "','" + qty + "')";
            MySqlConnectionString = con.connectionString();
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlCommand commandDatabase;
            try
            {
                databaseConnection.Open();
                commandDatabase = new MySqlCommand();
                commandDatabase.Connection = databaseConnection;
                commandDatabase.CommandText = query;
                result = commandDatabase.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("This item has successfuly been added to your cart!", "Great");
                    databaseConnection.Close();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("Query error " + a.Message);
            }
        }

        void qty_changed(object sender, EventArgs e)
        {   // refreshes the cart either removing items where qty == 0, or updating price based on new quantities

            int qty = Convert.ToInt32(((System.Windows.Forms.NumericUpDown)sender).Value);
            string id = Convert.ToString(((System.Windows.Forms.NumericUpDown)sender).Name);
            int result;
            string query = null;
            if (qty == 0)
            {
                query = "Delete from customer_management.order_item where order_item_id = '"+id+"';";
            }
            else if (qty > 0)
            {
                query = "update order_item set quantity = '" + qty + "' where order_item_id = '" + id + "';";
            }
            MySqlConnectionString = con.connectionString();
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlCommand commandDatabase;
            try
            {
                databaseConnection.Open();
                commandDatabase = new MySqlCommand();
                commandDatabase.Connection = databaseConnection;
                commandDatabase.CommandText = query;
                result = commandDatabase.ExecuteNonQuery();
                if (result > 0)
                {
                    databaseConnection.Close();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("Query error " + a.Message);
            }
        }

        public void CartList(Form CP)
        {   // displays all items in the cart on the cart page, including name, price and quantitiy
            string query = "select order_item_id, description, price, quantity from customer_management.order_item";
            int counter = 0;
            int x = 100;
            int y = 0;
            string id;
            string description;
            string price;
            string qty;
            double total = 0;
            double Unittotal;
            
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
                    id = dr["order_item_id"].ToString();
                    description = dr["description"].ToString();
                    price = dr["price"].ToString();
                    qty = dr["quantity"].ToString();
                    Unittotal = Convert.ToDouble(price) * Convert.ToInt32(qty);

                    var ID = new Label()
                    {
                        Name = "id" + id,
                        Text = id,
                        Location = new Point(y+45, x),
                        Height = 20,
                        Width = 20,
                    };
                    CP.Controls.Add(ID);
                    var title = new Label()
                    {
                        Name = "title" + id,
                        Text = description,
                        Location = new Point(y+70, x),
                        Height = 20,
                        Width = 350,
                    };
                    CP.Controls.Add(title);
                    var Pricelabel = new Label()
                    {
                        Name = "desc" + id,
                        Text = price,
                        Location = new Point(y + 520, x),
                        Height = 20,
                        Width = 50
                    };
                    CP.Controls.Add(Pricelabel);
                    var UnitTlabel = new Label()
                    {
                        Name = "unittotal",
                        Text = Convert.ToString(Unittotal),
                        Location = new Point(y + 580, x),
                        Height = 20,
                        Width = 50
                    };
                    CP.Controls.Add(UnitTlabel);
                    var qtylabel = new NumericUpDown()
                    {
                        Name = id,
                        Text = qty,
                        Location = new Point(y+670, x),
                        Height = 20,
                        Width = 50,
                    };
                    qtylabel.ValueChanged += new EventHandler(qty_changed);
                    CP.Controls.Add(qtylabel);
                    // register the event handler for this picture box
                    y = 0;
                    x += 20;
                    total += Unittotal;
                }
                if (counter == 0)
                {
                    return;
                }
                mySqlConnection.Close();
                var separator = new Label()
                {
                    BackColor = System.Drawing.Color.Red,
                    Location = new Point(20,x+10),
                    Width = 760,
                    Height = 5,
                };
                CP.Controls.Add(separator);
                string final = Convert.ToString(total);
                var Total = new Label()
                {
                    Name = "total",
                    Text = final,
                    Location = new Point(y + 580, x + 20),
                    Height = 20,
                    Width = 50,
                };
                CP.Controls.Add(Total);
            }
            catch (Exception a)
            {
                MessageBox.Show("Query error " + a.Message);
            }
        }
    }
}
