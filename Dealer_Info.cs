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
    public partial class Dealer_Info : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Dagim\documents\visual studio 2013\Projects\Betony_Drug_Shop\Betony_Drug_Shop\BetonyDrugShop.mdf;Integrated Security=True");

        public Dealer_Info()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Dealer_Info values('"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"','"+textBox4.Text+"','"+textBox5.Text+"')";
            cmd.ExecuteNonQuery();

            textBox1.Text=""; 
            textBox2.Text="";  
            textBox3.Text="";
            textBox4.Text=""; 
            textBox5.Text="";

            dg();

            MessageBox.Show("Dealer Information Saved Successfully");
        }

        private void Dealer_Info_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            dg();
        }
        public void dg()
        {
            
            

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Dealer_Info";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;

            }

        
        

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                int id;
                id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Dealer_Info where id = " + id + "";
                cmd.ExecuteNonQuery();

                MessageBox.Show("Dealer Deleted ");
                dg();
            }
            catch(Exception ex)
            {

            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                panel2.Visible = true;
                panel2.BringToFront();
                panel1.SendToBack();
                int id;
                id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Dealer_Info where id = " + id + "";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    textBox6.Text = dr["Dealer_Name"].ToString();
                    textBox7.Text = dr["Dealer_Company_Name"].ToString();
                    textBox8.Text = dr["Contact"].ToString();
                    textBox9.Text = dr["Address"].ToString();
                    textBox10.Text = dr["City"].ToString();
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int id;
                id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Dealer_Info set Dealer_Name='" + textBox6.Text + "',Dealer_company_name='" + textBox7.Text + "',Contact='" + textBox8.Text + "',Address='" + textBox9.Text + "',city='" + textBox10.Text + "' where id=" + id + "";
                cmd.ExecuteNonQuery();

                MessageBox.Show("Dealer Information Updated Successfully");
                panel2.Visible = false;
                //  panel1.Visible = true;
                panel1.BringToFront();
                panel2.SendToBack();
                dg();
            }
            catch(Exception ex)
            {

            }
        }

    }
}
