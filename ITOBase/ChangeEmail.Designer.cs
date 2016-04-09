namespace ITOBase
{
    partial class ChangeEmail
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
            this.txbEmail = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnMakeMain = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txbEmail
            // 
            this.txbEmail.Location = new System.Drawing.Point(12, 21);
            this.txbEmail.Name = "txbEmail";
            this.txbEmail.Size = new System.Drawing.Size(260, 20);
            this.txbEmail.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(197, 93);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnMakeMain
            // 
            this.btnMakeMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMakeMain.Location = new System.Drawing.Point(135, 47);
            this.btnMakeMain.Name = "btnMakeMain";
            this.btnMakeMain.Size = new System.Drawing.Size(137, 23);
            this.btnMakeMain.TabIndex = 2;
            this.btnMakeMain.Text = "Сделать основным";
            this.btnMakeMain.UseVisualStyleBackColor = true;
            this.btnMakeMain.Click += new System.EventHandler(this.btnMakeMain_Click);
            // 
            // ChangeEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 128);
            this.Controls.Add(this.btnMakeMain);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txbEmail);
            this.Name = "ChangeEmail";
            this.Text = "ChangeEmail";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbEmail;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnMakeMain;
    }
}