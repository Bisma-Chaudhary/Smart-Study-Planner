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
    public partial class frmCalendar : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-DA9BHPM\SQLEXPRESS;Initial Catalog=SmartStudyPlanner;Integrated Security=True");
        public frmCalendar()
        {
            InitializeComponent();
        }
        private void LoadCalendarTasks(DateTime selectedDate)
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT TaskName, SubjectName, Priority FROM Tasks INNER JOIN Subjects ON Tasks.SubjectID = Subjects.SubjectID WHERE Deadline=@Deadline AND Tasks.UserID=@UserID",
                con);

            da.SelectCommand.Parameters.AddWithValue("@Deadline", selectedDate.Date);
            da.SelectCommand.Parameters.AddWithValue("@UserID", LoginForm.LoggedInUserID);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvCalendar.DataSource = dt;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            lblDate.Text = monthCalendar1.SelectionStart.ToString("dd MMMM yyyy");

            LoadCalendarTasks(monthCalendar1.SelectionStart);

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            frmDashboard d = new frmDashboard();
            d.Show();
            this.Hide();
        }

        private void dgvCalendar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmCalendar_Load(object sender, EventArgs e)
        {
            lblDate.Text = monthCalendar1.SelectionStart.ToString("dd MMMM yyyy");
            LoadCalendarTasks(monthCalendar1.SelectionStart);
        }
    }
}