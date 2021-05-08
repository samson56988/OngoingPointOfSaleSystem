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
    public partial class CategoryList : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        DBconnection db = new DBconnection();
        public CategoryList()
        {
            InitializeComponent();
            conn = new SqlConnection(db.Myconnection());
            Loadrecords();
        }

        public void Loadrecords()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new SqlCommand("Select * from Category order by Category", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dr["ID"].ToString(), dr["Category"].ToString());



            }
            conn.Close();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            Category module = new Category(this);
            
            module.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = dataGridView1.Columns[e.ColumnIndex].Name;

            if (colname == "Edit")
            {

                Category cate = new Category(this);
                cate.lblID.Text= dataGridView1[1, e.RowIndex].Value.ToString();
                cate.txt_category.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                cate.ShowDialog();


            }
            else if (colname == "Delete")
            {

                if (MessageBox.Show("Are you sure you want to Delete this Category?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("delete from Category where id like'" + dataGridView1[1, e.RowIndex].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Category Delete Successfully", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Loadrecords();

                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
