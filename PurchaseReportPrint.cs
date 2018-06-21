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

namespace Betony_Drug_Shop
{
    public partial class PurchaseReportPrint : Form
    {
        string j;
        int tot = 0;
        int qty = 0;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Dagim\documents\visual studio 2013\Projects\Betony_Drug_Shop\Betony_Drug_Shop\BetonyDrugShop.mdf;Integrated Security=True");
        public void get_value(string i)
        {
            j = i;
        }
              public PurchaseReportPrint()
        {
            InitializeComponent();
        }

        private void PurchaseReportPrint_Load(object sender, EventArgs e)
        {
            if(con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            DataSet2 ds = new DataSet2();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = j;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds.DataTable1);
            da.Fill(dt);

            tot = 0;
            qty = 0;
            foreach(DataRow dr in dt.Rows)
            {
                tot = tot + Convert.ToInt32(dr["Product_Total"].ToString());
                qty = qty + Convert.ToInt32(dr["Product_Quantity"].ToString());
            }
            CrystalReport3 myreport = new CrystalReport3();
            myreport.SetDataSource(ds);
            myreport.SetParameterValue("Total", tot.ToString());
            myreport.SetParameterValue("Total Quantity", qty.ToString());
            crystalReportViewer1.ReportSource = myreport;
        }
    }
}
