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
    public partial class UserAccount : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBconnection db = new DBconnection();
        SqlDataReader dr;

        public UserAccount()
        {
            InitializeComponent();
            conn = new SqlConnection(db.Myconnection());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void UserAccount_Resize(object sender, EventArgs e)
        {
            tabControl1.Left = (this.Width - tabControl1.Width) / 2;
            tabControl1.Top = (this.Height - tabControl1.Height) / 2;
        }

        private void clear()
        {
            tXT_name.Clear();
            txt_password.Clear();
            txt_retypepassword.Clear();
            txt_username.Clear();
            CBROLR.Text = "";
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_password.Text != txt_retypepassword.Text)
                {
                    MessageBox.Show("Password did not match","Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                
                }
                conn.Open();
                cmd = new SqlCommand("insert into Users (username, password,role,name)Values(@username,@password,@role,@name)", conn);
                cmd.Parameters.AddWithValue("@username", txt_username.Text);
                cmd.Parameters.AddWithValue("@password", txt_password.Text);
                cmd.Parameters.AddWithValue("@role", CBROLR.Text);
                cmd.Parameters.AddWithValue("@name", tXT_name.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Account Saved Successfully");


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }




    }
}
