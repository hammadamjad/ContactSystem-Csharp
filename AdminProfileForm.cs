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
    public partial class AdminProfileForm : Form
    {
        public int add_id = 0;
        public int info_id = 0;
        public String num = string.Empty;
        Connection con = new Connection();
        public AdminProfileForm()
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
                                                    full join Contact_Info on Contact_Info.info_id = User_Details.Info_id
                                                    full join Contact_Address on Contact_Info.Address_id = Contact_Address.address_id
                                                    where detail_id = {0}", LoginForm.main_id);
                SqlCommand cmd1 = new SqlCommand(selectString1, con.conn);
                SqlDataReader rdr1 = cmd1.ExecuteReader();
                while (rdr1.Read())
                {
                    info_id = int.Parse(rdr1["info_id"].ToString());
                    add_id = int.Parse(rdr1["address_id"].ToString());
                    username.Text  = rdr1["con_name"].ToString();
                    numbe.Text = num = rdr1["con_number"].ToString();
                    email.Text = rdr1["con_email"].ToString();
                    add.Text = rdr1["address_1"].ToString();
                    city.Text = rdr1["city"].ToString();
                }
                con.conn.Close();
            }
            catch (Exception e)
            {

            }
        }

        private void label12_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
        public bool Check()
        {
            try
            {
                con.conn.Open();
                string selectString = @"select * from Contact_Info";
                SqlCommand cmd = new SqlCommand(selectString, con.conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (numbe.Text.Equals(rdr["con_number"].ToString()) &&
                         !numbe.Text.Equals(num))
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
                MessageBox.Show("User with this number is already present.");
            }
            else
            {
                try
                {
                    con.conn.Open();
                    string updateString1 = string.Format(@"update Contact_Info set con_name='{0}',con_number='{1}',con_email='{2}' where
                                                           info_id = {3}", username.Text, numbe.Text,email.Text, info_id);
                    SqlCommand updateCommand1 = new SqlCommand(updateString1, con.conn);
                    updateCommand1.ExecuteNonQuery();
                    con.conn.Close();
                } 
                catch (Exception r)
                {

                }
                try
                {
                    con.conn.Open();
                    string updateString2 = string.Format(@"update Contact_Address set address_1='{0}',city='{1}' where
                                                           address_id = {2}", add.Text,city.Text,add_id);
                    SqlCommand updateCommand2 = new SqlCommand(updateString2, con.conn);
                    updateCommand2.ExecuteNonQuery();
                    con.conn.Close();
                }
                catch (Exception t)
                {

                }
                MessageBox.Show("Profile details updated Successfully");
                this.Hide();
                new AdminForm().Show();
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AdminForm().Show();
        }
    }
}
