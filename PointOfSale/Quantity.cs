using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace PointOfSale
{
    public partial class Quantity : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBconnection db = new DBconnection();
        SqlDataReader dr;
        private string pcode;
        private double price;
        private string transno;
        string Stitle = "Simple POS System";
        CashierModule cashier;

        public Quantity(CashierModule cash)
        {
            InitializeComponent();
            conn = new SqlConnection(db.Myconnection());
            cashier = cash;
        }

        public void ProductDetails(String pcode, double price, String transno)
        {

            this.pcode = pcode;
            this.price = price;
            this.transno = transno;
        
        
        }

        private void Quantity_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) && (textBox1.Text != String.Empty))
            {
                String id = "";
                bool found = false;
                conn.Open();
                cmd = new SqlCommand("Select * from tblCart where transno = @transno and pcode =@pcode",conn);
                cmd.Parameters.AddWithValue("@transno", cashier.lblTransno.Text);
                cmd.Parameters.AddWithValue("@pcode", pcode);
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {

                    found = true;
                    id = dr["id"].ToString();

                }

                else
                {
                    found = false;
                
                }
                dr.Close();
                conn.Close();

                if (found == true)
                {
                    conn.Open();
                    cmd = new SqlCommand("update tblCart set Qty = (Qty + " +int.Parse(textBox1.Text)+") where id = '"+id+"' ", conn);
                   
                    cmd.ExecuteNonQuery();
                    conn.Close();


                    cashier.textBox1.Clear();
                    cashier.textBox1.Focus();
                    cashier.Loadcart();
                    this.Dispose();

                }
                else
                {

                    conn.Open();
                    cmd = new SqlCommand("insert into tblCart (transno,pcode,price,Qty,sdate,Cashier)Values(@transno,@pcode,@price,@Qty,@sdate,@Cashier)", conn);
                    cmd.Parameters.AddWithValue("@transno", transno);
                    cmd.Parameters.AddWithValue("@pcode", pcode);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@Qty", int.Parse(textBox1.Text));
                    cmd.Parameters.AddWithValue("@sdate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Cashier", cashier.label3.Text);

                    cmd.ExecuteNonQuery();
                    conn.Close();


                    cashier.textBox1.Clear();
                    cashier.textBox1.Focus();
                    cashier.Loadcart();
                    this.Dispose();
                
                }
                

            
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
