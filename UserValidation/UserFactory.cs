using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using loginsec;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;
using System.Drawing;

namespace CustomerManagementSystem.UserValidation
{
    class UserFactory
    {
        Signup SU = new Signup();
        MenuBar MB = new MenuBar();
        Connection con = new Connection();
        public string MySqlConnectionString;
        public static string discid;
        public static string total;
        public static string created;
        public static string active;
        public static string id;
        public static string fname;
        public static string lname;
        public static string orderN;
        public static string userId 
        { 
            get { return id; }
            set { userId = id; }
        }
        public static string Fname
        {
            get { return fname; }
            set { userId = fname; }
        }
        public static string Lname
        {
            get { return lname; }
            set { userId = lname; }
        }

        // User login method
        public void UserLogin(string email, string password)
        {
            //LF.Show();
            try
            {
                if (email != "" && password != "")
                {
                    con.Open();
                    string query = "select * from customer_management.customer WHERE email ='" + email + "' AND password ='" + password + "'";
                    MySqlDataReader myReader;
                    myReader = con.ExecuteReader(query);
                    if (myReader.HasRows)
                    {
                        while (myReader.Read())
                        {
                            fname = myReader.GetString(1);
                            lname = myReader.GetString(2);
                            string em = myReader.GetString(3);
                            string pass = myReader.GetString(11);
                            id = myReader.GetString(0);
                        }
                        con.Close();
                        MB.StartPosition = FormStartPosition.WindowsDefaultLocation;
                        MB.ShowDialog();
                    }
                    else{
                        MessageBox.Show("Incorrect Username or Password!", "Login Page");
                        con.Close();
                    }
                }
                else{
                    MessageBox.Show("Username or Password is empty!", "Login Page"); con.Close();
                }
            }
            catch{
                MessageBox.Show("Connection Error!", "Database Information");}
        }

        // New user method
        public void NewUser(string fName, string lName, string email, string password, string password2)
        {
            int result;

            string query = "Insert into customer (firstname, lastname, email, password) values ('" + fName + "','" + lName + "','" + email + "','" + password + "')";
            MySqlConnectionString = con.connectionString();
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlCommand commandDatabase;

            if (fName == "" || lName == "" || password == "" || email == "" || password != password2)
            {
                MessageBox.Show("Please check that all information is entered correctly, and that both passwords match", "Sign up error");
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
                        MessageBox.Show("Your account has been sucessfully created!", "Welcome!");
                        databaseConnection.Close();
                        SU.Close();
                    }
                }
                catch (Exception a)
                {
                    MessageBox.Show("Query error " + a.Message);
                }
            }
        }

        // User profile page. Display all current and previous orders
        public void UserProfile(Form UP)
        {
            int x = 100;
            int y = 400;
            int counter = 0;
            string orderitemid;
            string desc;
            string price;
            string prodid;
            string qty;

            string query1 = "SELECT orders.order_id, discount_id, total, created_date, completed_date, active " +
                "FROM orders, customer_orders, order_items, order_item where customer_orders.order_id = orders.order_id and order_item.order_item_id = order_items.order_item_id " +
                   "and order_items.order_id = orders.order_id and customer_orders.customer_id = '"+userId+"'";
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
                    discid = dr["discount_id"].ToString();
                    total = dr["total"].ToString();
                    created = dr["created_date"].ToString();
                    active = dr["active"].ToString();
                    var ID = new Label()
                    {
                        Name = "id",
                        Text = "order id = " + id,
                        Location = new Point(y, x),
                        Height = 20,
                        Width = 20,
                    };
                    UP.Controls.Add(ID);
                    var discount = new Label()
                    {
                        Name = "discount",
                        Text = "discount ID = " + discid,
                        Location = new Point(y + 50, x),
                        Height = 20,
                        Width = 350,
                    };
                    UP.Controls.Add(discount);
                    var create = new Label()
                    {
                        Name = "createddate",
                        Text = "order date: " + created,
                        Location = new Point(y + 100, x),
                        Height = 20,
                        Width = 50,
                    };
                    UP.Controls.Add(create);
                    var totalprice = new Label()
                    {
                        Name = "total",
                        Text = "total $" + total,
                        Location = new Point(y + 100, x),
                        Height = 20,
                        Width = 50,
                    };
                    UP.Controls.Add(totalprice);
                    y = 0;
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

/*
 string query2 = "Select order_item.order_item_id, order_item.description, order_item.price, order_item.product_id, order_item.quantity FROM orders, customer_orders, order_items, order_item " +
                   "where customer_orders.order_id = orders.order_id and order_item.order_item_id = order_items.order_item_id and " +
                   "order_items.order_id = orders.order_id and customer_orders.customer_id = '" + userId + "' and orders.order_id = '" + orderN + "'; ";
                    con.Open();
                    cmd.CommandText = query2;
                    dt.Load(cmd.ExecuteReader());
                    foreach (DataRow row in dt.Rows)
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
                            Width = 50
                        };
                        var productid = new Label()
                        {
                            Name = "productid",
                            Text = prodid,
                            Location = new Point(y + 50, x),
                            Height = 20,
                            Width = 50
                        };
                        var description = new Label()
                        {
                            Name = "description",
                            Text = desc,
                            Location = new Point(y + 100, x),
                            Height = 20,
                            Width = 50
                        };
                        var prodprice = new Label()
                        {
                            Name = "price",
                            Text = "$"+price,
                            Location = new Point(y + 250, x),
                            Height = 20,
                            Width = 50
                        };
                        var prodqty = new Label()
                        {
                            Name = "quantity",
                            Text = qty,
                            Location = new Point(y + 300, x),
                            Height = 20,
                            Width = 50
                        };
                        UP.Controls.Add(itemord);
                        UP.Controls.Add(productid);
                        UP.Controls.Add(description);
                        UP.Controls.Add(prodprice);
                        UP.Controls.Add(prodqty);
                        x += 20;
                    }
*/