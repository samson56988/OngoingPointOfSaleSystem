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
    public partial class Product : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBconnection db = new DBconnection();
        SqlDataReader dr;
        ProductList frmlist;
        public Product(ProductList flist)
        {
            InitializeComponent();
            conn = new SqlConnection(db.Myconnection());
            frmlist = flist;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public void loadcategory()
        {

            txt_cat.Items.Clear();
            conn.Open();
            cmd = new SqlCommand("select Category from Category",conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                txt_cat.Items.Add(dr[0].ToString());

            
            }
            dr.Close();
            conn.Close();

            
        
        }

        public void loadBrand()
        {

            TXT_brand.Items.Clear();
            conn.Open();
            cmd = new SqlCommand("select Brand from tblBrand",conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                TXT_brand.Items.Add(dr[0].ToString()); 

            
            }
            dr.Close();
            conn.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to Save this Product?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string bid = "", cid = "";

                    conn.Open();
                    cmd = new SqlCommand("Select id from tblBrand where Brand like '"+TXT_brand.Text+"'",conn);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        bid = dr[0].ToString();
                    
                    }
                    dr.Close();
                    conn.Close();


                    conn.Open();
                    cmd = new SqlCommand("Select id from Category where Category like '" + txt_cat.Text + "'", conn);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        cid = dr[0].ToString();

                    }
                    dr.Close();
                    conn.Close();

                    conn.Open();
                    cmd = new SqlCommand("insert into ProductTble(pcode,barcode,pdesc,bid,cid,Price)Values(@pcode,@barcode,@pdesc,@bid,@cid,@Price)", conn);
                    cmd.Parameters.AddWithValue("@pcode", txt_pcode.Text);
                    cmd.Parameters.AddWithValue("@barcode", txt_barcode.Text);
                    cmd.Parameters.AddWithValue("@pdesc", Txtdescription.Text);
                    cmd.Parameters.AddWithValue("@bid", bid);
                    cmd.Parameters.AddWithValue("@cid", cid);
                    cmd.Parameters.AddWithValue("@Price", txt_price.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Record inserted successfully");
                    clear();
                    frmlist.LoadProduct();

                }
                
            }
            catch(Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            
            }

        }

        public void clear()
        {

            txt_price.Text = "";
            txt_barcode.Text = "";
            Txtdescription.Text = "";
            txt_cat.Text = "";
            TXT_brand.Text = "";
            txt_barcode.Focus();
        
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to Update this Product?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string bid = "", cid = "";

                    conn.Open();
                    cmd = new SqlCommand("Select id from tblBrand where Brand like '" + TXT_brand.Text + "'", conn);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        bid = dr[0].ToString();

                    }
                    dr.Close();
                    conn.Close();


                    conn.Open();
                    cmd = new SqlCommand("Select id from Category where Category like '" + txt_cat.Text + "'", conn);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        cid = dr[0].ToString();

                    }
                    dr.Close();
                    conn.Close();
                    conn.Open();
                    cmd = new SqlCommand("Update ProductTble set barcode = @barcode,pdesc = @pdesc,bid = @bid,cid = @cid,Price = @Price where pcode like @pcode" , conn);
                    cmd.Parameters.AddWithValue("@pcode", txt_pcode.Text);
                    cmd.Parameters.AddWithValue("@barcode", txt_barcode.Text);
                    cmd.Parameters.AddWithValue("@pdesc", Txtdescription.Text);
                    cmd.Parameters.AddWithValue("@bid", bid);
                    cmd.Parameters.AddWithValue("@cid", cid);
                    cmd.Parameters.AddWithValue("@Price", txt_price.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Record updated successfully");
                    clear();
                    frmlist.LoadProduct();
                    this.Dispose();

                }

            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);

            }
        }

        private void txt_price_KeyPress(object sender, KeyPressEventArgs e)
        {


            if(e.KeyChar == 48)
            {
            
            
            }

           else if(e.KeyChar == 8)
            {
           
           
           
            }
            else  if((e.KeyChar < 48) || (e.KeyChar > 57))
            {
               
                e.Handled = true;
            
            }
        }

        private void Product_Load(object sender, EventArgs e)
        {

        }
    }
}
