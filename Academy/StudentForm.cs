using DBtools;
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
        public StudentForm(int stud_id, string last_name, string first_name, string middle_name, string birth_date, string email, string phone, Image photo, int group)//MY CODE
                          : base(stud_id, last_name, first_name, middle_name, birth_date, email, phone, photo)
        {
            cbStudentsGroup.DataSource = DataBase.Connector.Load($"SELECT * FROM Student WHERE [group]= N'{group}'");
            cbStudentsGroup.DisplayMember = "direction_name";
            cbStudentsGroup.ValueMember = "direction_id";
        }


        protected override void buttonOK_Click(object sender, EventArgs e)
        {
            base.buttonOK_Click(sender, e);
            student = new Models.Student(human, (int)cbStudentsGroup.SelectedValue);
            if (student.id == 0) student.id = Convert.ToInt32(DataBase.Connector.Scalar($"INSERT Students({student.GetNames()}) VALUES({student.GetValues()});SELECT SCOPE_IDENTITY();"));
        }
    }
}
