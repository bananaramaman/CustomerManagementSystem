using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using loginsec;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace CustomerManagementSystem.UserValidation
{
    class UserFactory
    {
        LoginForm LF = new LoginForm();
        Signup SU = new Signup();
        Connection con = new Connection();
        public string MySqlConnectionString;

        // User login method
        public void UserLogin(string email, string password)
        {
            LF.Show();
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
                            string em = myReader.GetString(3);
                            string pass = myReader.GetString(10);
                        }
                        MenuBar MB = new MenuBar();
                        LF.Hide();
                        MB.StartPosition = FormStartPosition.WindowsDefaultLocation;
                        MB.ShowDialog();
                        con.Close();
                    }
                    else{
                        MessageBox.Show("Incorrect Username or Password!", "Login Page");}
                }
                else{
                    MessageBox.Show("Username or Password is empty!", "Login Page");}
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
    }
}
