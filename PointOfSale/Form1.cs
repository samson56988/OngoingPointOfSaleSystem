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
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBconnection db = new DBconnection();
        public Form1()
        {
            InitializeComponent();
            conn = new SqlConnection(db.Myconnection());
            //conn.Open();
            MessageBox.Show("Connected");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_brand_Click(object sender, EventArgs e)
        {
            FrmBrandList bd = new FrmBrandList();

            bd.TopLevel = false;
            panel3.Controls.Add(bd);
            bd.BringToFront();
            bd.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CategoryList ct = new CategoryList();
            ct.TopLevel = false;
            panel3.Controls.Add(ct);
            ct.BringToFront();
            ct.Show();


            }

        private void button3_Click(object sender, EventArgs e)
        {
            ProductList list = new ProductList();
            list.TopLevel = false;
            panel3.Controls.Add(list);
            list.LoadProduct();
            list.BringToFront();
            list.Show();
         ;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Stock_Module stock = new Stock_Module();
            stock.LoadProduct();
            stock.LoadStockIN();
            stock.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CashierModule cash = new CashierModule();
            cash.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            UserAccount frm = new UserAccount();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            DisplaySoldItems dis = new DisplaySoldItems();
            dis.suser = label1.Text;
            
            dis.Show();
        }

        
    }
}
