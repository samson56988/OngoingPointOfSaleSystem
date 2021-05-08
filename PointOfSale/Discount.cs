using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PointOfSale
{
    public partial class Discount : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBconnection db = new DBconnection();
        SqlDataReader dr;
        string Stitle = "Simple POS System";
        CashierModule cm;
        public Discount(CashierModule cms)
        {
            InitializeComponent();
            conn = new SqlConnection(db.Myconnection());
            cm = cms;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();

            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_Price_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Discount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double discount = Double.Parse(txt_Price.Text) * Double.Parse(txt_Discount.Text);
                txt_DiscountAmount.Text = discount.ToString("#,##0.00");
             
            }
            catch
            {
                txt_DiscountAmount.Text = "0.00";
            
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            
           
        }

        private void button2_Click(object sender, EventArgs e)
        {



            conn.Open();
            cmd = new SqlCommand("update tblCart set disc = @disc where id = @id", conn);
            cmd.Parameters.AddWithValue("@disc", Double.Parse(txt_DiscountAmount.Text));
            cmd.Parameters.AddWithValue("@id", int.Parse(LBLID.Text));
            cmd.ExecuteNonQuery();
            conn.Close();
            cm.Loadcart();
            this.Dispose();


        



            
        }
    }
}
