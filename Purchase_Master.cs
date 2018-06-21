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
    public partial class Purchase_Master : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Dagim\documents\visual studio 2013\Projects\Betony_Drug_Shop\Betony_Drug_Shop\BetonyDrugShop.mdf;Integrated Security=True");

        public Purchase_Master()
        {
            InitializeComponent();
        }

        private void Purchase_Master_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();

            }
            con.Open();
         //   fill_product_name();
            fill_dealer_name();
        }
      /*  public void fill_product_name()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " select * from Product_Name";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["Product_Name"].ToString());
               
                

            }
        }*/

        public void fill_dealer_name()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " select * from Dealer_Info";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox2.Items.Add(dr["Dealer_Name"].ToString());

            }
        }

    /*    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " select * from Product_Name where Product_name = '" + comboBox1.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                label3.Text = dr["Unit"].ToString();
                textBox4.Text = (dr["Product_Price"].ToString());

            }

        }*/

        private void textBox3_leave(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "select * from Stock where Product_name = '" + textBox7.Text + "'";
                cmd1.ExecuteNonQuery();
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                da1.Fill(dt1);
                i = Convert.ToInt32(dt1.Rows.Count.ToString());

                int j = 0;
                SqlCommand cmd10 = con.CreateCommand();
                cmd10.CommandType = CommandType.Text;
                cmd10.CommandText = "select * from Balance where Product_name = '" + textBox7.Text + "'";
                cmd10.ExecuteNonQuery();
                DataTable dt10 = new DataTable();
                SqlDataAdapter da10 = new SqlDataAdapter(cmd10);
                da10.Fill(dt10);
                j = Convert.ToInt32(dt10.Rows.Count.ToString());



                if (i == 0 && j == 0)
                {

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into Purchase_Master values('" + textBox7.Text+ "','" + textBox3.Text + "','" + label3.Text + "','" + textBox2.Text + "','" + textBox1.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + dateTimePicker2.Value.ToString("dd-MM-yyyy") + "','" + textBox6.Text + "') ";
                    cmd.ExecuteNonQuery();

                    SqlCommand cmd3 = con.CreateCommand();
                    cmd3.CommandType = CommandType.Text;
                    cmd3.CommandText = "insert into Stock values('" + textBox7.Text + "','" + textBox3.Text + "','" + label3.Text + "','" + textBox4.Text + "')";
                    cmd3.ExecuteNonQuery();

                    SqlCommand cmd4 = con.CreateCommand();
                    cmd4.CommandType = CommandType.Text;
                    cmd4.CommandText = "insert Into Balance(Product_Name,Purchased_QtY,Balance,purchased_Date) values('" + textBox7.Text + "','" + textBox3.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "')";
                    cmd4.ExecuteNonQuery();






                }
                else
                {

                    SqlCommand cmd2 = con.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "insert into Purchase_Master values('"+textBox7.Text +"','" + textBox3.Text + "','" + label3.Text + "','" + textBox2.Text + "','" + textBox1.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + dateTimePicker2.Value.ToString("dd-MM-yyyy") + "','" + textBox6.Text + "') ";
                    cmd2.ExecuteNonQuery();

                    SqlCommand cmd4 = con.CreateCommand();
                    cmd4.CommandType = CommandType.Text;
                    cmd4.CommandText = "insert Into Balance(Product_Name,Purchased_QtY,purchased_Date) values('" + textBox7.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "')";
                    cmd4.ExecuteNonQuery();

                    SqlCommand cmd5 = con.CreateCommand();
                    cmd5.CommandType = CommandType.Text;
                    cmd5.CommandText = "update Stock set Product_Quantity = Product_Quantity + " + textBox3.Text + " where Product_Name= '" + textBox7.Text + "'";
                    cmd5.ExecuteNonQuery();

                    SqlCommand cmd8 = con.CreateCommand();
                    cmd8.CommandType = CommandType.Text;
                    cmd8.CommandText = "insert into Balance(Product_Name,Balance) select Product_Name,Product_Quantity from Stock where Product_Name='" + textBox7.Text+ "'";
                    //  cmd8.CommandText = "update Balance set Balance = Balance +'" + textBox3.Text + "  where Product_Name= '" + comboBox1.Text + "'";
                    cmd8.ExecuteNonQuery();
                }







                MessageBox.Show("You Successfully Purchased This Product ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Fill all fields!", ex.Message);
            }

            textBox7.Text = ""; comboBox2.Text = ""; comboBox3.Text = "";
            textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = "";
            textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = "";
            dateTimePicker2.Value = DateTime.Now;
        }


         
        
    

        private void button3_Click(object sender, EventArgs e)
        {
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Update_Product up = new Update_Product();
            up.Show();
        }

        private void textBox3_Leave_1(object sender, EventArgs e)
        {
            textBox5.Text = Convert.ToString(Convert.ToInt32(textBox6.Text) * Convert.ToDouble(textBox4.Text));
        }

        private void textBox6_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox6.Text))
            {
                e.Cancel = true;
                textBox6.Focus();
                errorProvider1.SetError(textBox6, "Please Add Buying Price!");
            }
            else
            {
                // e.Cancel = true;
                errorProvider1.SetError(textBox6, null);
                //  comboBox1.Focus();
            }
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                e.Cancel = true;
                textBox3.Focus();
                errorProvider1.SetError(textBox3, "Please Add Product Quantity!");
            }
            else
            {
                // e.Cancel = true;
                errorProvider1.SetError(textBox3, null);
                //  comboBox1.Focus();
            }
        }

        private void textBox7_KeyUp(object sender, KeyEventArgs e)
        {
            listBox1.Visible = true;

            listBox1.Items.Clear();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Product_Name where Product_Name like('" + textBox7.Text + "%')";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                listBox1.Items.Add(dr["Product_Name"].ToString());
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
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
                    textBox7.Text = listBox1.SelectedItem.ToString();
                    listBox1.Visible = false;

                   // textBox4.Focus();
                }
            }
            catch (Exception ex)
            {

            }
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " select * from Product_Name where Product_name = '" + textBox7.Text + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    label3.Text = dr["Unit"].ToString();
                    textBox4.Text = (dr["Product_Price"].ToString());

                }
            }
            catch(Exception ex)
            {

            }
        }

       
  
    }
}