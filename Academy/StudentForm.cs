using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Academy
{
    public partial class StudentForm : HumanForm
    {
        Models.Student student;
        public StudentForm()
        {
            InitializeComponent();
            cbStudentsGroup.DataSource = DataBase.Connector.Load("SELECT * FROM Groups");
            cbStudentsGroup.DisplayMember = "group_name";
            cbStudentsGroup.ValueMember = "group_id";
        }

        protected override void buttonOK_Click(object sender, EventArgs e)
        {
            base.buttonOK_Click(sender, e);
            student = new Models.Student(human, (int)cbStudentsGroup.SelectedValue);
            if (student.id == 0) student.id = Convert.ToInt32(DataBase.Connector.Scalar($"INSERT Students({student.GetNames()}) VALUES({student.GetValues()});SELECT SCOPE_IDENTITY();"));
        }
    }
}
