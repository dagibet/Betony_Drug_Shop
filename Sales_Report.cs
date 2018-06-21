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
    public partial class Sales_Report : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Dagim\documents\visual studio 2013\Projects\Betony_Drug_Shop\Betony_Drug_Shop\BetonyDrugShop.mdf;Integrated Security=True");
        string query = "";
        public Sales_Report()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            int j = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select  Order_Id,Product,Price,Qty,Total,Order_Date from Order_Item";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            foreach (DataRow dr in dt.Rows)
            {
                i = Convert.ToInt32(i + Convert.ToDouble(dr["Total"].ToString()));
                j = j + Convert.ToInt32(dr["Qty"].ToString());
            }
            label1.Text = i.ToString();
            label2.Text = j.ToString();
            query = "select  Order_Id,Product,Price,Qty,Total,Order_Date from Order_Item";
        }

        private void Sales_Report_Load(object sender, EventArgs e)
        {
            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string startdate;
            string enddate;

            startdate = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            enddate = dateTimePicker2.Value.ToString("dd/MM/yyyy");


            int i = 0;
            int j = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Order_Item where Order_Date>='" + startdate.ToString() + "' AND Order_Date<='" + enddate.ToString() + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            foreach (DataRow dr in dt.Rows)
            {
                i = Convert.ToInt32(i + Convert.ToDouble(dr["Total"].ToString()));
                j = j + Convert.ToInt32(dr["Qty"].ToString());
            }
            label1.Text = i.ToString();
            label2.Text = j.ToString();
            query = "select * from Order_Item where Order_Date>='" + startdate.ToString() + "' AND Order_Date<='" + enddate.ToString() + "'";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaleReportPrint prp = new SaleReportPrint();
            prp.get_value(query.ToString());
            prp.Show();
        }
    }
}
