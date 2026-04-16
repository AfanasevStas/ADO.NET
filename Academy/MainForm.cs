using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;
using DBtools;
using System.Data.SqlClient;

namespace Academy
{
    public partial class MainForm : Form
    {
        Connector connector;
        DataGridView[] tables;
        Query[] queries =
            {
                new Query("stud_id,last_name,first_name,middle_name,birth_date,group_name,direction_name",
                            "Students,Groups,Directions",
                            "[group]=group_id AND direction=direction_id"),
                new Query("group_id,group_name,start_date,start_time,learning_days,direction_name",
                            "Groups,Directions",
                            "direction=direction_id"),
                new Query("*", "Directions"),
                new Query("*", "Disciplines"),
                new Query("*", "Teachers")
            };
        public MainForm()
        {
            InitializeComponent();
            tables = new DataGridView[] { dgvStudents,dgvGroups,dgvDirections,dgvDisciplines,dgvTeachers };
            connector = new Connector(ConfigurationManager.ConnectionStrings["PV_522_Import"].ConnectionString);
            dgvDirections.DataSource = connector.Select("SELECT * FROM Directions");
            tabControl_SelectedIndexChanged(tabControl, null);

            cbGroupsDirection.DataSource = connector.Load("SELECT * FROM Directions");
            cbGroupsDirection.DisplayMember = "direction_name";
            cbGroupsDirection.ValueMember = "direction_id";
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = (sender as TabControl).SelectedIndex;
            tables[i].DataSource = connector.Load(queries[i].ToString());
            //tables[i].DataSource = connector.Select("*",tabControl.SelectedTab.Text);
            toolStripStatusLabel.Text = $"Количество записей: {tables[i].RowCount - 1}";
        }

        private void cbGroupsDirection_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dgvGroups.DataSource = connector.Load(queries[1].ToString() + $" AND direction={cbGroupsDirection.SelectedValue}");
            toolStripStatusLabel.Text = $"Количество записей: {dgvGroups.RowCount - 1}";
        }

        private void buttonAddStudent_Click(object sender, EventArgs e)
        {
            StudentForm student = new StudentForm();
            student.ShowDialog();
            dgvStudents.DataSource = connector.Select("SELECT * FROM Students");//MY CODE
        }

        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)//MY CODE
        {
            if (e.RowIndex >= 0)
            {
                dgvStudents.ClearSelection();
                dgvStudents.Rows[e.RowIndex].Selected = true;
            }
        }

        private void dgvStudents_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)//MY CODE
        {
            DataGridViewRow row = dgvStudents.Rows[e.RowIndex];
            int stud_id = Convert.ToInt32(row.Cells[0].Value);
            string last_name = row.Cells[1].Value.ToString();
            string first_name = row.Cells[2].Value.ToString();
            string middle_name = row.Cells[3].Value.ToString();
            string birth_date = row.Cells[4].Value.ToString();
            string group = row.Cells[5].Value.ToString();

            StudentUPDATE student = new StudentUPDATE(stud_id,
                                                      last_name,
                                                      first_name,
                                                      middle_name,
                                                      birth_date,
                                                      group);
            student.ShowDialog();
            dgvStudents.DataSource = connector.Select("SELECT * FROM Students");
            //D:\Загрузки Яндекс
        }
    }
}
