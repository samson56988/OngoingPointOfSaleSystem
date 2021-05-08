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
    public partial class Category : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBconnection db = new DBconnection();
        CategoryList frmlist;
        public Category(CategoryList flist)
        {
            InitializeComponent();
            conn = new SqlConnection(db.Myconnection());
            frmlist = flist;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
             try
            {
                if (MessageBox.Show("Are you sure you want to Save this Category?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("insert into Category(Category)Values(@Category)",conn);
                    cmd.Parameters.AddWithValue("@Category", txt_category.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Record inserted successfully");
                 
                    frmlist.Loadrecords();

                }
                
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("Are you sure you want to update this Category?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    conn.Open();
                    cmd = new SqlCommand("update Category set Category = @Category where ID like'" + lblID.Text + "'", conn);
                    cmd.Parameters.AddWithValue("@Category", txt_category.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Category has been Update successfully");
                    
                    frmlist.Loadrecords();
                    this.Dispose();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        }
    }

