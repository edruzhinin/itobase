namespace InventHelper
{
    partial class Form2
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
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txbModel = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txbModelNo = new System.Windows.Forms.TextBox();
            this.txbSerialNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txbInvNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.cmbUser = new System.Windows.Forms.ComboBox();
            this.txbRoom = new System.Windows.Forms.TextBox();
            this.txbComment = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbPlace = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.cbMOL_name = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(54, 37);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(121, 21);
            this.cmbType.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Тип";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Модель:";
            // 
            // txbModel
            // 
            this.txbModel.Location = new System.Drawing.Point(70, 6);
            this.txbModel.Name = "txbModel";
            this.txbModel.Size = new System.Drawing.Size(523, 20);
            this.txbModel.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(198, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Номер модели";
            // 
            // txbModelNo
            // 
            this.txbModelNo.Location = new System.Drawing.Point(299, 37);
            this.txbModelNo.Name = "txbModelNo";
            this.txbModelNo.Size = new System.Drawing.Size(294, 20);
            this.txbModelNo.TabIndex = 7;
            // 
            // txbSerialNo
            // 
            this.txbSerialNo.Location = new System.Drawing.Point(392, 70);
            this.txbSerialNo.Name = "txbSerialNo";
            this.txbSerialNo.Size = new System.Drawing.Size(201, 20);
            this.txbSerialNo.TabIndex = 8;
            this.txbSerialNo.TextChanged += new System.EventHandler(this.txbSerialNo_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(293, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Серийный номер";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Инв. №";
            // 
            // txbInvNo
            // 
            this.txbInvNo.Location = new System.Drawing.Point(110, 73);
            this.txbInvNo.Name = "txbInvNo";
            this.txbInvNo.Size = new System.Drawing.Size(148, 20);
            this.txbInvNo.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Пользователь";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 183);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Здание";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(251, 183);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Комната";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 213);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Комментарий";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(296, 115);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Состояние";
            // 
            // cmbState
            // 
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(392, 106);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(201, 21);
            this.cmbState.TabIndex = 17;
            // 
            // cmbUser
            // 
            this.cmbUser.FormattingEnabled = true;
            this.cmbUser.Location = new System.Drawing.Point(110, 147);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Size = new System.Drawing.Size(483, 21);
            this.cmbUser.TabIndex = 18;
            // 
            // txbRoom
            // 
            this.txbRoom.Location = new System.Drawing.Point(325, 175);
            this.txbRoom.Name = "txbRoom";
            this.txbRoom.Size = new System.Drawing.Size(130, 20);
            this.txbRoom.TabIndex = 20;
            // 
            // txbComment
            // 
            this.txbComment.Location = new System.Drawing.Point(110, 205);
            this.txbComment.Name = "txbComment";
            this.txbComment.Size = new System.Drawing.Size(483, 20);
            this.txbComment.TabIndex = 21;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(518, 285);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "Записать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbPlace
            // 
            this.cmbPlace.FormattingEnabled = true;
            this.cmbPlace.Location = new System.Drawing.Point(110, 178);
            this.cmbPlace.Name = "cmbPlace";
            this.cmbPlace.Size = new System.Drawing.Size(121, 21);
            this.cmbPlace.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 290);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "label1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(392, 285);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 25;
            this.button2.Text = "Удалить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cbMOL_name
            // 
            this.cbMOL_name.FormattingEnabled = true;
            this.cbMOL_name.Location = new System.Drawing.Point(110, 232);
            this.cbMOL_name.Name = "cbMOL_name";
            this.cbMOL_name.Size = new System.Drawing.Size(483, 21);
            this.cbMOL_name.TabIndex = 26;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 240);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "МОЛ:";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 332);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cbMOL_name);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPlace);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txbComment);
            this.Controls.Add(this.txbRoom);
            this.Controls.Add(this.cmbUser);
            this.Controls.Add(this.cmbState);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txbInvNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txbSerialNo);
            this.Controls.Add(this.txbModelNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txbModel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbType);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbModel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbModelNo;
        private System.Windows.Forms.TextBox txbSerialNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txbInvNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbState;
        private System.Windows.Forms.ComboBox cmbUser;
        private System.Windows.Forms.TextBox txbRoom;
        private System.Windows.Forms.TextBox txbComment;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbPlace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cbMOL_name;
        private System.Windows.Forms.Label label12;
    }
}