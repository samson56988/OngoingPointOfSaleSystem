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
    public partial class Lookup : Form
    {
        CashierModule cm;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBconnection db = new DBconnection();
        SqlDataReader dr;
        string stitle = "Simple POS System";

        public Lookup(CashierModule cms)
        {
            InitializeComponent();
            conn = new SqlConnection(db.Myconnection());
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colNmae = dataGridView1.Columns[e.ColumnIndex].Name;

            if (colNmae == "Select")
            {
                

                Quantity quantity = new Quantity(cm);
                quantity.ProductDetails(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), Double.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()),cm.lblTransno.Text);
                quantity.ShowDialog();


            
            }
        }

        public void LoadProduct()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            conn.Open();
            cmd = new SqlCommand("Select p.pcode,p.barcode,p.pdesc,b.Brand,c.Category,p.price,p.Quantity from ProductTble as p inner join tblBrand as b on b.id = p.bid inner join Category as c on c.id = p.cid where p.pdesc like'" + txt_search.Text + "%'", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());



            }
            dr.Close();
            conn.Close();


        }
    }
}
