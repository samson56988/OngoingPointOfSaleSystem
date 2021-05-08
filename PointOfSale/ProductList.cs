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
    public partial class ProductList : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBconnection db = new DBconnection();
        SqlDataReader dr;
        public ProductList()
        {
            InitializeComponent();
            conn = new SqlConnection(db.Myconnection());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Product pro = new Product(this);
            pro.loadBrand();
            pro.loadcategory();
            pro.ShowDialog();
        }

        public void LoadProduct()
        {
           int  i = 0;
            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new SqlCommand("Select p.pcode,p.barcode,p.pdesc,b.Brand,c.Category,p.price,p.Quantity from ProductTble as p inner join tblBrand as b on b.id = p.bid inner join Category as c on c.id = p.cid where p.pdesc like'"+txt_search.Text+"%'", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            { 
            
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(),dr[6].ToString());


            
            }
            dr.Close();
            conn.Close();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = dataGridView1.Columns[e.ColumnIndex].Name;

            if (colname == "Edit")
            {

                Product pro = new Product(this);
                pro.btnSave.Enabled = false;
                pro.btnEdit.Enabled = true;
                pro.txt_pcode.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                pro.Txtdescription.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                pro.txt_price.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                pro.TXT_brand.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                pro.txt_cat.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                pro.txt_barcode.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                pro.ShowDialog();


            }
            else if (colname == "Delete")
            {

                if (MessageBox.Show("Are you sure you want to Delete this Product?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("delete from Product where id like'" + dataGridView1[1, e.RowIndex].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Product Delete Successfully", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProduct();

                }

            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }
    }
}
