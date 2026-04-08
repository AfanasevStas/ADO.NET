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

namespace Academy
{
    public partial class MainForm : Form
    {
        Connector connector;
        public MainForm()
        {
            InitializeComponent();
            connector = new Connector(ConfigurationManager.ConnectionStrings["PV_522_Import"].ConnectionString);
            dgvStudents.DataSource = connector.Select("SELECT * FROM Students");
            dgvGroups.DataSource = connector.Select("SELECT * FROM Groups");
            dgvDirections.DataSource = connector.Select("SELECT * FROM Directions");
            dgvDisciplines.DataSource = connector.Select("SELECT * FROM Disciplines");
            dgvTeachers.DataSource = connector.Select("SELECT * FROM Teachers");
        }
    }
}
