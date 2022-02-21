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
    public partial class AdminShowForm : Form
    {
        public AdminShowForm()
        {
            InitializeComponent();
            load();
        }


        public void load()
        {
            Connection con = new Connection();
            try
            {
                con.conn.Open();
                string selectString1 = string.Format(@"select * from User_Details
                                                full join Contact_Info on Contact_Info.info_id = User_Details.Info_id
                                                full join Contact_Address on Contact_Info.Address_id = Contact_Address.address_id
                                                full join Contact_Login on Contact_Login.login_id = User_Details.Login_id
                                                where detail_id = {0}", LoginForm.main_id);
                SqlCommand cmd1 = new SqlCommand(selectString1, con.conn);
                SqlDataReader rdr1 = cmd1.ExecuteReader();
                while (rdr1.Read())
                {
                    name.Text = rdr1["con_name"].ToString();
                    num.Text = rdr1["con_number"].ToString();
                    email.Text = rdr1["con_email"].ToString();
                    add.Text = rdr1["address_1"].ToString();
                    city.Text = rdr1["city"].ToString();
                    Uname.Text = rdr1["username"].ToString();
                    Pass.Text = rdr1["userpassword"].ToString();
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

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AdminForm().Show();
        }
    }
}
