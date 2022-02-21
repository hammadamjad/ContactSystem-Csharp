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
    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            DataTable data = new DataTable();
            Connection con = new Connection();
            try
            {
                con.conn.Open();
                string selectString1 =
                        string.Format(@"select con_name,con_number,con_email,address_1,city from MyContacts
                                    full join Contact_Info  on MyContacts.Info_id = Contact_Info.info_id
                                    full join Contact_Address on Contact_Info.Address_id = Contact_Address.address_id 
                                    where Detail_id  = {0} and con_number = '{1}'", LoginForm.main_id,number.Text);
                SqlDataAdapter sqlDa = new SqlDataAdapter(selectString1, con.conn);
                sqlDa.Fill(data);
                SearchTable.DataSource = data;
                con.conn.Close();
            }
            catch (Exception t)
            {

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
