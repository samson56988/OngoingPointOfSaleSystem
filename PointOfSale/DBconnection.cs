using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PointOfSale
{
    class DBconnection
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
      
        SqlDataReader dr;
        public string Myconnection()
        {

            string con = "Data Source=DESKTOP-J3DHBNP\\SQLEXPRESS;Initial Catalog=POS;Integrated Security=True";
            return con;
        
        }

        public double GetVal()
        {

            double vat = 0;
            conn.ConnectionString = Myconnection();
            conn.Open();
            cmd = new SqlCommand("select * from Vat", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            { 
            
                vat = Double.Parse(dr["vat"].ToString());
            
            }
            dr.Close();
            conn.Close();
            return vat;
        
        }
    }
}
