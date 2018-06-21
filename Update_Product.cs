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
    public partial class Update_Product : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Dagim\documents\visual studio 2013\Projects\Betony_Drug_Shop\Betony_Drug_Shop\BetonyDrugShop.mdf;Integrated Security=True");
        public Update_Product()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Product_Name where Product_Name = '" + comboBox1.Text + "'";
            cmd.ExecuteNonQuery();
          //  cmd.ExecuteReader();

           // SqlCommand com = new SqlCommand("Select * from Customers Where CustomerId = '" + comboBox1.Text + "'", con);
          //  SqlDataReader reader;

       //     con.Open();
          //  reader = com.ExecuteReader();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
         //   dataGridView1.DataSource = dt;

            foreach(DataRow dr in dt.Rows )
            {
                textBox1.Text = dr["Product_Price"].ToString();
            }

          
              
        }

        private void Update_Product_Load(object sender, EventArgs e)
        {
            if(conn.State ==ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            fillcombo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Product_Name set Product_price='" + textBox2.Text + "' where Product_Name = '" + comboBox1.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            MessageBox.Show("Price Updated");
        }
           
        public void fillcombo()
        {
            comboBox1.Items.Clear();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Product_Name";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["Product_Name"].ToString());
            }

        }

    }
}
