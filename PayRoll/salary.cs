using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PayRoll
{
    public partial class salary : Form
    {
        public salary()
        {
            InitializeComponent();
            ShowSalary();
            GetEmployees();
            GetAttendance();
            GetAttendanceDetails();
            GetEmployeeName();
            GetBonus();
            GetBonusDetails();
            Clear();
        }
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\jaide\OneDrive\Documents\PayRoll.mdf;Integrated Security = True; Connect Timeout = 30");

        private void ShowSalary()
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from SalTbl", con);
            var ds = new DataSet();
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            da.Fill(ds);
            salarydgv.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Clear()
        {
            nametb.Text = "";
            presenttb.Text = "";
            absenttb.Text = "";
            excusedtb.Text = "";
            periodpkr.Text = "";
            basesaltb.Text = "";
            advancetb.Text = "";
            balancetb.Text = "";
            empidcb.Text = "";
            bonuscb.Text = "";
            attendancecb.Text = "";
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
        private void GetAttendance()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from AttendanceTbl where EmpId="+empidcb.SelectedValue.ToString()+"", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("AttendNum", typeof(int));
            dt.Load(dr);
            attendancecb.ValueMember = "AttendNum";
            attendancecb.DataSource = dt;
            con.Close();
        }
        private void GetBonus()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from BonusTbl", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("BName", typeof(string));
            dt.Load(dr);
            bonuscb.ValueMember = "BName";
            bonuscb.DataSource = dt;
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
                basesaltb.Text = dr["EmpBaseSal"].ToString();
            }
            con.Close();
        }
        private void GetAttendanceDetails()
        {
            con.Open();
            string Query = "select * from AttendanceTbl where EmpId=" + empidcb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                presenttb.Text = dr["DayPresent"].ToString();
                absenttb.Text = dr["DayAbsent"].ToString();
                excusedtb.Text = dr["DayExcused"].ToString();

            }
            con.Close();
        }
        private void GetBonusDetails()
        {
            con.Open();
            string Query = "select * from BonusTbl where BName='" + bonuscb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                bamounttb.Text = dr["BAmount"].ToString();
            }
            con.Close();
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void salary_Load(object sender, EventArgs e)
        {

        }

        private void empidcb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetEmployeeName();
           
           
        }

        private void bonuscb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetBonusDetails();
        }

        private void attendancecb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetAttendanceDetails();
        }
        

        private void savebtn_Click(object sender, EventArgs e)
        {
            if (nametb.Text == ""|| empidcb.SelectedIndex == -1 || basesaltb.Text =="" || advancetb.Text =="")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    string Period = (periodpkr.Value.Month) + "-" + (periodpkr.Value.Year);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into SalTbl(EmpId,EmpName,EmpBaseSal,EmpBonus,EmpAdvance,EmpTax,EmpBalance,SalPeriod) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", con);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@p1", empidcb.Text);
                    cmd.Parameters.AddWithValue("@p2", nametb.Text);
                    cmd.Parameters.AddWithValue("@p3", basesaltb.Text);
                    cmd.Parameters.AddWithValue("@p4", bamounttb.Text);
                    cmd.Parameters.AddWithValue("@p5", advancetb.Text);
                    cmd.Parameters.AddWithValue("@p6", totaltax);
                    cmd.Parameters.AddWithValue("@p7", balancetb.Text);
                    cmd.Parameters.AddWithValue("@p8", Period);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Salary Saved Successfully!!!");
                    con.Close();
                    ShowSalary();
                    Clear();

                }

                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
            
            
        }
        int pres = 0, abs = 0, exc = 0, dailybase = 0, total = 0;

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            attendance obj = new attendance();
            obj.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            bonus obj = new bonus();
            obj.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (nametb.Text == "" || empidcb.SelectedIndex == -1 )
            {
                {
                    MessageBox.Show("Missing Information");
                }
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from SalTbl where EmpName=@p1", con);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@p1", nametb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Issued Salary Deleted Successfully!!!");
                    con.Close();
                    ShowSalary();
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
            if(nametb.Text == "" || empidcb.SelectedIndex == -1 || basesaltb.Text == "" || bamounttb.Text == "" || advancetb.Text == "")    
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    string Period = (periodpkr.Value.Month) + "-" + (periodpkr.Value.Year);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update SalTbl set EmpId=@p1,EmpName=@p2,EmpBaseSal=@p3,EmpBonus=@p4,EmpAdvance=@p5,EmpTax=@p6,EmpBalance=@p7,SalPeriod=@p8 where EmpName=@p9", con);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@p1", empidcb.Text);
                    cmd.Parameters.AddWithValue("@p2", nametb.Text);
                    cmd.Parameters.AddWithValue("@p3", basesaltb.Text);
                    cmd.Parameters.AddWithValue("@p4", bamounttb.Text);
                    cmd.Parameters.AddWithValue("@p5", advancetb.Text);
                    cmd.Parameters.AddWithValue("@p6", tax);
                    cmd.Parameters.AddWithValue("@p7", balancetb.Text);
                    cmd.Parameters.AddWithValue("@p8", Period);
                    cmd.Parameters.AddWithValue("@p9", nametb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Salary Updated Successfully!!!");
                    con.Close();
                    ShowSalary();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        double tax, totaltax, grdtotal;

        private void computebtn_Click(object sender, EventArgs e)
        {
            dailybase = Convert.ToInt32(basesaltb.Text) / 28;
            pres = Convert.ToInt32(presenttb.Text);
            abs = Convert.ToInt32(absenttb.Text);
            exc = Convert.ToInt32(excusedtb.Text);
            total = (dailybase * pres) + (dailybase / 2 * exc);
            tax = total * 0.16;
            totaltax = total - tax;
            grdtotal = totaltax + Convert.ToInt32(bamounttb.Text);
            balancetb.Text = grdtotal.ToString();
        }
    }
}
