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

namespace Smart_Study_Planner
{
    public partial class frmProgress : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-DA9BHPM\SQLEXPRESS;Initial Catalog=SmartStudyPlanner;Integrated Security=True");
        public frmProgress()
        {
            InitializeComponent();
        }
        private void LoadProgress()
        {
            SqlDataAdapter da = new SqlDataAdapter(
   @"SELECT
    Subjects.SubjectName,
    COUNT(Tasks.TaskID) AS [Total Tasks],
    ISNULL (SUM(CASE WHEN Status='Completed' THEN 1 ELSE 0 END),0) AS [Completed Tasks]
FROM Subjects
LEFT JOIN Tasks
ON Subjects.SubjectID = Tasks.SubjectID
WHERE Subjects.UserID=@UserID
GROUP BY Subjects.SubjectName", con);

            da.SelectCommand.Parameters.AddWithValue("@UserID", LoginForm.LoggedInUserID);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dt.Columns.Add("Progress");

            foreach (DataRow row in dt.Rows)
            {
                int total = Convert.ToInt32(row["Total Tasks"]);
                int completed = Convert.ToInt32(row["Completed Tasks"]);

                if (total == 0)
                    row["Progress"] = "0%";
                else
                    row["Progress"] = ((completed * 100) / total) + "%";
            }

            dgvProgress.DataSource = dt;
        }
        private void LoadOverallProgress()
        {
            con.Open();

            SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM Tasks WHERE UserID=@UserID", con);
            cmd1.Parameters.AddWithValue("@UserID", LoginForm.LoggedInUserID);
            int total = (int)cmd1.ExecuteScalar();

            SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM Tasks WHERE Status='Completed'AND UserID=@UserID", con);
            cmd2.Parameters.AddWithValue("@UserID", LoginForm.LoggedInUserID);
            int completed = (int)cmd2.ExecuteScalar();

            con.Close();

            int percentage = 0;

            if (total > 0)
                percentage = (completed * 100) / total;

            progressBar1.Value = percentage;
            lblOverallProgress.Text = percentage + "%";
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void frmProgress_Load(object sender, EventArgs e)
        {
           
        
            LoadProgress();
            LoadOverallProgress();
        
    }

        private void btnHome_Click(object sender, EventArgs e)
        {
            frmDashboard d = new frmDashboard();
            d.Show();
            this.Hide();
        }

        private void lblOverallProgress_Click(object sender, EventArgs e)
        {

        }
    }
}
