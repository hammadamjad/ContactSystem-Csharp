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
    public partial class DeleteForm : Form
    {
        string con_number = string.Empty;
        public DeleteForm()
        {
            InitializeComponent();
            loadData();
        }


        public void loadData()
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
                                    where Detail_id  = {0}", LoginForm.main_id);
                SqlDataAdapter sqlDa = new SqlDataAdapter(selectString1, con.conn);
                sqlDa.Fill(data);
                DeleteTable.DataSource = data;
                con.conn.Close();
            }
            catch (Exception t)
            {

            }
        }


        public int getAddress_id(string num )
        {
            int id=0;
            Connection con = new Connection();
            try
            {
                con.conn.Open();
                string selectString2 = string.Format(@"select Address_id from Contact_Info where con_number = '{0}'", num);

                SqlCommand cmd2 = new SqlCommand(selectString2, con.conn);
                SqlDataReader rdr2 = cmd2.ExecuteReader();
                while (rdr2.Read())
                {
                    id =  int.Parse(rdr2["Address_id"].ToString());
                }
                con.conn.Close();
                return id;
            }
            catch (Exception t)
            {

            }
            return 0;
        }

        private void CellSelect(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.DeleteTable.Rows[e.RowIndex];
                con_number = row.Cells["Num"].Value.ToString();
            }
        }
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {

            Connection con = new Connection();
            try
            {
                con.conn.Open();
                int id = getAddress_id(con_number);
                string deleteString2 = string.Format(@"delete  from Contact_Address where address_id = {0}",id);

                SqlCommand deleteCommand2 = new SqlCommand(deleteString2, con.conn);
                deleteCommand2.ExecuteNonQuery();
                con.conn.Close();
            }
            catch (Exception t)
            {

            }
            MessageBox.Show("Contact deleted Successfully");
            loadData();
        }
       

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
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
