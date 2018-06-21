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
    public partial class BinCardReport : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Dagim\documents\visual studio 2013\Projects\Betony_Drug_Shop\Betony_Drug_Shop\BetonyDrugShop.mdf;Integrated Security=True");

        public BinCardReport()
        {
            InitializeComponent();
        }

        private void BinCardReport_Load(object sender, EventArgs e)
        {
            if(con.State== ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            listBox1.Visible = true;

            listBox1.Items.Clear();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from stock where Product_Name like('" + textBox1.Text + "%')";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                listBox1.Items.Add(dr["Product_Name"].ToString());
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                listBox1.Focus();
                listBox1.SelectedIndex = 0;
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    this.listBox1.SelectedIndex = this.listBox1.SelectedIndex + 1;
                }
                if (e.KeyCode == Keys.Up)
                {
                    this.listBox1.SelectedIndex = this.listBox1.SelectedIndex - 1;
                }
                if (e.KeyCode == Keys.Enter)
                {
                    textBox1.Text = listBox1.SelectedItem.ToString();
                    listBox1.Visible = false;
                    // textBox4.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                //    cmd.CommandText = "select Distinct Balance from Balance";
                //   cmd.CommandText = "SELECT Stock.Product_Name,Stock.Product_Quantity , Balance.Purchased_Qty,Balance.Balance FROM Balance CROSS JOIN Stock ";
                cmd.CommandText = "Select Product_Name, Purchased_Qty,Soled_Qty,Balance,Purchased_Date,Sell_Date from Balance where Product_Name = '" + textBox1.Text + "'";
                // cmd.CommandText = "Select First_Name FROM Order_User CROSS JOIN Purchase_Party_Name FROM Purchase_Master CROSS JOIN Balance";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
