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
using System.Runtime.Remoting.Contexts;
using System.Drawing.Text;

namespace PayRoll
{
    public partial class employee : Form
    {
        public employee()
        {
            InitializeComponent();
            ShowEmployee();
            Clear();
        }
        private void label10_Click(object sender, EventArgs e)
        {

        }
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\jaide\OneDrive\Documents\PayRoll.mdf;Integrated Security = True; Connect Timeout = 30");

        private void ShowEmployee()
        {
            con.Open();
            string query = "select * from EmployeeTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            var ds = new DataSet();
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            da.Fill(ds);
            employeedgv.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Clear()
        {
            nametb.Text = "";
            gencb.SelectedIndex = 0;
            dobpkr.Text = "";
            phonetb.Text = "";
            poscb.SelectedIndex = 0;
            joindatepkr.Text = "";
            qualcb.SelectedIndex = 0;
            basesaltb.Text = "";
            addresstb.Text = "";
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            if (nametb.Text == "" || phonetb.Text == "" || gencb.SelectedIndex == -1 || addresstb.Text == "" || basesaltb.Text == "" || qualcb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmployeeTbl(EmpName,EmpGen,EmpDOB,EmpPhone,EmpAdd,EmpPos,JoinDate,EmpQual,EmpBaseSal) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", con);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@p1", nametb.Text);
                    cmd.Parameters.AddWithValue("@p2", gencb.Text);
                    cmd.Parameters.AddWithValue("@p3", dobpkr.Value.Date);
                    cmd.Parameters.AddWithValue("@p4", phonetb.Text);
                    cmd.Parameters.AddWithValue("@p5", addresstb.Text);
                    cmd.Parameters.AddWithValue("@p6", poscb.Text);
                    cmd.Parameters.AddWithValue("@p7", joindatepkr.Value.Date);
                    cmd.Parameters.AddWithValue("@p8", qualcb.Text);
                    cmd.Parameters.AddWithValue("@p9", basesaltb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Saved Successfully!!!");
                    con.Close();
                    ShowEmployee();
                    Clear();

                }

                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void employeedgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void editbtn_Click(object sender, EventArgs e)
        {
            if (nametb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update EmployeeTbl set EmpName=@p1,EmpGen=@p2,EmpDOB=@p3,EmpPhone=@p4,EmpAdd=@p5,EmpPos=@p6,JoinDate=@p7,EmpQual=@p8,EmpBaseSal=@p9 where EmpName=@p10", con);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@p1", nametb.Text);
                    cmd.Parameters.AddWithValue("@p2", gencb.Text);
                    cmd.Parameters.AddWithValue("@p3", dobpkr.Value.Date);
                    cmd.Parameters.AddWithValue("@p4", phonetb.Text);
                    cmd.Parameters.AddWithValue("@p5", addresstb.Text);
                    cmd.Parameters.AddWithValue("@p6", poscb.Text);
                    cmd.Parameters.AddWithValue("@p7", joindatepkr.Value.Date);
                    cmd.Parameters.AddWithValue("@p8", qualcb.Text);
                    cmd.Parameters.AddWithValue("@p9", basesaltb.Text);
                    cmd.Parameters.AddWithValue("@p10", nametb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Updated Successfully!!!");
                    con.Close();
                    ShowEmployee();
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
            if(nametb.Text == "")
            {
                MessageBox.Show("Please Enter the Employee name to be Deleted");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from EmployeeTbl where EmpName=@p1", con);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@p1", nametb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Deleted Successfully!!!");
                    con.Close();
                    ShowEmployee();
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            attendance obj = new attendance();
            obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            salary obj = new salary();
            obj.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            bonus obj = new bonus();
            obj.Show();
            this.Hide();
        }
    }
}

