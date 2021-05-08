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
    public partial class FrmBrandList : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        DBconnection db = new DBconnection();


        public FrmBrandList()
        {
            InitializeComponent();
            conn = new SqlConnection(db.Myconnection());
            Loadrecords();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = dataGridView1.Columns[e.ColumnIndex].Name;

            if (colname == "Edit")
            {

                BrandModule brand = new BrandModule(this);
                brand.lblID.Text = dataGridView1[1, e.RowIndex].Value.ToString();
                brand.txt_brand.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                brand.ShowDialog();
                 
            
            }
            else if (colname == "Delete")
            {

                if (MessageBox.Show("Are you sure you want to Delete this brand?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("delete from tblBrand where id like'" + dataGridView1[1, e.RowIndex].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Brand Delete Successfully","POS",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    Loadrecords();
                
                }
            
            }
        }

        public void Loadrecords()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new SqlCommand("Select * from tblBrand order by Brand",conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dr["id"].ToString(),dr["brand"].ToString());


            
            }
            conn.Close();

        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BrandModule module = new BrandModule(this);
            module.ShowDialog();
        }

        private void FrmBrandList_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
