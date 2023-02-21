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
        Connection con = new Connection();
        public static List<string> descList = new List<string>();
        public static List<double> priceList = new List<double>();
        public static List<string> idList = new List<string>();
        public static List<int> qtyList = new List<int>();


        public void CartContents(string desc, double price, string ID, int qty)
        {   //adds items to the order_item table database 

            descList.Add(desc);
            priceList.Add(price);
            idList.Add(ID);
            qtyList.Add(qty);
        }

        void qty_changed(object sender, EventArgs e)
        {   // refreshes the cart either removing items where qty == 0, or updating price based on new quantities

            int qty = Convert.ToInt32(((System.Windows.Forms.NumericUpDown)sender).Value);
            int id = Convert.ToInt32(((System.Windows.Forms.NumericUpDown)sender).Name);

            if (qty == 0)
            {
                descList.RemoveAt(id);
                priceList.RemoveAt(id);
                idList.RemoveAt(id);
                qtyList.RemoveAt(id);
            }
            else if (qty > 0)
            {
                qtyList.RemoveAt(id);
                qtyList.Insert(id, qty);
            }
        }

        public void CartList(Form CP)
        {   // displays all items in the cart on the cart page, including name, price and quantitiy
            int x = 100;
            int y = 0;
            double total = 0;
            double Unittotal = 0;

            for (int i = 0; i < descList.Count; i++)
            {
                var ID = new Label()
                {
                    Name = "id" + i,
                    Text = idList[i],
                    Location = new Point(y + 45, x),
                    Height = 20,
                    Width = 20,
                };
                CP.Controls.Add(ID);
                var title = new Label()
                {
                    Name = "title"+i,
                    Text = descList[i],
                    Location = new Point(y + 70, x),
                    Height = 20,
                    Width = 350,
                };
                CP.Controls.Add(title);
                var Pricelabel = new Label()
                {
                    Name = "price"+i,
                    Text = priceList[i].ToString(),
                    Location = new Point(y + 520, x),
                    Height = 20,
                    Width = 50
                };
                CP.Controls.Add(Pricelabel);
                var qtylabel = new NumericUpDown()
                {
                    Name = i.ToString(),
                    Text = qtyList[i].ToString(),
                    Location = new Point(y + 670, x),
                    Height = 20,
                    Width = 50,
                };
                // register the event handler for the qty box
                qtylabel.ValueChanged += new EventHandler(qty_changed);
                CP.Controls.Add(qtylabel);
                Unittotal = priceList[i] * Convert.ToInt32(qtyList[i]);
                var UnitTlabel = new Label()
                {
                    Name = "unittotal",
                    Text = Convert.ToString(Unittotal),
                    Location = new Point(y + 580, x),
                    Height = 20,
                    Width = 50
                };
                CP.Controls.Add(UnitTlabel);
                y = 0;
                x += 20;
                total += Unittotal;
            }
            var separator = new Label()
            {
                BackColor = System.Drawing.Color.Red,
                Location = new Point(20, x + 10),
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
    }
}
