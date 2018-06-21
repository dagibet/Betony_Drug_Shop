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
    public partial class Sales : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Dagim\documents\visual studio 2013\Projects\Betony_Drug_Shop\Betony_Drug_Shop\BetonyDrugShop.mdf;Integrated Security=True");
        DataTable dt = new DataTable();
        int tot = 0;
        public Sales()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Sales_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            dt.Clear();
            dt.Columns.Add("product");
            dt.Columns.Add("price");
            dt.Columns.Add("qty");
            dt.Columns.Add("total");
          //  dt.Columns.Add("Order_Date(dd/MM/yyyy)");
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            listBox1.Visible = true;

            listBox1.Items.Clear();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from stock where Product_Name like('" + textBox3.Text + "%')";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                listBox1.Items.Add(dr["Product_Name"].ToString());
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
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
                    textBox3.Text = listBox1.SelectedItem.ToString();
                    listBox1.Visible = false;
                    textBox4.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 1 * from Purchase_Master where Product_Name = '" + textBox3.Text + "' order by id desc";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textBox4.Text = dr["Product_Price"].ToString();
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            try
            {
                textBox6.Text = Convert.ToString(Convert.ToDouble(textBox4.Text) * Convert.ToInt32(textBox5.Text));

            }
            catch (Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(ValidateChildren(ValidationConstraints.Enabled))
                {
                    MessageBox.Show(textBox5.Text,"Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                int stock = 0;
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "select * from Stock where Product_name= '" + textBox3.Text + "'";
                cmd1.ExecuteNonQuery();
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                da1.Fill(dt1);
                foreach (DataRow dr1 in dt1.Rows)
                {

                    stock = Convert.ToInt32(dr1["Product_Quantity"].ToString());

                }

                if (Convert.ToInt32(textBox5.Text) > stock)
                {
                    MessageBox.Show("The is no " + textBox5.Text + " Stocks.Please Check Your Stock Status Report!");
                }


                else
                {
                    int bal = 0;
                    DataRow dr = dt.NewRow();
                    dr["product"] = textBox3.Text;
                    dr["price"] = textBox4.Text;
                    dr["qty"] = textBox5.Text;
                    dr["total"] = textBox6.Text;

                    //  dateTimePicker1.Value.ToString("dd/MM/yyyy");
                    dt.Rows.Add(dr);
                    dataGridView1.DataSource = dt;

                    tot = tot + Convert.ToInt32(dr["total"].ToString());
                    label10.Text = tot.ToString();
                }
            }
                catch(Exception ex)
                 {

                 }
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
            }
        
        
        

        private void button2_Click(object sender, EventArgs e)
        {
            
            
                tot = 0;
                dt.Rows.RemoveAt(Convert.ToInt32(dataGridView1.CurrentCell.RowIndex.ToString()));
                foreach(DataRow dr1 in dt.Rows)
                {
                    tot = tot + Convert.ToInt32(dr1["total"].ToString());
                    label10.Text = tot.ToString();
                }
        
            

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string orederid = "";
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "insert into Order_User values('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "','" + dateTimePicker1.Text + "')";
                cmd1.ExecuteNonQuery();


                SqlCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "select top 1 * from Order_User order by id desc";
                cmd2.ExecuteNonQuery();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);
                foreach (DataRow dr2 in dt2.Rows)
                {
                    orederid = dr2["id"].ToString();
                }

                foreach (DataRow dr in dt.Rows)
                {
                    int qty = 0;
                    int bal = 0;

                    string pname = "";
                    SqlCommand cmd3 = con.CreateCommand();
                    cmd3.CommandType = CommandType.Text;
                    cmd3.CommandText = "insert into Order_Item values('" + orederid.ToString() + "','" + dr["product"].ToString() + "','" + dr["price"].ToString() + "','" + dr["qty"].ToString() + "','" + dr["total"].ToString() + "','" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "')";
                    cmd3.ExecuteNonQuery();


                    SqlCommand cmd4 = con.CreateCommand();
                    cmd4.CommandType = CommandType.Text;
                    cmd4.CommandText = "insert into Balance(Product_Name,Soled_Qty,Sell_Date) values('" + dr["product"].ToString() + "','" + dr["qty"].ToString() + "','" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "')";
                    cmd4.ExecuteNonQuery();

                    qty = Convert.ToInt32(dr["qty"].ToString());
                    pname = dr["product"].ToString();

                    SqlCommand cmd6 = con.CreateCommand();
                    cmd6.CommandType = CommandType.Text;
                    cmd6.CommandText = "update Stock set Product_Quantity= Product_Quantity - " + qty + " where Product_Name = '" + pname.ToString() + "' ";
                    cmd6.ExecuteNonQuery();

                    SqlCommand cmd7 = con.CreateCommand();
                    cmd7.CommandType = CommandType.Text;
                    cmd7.CommandText = "insert into Balance(Product_Name, Balance) select Product_Name,Product_Quantity from Stock where Product_Name = '" + pname.ToString() + "' ";
                    cmd7.ExecuteNonQuery();


                }

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                label10.Text = "";

                dt.Clear();
                dataGridView1.DataSource = dt;

                MessageBox.Show("record Inserted Successfully");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string orederid = "";
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "insert into Order_User values('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "','" + dateTimePicker1.Value.ToString("dd/mm/yyy") + "')";
                cmd1.ExecuteNonQuery();


                SqlCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "select top 1 * from Order_User order by id desc";
                cmd2.ExecuteNonQuery();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);
                foreach (DataRow dr2 in dt2.Rows)
                {
                    orederid = dr2["id"].ToString();
                }

                foreach (DataRow dr in dt.Rows)
                {
                    int qty = 0;
                    string pname = "";
                    SqlCommand cmd3 = con.CreateCommand();
                    cmd3.CommandType = CommandType.Text;
                    cmd3.CommandText = "insert into Order_Item values('" + orederid.ToString() + "','" + dr["product"].ToString() + "','" + dr["price"].ToString() + "','" + dr["qty"].ToString() + "','" + dr["total"].ToString() + "','" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "')";
                    cmd3.ExecuteNonQuery();

                    SqlCommand cmd4 = con.CreateCommand();
                    cmd4.CommandType = CommandType.Text;
                    cmd4.CommandText = "insert into Balance(Product_Name,Soled_Qty,Sell_Date) values('" + dr["product"].ToString() + "','" + dr["qty"].ToString() + "','" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "')";
                    cmd4.ExecuteNonQuery();


                    qty = Convert.ToInt32(dr["qty"].ToString());
                    pname = dr["product"].ToString();

                    SqlCommand cmd6 = con.CreateCommand();
                    cmd6.CommandType = CommandType.Text;
                    cmd6.CommandText = "update Stock set Product_Quantity= Product_Quantity - " + qty + " where Product_Name = '" + pname.ToString() + "' ";
                    cmd6.ExecuteNonQuery();

                    SqlCommand cmd7 = con.CreateCommand();
                    cmd7.CommandType = CommandType.Text;
                    cmd7.CommandText = "update Balance set Balance= Balance - " + qty + " where Product_Name = '" + pname.ToString() + "' ";
                    cmd7.ExecuteNonQuery();
                }

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                label10.Text = "";

                dt.Clear();
                dataGridView1.DataSource = dt;

                Print_Bill pb = new Print_Bill();
                pb.get_value(Convert.ToInt32(orederid.ToString()));
                pb.Show();
            }
            catch(Exception ex)
            {

            }

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                e.Cancel = true;
                textBox5.Focus();
                errorProvider1.SetError(textBox5, "Please Add Product Name!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox1, null);
                //  comboBox1.Focus();
            }
        }
    }
} 