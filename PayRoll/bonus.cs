using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayRoll
{
    public partial class bonus : Form
    {
        public bonus()
        {
            InitializeComponent();
            ShowBonus();
            Clear();
        }
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\jaide\OneDrive\Documents\PayRoll.mdf;Integrated Security = True; Connect Timeout = 30");

        private void ShowBonus()
        {
            con.Open();
            string query = "select * from BonusTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            var ds = new DataSet();
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            da.Fill(ds);
            bonusdgv.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Clear()
        {
            bnametb.Text = "";
            bamounttb.Text = "";
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            if (bnametb.Text =="" || bamounttb.Text =="")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BonusTbl(BName,BAmount) values(@p1,@p2)", con);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@p1", bnametb.Text);
                    cmd.Parameters.AddWithValue("@p2", bamounttb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bonus Saved Successfully!!!");
                    con.Close();
                    ShowBonus();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
            }

        private void editbtn_Click(object sender, EventArgs e)
        {
            if (bnametb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update BonusTbl set BName=@p1,BAmount=@p2 where BName=@p3", con);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@p1", bnametb.Text);
                    cmd.Parameters.AddWithValue("@p2", bamounttb.Text);
                    cmd.Parameters.AddWithValue("@p3", bnametb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bonus updated Successfully!!!");
                    con.Close();
                    ShowBonus();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (bnametb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from BonusTbl where BName=@p1", con);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@p1", bnametb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bonus deleted Successfully!!!");
                    con.Close();
                    ShowBonus();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
        }
    }
}
