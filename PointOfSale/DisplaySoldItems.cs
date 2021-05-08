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
    public partial class DisplaySoldItems : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBconnection db = new DBconnection();
        SqlDataReader dr;
        

         public string suser;

        public DisplaySoldItems()
        {
            InitializeComponent();
            conn = new SqlConnection(db.Myconnection());
            date1.Value = DateTime.Now;
            date2.Value = DateTime.Now;
            LoadRecord();
            LoadCashier();
            
        }

      


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void LoadRecord()
        {
            int i = 0;
            double _total = 0;
            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new SqlCommand("Select c.id, c.transno,c.pcode, p.pdesc,c.price,c.Qty,c.Disc,c.total from tblCart as c inner join ProductTble as p on c.pcode = p.pcode where status like 'Sold' and sdate between '"+date1.Value+"' and '"+date2.Value+"'  OR Cashier like'"+comboBox1.Text+"' ",conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                i += 1;
                _total += double.Parse(dr["total"].ToString());
                dataGridView1.Rows.Add(i, dr["id"].ToString(), dr["transno"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["price"].ToString(), dr["Qty"].ToString(), dr["Disc"].ToString(), dr["total"].ToString());
            
            }
            dr.Close();
            conn.Close();
            labelTotal.Text = _total.ToString("#,##0.00");

        
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SoldReport sold = new SoldReport(this);
            sold.loadReport();
            sold.ShowDialog();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void LoadCashier()
        {

            comboBox1.Items.Clear();
            comboBox1.Items.Add("All Cashier");
            conn.Open();
            cmd = new SqlCommand("Select * from Users where role like 'Cashier'",conn);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                comboBox1.Items.Add(dr["username"].ToString());
            
            }
            dr.Close();
            conn.Close();
        
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;


            if (colName == "colCancel")
            {

                CancelOrderDetails cancle = new CancelOrderDetails(this);
                cancle.txt_ID.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                cancle.txt_Transno.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                cancle.txtprocode.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                cancle.txt_description.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                cancle.txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                cancle.textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                cancle.txtDisc.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                cancle.txtTotal.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                cancle.txtCancelledvy.Text = suser;

              

                cancle.Show();



            
            }
        }
    }
}
