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
    public partial class attendance : Form
    {
        public attendance()
        {
            InitializeComponent();
            ShowAttendance();
            GetEmployees();
            GetEmployeeName();
            Clear();
        }
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\jaide\OneDrive\Documents\PayRoll.mdf;Integrated Security = True; Connect Timeout = 30");

        private void ShowAttendance()
        {
            con.Open();
            string query = "select * from AttendanceTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            var ds = new DataSet();
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            da.Fill(ds);
            attendancedgv.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Clear()
        {
            nametb.Text = "";
            presenttb.Text = "";
            absenttb.Text = "";
            excusedtb.Text = "";
            periodpkr.Text = "";
            
        }
        private void GetEmployees()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from EmployeeTbl", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpId", typeof(int));
            dt.Load(dr);
            empidcb.ValueMember = "EmpId";
            empidcb.DataSource = dt;
            con.Close();
        }
        private void GetEmployeeName()
        {
            con.Open();
            string Query = "select * from EmployeeTbl where EmpId=" + empidcb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                nametb.Text = dr["EmpName"].ToString();
            }
            con.Close();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (nametb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    string Period = (periodpkr.Value.Month) + "-" + (periodpkr.Value.Year);

                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into AttendanceTbl(EmpId,EmpName,DayPresent,DayAbsent,DayExcused,Period) values(@p1,@p2,@p3,@p4,@p5,@p6)", con);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@p1", empidcb.Text);
                    cmd.Parameters.AddWithValue("@p2", nametb.Text);
                    cmd.Parameters.AddWithValue("@p3", presenttb.Text);
                    cmd.Parameters.AddWithValue("@p4", absenttb.Text);
                    cmd.Parameters.AddWithValue("@p5", excusedtb.Text);
                    cmd.Parameters.AddWithValue("@p6", Period);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Attendance Saved Successfully!!!");
                    con.Close();
                    ShowAttendance();
                    Clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void empidcb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetEmployeeName();
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
                    string Period = (periodpkr.Value.Month) + "-" + (periodpkr.Value.Year);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update AttendanceTbl set EmpId=@p1,DayPresent=@p3,DayAbsent=@p4,DayExcused=@p5,Period=@p6 where EmpName=@p2", con);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@p1", empidcb.Text);
                    cmd.Parameters.AddWithValue("@p2", nametb.Text);
                    cmd.Parameters.AddWithValue("@p3", presenttb.Text);
                    cmd.Parameters.AddWithValue("@p4", absenttb.Text);
                    cmd.Parameters.AddWithValue("@p5", excusedtb.Text);
                    cmd.Parameters.AddWithValue("@p6", Period);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Attendance Updated Successfully!!!");
                    con.Close();
                    ShowAttendance();
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
            if (nametb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from AttendanceTbl where EmpId=@p1", con);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@p1", empidcb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Attendance Deleted Successfully!!!");
                    con.Close();
                    ShowAttendance();
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            employee obj = new employee();
            obj.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            bonus obj = new bonus();
            obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            salary obj = new salary();
            obj.Show();
            this.Hide();
        }
    }

}
