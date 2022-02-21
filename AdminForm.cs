using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactSystem
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(new Point(0, 588), new Point(1062, 588),
                                                                      Color.FromArgb(0, 210, 255),
                                                                      Color.FromArgb(58, 123, 213)))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

        }

        private void Instagram(object sender, EventArgs e)
        {
            Process.Start("https://www.instagram.com/?hl=en");
        }

        private void Twitter(object sender, EventArgs e)
        {
            Process.Start("https://twitter.com/?lang=en");
        }

        private void Facebook(object sender, EventArgs e)
        {
            Process.Start("https://www.facebook.com/");
        }

        private void show(object sender, EventArgs e)
        {
            this.Hide();
            new AdminShowForm().Show();
        }

        private void loginInfo(object sender, EventArgs e)
        {
            this.Hide();
            new AdminLoginFrom().Show();
        }

        private void Profileupdate(object sender, EventArgs e)
        {
            this.Hide();
            new AdminProfileForm().Show();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MainForm().Show();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }
    }
}
