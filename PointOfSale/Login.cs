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
    public partial class Login : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBconnection db = new DBconnection();
        SqlDataReader dr;
        
        public Login()
        {
            InitializeComponent( );
            conn = new SqlConnection(db.Myconnection());
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = "" , role_name = "", name = "" ;
            try
            {
                bool found = false;
                conn.Open();
                cmd = new SqlCommand("select * from Users where username = @username and password = @password ",conn);
                cmd.Parameters.AddWithValue("@username", txt_user.Text);
                cmd.Parameters.AddWithValue("@password", txt_password.Text);
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {

                    found = true;
                    username = dr["username"].ToString();
                    role_name = dr["role"].ToString();
                    name = dr["name"].ToString();



                }
                else
                {

                    found = false;
                
                }
                dr.Close();
                conn.Close();


                if (found == true)
                {

                    if (role_name == "Cashier")
                    {
                        MessageBox.Show("Welcome " + name, "Access Granted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_password.Clear();
                        txt_user.Clear();
                        this.Hide();

                        CashierModule cash = new CashierModule();
                        cash.label3.Text = name;
                        
                        cash.Show();

                    }

                    else
                    {

                        MessageBox.Show("Welcome " + name, "Access Granted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_password.Clear();
                        txt_user.Clear();
                        this.Hide();
                        Form1 cash = new Form1();
                        cash.label2.Text = name + "|" + role_name;
                        cash.Show();

                    }


                }
                else
                {
                    MessageBox.Show("Invalid Details");
                
                }

            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txt_password.Clear();
            txt_user.Clear();
        }
    }
}
