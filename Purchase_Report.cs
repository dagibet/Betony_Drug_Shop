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
    public partial class Purchase_Report : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Dagim\documents\visual studio 2013\Projects\Betony_Drug_Shop\Betony_Drug_Shop\BetonyDrugShop.mdf;Integrated Security=True");
        string query = "";
        public Purchase_Report()
        {
            InitializeComponent();
        }

        private void Purchase_Report_Load(object sender, EventArgs e)
        {
            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = 0;
            int j = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Purchase_Master";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            foreach(DataRow dr in dt.Rows)
            {
                i = Convert.ToInt32(i + Convert.ToDouble(dr["Product_Total"].ToString()));
                j = j + Convert.ToInt32(dr["Product_Quantity"].ToString());
            }
            label3.Text = i.ToString();
            label6.Text = j.ToString();
            query = "select * from Purchase_Master";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string startdate;
            string enddate;

            startdate = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            enddate = dateTimePicker2.Value.ToString("dd/MM/yyyy");


            int i = 0;
            int j = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Purchase_Master where Purchase_Date>='"+startdate.ToString()+"' AND Purchase_Date<='"+enddate.ToString()+"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            foreach (DataRow dr in dt.Rows)
            {
                i = Convert.ToInt32(i + Convert.ToDouble(dr["Product_Total"].ToString()));
                j = j + Convert.ToInt32(dr["Product_Quantity"].ToString());
            }
            label3.Text = i.ToString();
            label6.Text = j.ToString();
            query = "select * from Purchase_Master where Purchase_Date>='" + startdate.ToString() + "' AND Purchase_Date<='" + enddate.ToString() + "'";
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            PurchaseReportPrint prp = new PurchaseReportPrint();
            prp.get_value(query.ToString());
            prp.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
