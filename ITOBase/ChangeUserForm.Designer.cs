namespace ITOBase
{
    partial class ChangeUserForm
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
            this.txbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txbSecondName = new System.Windows.Forms.TextBox();
            this.txbLastName = new System.Windows.Forms.TextBox();
            this.dtpBirthday = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cbDepartment = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbPhone = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbPhoneType = new System.Windows.Forms.ListBox();
            this.btnAddPhone = new System.Windows.Forms.Button();
            this.cbPhoneType = new System.Windows.Forms.ComboBox();
            this.txbPhone = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnAddEmail = new System.Windows.Forms.Button();
            this.txbEmail = new System.Windows.Forms.TextBox();
            this.txbRoom = new System.Windows.Forms.TextBox();
            this.cbWorkPlace = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txbLogin = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbPosition = new System.Windows.Forms.ComboBox();
            this.btnEmailFromLogin = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.chbAddAD = new System.Windows.Forms.CheckBox();
            this.cbLog = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxNotCheckEmail = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txbName
            // 
            this.txbName.Location = new System.Drawing.Point(84, 21);
            this.txbName.Name = "txbName";
            this.txbName.Size = new System.Drawing.Size(148, 20);
            this.txbName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Имя:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Отчество:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Фамилия:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txbSecondName
            // 
            this.txbSecondName.Location = new System.Drawing.Point(84, 54);
            this.txbSecondName.Name = "txbSecondName";
            this.txbSecondName.Size = new System.Drawing.Size(148, 20);
            this.txbSecondName.TabIndex = 4;
            // 
            // txbLastName
            // 
            this.txbLastName.Location = new System.Drawing.Point(84, 81);
            this.txbLastName.Name = "txbLastName";
            this.txbLastName.Size = new System.Drawing.Size(148, 20);
            this.txbLastName.TabIndex = 5;
            // 
            // dtpBirthday
            // 
            this.dtpBirthday.Location = new System.Drawing.Point(342, 18);
            this.dtpBirthday.Name = "dtpBirthday";
            this.dtpBirthday.Size = new System.Drawing.Size(184, 20);
            this.dtpBirthday.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(247, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Дата рождения:";
            // 
            // cbDepartment
            // 
            this.cbDepartment.FormattingEnabled = true;
            this.cbDepartment.Location = new System.Drawing.Point(343, 53);
            this.cbDepartment.Name = "cbDepartment";
            this.cbDepartment.Size = new System.Drawing.Size(183, 21);
            this.cbDepartment.TabIndex = 8;
            this.cbDepartment.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(247, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Подразделение:";
            // 
            // lbPhone
            // 
            this.lbPhone.FormattingEnabled = true;
            this.lbPhone.Location = new System.Drawing.Point(104, 166);
            this.lbPhone.Name = "lbPhone";
            this.lbPhone.Size = new System.Drawing.Size(128, 95);
            this.lbPhone.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Телефоны:";
            // 
            // lbPhoneType
            // 
            this.lbPhoneType.FormattingEnabled = true;
            this.lbPhoneType.Location = new System.Drawing.Point(20, 166);
            this.lbPhoneType.Name = "lbPhoneType";
            this.lbPhoneType.Size = new System.Drawing.Size(78, 95);
            this.lbPhoneType.TabIndex = 12;
            // 
            // btnAddPhone
            // 
            this.btnAddPhone.Location = new System.Drawing.Point(152, 294);
            this.btnAddPhone.Name = "btnAddPhone";
            this.btnAddPhone.Size = new System.Drawing.Size(80, 23);
            this.btnAddPhone.TabIndex = 13;
            this.btnAddPhone.Text = "Добавить";
            this.btnAddPhone.UseVisualStyleBackColor = true;
            this.btnAddPhone.Click += new System.EventHandler(this.btnAddPhone_Click);
            // 
            // cbPhoneType
            // 
            this.cbPhoneType.FormattingEnabled = true;
            this.cbPhoneType.Location = new System.Drawing.Point(20, 267);
            this.cbPhoneType.Name = "cbPhoneType";
            this.cbPhoneType.Size = new System.Drawing.Size(78, 21);
            this.cbPhoneType.TabIndex = 14;
            // 
            // txbPhone
            // 
            this.txbPhone.Location = new System.Drawing.Point(104, 268);
            this.txbPhone.Name = "txbPhone";
            this.txbPhone.Size = new System.Drawing.Size(128, 20);
            this.txbPhone.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(250, 146);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Электронная почта:";
            // 
            // btnAddEmail
            // 
            this.btnAddEmail.Location = new System.Drawing.Point(451, 294);
            this.btnAddEmail.Name = "btnAddEmail";
            this.btnAddEmail.Size = new System.Drawing.Size(75, 23);
            this.btnAddEmail.TabIndex = 18;
            this.btnAddEmail.Text = "Добавить";
            this.btnAddEmail.UseVisualStyleBackColor = true;
            this.btnAddEmail.Click += new System.EventHandler(this.btnAddEmail_Click);
            // 
            // txbEmail
            // 
            this.txbEmail.Location = new System.Drawing.Point(250, 166);
            this.txbEmail.Name = "txbEmail";
            this.txbEmail.Size = new System.Drawing.Size(276, 20);
            this.txbEmail.TabIndex = 19;
            this.txbEmail.TextChanged += new System.EventHandler(this.txbEmail_TextChanged);
            // 
            // txbRoom
            // 
            this.txbRoom.Location = new System.Drawing.Point(452, 112);
            this.txbRoom.Name = "txbRoom";
            this.txbRoom.Size = new System.Drawing.Size(74, 20);
            this.txbRoom.TabIndex = 20;
            // 
            // cbWorkPlace
            // 
            this.cbWorkPlace.FormattingEnabled = true;
            this.cbWorkPlace.Location = new System.Drawing.Point(250, 112);
            this.cbWorkPlace.Name = "cbWorkPlace";
            this.cbWorkPlace.Size = new System.Drawing.Size(192, 21);
            this.cbWorkPlace.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Логин";
            // 
            // txbLogin
            // 
            this.txbLogin.Location = new System.Drawing.Point(87, 113);
            this.txbLogin.Name = "txbLogin";
            this.txbLogin.Size = new System.Drawing.Size(145, 20);
            this.txbLogin.TabIndex = 23;
            this.txbLogin.TextChanged += new System.EventHandler(this.txbLogin_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(250, 81);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Должность:";
            // 
            // cbPosition
            // 
            this.cbPosition.FormattingEnabled = true;
            this.cbPosition.Location = new System.Drawing.Point(342, 78);
            this.cbPosition.Name = "cbPosition";
            this.cbPosition.Size = new System.Drawing.Size(184, 21);
            this.cbPosition.TabIndex = 25;
            // 
            // btnEmailFromLogin
            // 
            this.btnEmailFromLogin.Location = new System.Drawing.Point(367, 293);
            this.btnEmailFromLogin.Name = "btnEmailFromLogin";
            this.btnEmailFromLogin.Size = new System.Drawing.Size(75, 23);
            this.btnEmailFromLogin.TabIndex = 26;
            this.btnEmailFromLogin.Text = "из логина";
            this.btnEmailFromLogin.UseVisualStyleBackColor = true;
            this.btnEmailFromLogin.Click += new System.EventHandler(this.btnEmailFromLogin_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(452, 347);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 27;
            this.btnSave.Text = "Записать";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chbAddAD
            // 
            this.chbAddAD.AutoSize = true;
            this.chbAddAD.Location = new System.Drawing.Point(347, 351);
            this.chbAddAD.Name = "chbAddAD";
            this.chbAddAD.Size = new System.Drawing.Size(95, 17);
            this.chbAddAD.TabIndex = 28;
            this.chbAddAD.Text = "Создать в AD";
            this.chbAddAD.UseVisualStyleBackColor = true;
            // 
            // cbLog
            // 
            this.cbLog.FormattingEnabled = true;
            this.cbLog.Location = new System.Drawing.Point(20, 351);
            this.cbLog.Name = "cbLog";
            this.cbLog.Size = new System.Drawing.Size(316, 21);
            this.cbLog.TabIndex = 29;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 312);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 31;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBoxNotCheckEmail
            // 
            this.checkBoxNotCheckEmail.AutoSize = true;
            this.checkBoxNotCheckEmail.Location = new System.Drawing.Point(250, 192);
            this.checkBoxNotCheckEmail.Name = "checkBoxNotCheckEmail";
            this.checkBoxNotCheckEmail.Size = new System.Drawing.Size(195, 17);
            this.checkBoxNotCheckEmail.TabIndex = 32;
            this.checkBoxNotCheckEmail.Text = "Пропустить проверку на сервере";
            this.checkBoxNotCheckEmail.UseVisualStyleBackColor = true;
            this.checkBoxNotCheckEmail.Visible = false;
            // 
            // ChangeUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 382);
            this.Controls.Add(this.checkBoxNotCheckEmail);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbLog);
            this.Controls.Add(this.chbAddAD);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnEmailFromLogin);
            this.Controls.Add(this.cbPosition);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txbLogin);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbWorkPlace);
            this.Controls.Add(this.txbRoom);
            this.Controls.Add(this.txbEmail);
            this.Controls.Add(this.btnAddEmail);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txbPhone);
            this.Controls.Add(this.cbPhoneType);
            this.Controls.Add(this.btnAddPhone);
            this.Controls.Add(this.lbPhoneType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbPhone);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbDepartment);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpBirthday);
            this.Controls.Add(this.txbLastName);
            this.Controls.Add(this.txbSecondName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txbName);
            this.Name = "ChangeUserForm";
            this.Text = "ChangeUserForm";
            this.Load += new System.EventHandler(this.ChangeUserForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbSecondName;
        private System.Windows.Forms.TextBox txbLastName;
        private System.Windows.Forms.DateTimePicker dtpBirthday;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbDepartment;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lbPhone;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lbPhoneType;
        private System.Windows.Forms.Button btnAddPhone;
        private System.Windows.Forms.ComboBox cbPhoneType;
        private System.Windows.Forms.TextBox txbPhone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnAddEmail;
        private System.Windows.Forms.TextBox txbEmail;
        private System.Windows.Forms.TextBox txbRoom;
        private System.Windows.Forms.ComboBox cbWorkPlace;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txbLogin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbPosition;
        private System.Windows.Forms.Button btnEmailFromLogin;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chbAddAD;
        private System.Windows.Forms.ComboBox cbLog;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBoxNotCheckEmail;
    }
}