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

    public partial class Add_Product_Name : Form
    {
       
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Dagim\documents\visual studio 2013\Projects\Betony_Drug_Shop\Betony_Drug_Shop\BetonyDrugShop.mdf;Integrated Security=True");
        public Add_Product_Name()
        {
            InitializeComponent();
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                e.Cancel = false;
                textBox1.Focus();
                errorProvider1.SetError(textBox1, "Please Add Product Name!");
            }
            else
            {
               // e.Cancel = true;
                errorProvider1.SetError(textBox1, null);
              //  comboBox1.Focus();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Add_Product_Name_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        //    fillcombo();
            filldg();
        }




        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Product_Name values('" + textBox1.Text + "','" + textBox5.Text + "','" + comboBox2.SelectedItem.ToString() + "','" + textBox3.Text + "')";
                cmd.ExecuteNonQuery();


                textBox1.Text = "";
                filldg();
                MessageBox.Show("Product Added Succesfully");
            }
            catch(Exception ex)
            {
                MessageBox.Show("please fill all fields!");
            }
        }
       

        public void filldg()
        {
            
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Product_Name";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

         

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel2.Visible = true;
            textBox1.Visible = false;
            int i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

         //   comboBox3.Items.Clear();
            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "select * from Product_Name";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);

            foreach (DataRow dr2 in dt2.Rows)
            {
              //  comboBox3.Items.Add(dr2["Unit"].ToString());
            }

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Product_Name where id = " + i + "";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textBox2.Text = dr["Product_Name"].ToString();
              //  comboBox3.SelectedItem = dr["Unit"].ToString();
                textBox6.Text = dr["Unit"].ToString();
                comboBox4.SelectedItem = dr["Product_Type"].ToString();
                textBox4.Text = Convert.ToString(Convert.ToInt32(dr["Product_Price"].ToString()));

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Product_Name set Product_Name='" + textBox2.Text + "',unit='" + textBox6.Text + "',Product_Type='" + comboBox4.SelectedItem.ToString() + "',Product_Price='"+textBox4.Text+"' where id=" + i + "";
            cmd.ExecuteNonQuery();
            panel2.Visible = false;
         //   textBox1.Visible = false;
            filldg();



        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                e.Cancel = false;
                textBox1.Focus();
                errorProvider1.SetError(textBox3, "Please Add Selling Price!");
            }
            else
            {
                // e.Cancel = true;
                errorProvider1.SetError(textBox3, null);
                //  comboBox1.Focus();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

       
        }

    }
