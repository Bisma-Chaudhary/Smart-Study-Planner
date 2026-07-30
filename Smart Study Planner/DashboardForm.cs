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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Smart_Study_Planner
{
    public partial class frmDashboard : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-DA9BHPM\SQLEXPRESS;Initial Catalog=SmartStudyPlanner;Integrated Security=True");
        public frmDashboard()
        {
            InitializeComponent();
        }

        private void panelSide_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSubject_Click(object sender, EventArgs e)
        {
            frmSubjects f = new frmSubjects();
            f.Show();
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            frmTasks f = new frmTasks();
            f.Show();
        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {
            frmCalendar f = new frmCalendar();
            f.Show();
        }

        private void btnProgress_Click(object sender, EventArgs e)
        {
            frmProgress f = new frmProgress();
            f.Show();
        }

        private void btnSettins_Click(object sender, EventArgs e)
        {
            frmSetting f = new frmSetting();
            f.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            con.Open();

            // Total Subjects
            SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM Subjects WHERE UserID=@UserID", con);
            cmd1.Parameters.AddWithValue("@UserID", LoginForm.LoggedInUserID);
            lblTotalSubjects.Text = cmd1.ExecuteScalar().ToString();

            // Total Tasks
            SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM Tasks WHERE UserID=@UserID", con);
            cmd2.Parameters.AddWithValue("@UserID", LoginForm.LoggedInUserID);
            lblTotalTasks.Text = cmd2.ExecuteScalar().ToString();

            // Progress
            SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM Tasks WHERE Status='Completed' AND UserID=@UserID", con);
            cmd3.Parameters.AddWithValue("@UserID", LoginForm.LoggedInUserID);
            int completed = Convert.ToInt32(cmd3.ExecuteScalar());

            SqlCommand cmd4 = new SqlCommand("SELECT COUNT(*) FROM Tasks WHERE UserID=@UserID", con);
            cmd4.Parameters.AddWithValue("@UserID", LoginForm.LoggedInUserID);
            int total = Convert.ToInt32(cmd4.ExecuteScalar());

            if (total > 0)
            {
                int percent = (completed * 100) / total;
                lblProgress.Text = percent + "%";
            }
            else
            {
                lblProgress.Text = "0%";
            }
            con.Close();
        }

        private void lblProgress_Click(object sender, EventArgs e)
        {

        }
    }
}
