using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smart_Study_Planner
{
    public partial class frmSplash : Form
    {
        int progress = 0;
        public frmSplash()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progress += 2;
            progressBar1.Value = progress;

            if(progress>=100)
            {
                timer1.Stop();

                LoginForm f = new LoginForm();
                f.Show();
                this.Hide();
            }

        }
    }
}
