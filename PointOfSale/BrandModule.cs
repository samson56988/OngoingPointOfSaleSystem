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
    public partial class BrandModule : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBconnection db = new DBconnection();
        FrmBrandList frmlist;


        public BrandModule(FrmBrandList flist)
        {
            InitializeComponent();
            conn = new SqlConnection(db.Myconnection());
            frmlist = flist;
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Clear()
        {

            btnSave.Enabled = true;
            btnEdit.Enabled = false;
            txt_brand.Clear();
            txt_brand.Focus();
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to Save this brand?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("insert into tblBrand(Brand)Values(@Brand)",conn);
                    cmd.Parameters.AddWithValue("@Brand", txt_brand.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Record inserted successfully");
                    Clear();
                    frmlist.Loadrecords();

                }
                
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void BrandModule_Load(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("Are you sure you want to update this brand?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    conn.Open();
                    cmd = new SqlCommand("update tblBrand set Brand = @brand where ID like'"+lblID.Text+"'",conn);
                    cmd.Parameters.AddWithValue("@brand", txt_brand.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Brand has been Update successfully");
                    Clear();
                    frmlist.Loadrecords();
                    this.Dispose();
                
                }
            
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
