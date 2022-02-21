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

namespace ContactSystem
{
    public partial class AdminLoginFrom : Form
    {
        Connection con = new Connection();
        public string User = string.Empty;
        public AdminLoginFrom()
        {
            InitializeComponent();
            load();
        }
        public void load()
        {
            try
            {
                con.conn.Open();
                string selectString1 = string.Format(@"select * from User_Details
                                                    full join Contact_Login on Contact_Login.login_id = User_Details.Login_id
                                                    where detail_id = {0}", LoginForm.main_id);
                SqlCommand cmd1 = new SqlCommand(selectString1, con.conn);
                SqlDataReader rdr1 = cmd1.ExecuteReader();
                while (rdr1.Read())
                {
                    username.Text = User = rdr1["username"].ToString();
                    cpass.Text = rdr1["userpassword"].ToString();
                }
                con.conn.Close();
            }
            catch (Exception e)
            {

            }
        }
        public bool Check()
        {
            try
            {
                con.conn.Open();
                string selectString = @"select * from Contact_Login";
                SqlCommand cmd = new SqlCommand(selectString, con.conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (username.Text.Equals(rdr["username"].ToString()) &&
                        !username.Text.Equals(User))
                    {
                        return true;
                    }
                }
                con.conn.Close();
            }
            catch (Exception r)
            {

            }
            return false;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (Check() == true)
            {
                MessageBox.Show("User with this name is already present.");
            }
            else
            {
                if (newpass.Text.Equals(conpass.Text))
                {
                    con.conn.Open();
                    string updateString1 = string.Format(@"update Contact_Login set username='{0}',userpassword='{1}' where
                                                           login_id = {2}",username.Text, conpass.Text,LoginForm.id);
                    SqlCommand updateCommand1 = new SqlCommand(updateString1, con.conn);
                    updateCommand1.ExecuteNonQuery();
                    con.conn.Close();
                    MessageBox.Show("Login details updated Successfully");
                    this.Hide();
                    new AdminForm().Show();
                }
                else
                {
                    MessageBox.Show("Password does not match");
                }
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AdminForm().Show();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
