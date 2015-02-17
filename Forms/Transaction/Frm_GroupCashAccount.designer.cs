namespace Finance_Management_System
{
    partial class Frm_GroupCashAccount
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_GroupCashAccount));
            this.lblCoName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LstItem = new vControls.vListBox();
            this.DgvTransport = new System.Windows.Forms.DataGridView();
            this.ColTName = new vControls.vListBoxColumn();
            this.ColTId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DgvTransport)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCoName
            // 
            this.lblCoName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCoName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCoName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCoName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCoName.ForeColor = System.Drawing.Color.White;
            this.lblCoName.Location = new System.Drawing.Point(0, 49);
            this.lblCoName.Name = "lblCoName";
            this.lblCoName.Size = new System.Drawing.Size(433, 26);
            this.lblCoName.TabIndex = 1390;
            this.lblCoName.Text = "Ashok Millborn ";
            this.lblCoName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Teal;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(433, 49);
            this.label1.TabIndex = 1389;
            this.label1.Text = "Group Cash Book Account";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LstItem
            // 
            this.LstItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.LstItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LstItem.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstItem.FormattingEnabled = true;
            this.LstItem.ItemHeight = 14;
            this.LstItem.Location = new System.Drawing.Point(146, 182);
            this.LstItem.Name = "LstItem";
            this.LstItem.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.LstItem.Size = new System.Drawing.Size(261, 86);
            this.LstItem.TabIndex = 1421;
            this.LstItem.Visible = false;
            this.LstItem.ValueChanged += new vControls.vListBox.ValueChangedEventHandler(this.LstItem_ValueChanged);
            // 
            // DgvTransport
            // 
            this.DgvTransport.AllowUserToAddRows = false;
            this.DgvTransport.AllowUserToDeleteRows = false;
            this.DgvTransport.AllowUserToResizeRows = false;
            this.DgvTransport.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.DgvTransport.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvTransport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.DgvTransport.ColumnHeadersHeight = 25;
            this.DgvTransport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DgvTransport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTName,
            this.ColTId});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvTransport.DefaultCellStyle = dataGridViewCellStyle7;
            this.DgvTransport.EnableHeadersVisualStyles = false;
            this.DgvTransport.GridColor = System.Drawing.Color.Black;
            this.DgvTransport.Location = new System.Drawing.Point(34, 96);
            this.DgvTransport.Name = "DgvTransport";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvTransport.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.DgvTransport.RowHeadersVisible = false;
            this.DgvTransport.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvTransport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DgvTransport.Size = new System.Drawing.Size(384, 181);
            this.DgvTransport.TabIndex = 1420;
            this.DgvTransport.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvTransport_KeyDown);
            // 
            // ColTName
            // 
            this.ColTName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColTName.DefaultCellStyle = dataGridViewCellStyle6;
            this.ColTName.FillWeight = 72.71774F;
            this.ColTName.HeaderText = "Account Name";
            this.ColTName.Name = "ColTName";
            this.ColTName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColTName.TextingMode = false;
            // 
            // ColTId
            // 
            this.ColTId.HeaderText = "UnitRate";
            this.ColTId.Name = "ColTId";
            this.ColTId.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(34, 283);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 35);
            this.btnSave.TabIndex = 1422;
            this.btnSave.Text = "&Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(115, 283);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 35);
            this.btnExit.TabIndex = 1423;
            this.btnExit.Text = "&Exit";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Frm_GroupCashAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 332);
            this.Controls.Add(this.LstItem);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.DgvTransport);
            this.Controls.Add(this.lblCoName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Frm_GroupCashAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Group Cash Account  ";

            this.Load += new System.EventHandler(this.Frm_GroupCashAccount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvTransport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCoName;
        private System.Windows.Forms.Label label1;
        private vControls.vListBox LstItem;
        public System.Windows.Forms.DataGridView DgvTransport;
        private vControls.vListBoxColumn ColTName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTId;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
    }
}