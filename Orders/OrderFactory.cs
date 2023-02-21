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
using CustomerManagementSystem.Payment;

namespace CustomerManagementSystem.Orders
{
    class OrderFactory
    {
        public string MySqlConnectionString;
        public int x = 0;
        public static double total;
        public static int discountid;
        public static string orderid;

        public static string disPer;
        public static double final;
        public static int deliveryFee;
        public static string Discount
        {
            get { return disPer; }
            set { Discount = disPer; }
        }
        public static int Delivery
        {
            get { return deliveryFee; }
            set { Delivery = deliveryFee; }
        }
        public static double TotalBill
        {
            get { return final; }
            set { TotalBill = final; }
        }
        Connection con = new Connection();

        public void UserPurchase(Form PP,string street, string suburb, string city, string postcode, string Phone, string country, DateTime DOB,string dob,string paymentMeth,string cardName, string cardNum,string cardExp,string cardCVS,int delivery)
        {  
            //updates user database with the users address details and payment details etc
            string userId = UserFactory.id;
            int result;
            var date = DateTime.Today;
            var age = date.Year - DOB.Year;
            int day = (int)DateTime.Now.DayOfWeek;
            deliveryFee = delivery;
            int x = 0;
            double discAmount = 0;

            // calculate users age
            if (DOB.Date > date.AddYears(-age)) 
            {
                age--;
            }
            // Logic for calculating the discount to be applied on the order
            if (age >= 60)
            {
                x+= 10;
            }
            if (day == 0 || day == 6)
            {
                x += 2;
            }
            if (city == "auckland" || city == "Auckland" || city == "wellington" || city == "Wellington")
            {
                x += 1;
            }
            //  Enter user details into the database
            string query = "update customer_management.customer set dob = '" + dob + "',phonenumber = '" + Phone +
                "',street = '" + street + "',suburb = '" + suburb + "',city = '" + city + "',postcode = '" +postcode+"',country = '" + country + "' where customer_id = '" + userId + "'";
            MySqlConnectionString = con.connectionString();
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlCommand commandDatabase;
            if (street == null || suburb == null || Phone == null || city == null || postcode == null || country == null)
            {
                MessageBox.Show("Please check that all information is entered correctly", "Error");
            }
            else
            {
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
                    databaseConnection.Close();
                }
            }
            // Display discount amount and final to pay plus delivery if applicable
            string query1 = "select discount_id ,discount_amount from customer_management.discount where discount_amount = '" + x + "'";
            try
            {
                con.Open();
                MySqlDataReader dr;
                dr = con.ExecuteReader(query1);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        disPer = dr["discount_amount"].ToString();
                        discountid = Convert.ToInt32(dr["discount_id"]);
                        if (x > 0)
                        {
                            MessageBox.Show("Congratulations, a discount of " + disPer + "% has been added to your order", "Discount");
                            discAmount = (total * Convert.ToInt16(disPer)) / 100;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Connection Error!", "Database Information");
                }
            }
            catch
            {
                MessageBox.Show("Connection Error!", "Database Information");
            }
            // adding delivery costs, discount, tax, and totals to order 
            final = (total + delivery) - discAmount;
            con.Close();
        }
        public void OrderEnter()
        {
            // finalises the order process by entering details from the order into appropriate DB tables
            int result;
            int counter = 0;
            string orderitemid;
            string userId = UserFactory.id;
            var date = DateTime.Now;
            string Cdate = date.ToString("yyyy-MM-dd");
            int active = 1;
            string query1 = "Insert into orders (discount_id, total, created_date, active) values ('" + discountid + "','" + final + "','" + Cdate + "','" + active + "')";
            string query2 = "select max(order_id) from customer_management.orders";

            MySqlConnectionString = con.connectionString();
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlCommand cmd;
            DataTable dt = new DataTable();
            try
            {
                databaseConnection.Open();
                cmd = new MySqlCommand();
                cmd.Connection = databaseConnection;
                cmd.CommandText = query1;
                result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    //databaseConnection.Close();
                    con.Open();
                    MySqlDataReader dr;
                    dr = con.ExecuteReader(query2);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            orderid = dr["max(order_id)"].ToString();
                        }
                    }
                }
                string query = "select order_item_id from customer_management.order_item;";
                cmd.CommandText = query;
                dt.Load(cmd.ExecuteReader());
                foreach (DataRow DR in dt.Rows)
                {
                    result = 0;
                    counter += 1;
                    orderitemid = DR["order_item_id"].ToString();

                    string query3 = "Insert into order_items (order_id, order_item_id) values ('" + orderid + "','" + orderitemid + "');";
                    cmd.CommandText = query3;
                    result = cmd.ExecuteNonQuery();
                }
                if (counter == 0)
                {
                    con.Close();
                }
                string query4 = "Insert into customer_orders (customer_id, order_id) values ('" + userId + "','" + orderid + "')";
                cmd = new MySqlCommand();
                cmd.Connection = databaseConnection;
                cmd.CommandText = query4;
                result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Your order has been sucessfully processed!", "Thank you!");
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
            
            int x = 100;
            int y = 0;
            double Unittotal = 0;
            
            List<string> desc = PaymentFactory.descList;
            List<double> price = PaymentFactory.priceList;
            List<string> id = PaymentFactory.idList;
            List<int> qty = PaymentFactory.qtyList;
            
            for (int i = 0; i < desc.Count; i++)
            {
                var ID = new Label()
                {
                    Name = "id" + id,
                    Text = id[i],
                    Location = new Point(y + 45, x),
                    Height = 20,
                    Width = 20,
                };
                PP.Controls.Add(ID);
                var title = new Label()
                {
                    Name = "title" + id,
                    Text = desc[i],
                    Location = new Point(y + 70, x),
                    Height = 20,
                    Width = 350,
                };
                PP.Controls.Add(title);
                var qtylabel = new Label()
                {
                    Name = id[i],
                    Text = qty[i].ToString(),
                    Location = new Point(y + 520, x),
                    Height = 20,
                    Width = 50,
                };
                PP.Controls.Add(qtylabel);
                Unittotal = price[i] * Convert.ToInt32(qty[i]);
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



            /*
            string id;
            string prodid;
            string description;
            string price;
            string qty;
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
            }
            catch (Exception a)
            {
                MessageBox.Show("Query error " + a.Message);
            }
            */
        }
    }
}
