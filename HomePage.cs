﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MySql.Data;
using System.Net;
using loginsec;
using MySql.Data.MySqlClient;




namespace CustomerManagementSystem
{
    public partial class HomePage : Form
    {
        Connection con = new Connection();
        public string MySQLConnectionString;
        

        public HomePage()
        {
            InitializeComponent();
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            // populating form with products
            string query = "select product_id, prod_name, prod_category, prod_description, image_url, price, stock_qty from customer_management.products";
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

            MySQLConnectionString = con.connectionString();
            MySqlConnection mySqlConnection = new MySqlConnection(MySQLConnectionString);
            MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
            mySqlConnection.Open();
            DataTable dt = new DataTable();
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
                /*
                string path = "C:/Users/DJBur/Documents/CustomerManagement/CustomerManagementSystem/Images/" +id+".jpg";

                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(url, path);
                }
                */
                var title = new Label()
                {
                    Name = "title"+counter,
                    Text = name,
                    Location = new Point(y, x),
                    Width = 125,
                    Height = 30,
                };
                this.Controls.Add(title);
                var label = new Label()
                {
                    Name = "desc"+counter,
                    Text = description,
                    Location = new Point(y, x+155),
                    Width = 125,
                    Height = 100,
                };
                this.Controls.Add(label);
                var picture = new PictureBox
                {
                    Name = "pictureBox" + counter,
                    Size = new Size(125, 125),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Location = new Point(y, x + 30),
                    ImageLocation = url,
                    WaitOnLoad = true,
                };
                this.Controls.Add(picture);
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