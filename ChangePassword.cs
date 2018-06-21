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
    public partial class ChangePassword : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Dagim\documents\visual studio 2013\Projects\Betony_Drug_Shop\Betony_Drug_Shop\BetonyDrugShop.mdf;Integrated Security=True");
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlDataAdapter ad = new SqlDataAdapter("select count(*) from UserTable where UserName='" + textBox1.Text + "'and PassWord='" + textBox2.Text + "'",con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            if(dt.Rows[0][0].ToString()=="1")
            {
                if(textBox3.Text==textBox4.Text)
                {
                    SqlDataAdapter cc = new SqlDataAdapter("update UserTable set PassWord='" + textBox3.Text + "'where UserName='" + textBox1.Text + "'and PassWord='" + textBox2.Text + "'", con);
                    DataTable dt1 = new DataTable();
                    cc.Fill(dt1);
                    MessageBox.Show("password Changed");
                }
                    else
                    {
                        MessageBox.Show("Not Match");
                    }
            }
                else{
                    MessageBox.Show("no data");
                }

                }
            
           

            
        


        private void ChangePassword_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
           
        }
      

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
