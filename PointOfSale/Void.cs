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
    public partial class Void : Form
    {

        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBconnection db = new DBconnection();
        SqlDataReader dr;
        CancelOrderDetails can;
        public Void(CancelOrderDetails cn)
        {
            InitializeComponent();
            conn = new SqlConnection(db.Myconnection());
            can = cn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                

                    string user;
                    conn.Open();
                    cmd = new SqlCommand("select username from Users where username = @username and password = @password", conn);
                    cmd.Parameters.AddWithValue("@username", txtPass.Text);
                    cmd.Parameters.AddWithValue("@password", textBox1.Text);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        user = dr["username"].ToString(); 

                        dr.Close();

                        conn.Close();


                        saveCanceOrder(user);
                        if (can.Actioncb.Text == "Yes")
                        {



                            backToInventory("update ProductTble set Quantity = Quantity + " + int.Parse(can.txt_Cancel.Text) + " where pcode = '" + can.txtprocode.Text + "'");
                        
                        }

                        backToInventory("update tblCart set Qty = Qty - " + int.Parse(can.txt_Cancel.Text) + "where id like '" + can.txt_ID.Text + "'");
                        conn.Open();
                        cmd = new SqlCommand("update tblCart set status = 'Canceled' where id = '" + can.txt_ID.Text + "'",conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        
                        MessageBox.Show("Order transaction successfully canceled");
                        this.Dispose();
                        can.RefreshList();
                        can.Dispose();
                    
                    }
                    dr.Close();
                    conn.Close();
                
                
            }
            catch (Exception ex)
            {

                conn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
            }

        }

        public void saveCanceOrder(string user)
        {


            conn.Open();
            cmd = new SqlCommand("insert into tblcancel (transno,pcode,price,Quantity,total,sdate,voidby,canceledby,reason,action)values(@transno,@pcode,@price,@Quantity,@total,@sdate,@voidby,@canceledby,@reason,@action)", conn);
            cmd.Parameters.AddWithValue("@transno", can.txt_Transno.Text);
            cmd.Parameters.AddWithValue("@pcode", can.txtprocode.Text);
            cmd.Parameters.AddWithValue("@price",double.Parse( can.txtPrice.Text));
            cmd.Parameters.AddWithValue("@Quantity" ,int.Parse( can.txt_Cancel.Text));
            cmd.Parameters.AddWithValue("@total", can.txtTotal.Text);
            cmd.Parameters.AddWithValue("@sdate", DateTime.Now);
            cmd.Parameters.AddWithValue("@voidby", can.txt_Transno.Text);
            cmd.Parameters.AddWithValue("@canceledby", can.txtCancelledvy.Text);
            cmd.Parameters.AddWithValue("@reason", can.Txt_reason.Text);
            cmd.Parameters.AddWithValue("@action", can.Actioncb.Text);
            cmd.ExecuteNonQuery();




            conn.Close();
        
        }

        public void backToInventory(string sql)
        {

            conn.Open();
            cmd = new SqlCommand(sql,conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        
        }
    }
}
