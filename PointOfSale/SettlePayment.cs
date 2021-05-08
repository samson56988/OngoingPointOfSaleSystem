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
    public partial class SettlePayment : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBconnection db = new DBconnection();
        SqlDataReader dr;
        CashierModule cm;
        public SettlePayment(CashierModule cms)
        {
            InitializeComponent();
            conn = new SqlConnection(db.Myconnection());
            cm = cms;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void txt_cash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double sale = double.Parse(txt_sales.Text);
                double cash = double.Parse(txt_cash.Text);
                double change = cash - sale;
                txt_change.Text = change.ToString("#,##0.00");
            }
            catch(Exception ex)
            {

                txt_change.Text = "0.00";

            
            }
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txt_cash.Text += btn7.Text;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txt_cash.Text += btn8.Text;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txt_cash.Text += btn9.Text;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txt_cash.Text += btn4.Text;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txt_cash.Text += btn5.Text;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txt_cash.Text += btn6.Text;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txt_cash.Text += btn1.Text;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txt_cash.Text += btn2.Text;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txt_cash.Text += btn3.Text;
        }

        private void btn00_Click(object sender, EventArgs e)
        {
            txt_cash.Text += btn00.Text;
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            txt_cash.Clear();
            txt_cash.Focus();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                if ((double.Parse(txt_change.Text) < 0) || (txt_cash.Text == String.Empty))
                {

                    MessageBox.Show("Insufficient amount. Please enter the correct amount!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
                else
                {

                    for (int i = 0; i < cm.dataGridView1.Rows.Count; i++)
                    {

                        conn.Open();
                        cmd = new SqlCommand("update  ProductTble set Quantity = Quantity - "+ int.Parse(cm.dataGridView1.Rows[i].Cells[5].Value.ToString()) +" where  pcode = '"+cm.dataGridView1.Rows[i].Cells[2].Value.ToString()+"'", conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        conn.Open();
                        cmd = new SqlCommand("update tblCart set status = 'Sold' where id ='" +cm.dataGridView1.Rows[i].Cells[1].Value.ToString()+"'", conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    
                    }

                    Reciept rp = new Reciept(cm);
                    rp.Loadreport(txt_cash.Text,txt_change.Text);
                    rp.ShowDialog();
                    MessageBox.Show("Payment Saved Successfully");
                    cm.GetTransNo();
                    cm.Loadcart();
                    this.Dispose();

                
                }

            }
            catch(Exception ex)
            {
               // MessageBox.Show("Insufficient amount. Please enter the correct amount!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show(ex.Message);
              
            }
        }
    }
}
