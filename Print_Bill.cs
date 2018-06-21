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
    public partial class Print_Bill : Form
    {

        int j;
        int tot = 0;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Dagim\documents\visual studio 2013\Projects\Betony_Drug_Shop\Betony_Drug_Shop\BetonyDrugShop.mdf;Integrated Security=True");
       
     
        public Print_Bill()
        {
            InitializeComponent();
        }

        public void get_value(int i)
        {
            j = i;
        }


        private void Print_Bill_Load(object sender, EventArgs e)
        {
            if(con.State==ConnectionState.Open)
            {
                con.Close();

            }
            con.Open();

            DataSet1 ds = new DataSet1();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Order_User where Id=" + j + "";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds.DataTable1);


            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select * from Order_Item where Order_Id=" + j + "";
            cmd1.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(ds.DataTable2);
            da1.Fill(dt1);

            tot = 0;
            foreach(DataRow dr2 in dt1.Rows)
            {
                tot = tot + Convert.ToInt32(dr2["total"].ToString());
            }

            CrystalReport1 myreport = new CrystalReport1();
            myreport.SetDataSource(ds);
            myreport.SetParameterValue("total", tot.ToString());
            crystalReportViewer1.ReportSource = myreport;

        }
    }
}
