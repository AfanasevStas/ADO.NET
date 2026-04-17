namespace Academy
{
    partial class TeacherForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtpWorkSince = new System.Windows.Forms.DateTimePicker();
            this.textBoxRate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dtpWorkSince
            // 
            this.dtpWorkSince.CustomFormat = "yyyy-MMMM-dd";
            this.dtpWorkSince.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpWorkSince.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpWorkSince.Location = new System.Drawing.Point(206, 278);
            this.dtpWorkSince.Name = "dtpWorkSince";
            this.dtpWorkSince.Size = new System.Drawing.Size(251, 31);
            this.dtpWorkSince.TabIndex = 17;
            // 
            // textBoxRate
            // 
            this.textBoxRate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBoxRate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.textBoxRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxRate.Location = new System.Drawing.Point(206, 323);
            this.textBoxRate.Name = "textBoxRate";
            this.textBoxRate.Size = new System.Drawing.Size(251, 31);
            this.textBoxRate.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(14, 282);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 25);
            this.label1.TabIndex = 19;
            this.label1.Text = "Начало работы:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(72, 325);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 25);
            this.label2.TabIndex = 20;
            this.label2.Text = "Зарплата:";
            // 
            // TeacherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 419);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxRate);
            this.Controls.Add(this.dtpWorkSince);
            this.Name = "TeacherForm";
            this.Text = "TeacherForm";
            this.Controls.SetChildIndex(this.dtpWorkSince, 0);
            this.Controls.SetChildIndex(this.textBoxRate, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpWorkSince;
        private System.Windows.Forms.TextBox textBoxRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}