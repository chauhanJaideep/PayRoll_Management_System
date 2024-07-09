using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayRoll
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void cancelpb_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            if(loginidtb.Text == "" || passwordtb.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                if(loginidtb.Text == "Admin" && passwordtb.Text == "Password")
                {
                    Form1 obj = new Form1();
                    obj.Show();
                    this.Hide();
                    MessageBox.Show("Logged In Successfully!!!");
                }
                else
                {
                    MessageBox.Show("LoginId or Password is incorrect");
                }
            }
        }
    }
}
