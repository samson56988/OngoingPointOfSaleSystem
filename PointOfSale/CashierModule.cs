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
    public partial class CashierModule : Form
    {
        String ID;
        String Price;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBconnection db = new DBconnection();
        SqlDataReader dr;
        string Stitle = "Simple POS System";

        public CashierModule()
        {
            InitializeComponent();
            conn = new SqlConnection(db.Myconnection());
            label6.Text = DateTime.Now.ToLongDateString();
            this.KeyPreview = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {

                MessageBox.Show("Unable to logout. Please Cancel Transaction");
            
            }


            this.Close();
            Login log = new Login();
            log.ShowDialog();
        }

        private void CashierModule_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
        }
        public void GetTransNo()
        {

            try
            {

                string sdate = DateTime.Now.ToString("yyyyMMdd");
                string transno;
                int Count;
                conn.Open();
                cmd = new SqlCommand("select top 1 transno from tblCart where transno like '" + sdate + "%' order by id desc", conn);
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {

                    transno = dr[0].ToString();
                   Count = int.Parse(transno.Substring(8, 4));
                   lblTransno.Text = sdate + (Count + 1);

                
                } 
                else 
                
                {
                    
                    transno = sdate + "1001";
                    lblTransno.Text = transno;

                
                
                
                }

                dr.Close();
             

                conn.Close();



            }
            catch(Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message,Stitle,MessageBoxButtons.OK,MessageBoxIcon.Warning);
            
            }
        
        }

        private void button10_Click(object sender, EventArgs e)
        {
            GetTransNo();
            textBox1.Enabled = true;
            textBox1.Focus();
        }

        public void Loadcart()
        {

            try
            {
                ////////////////Boolean hasrecord = false;
                dataGridView1.Rows.Clear();
                int i = 0;
                double total = 0;
                double discount = 0;
                conn.Open();
                cmd = new SqlCommand("select c.id, c.pcode,p.pdesc,c.price,c.Qty,c.disc,c.total from tblCart c inner join ProductTble p on c.pcode = p.pcode where transno like '"+lblTransno.Text+"' and status like 'pending'",conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                
                    i++;
                    total += Double.Parse(dr["total"].ToString());
                    discount += Double.Parse(dr["disc"].ToString());
                    dataGridView1.Rows.Add(i, dr["id"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["price"].ToString(), dr["Qty"].ToString(), dr["disc"].ToString(), Double.Parse(dr["total"].ToString()).ToString("#,##0.00"));

                
                }
                dr.Close();
                conn.Close();
                lblSalesTotal.Text = total.ToString("#,##0.00");
                lblDiscount.Text = discount.ToString("#,##0.00");
                
                getSaletotal();
                ////if (hasrecord == true)
                ////{
                ////    button3.Enabled = true;

                ////}
                ////else
                ////{
                ////    button3.Enabled = false;
                
                ////}

            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message, Stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                conn.Close();
            
            }
        
        
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == String.Empty) { return; }
                else 
                {

                    conn.Open();
                    cmd = new SqlCommand("Select * from ProductTble where barcode like '" + textBox1.Text + "'", conn);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        Quantity quantity = new Quantity(this);
                        quantity.ProductDetails(dr["pcode"].ToString(), double.Parse(dr["price"].ToString()), lblTransno.Text);
                        dr.Close();
                        conn.Close();
                        quantity.ShowDialog();


                    }
                    else
                    {

                        dr.Close();
                        conn.Close();
                    
                    
                    }
                    
                
                }

            }
            catch (Exception ex)
            {

                conn.Close();
                MessageBox.Show(ex.Message, Stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
            }
        }

        public void getSaletotal()
        {
            
            double discount = Double.Parse(lblDiscount.Text);
            double sales = Double.Parse(lblSalesTotal.Text);
            
            double vat = sales * db.GetVal();
            double vatable = sales - vat;
            lblVat.Text = vat.ToString("#,##0.00");
            lblVatable.Text = vatable.ToString("#,##0.00");
            DisplayTotal.Text = sales.ToString("#,##0.00");



        
        }

        private void button2_Click(object sender, EventArgs e)
        {
           if (lblTransno.Text == "00000000000") { return; }
            Lookup up = new Lookup(this);
            up.LoadProduct();
            up.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            string colname = dataGridView1.Columns[e.ColumnIndex].Name;

            if (colname == "Delete")
            {


                if (MessageBox.Show("Remove this items?", Stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    conn.Open();
                    cmd = new SqlCommand("delete from tblCart where id like '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Item has been Deleted Successfully", Stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Loadcart();

                    



                }
                

            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
             
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Discount disc = new Discount(this);
            disc.LBLID.Text = ID;
            disc.txt_Price.Text = Price;
            disc.ShowDialog();

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            ID = dataGridView1[1, i].Value.ToString();
            Price = dataGridView1[4, i].Value.ToString();



        }

        private void DisplayTotal_Click(object sender, EventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblDate.Text = DateTime.Now.ToLongDateString();
        }
        private void lblTime_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SettlePayment payment = new SettlePayment(this);
            payment.txt_sales.Text = DisplayTotal.Text;
            payment.ShowDialog();
        }

        private void btn_brand_Click(object sender, EventArgs e)
        {
            DisplaySoldItems sold = new DisplaySoldItems();
            sold.suser = label3.Text;
            sold.Show();

            
        }

        


    }
}
