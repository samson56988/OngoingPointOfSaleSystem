using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSale
{
    public partial class CancelOrderDetails : Form
    {
        DisplaySoldItems sold;
        public CancelOrderDetails(DisplaySoldItems so)
        {
            InitializeComponent();
            sold = so;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Actioncb.Text != String.Empty) && (txtDisc.Text != String.Empty) && (Txt_reason.Text != String.Empty))
                { 

                    if(int.Parse(textBox1.Text) >= int.Parse(txt_Cancel.Text))
                    {
                        Void voidd = new Void(this);
                        voidd.Show();
                    
                    }
                


                    


                
                }

            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            
            }
        }

        private void CancelOrderDetails_Load(object sender, EventArgs e)
        {

        }

        public void RefreshList()
        {

            sold.LoadRecord();
        
        
        }
    }
}
