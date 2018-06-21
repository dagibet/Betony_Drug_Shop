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
    public partial class SaleReportPrint : Form
    {
        string j;
        int tot = 0;
        int qty = 0;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Dagim\documents\visual studio 2013\Projects\Betony_Drug_Shop\Betony_Drug_Shop\BetonyDrugShop.mdf;Integrated Security=True");
        public void get_value(string i)
        {
            j = i;
        }

        public SaleReportPrint()
        {
            InitializeComponent();
        }


        private void SaleReportPrint_Load(object sender, EventArgs e)
        {
              if(con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            DataSet3 ds = new DataSet3();

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
                tot = tot + Convert.ToInt32(dr["Total"].ToString());
                qty = qty + Convert.ToInt32(dr["QTY"].ToString());
            }
            CrystalReport4 myreport = new CrystalReport4();
            myreport.SetDataSource(ds);
            myreport.SetParameterValue("total", tot.ToString());
            myreport.SetParameterValue("Qty", qty.ToString());
            crystalReportViewer1.ReportSource = myreport;
        }
        }
    }

