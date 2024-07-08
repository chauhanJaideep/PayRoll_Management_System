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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CountEmployees();
            CountManagers();
            SalaryIssued();
            AttendanceMarked();
        }
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\jaide\OneDrive\Documents\PayRoll.mdf;Integrated Security = True; Connect Timeout = 30");

        private void CountEmployees()
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select Count(*) from EmployeeTbl",con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            employeelbl.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void CountManagers()
        {
            string Position = "Manager";
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select Count(*) from EmployeeTbl where EmpPos='"+Position+"'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            managerlbl.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void SalaryIssued()
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select Count(*) from SalTbl", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            sallbl.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void AttendanceMarked()
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select Count(*) from AttendanceTbl", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            attendancelbl.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            salary obj = new salary();
            obj.Show();
            this.Hide();
        }
    }
}
