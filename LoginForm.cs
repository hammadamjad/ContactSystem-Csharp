using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactSystem
{
    public partial class LoginForm : Form
    {
        public static int id = 0;
        public static int main_id = 0;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            bool flag = true;
            Connection con = new Connection();
            try
            {
                con.conn.Open();
                string selectString = @"select * from Contact_login";
                SqlCommand cmd = new SqlCommand(selectString, con.conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    if (User.Text.Equals(rdr["username"].ToString()) &&
                        Pass.Text.Equals(rdr["userpassword"].ToString()))
                    {
                        id = int.Parse(rdr["login_id"].ToString());
                        flag = false;
                        break;
                    }
                }
                con.conn.Close();
            }
            catch (Exception r)
            {

            }
            try
            {
                con.conn.Open();
                string selectString1 = string.Format(@"select * from User_Details where Login_id = {0}", id);
                SqlCommand cmd1 = new SqlCommand(selectString1, con.conn);
                SqlDataReader rdr1 = cmd1.ExecuteReader();
                while (rdr1.Read())
                {
                    main_id = int.Parse(rdr1["detail_id"].ToString());
                }
                con.conn.Close();
            }
            catch (Exception f)
            {

            }


            if (flag == true)
            {
                MessageBox.Show("Incorrect details");

            }
            else
            {
                MessageBox.Show("Successfully logged In");
                this.Hide();
                new MainForm().Show();
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new SignupForm().Show();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
