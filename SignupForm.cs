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
    public partial class SignupForm : Form
    {
        public SignupForm()
        {
            InitializeComponent();
        }

        public bool Check()
        {
            Connection con = new Connection();
            try
            {
                con.conn.Open();
                string selectString = @"select * from Contact_Login";
                SqlCommand cmd = new SqlCommand(selectString, con.conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (User.Text.Equals(rdr["username"].ToString()))
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

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new LoginForm().Show();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            string login_id = string.Empty;
            string address_id = string.Empty;
            string info_id = string.Empty;

            if (Check() == true)
            {
                MessageBox.Show("User with this name is already present.");
            }
            else
            {
                if (Pass.Text.Equals(CPass.Text))
                {
                    Connection con = new Connection();
                    try
                    {
                        con.conn.Open();

                        //inserting into login table.
                        string insertString1 = string.Format(@"insert into Contact_Login values('{0}','{1}')", User.Text, Pass.Text);
                        SqlCommand insertCommand1 = new SqlCommand(insertString1, con.conn);
                        int a = insertCommand1.ExecuteNonQuery();
                        //getting last row of login table
                        string selectString1 = @"SELECT * FROM Contact_Login WHERE login_id=(SELECT max(login_id) FROM Contact_Login)";
                        SqlCommand cmd1 = new SqlCommand(selectString1, con.conn);
                        SqlDataReader rdr1 = cmd1.ExecuteReader();
                        while (rdr1.Read())
                        {
                            login_id = rdr1["login_id"].ToString();
                        }

                        con.conn.Close();
                    }
                    catch (Exception r)
                    {

                    }
                    try
                    {
                        con.conn.Open();
                        //inserting into contact address
                        string insertString3 = string.Format(@"insert into Contact_Address values('{0}','{1}')", addres.Text, cit.Text);
                        SqlCommand insertCommand3 = new SqlCommand(insertString3, con.conn);
                        int b = insertCommand3.ExecuteNonQuery();
                        //getting last row of login table
                        string selectString3 = @"SELECT * FROM Contact_Address WHERE address_id=(SELECT max(address_id) FROM Contact_Address)";
                        SqlCommand cmd3 = new SqlCommand(selectString3, con.conn);
                        SqlDataReader rdr3 = cmd3.ExecuteReader();
                        while (rdr3.Read())
                        {
                            address_id = rdr3["address_id"].ToString();
                        }
                        con.conn.Close();
                    }
                    catch (Exception t)
                    {

                    }
                    try
                    {
                        con.conn.Open();
                        //inserting into contact info.
                        string insertString2 = string.Format(@"insert into Contact_Info values('{0}','{1}','{2}','{3}')"
                                                                , name.Text, num.Text, ema.Text, address_id);
                        SqlCommand insertCommand2 = new SqlCommand(insertString2, con.conn);
                        int c = insertCommand2.ExecuteNonQuery();
                        //getting last id from contact info.
                        string selectString2 = @"SELECT * FROM Contact_Info WHERE info_id=(SELECT max(info_id) FROM Contact_Info)";
                        SqlCommand cmd2 = new SqlCommand(selectString2, con.conn);
                        SqlDataReader rdr2 = cmd2.ExecuteReader();
                        while (rdr2.Read())
                        {
                            info_id = rdr2["info_id"].ToString();
                        }
                        con.conn.Close();
                    }
                    catch (Exception t)
                    {

                    }
                    try
                    {
                        con.conn.Open();
                        //inserting into user detail
                        string insertString4 = string.Format(@"insert into User_Details values('{0}','{1}')", login_id, info_id);
                        SqlCommand insertCommand4 = new SqlCommand(insertString4, con.conn);
                        int d = insertCommand4.ExecuteNonQuery();
                        con.conn.Close();
                    }
                    catch (Exception t)
                    {

                    }
                    MessageBox.Show("User registered Successfully.");
                    this.Hide();
                    new LoginForm().Show();
                }
                else
                {
                    MessageBox.Show("Password does not Match.");
                }

            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void label12_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
