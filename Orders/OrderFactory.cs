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
using CustomerManagementSystem.UserValidation;

namespace CustomerManagementSystem.Orders
{
    class OrderFactory
    {
        public string MySqlConnectionString;
        public int x = 0;
        public string userId
        {
            get => userId;
        }
        Connection con = new Connection();
        public void UserPurchase(string street, string suburb, string city, string postcode, string Phone, string country, string DOB)
        {
            
            MessageBox.Show(userId);
            int result;
            string query = "update customer_management.customer set dob = '" + DOB + "',phonenumber = '" + Phone +
                "',street = '" + street + "',suburb = '" + suburb + "',city = '" + city + "', country = '" + country + "' where customer_id = '" + userId + "'";
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
                    MessageBox.Show("Your account has been sucessfully created!", "Welcome!");
                    databaseConnection.Close();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("Query error " + a.Message);
            }
        }
        public void Orderlist(Form PP)
        {
            // displays all items in the cart on the cart page, including name, price and quantitiy
            string query = "select order_item_id, product_id, description, price, quantity from customer_management.order_item";
            int counter = 0;
            int x = 100;
            int y = 0;
            string id;
            string prodid;
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
                    prodid = dr["product_id"].ToString();
                    description = dr["description"].ToString();
                    price = dr["price"].ToString();
                    qty = dr["quantity"].ToString();
                    Unittotal = Convert.ToDouble(price) * Convert.ToInt32(qty);
                    var ID = new Label()
                    {
                        Name = "id" + id,
                        Text = prodid,
                        Location = new Point(y + 45, x),
                        Height = 20,
                        Width = 20,
                    };
                    PP.Controls.Add(ID);
                    var title = new Label()
                    {
                        Name = "title" + id,
                        Text = description,
                        Location = new Point(y + 70, x),
                        Height = 20,
                        Width = 350,
                    };
                    PP.Controls.Add(title);
                    var qtylabel = new Label()
                    {
                        Name = id,
                        Text = qty,
                        Location = new Point(y + 520, x),
                        Height = 20,
                        Width = 50,
                    };
                    PP.Controls.Add(qtylabel);
                    var UnitTlabel = new Label()
                    {
                        Name = "unittotal",
                        Text = Convert.ToString(Unittotal),
                        Location = new Point(y + 680, x),
                        Height = 20,
                        Width = 50
                    };
                    PP.Controls.Add(UnitTlabel);
                    y = 0;
                    x += 20;
                    total += Unittotal;
                }
                if (counter == 0)
                {
                    return;
                }
                mySqlConnection.Close();
                string final = Convert.ToString(total);
                var Total = new Label()
                {
                    Name = "total",
                    Text = final,
                    Location = new Point(548, 235),
                    Height = 20,
                    Width = 50,
                };
                PP.Controls.Add(Total);
            }
            catch (Exception a)
            {
                MessageBox.Show("Query error " + a.Message);
            }
        }
    }
}
