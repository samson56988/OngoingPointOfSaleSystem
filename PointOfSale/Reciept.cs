using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;

namespace PointOfSale
{
    public partial class Reciept : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBconnection db = new DBconnection();
        SqlDataReader dr;
        string store = "Dante Software Solution";
        string address = "21 Kuje , Abuja";
        CashierModule cms;
       
        public Reciept(CashierModule cm)
        {
            InitializeComponent();
            conn = new SqlConnection(db.Myconnection());
            cms = cm;
        }

        private void Reciept_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        public void Loadreport(string pcash,string pchange)
        {
            ReportDataSource rptDataSource;
            try
            {
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Report1.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();

                conn.Open();
                da.SelectCommand = new SqlCommand("select c.id, c.transno,c.pcode,c.price,c.Qty,c.disc,c.total,c.sdate,c.status,p.pdesc from tblCart as c inner join ProductTble p on c.pcode = p.pcode where transno like '"+cms.lblTransno.Text+"'", conn);
                da.Fill(ds.Tables["dtSold"]);
                conn.Close();

                ReportParameter pVatable = new ReportParameter("pVatable" ,cms.lblVatable.Text);
                ReportParameter Vat = new ReportParameter("pVat", cms.lblVatable.Text);
                ReportParameter pDiscount = new ReportParameter("pdiscount", cms.lblDiscount.Text);
                ReportParameter pTotal = new ReportParameter("pTotal",cms.lblSalesTotal.Text);
                ReportParameter pCash = new ReportParameter("pCash", pcash);
                ReportParameter pChange = new ReportParameter("pChange", pchange);
                ReportParameter pStore = new ReportParameter("pStore", store);
                ReportParameter pAddress = new ReportParameter("pAddress", address);
                ReportParameter pTransaction = new ReportParameter("pTransaction","Invoice #:"+  cms.lblTransno.Text);
                ReportParameter pCashier = new ReportParameter("Cashier", cms.label3.Text);

               // ReportParameter pCash = new ReportParameter("PTotal", cms.lblSalesTotal.Text);


                reportViewer1.LocalReport.SetParameters(pVatable);
                reportViewer1.LocalReport.SetParameters(Vat);
                reportViewer1.LocalReport.SetParameters(pDiscount);
                reportViewer1.LocalReport.SetParameters(pTotal);
                reportViewer1.LocalReport.SetParameters(pCash);
                reportViewer1.LocalReport.SetParameters(pChange);
                reportViewer1.LocalReport.SetParameters(pStore);
                reportViewer1.LocalReport.SetParameters(pAddress);
                reportViewer1.LocalReport.SetParameters(pTransaction);
                reportViewer1.LocalReport.SetParameters(pCashier);








                rptDataSource = new ReportDataSource("DataSet1", ds.Tables["dtSold"]);
                reportViewer1.LocalReport.DataSources.Add(rptDataSource);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 30;


            }
            catch
            { 
            
            
            }
        
        
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
