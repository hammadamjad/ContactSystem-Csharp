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
    public partial class CreateForm : Form
    {
        Connection con = new Connection();
        public CreateForm()
        {
            InitializeComponent();
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
                    if (numbe.Text.Equals(rdr["con_number"].ToString()))
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
            string add_id = string.Empty;
            string info_id = string.Empty;
            string detail_id = string.Empty;
            if (Check() == true)
            {
                MessageBox.Show("Contact with this number Already exists.");
            }
            else
            {
                
                try
                {
                    con.conn.Open();
                    //inserting into contact address.
                    string insertString1 = string.Format(@"insert into Contact_Address values('{0}','{1}')", add.Text, city.Text);
                    SqlCommand insertCommand1 = new SqlCommand(insertString1, con.conn);
                    insertCommand1.ExecuteNonQuery();
                    //getting the id from contact address.
                    string selectString1 = @"SELECT * FROM Contact_Address WHERE address_id=(SELECT max(address_id) FROM Contact_Address)";
                    SqlCommand cmd1 = new SqlCommand(selectString1, con.conn);
                    SqlDataReader rdr1 = cmd1.ExecuteReader();
                    while (rdr1.Read())
                    {
                        add_id = rdr1["address_id"].ToString();
                    }
                    con.conn.Close();
                }
                catch (Exception r)
                {

                }
                try
                {
                    con.conn.Open();
                    //inserting into contact info.
                    string insertString2 = string.Format(@"insert into Contact_Info values('{0}','{1}','{2}','{3}')"
                                                            , name.Text, numbe.Text, email.Text, add_id);
                    SqlCommand insertCommand2 = new SqlCommand(insertString2, con.conn);
                    insertCommand2.ExecuteNonQuery();
                    //getting the id from contact info.
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
                    ////getting detail id of the main user which is logged in
                    string selectString3 = string.Format(@"SELECT * FROM User_Details WHERE Login_id = {0}", LoginForm.id);
                    SqlCommand cmd3 = new SqlCommand(selectString3, con.conn);
                    SqlDataReader rdr3 = cmd3.ExecuteReader();
                    while (rdr3.Read())
                    {
                        detail_id = rdr3["detail_id"].ToString();
                    }
                    con.conn.Close();
                }
                catch (Exception f)
                {

                }
                try
                {
                    con.conn.Open();
                    string insertString4 = string.Format(@"insert into MyContacts values('{0}','{1}')", info_id, detail_id);
                    SqlCommand insertCommand4 = new SqlCommand(insertString4, con.conn);
                    insertCommand4.ExecuteNonQuery();
                    con.conn.Close();
                }
                catch (Exception g)
                {

                }
                MessageBox.Show("Contact Added Successfully");
                name.Text = string.Empty;
                numbe.Text = string.Empty;
                email.Text = string.Empty;
                add.Text = string.Empty;
                city.Text = string.Empty;

            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MainForm().Show();
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
