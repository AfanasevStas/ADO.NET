using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Data.SqlClient;

namespace Academy
{
    public partial class HumanForm : Form
    {
        internal Models.Human human;
        public HumanForm()
        {
            InitializeComponent();
        }
        public HumanForm(int stud_id, string last_name, string first_name, string middle_name, string birth_date, string email, string phone, Image photo)
        {
            textBoxLastName.Text = last_name;
            textBoxFirstName.Text = first_name;
            textBoxMiddleName.Text = middle_name;
            textBoxMiddleName.Text = middle_name;
            dtpBirthDate.Text = birth_date;
            textBoxEmail.Text = email;
            textBoxPhone.Text = phone;
            pictureBoxPhoto.Image = photo;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        protected virtual void Compress()
        {
            human = new Models.Human
                (
                   Convert.ToInt32(labelID.Text == "" ? "0" : labelID.Text.Split(':').Last()),
                   textBoxLastName.Text,
                   textBoxFirstName.Text,
                   textBoxMiddleName.Text,
                   dtpBirthDate.Value.ToString("yyyy-MM-dd"),
                   textBoxEmail.Text,
                   textBoxPhone.Text,
                   pictureBoxPhoto.Image
                );
        }
        protected virtual void buttonOK_Click(object sender, EventArgs e)
        {
            Compress();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBoxPhoto.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBoxPhoto.Image = Image.FromFile(ofd.FileName);
                }
        }
    }
}
