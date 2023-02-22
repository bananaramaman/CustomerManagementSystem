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
                        MenuBar MB = new MenuBar();
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
                        Signup SU = new Signup();
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
            int x = 150;
            int y = 30;
            int counter = 0;
            string orderitemid;
            string desc;
            string price;
            string prodid;
            string qty;
            string query1 = "SELECT orders.order_id, discount.discount_amount, orders.total, orders.created_date, orders.completed_date, active " +
                "FROM orders, customer_orders, discount where customer_orders.customer_id = '" + userId + "' and orders.order_id = customer_orders.order_id and orders.discount_id = discount.discount_id;";
            
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
                    discid = dr["discount_amount"].ToString();
                    total = dr["total"].ToString();
                    created = dr["created_date"].ToString();
                    active = dr["active"].ToString();
                    var ID = new Label(){
                        Name = "id",Text = "Order# : "+orderN,Location = new Point(y, x),Height = 20,Width = 75, BackColor = Color.Red, ForeColor = Color.White,};
                    UP.Controls.Add(ID);
                    var create = new Label(){
                        Name = "createddate",Text = "Order Date: "+created, Location = new Point(y + 75, x),Height = 20,Width = 350,BackColor = Color.Red, ForeColor = Color.White, };
                    UP.Controls.Add(create);
                    var discount = new Label(){
                        Name = "discount", Text = "Discount %"+discid,Location = new Point(y + 425, x),Height = 20,Width = 200,BackColor = Color.Red, ForeColor = Color.White,};
                    UP.Controls.Add(discount);
                    var totalprice = new Label(){
                        Name = "total",Text = "Total $" + total, Location = new Point(y + 625, x), Height = 20, Width = 100, BackColor = Color.Red, ForeColor = Color.White, };
                    UP.Controls.Add(totalprice);
                    x += 22;
                    y = 30;

                    string query2 = "SELECT order_item.order_item_id, order_item.description, order_item.price, order_item.product_id, order_item.quantity " +
                       "FROM order_item, order_items, orders where orders.order_id = '" + orderN + "' and order_item.order_item_id = order_items.order_item_id and order_items.order_id = orders.order_id;";
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

                            var itemord = new Label(){
                                Name = "orderitem",Text = orderitemid,Location = new Point(y + 0, x),Height = 20,Width = 50, Visible = false,};
                            UP.Controls.Add(itemord);
                            var productid = new Label(){
                                Name = "productid",Text = "SKU# : "+prodid,Location = new Point(y + 10, x),Height = 20, Width = 65,};
                            UP.Controls.Add(productid);
                            var description = new Label(){
                                Name = "description",Text = desc,Location = new Point(y + 80, x),Height = 20, Width = 350, };
                            UP.Controls.Add(description);
                            var prodprice = new Label(){
                                Name = "price",Text = "price $" + price,Location = new Point(y + 435, x),Height = 20, Width = 100,};
                            UP.Controls.Add(prodprice);
                            var prodqty = new Label(){
                                Name = "quantity", Text = "QTY: "+qty,Location = new Point(y + 540, x),Height = 20, Width = 75,};
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
