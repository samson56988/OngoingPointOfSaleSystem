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
    public partial class StockHistory : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBconnection db = new DBconnection();
        SqlDataReader dr;
        public StockHistory()
        {
            InitializeComponent();
            conn = new SqlConnection(db.Myconnection());
        }

        private void StockHistory_Load(object sender, EventArgs e)
        {

        }
        public void LoadStockINHistory()
        {
            int i = 0;
            dataGridView2.Rows.Clear();
            conn.Open();
            cmd = new SqlCommand("select * from ViewStockin where status like 'Done'", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i += 1;
                dataGridView2.Rows.Add(i, dr[1].ToString(), dr[0].ToString(), dr[2].ToString(), dr[6].ToString(), dr[5].ToString(), dr[4].ToString(), dr[3].ToString());

            }
            dr.Close();
            conn.Close();

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int i = 0;
            dataGridView2.Rows.Clear();
            conn.Open();
            cmd = new SqlCommand("select * from ViewStockin where sdate between '"+date1.Value.ToShortDateString()+ "'and'"+date2.Value.ToShortDateString()+"' and status like 'Done'", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i += 1;
                dataGridView2.Rows.Add(i, dr[1].ToString(), dr[0].ToString(), dr[2].ToString(), dr[6].ToString(), dr[5].ToString(), dr[4].ToString(), dr[3].ToString());

            }
            dr.Close();
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
