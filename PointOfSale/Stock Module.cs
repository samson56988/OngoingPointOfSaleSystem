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
    public partial class Stock_Module : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBconnection db = new DBconnection();
        SqlDataReader dr;
        string Stitle = "Simple POS System";
        public Stock_Module()
        {
            InitializeComponent();
            conn = new SqlConnection(db.Myconnection());
        }

        public void LoadProduct()
        {

            conn.Open();
            int i = 0;
            dataGridView1.Rows.Clear();
            cmd = new SqlCommand("Select pcode,pdesc,Quantity from ProductTble where pdesc like '%" + txt_search.Text + "%'",conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i,dr[0].ToString(),dr[1].ToString(), dr[2].ToString());

            
            }
            dr.Close();
            conn.Close();
        
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dataGridView1.Columns[e.ColumnIndex].Name;

            if (ColName == "Select")
            {
                if (txt_ref.Text == string.Empty) { MessageBox.Show("Please Enter refrence no", Stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning); txt_ref.Focus(); return; }
                if (txt_stock.Text == string.Empty) { MessageBox.Show("Please Enter Stock in by", Stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning); txt_stock.Focus(); return; }
                if (MessageBox.Show("Add this item?",Stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    conn.Open();
                    cmd = new SqlCommand("insert into tblStock(refno,pcode,sdate,stockby)values(@refno,@pcode,@sdate,@stockby)", conn);
                    //'" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                    cmd.Parameters.AddWithValue("@refno", txt_ref.Text);
                    cmd.Parameters.AddWithValue("@pcode", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    cmd.Parameters.AddWithValue("@sdate", date_stockdate.Value);
                    cmd.Parameters.AddWithValue("@stockby", txt_stock.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Successfully Added!", Stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStockIN();

                    
                   

                
                }

                

            
            }
        }

        public void LoadStockIN()
        {
            int i = 0;
            dataGridView2.Rows.Clear();
            conn.Open();
            cmd = new SqlCommand("select * from ViewStockin where refno like '"+txt_ref.Text+"' and status like 'Pending' ", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            { 
                   i+=1;
                   dataGridView2.Rows.Add(i, dr[1].ToString(), dr[0].ToString(), dr[2].ToString(), dr[6].ToString(), dr[5].ToString(), dr[4].ToString(), dr[3].ToString());
            
            }
            dr.Close();
            conn.Close();
        
        }


        public void LoadStockINHistory()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new SqlCommand("select * from ViewStockin where status like 'Done'", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dr[1].ToString(), dr[0].ToString(), dr[2].ToString(), dr[6].ToString(), dr[5].ToString(), dr[4].ToString(), dr[3].ToString());

            }
            dr.Close();
            conn.Close();

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView2.Columns[e.ColumnIndex].Name;

            if (colName == "Delete")
            {

                if (MessageBox.Show("Remove this items?", Stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    conn.Open();
                    cmd = new SqlCommand("delete from tblStock where id = '" + dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString() + "'",conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Stock Deleted Successfully", Stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStockIN(); 


                
                }

                
            
            
            }
        }

        public void clear()
        {

            txt_stock.Clear();
            txt_ref.Clear();
            date_stockdate.Value = DateTime.Now;

        
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure you want to save this Record?", Stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        {
                            //update tblProduct QTY
                            conn.Open();
                            cmd = new SqlCommand("Update ProductTble set Quantity =" + int.Parse(dataGridView2.Rows[i].Cells[5].Value.ToString()) + " where pcode like '" + dataGridView2.Rows[i].Cells[3].Value.ToString() + "'", conn);
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            //update Tblstockin QTY

                            conn.Open();
                            cmd = new SqlCommand("Update tblStock set qty = " + int.Parse(dataGridView2.Rows[i].Cells[5].Value.ToString()) + ", Status = 'Done' where id like'" + dataGridView2.Rows[i].Cells[1].Value.ToString() + "'", conn);
                            cmd.ExecuteNonQuery();
                            conn.Close();




                        }
                        clear();
                        LoadStockIN();

                    }
                }
            }
            catch (Exception ex)
            {

                conn.Close();
                MessageBox.Show(ex.Message,Stitle,MessageBoxButtons.OK,MessageBoxIcon.Warning);
            
            
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StockHistory History = new StockHistory();
            History.LoadStockINHistory();
            History.Show();
        }
    }
}
