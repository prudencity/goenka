namespace Finance_Management_System
{
    partial class Frm_SqlSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SqlSettings));
            this.txt_servername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_Auth_Mode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_userName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.lbldatabase = new System.Windows.Forms.Label();
            this.cbodatabase = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txt_servername
            // 
            this.txt_servername.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_servername.Location = new System.Drawing.Point(140, 21);
            this.txt_servername.Name = "txt_servername";
            this.txt_servername.Size = new System.Drawing.Size(191, 21);
            this.txt_servername.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(137, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "[SERVER NAME]\\SQLEXPRESS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Authentication Mode";
            // 
            // cmb_Auth_Mode
            // 
            this.cmb_Auth_Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Auth_Mode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Auth_Mode.FormattingEnabled = true;
            this.cmb_Auth_Mode.Items.AddRange(new object[] {
            "Windows Authantication",
            "SQL Server Authantication"});
            this.cmb_Auth_Mode.Location = new System.Drawing.Point(140, 68);
            this.cmb_Auth_Mode.Name = "cmb_Auth_Mode";
            this.cmb_Auth_Mode.Size = new System.Drawing.Size(191, 21);
            this.cmb_Auth_Mode.TabIndex = 1;
            this.cmb_Auth_Mode.SelectedIndexChanged += new System.EventHandler(this.cmb_Auth_Mode_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "User Name";
            // 
            // txt_userName
            // 
            this.txt_userName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_userName.Location = new System.Drawing.Point(140, 95);
            this.txt_userName.Name = "txt_userName";
            this.txt_userName.Size = new System.Drawing.Size(191, 21);
            this.txt_userName.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(17, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Password";
            // 
            // txt_password
            // 
            this.txt_password.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_password.Location = new System.Drawing.Point(140, 122);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(191, 21);
            this.txt_password.TabIndex = 3;
            this.txt_password.UseSystemPasswordChar = true;
            // 
            // btn_save
            // 
            this.btn_save.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.Image = ((System.Drawing.Image)(resources.GetObject("btn_save.Image")));
            this.btn_save.Location = new System.Drawing.Point(253, 176);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(78, 34);
            this.btn_save.TabIndex = 4;
            this.btn_save.Text = "Save";
            this.btn_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancel.Location = new System.Drawing.Point(15, 40);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(55, 23);
            this.btn_cancel.TabIndex = 5;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Visible = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // lbldatabase
            // 
            this.lbldatabase.AutoSize = true;
            this.lbldatabase.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbldatabase.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldatabase.Location = new System.Drawing.Point(17, 152);
            this.lbldatabase.Name = "lbldatabase";
            this.lbldatabase.Size = new System.Drawing.Size(61, 13);
            this.lbldatabase.TabIndex = 11;
            this.lbldatabase.Text = "DataBase";
            this.lbldatabase.Click += new System.EventHandler(this.lbldatabase_Click);
            // 
            // cbodatabase
            // 
            this.cbodatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbodatabase.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbodatabase.FormattingEnabled = true;
            this.cbodatabase.Location = new System.Drawing.Point(140, 149);
            this.cbodatabase.Name = "cbodatabase";
            this.cbodatabase.Size = new System.Drawing.Size(191, 21);
            this.cbodatabase.TabIndex = 12;
            // 
            // Frm_SqlSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 213);
            this.ControlBox = false;
            this.Controls.Add(this.cbodatabase);
            this.Controls.Add(this.lbldatabase);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_userName);
            this.Controls.Add(this.cmb_Auth_Mode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_servername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_SqlSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL Settings";
            this.Load += new System.EventHandler(this.frm_SqlSettings_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_SqlSettings_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_SqlSettings_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_cancel;
        public System.Windows.Forms.TextBox txt_servername;
        public System.Windows.Forms.ComboBox cmb_Auth_Mode;
        public System.Windows.Forms.TextBox txt_userName;
        public System.Windows.Forms.TextBox txt_password;
        public System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Label lbldatabase;
        public System.Windows.Forms.ComboBox cbodatabase;
    }
}