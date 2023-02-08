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
        {
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
    }
}
