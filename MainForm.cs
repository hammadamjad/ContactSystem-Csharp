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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(new Point(0, 588), new Point(1062, 588),
                                                                      Color.FromArgb(255, 128, 8),
                                                                      Color.FromArgb(255, 200, 55)))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

        }
        private void Create(object sender, EventArgs e)
        {
            this.Hide();
            new CreateForm().Show();
        }

        private void Search(object sender, EventArgs e)
        {
            this.Hide();
            new SearchForm().Show();
        }

        private void Exit(object sender, EventArgs e)
        {

        }

        private void Update(object sender, EventArgs e)
        {
            this.Hide();
            new UpdateForm().Show();
        }

        private void Delete(object sender, EventArgs e)
        {
            this.Hide();
            new DeleteForm().Show();
        }

        private void List(object sender, EventArgs e)
        {
            this.Hide();
            new ListForm().Show();
        }

        private void Facebook(object sender, EventArgs e)
        {
            Process.Start("https://www.facebook.com/");
        }

        private void Instagram(object sender, EventArgs e)
        {
            Process.Start("https://www.instagram.com/?hl=en");
        }

        private void Twitter(object sender, EventArgs e)
        {
            Process.Start("https://twitter.com/?lang=en");
        }

        private void Adminform(object sender, EventArgs e)
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

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            this.Hide();
            new LoginForm().Show();
        }
    }
}
