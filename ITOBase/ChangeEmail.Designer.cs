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
            this.cbMakeMainEmail = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txbEmail
            // 
            this.txbEmail.Location = new System.Drawing.Point(16, 26);
            this.txbEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txbEmail.Name = "txbEmail";
            this.txbEmail.Size = new System.Drawing.Size(345, 22);
            this.txbEmail.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(261, 70);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbMakeMainEmail
            // 
            this.cbMakeMainEmail.AutoSize = true;
            this.cbMakeMainEmail.Location = new System.Drawing.Point(16, 55);
            this.cbMakeMainEmail.Name = "cbMakeMainEmail";
            this.cbMakeMainEmail.Size = new System.Drawing.Size(152, 21);
            this.cbMakeMainEmail.TabIndex = 2;
            this.cbMakeMainEmail.Text = "сделать основным";
            this.cbMakeMainEmail.UseVisualStyleBackColor = true;
            // 
            // ChangeEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 116);
            this.Controls.Add(this.cbMakeMainEmail);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txbEmail);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ChangeEmail";
            this.Text = "ChangeEmail";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbEmail;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox cbMakeMainEmail;
    }
}