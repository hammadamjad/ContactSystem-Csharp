using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactSystem
{
    public partial class UpdateForm : Form
    {

        Connection con = new Connection();
        public int user_add_id = 0;
        public int user_info_id = 0;
        public string user_num = string.Empty;

        public UpdateForm()
        {
            InitializeComponent();
            loadData();
        }

        public void loadData()
        {
            DataTable data = new DataTable();
            try
            {
                con.conn.Open();
                string selectString1 =
                        string.Format(@"select con_name,con_number from MyContacts
                                    full join Contact_Info  on MyContacts.Info_id = Contact_Info.info_id
                                    full join Contact_Address on Contact_Info.Address_id = Contact_Address.address_id 
                                    where Detail_id  = {0}", LoginForm.main_id);
                SqlDataAdapter sqlDa = new SqlDataAdapter(selectString1, con.conn);
                sqlDa.Fill(data);
                UpdateTable.DataSource = data;
                con.conn.Close();
            }
            catch (Exception t)
            {

            }
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
                    if (num.Text.Equals(rdr["con_number"].ToString())
                        && !num.Text.Equals(user_num))
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
                MessageBox.Show("Contact with this number Already exists.");
            }
            else
            {

                try
                {
                    con.conn.Open();
                    string selectString3 = string.Format(@"select info_id,Address_id from Contact_Info where con_number='{0}';", user_num);
                    SqlCommand cmd3 = new SqlCommand(selectString3, con.conn);
                    SqlDataReader rdr3 = cmd3.ExecuteReader();
                    while (rdr3.Read())
                    {
                        user_info_id = int.Parse(rdr3["info_id"].ToString());
                        user_add_id = int.Parse(rdr3["Address_id"].ToString());
                    }
                    con.conn.Close();
                }
                catch (Exception f)
                {
                    MessageBox.Show(f.Message);
                }
                try
                {
                    con.conn.Open();
                    string insertString1 = string.Format(@"update Contact_Address set address_1='{0}',city='{1}'
                                                       where address_id = {2};", address.Text, cit.Text, user_add_id);
                    SqlCommand insertCommand1 = new SqlCommand(insertString1, con.conn);
                    insertCommand1.ExecuteNonQuery();
                    con.conn.Close();
                }
                catch (Exception r)
                {
                    MessageBox.Show(r.Message);

                }
                try
                {
                    con.conn.Open();
                    string insertString2 = string.Format(@"update Contact_Info set con_name='{0}',con_number='{1}',
                                                con_email='{2}' where info_id = {3}", name.Text, num.Text, email.Text, user_info_id);
                    SqlCommand insertCommand2 = new SqlCommand(insertString2, con.conn);
                    insertCommand2.ExecuteNonQuery();
                    con.conn.Close();
                }
                catch (Exception g)
                {

                }
            }
            MessageBox.Show("Contact Updated Scuccessfully");
            name.Text = string.Empty;
            num.Text = string.Empty;
            email.Text = string.Empty;
            address.Text = string.Empty;
            cit.Text = string.Empty;
            loadData();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MainForm().Show();
        }

        private void clicking(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.UpdateTable.Rows[e.RowIndex];
                try
                {
                    con.conn.Open();
                    string selectString1 =
                            string.Format(@"select con_name,con_number,con_email,address_1,city from MyContacts
                                    full join Contact_Info  on MyContacts.Info_id = Contact_Info.info_id
                                    full join Contact_Address on Contact_Info.Address_id = Contact_Address.address_id 
                                    where Detail_id  = {0} and con_number = '{1}'", LoginForm.main_id, row.Cells["Number"].Value.ToString());
                    SqlCommand cmd = new SqlCommand(selectString1, con.conn);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        name.Text = rdr["con_name"].ToString();
                        num.Text = rdr["con_number"].ToString();
                        email.Text = rdr["con_email"].ToString();
                        address.Text = rdr["address_1"].ToString();
                        cit.Text = rdr["city"].ToString();
                        user_num = rdr["con_number"].ToString();
                    }
                    con.conn.Close();
                }
                catch (Exception t)
                {

                }
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
    }
}
