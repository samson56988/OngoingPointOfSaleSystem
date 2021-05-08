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
using Microsoft.Reporting.WinForms;

namespace PointOfSale
{
    public partial class SoldReport : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBconnection db = new DBconnection();
        DisplaySoldItems sold;

        public SoldReport(DisplaySoldItems sd)
        {
            InitializeComponent();
            conn = new SqlConnection(db.Myconnection());
            sold = sd;
        }

        private void SoldReport_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void loadReport()
        {

            try


            {
                ReportDataSource rptDS;

                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Report2.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();

                conn.Open();
                if (sold.comboBox1.Text == "All Cashier")
                { 
                    da.SelectCommand = new SqlCommand("Select c.id, c.transno,c.pcode, p.pdesc,c.price,c.Qty,c.Disc,c.total from tblCart as c inner join ProductTble as p on c.pcode = p.pcode where status like 'Sold' and sdate between '" + sold.date1.Value + "' and '" + sold.date2.Value + "'", conn);
                }
                else
                {

                    da.SelectCommand = new SqlCommand("Select c.id, c.transno,c.pcode, p.pdesc,c.price,c.Qty,c.Disc,c.total from tblCart as c inner join ProductTble as p on c.pcode = p.pcode where status like 'Sold' and sdate between '" +sold.date1.Value + "' and '" + sold.date2.Value + "'  OR Cashier like'" + sold.comboBox1.Text + "' ", conn);
                }
                da.Fill(ds.Tables["dtSoldReport"]);
                conn.Close();
                ReportParameter pDate = new ReportParameter("pDate","Date From:" +sold.date1.Value.ToShortDateString()+"To" + sold.date2.Value.ToShortDateString());
                ReportParameter pCashier = new ReportParameter("pCashier", "Cashier:" + sold.comboBox1.Text);
                ReportParameter pHeader = new ReportParameter("pHeader", "Sales Report");

                reportViewer1.LocalReport.SetParameters(pDate);
                reportViewer1.LocalReport.SetParameters(pCashier);
                reportViewer1.LocalReport.SetParameters(pHeader);

               
                rptDS = new ReportDataSource("DataSet1", ds.Tables["dtSoldReport"]);
                reportViewer1.LocalReport.DataSources.Add(rptDS);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;


            }
            catch(Exception ex)
            { 
            
                conn.Close();
                MessageBox.Show(ex.Message);
            
            
            }
        
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
