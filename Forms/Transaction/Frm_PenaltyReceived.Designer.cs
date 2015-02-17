namespace Finance_Management_System
{
    partial class Frm_PenaltyReceived
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_PenaltyReceived));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHead = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboSanctionNo = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPenaltyRate = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btnLedger = new System.Windows.Forms.Button();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.txtChequeDate = new System.Windows.Forms.TextBox();
            this.txtChequeno = new System.Windows.Forms.TextBox();
            this.txtAccountbalance = new System.Windows.Forms.TextBox();
            this.txtContactNo = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtParty = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.brnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpdate = new System.Windows.Forms.DateTimePicker();
            this.btnShow = new System.Windows.Forms.Button();
            this.dg_Late = new System.Windows.Forms.DataGridView();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.dg_Balance = new System.Windows.Forms.DataGridView();
            this.lblLate = new System.Windows.Forms.Label();
            this.lblbalance = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Late)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Balance)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loan Account No.";
            // 
            // lblHead
            // 
            this.lblHead.BackColor = System.Drawing.Color.Teal;
            this.lblHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHead.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHead.ForeColor = System.Drawing.Color.White;
            this.lblHead.Location = new System.Drawing.Point(0, 0);
            this.lblHead.Name = "lblHead";
            this.lblHead.Size = new System.Drawing.Size(900, 74);
            this.lblHead.TabIndex = 1;
            this.lblHead.Text = "Penalty Received";
            this.lblHead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(900, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = " ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboSanctionNo
            // 
            this.cboSanctionNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSanctionNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSanctionNo.FormattingEnabled = true;
            this.cboSanctionNo.Items.AddRange(new object[] {
            "Daily",
            "Monthly"});
            this.cboSanctionNo.Location = new System.Drawing.Point(140, 107);
            this.cboSanctionNo.Name = "cboSanctionNo";
            this.cboSanctionNo.Size = new System.Drawing.Size(157, 21);
            this.cboSanctionNo.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.txtPenaltyRate);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.btnLedger);
            this.panel1.Controls.Add(this.txtRemarks);
            this.panel1.Controls.Add(this.txtBankName);
            this.panel1.Controls.Add(this.txtChequeDate);
            this.panel1.Controls.Add(this.txtChequeno);
            this.panel1.Controls.Add(this.txtAccountbalance);
            this.panel1.Controls.Add(this.txtContactNo);
            this.panel1.Controls.Add(this.txtAddress);
            this.panel1.Controls.Add(this.txtParty);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(15, 134);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 247);
            this.panel1.TabIndex = 2;
            // 
            // txtPenaltyRate
            // 
            this.txtPenaltyRate.BackColor = System.Drawing.Color.White;
            this.txtPenaltyRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtPenaltyRate.Location = new System.Drawing.Point(123, 94);
            this.txtPenaltyRate.Name = "txtPenaltyRate";
            this.txtPenaltyRate.ReadOnly = true;
            this.txtPenaltyRate.Size = new System.Drawing.Size(144, 21);
            this.txtPenaltyRate.TabIndex = 1617;
            this.txtPenaltyRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Maroon;
            this.label16.Location = new System.Drawing.Point(3, 98);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(80, 13);
            this.label16.TabIndex = 1618;
            this.label16.Text = "Penalty Rate";
            // 
            // btnLedger
            // 
            this.btnLedger.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLedger.Image = ((System.Drawing.Image)(resources.GetObject("btnLedger.Image")));
            this.btnLedger.Location = new System.Drawing.Point(273, 71);
            this.btnLedger.Name = "btnLedger";
            this.btnLedger.Size = new System.Drawing.Size(183, 88);
            this.btnLedger.TabIndex = 11;
            this.btnLedger.TabStop = false;
            this.btnLedger.Text = "&Ledger Account";
            this.btnLedger.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLedger.UseVisualStyleBackColor = true;
            this.btnLedger.Click += new System.EventHandler(this.btnLedger_Click);
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(123, 183);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemarks.Size = new System.Drawing.Size(333, 54);
            this.txtRemarks.TabIndex = 12;
            // 
            // txtBankName
            // 
            this.txtBankName.Location = new System.Drawing.Point(123, 161);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(333, 21);
            this.txtBankName.TabIndex = 11;
            // 
            // txtChequeDate
            // 
            this.txtChequeDate.Location = new System.Drawing.Point(123, 139);
            this.txtChequeDate.Name = "txtChequeDate";
            this.txtChequeDate.Size = new System.Drawing.Size(144, 21);
            this.txtChequeDate.TabIndex = 10;
            // 
            // txtChequeno
            // 
            this.txtChequeno.Location = new System.Drawing.Point(123, 117);
            this.txtChequeno.Name = "txtChequeno";
            this.txtChequeno.Size = new System.Drawing.Size(144, 21);
            this.txtChequeno.TabIndex = 9;
            // 
            // txtAccountbalance
            // 
            this.txtAccountbalance.BackColor = System.Drawing.Color.White;
            this.txtAccountbalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtAccountbalance.Location = new System.Drawing.Point(123, 71);
            this.txtAccountbalance.Name = "txtAccountbalance";
            this.txtAccountbalance.ReadOnly = true;
            this.txtAccountbalance.Size = new System.Drawing.Size(144, 21);
            this.txtAccountbalance.TabIndex = 3;
            this.txtAccountbalance.TabStop = false;
            this.txtAccountbalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtContactNo
            // 
            this.txtContactNo.BackColor = System.Drawing.Color.White;
            this.txtContactNo.Location = new System.Drawing.Point(123, 49);
            this.txtContactNo.Name = "txtContactNo";
            this.txtContactNo.ReadOnly = true;
            this.txtContactNo.Size = new System.Drawing.Size(333, 21);
            this.txtContactNo.TabIndex = 2;
            this.txtContactNo.TabStop = false;
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.Color.White;
            this.txtAddress.Location = new System.Drawing.Point(123, 27);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ReadOnly = true;
            this.txtAddress.Size = new System.Drawing.Size(333, 21);
            this.txtAddress.TabIndex = 1;
            this.txtAddress.TabStop = false;
            // 
            // txtParty
            // 
            this.txtParty.BackColor = System.Drawing.Color.White;
            this.txtParty.Location = new System.Drawing.Point(123, 5);
            this.txtParty.Name = "txtParty";
            this.txtParty.ReadOnly = true;
            this.txtParty.Size = new System.Drawing.Size(333, 21);
            this.txtParty.TabIndex = 0;
            this.txtParty.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 186);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 1613;
            this.label12.Text = "Remarks";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 164);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Bank Name";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 142);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "Cheque Date";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 120);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Cheque No";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Account Balance";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Contact No";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Party Name";
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(357, 387);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 35);
            this.btnExit.TabIndex = 18;
            this.btnExit.Text = "Exit";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(185, 387);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(80, 35);
            this.btnPrint.TabIndex = 15;
            this.btnPrint.Text = "Print";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // brnDelete
            // 
            this.brnDelete.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.brnDelete.Image = ((System.Drawing.Image)(resources.GetObject("brnDelete.Image")));
            this.brnDelete.Location = new System.Drawing.Point(271, 387);
            this.brnDelete.Name = "brnDelete";
            this.brnDelete.Size = new System.Drawing.Size(80, 35);
            this.brnDelete.TabIndex = 16;
            this.brnDelete.Text = "Delete";
            this.brnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.brnDelete.UseVisualStyleBackColor = true;
            this.brnDelete.Click += new System.EventHandler(this.brnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(13, 387);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 35);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(99, 387);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 35);
            this.btnClear.TabIndex = 14;
            this.btnClear.Text = "Clear";
            this.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(334, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 1605;
            this.label4.Text = "Date.";
            // 
            // dtpdate
            // 
            this.dtpdate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpdate.Location = new System.Drawing.Point(377, 107);
            this.dtpdate.Name = "dtpdate";
            this.dtpdate.Size = new System.Drawing.Size(96, 21);
            this.dtpdate.TabIndex = 1;
            this.dtpdate.ValueChanged += new System.EventHandler(this.dtpdate_ValueChanged);
            // 
            // btnShow
            // 
            this.btnShow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShow.Image = ((System.Drawing.Image)(resources.GetObject("btnShow.Image")));
            this.btnShow.Location = new System.Drawing.Point(304, 105);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(24, 24);
            this.btnShow.TabIndex = 1608;
            this.btnShow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // dg_Late
            // 
            this.dg_Late.AllowUserToAddRows = false;
            this.dg_Late.AllowUserToDeleteRows = false;
            this.dg_Late.AllowUserToResizeRows = false;
            this.dg_Late.BackgroundColor = System.Drawing.Color.White;
            this.dg_Late.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_Late.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dg_Late.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_Late.DefaultCellStyle = dataGridViewCellStyle6;
            this.dg_Late.EnableHeadersVisualStyles = false;
            this.dg_Late.GridColor = System.Drawing.Color.Black;
            this.dg_Late.Location = new System.Drawing.Point(485, 134);
            this.dg_Late.Name = "dg_Late";
            this.dg_Late.RowHeadersVisible = false;
            this.dg_Late.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_Late.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dg_Late.Size = new System.Drawing.Size(403, 116);
            this.dg_Late.TabIndex = 1609;
            this.dg_Late.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dg_head_EditingControlShowing);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label13.Location = new System.Drawing.Point(486, 113);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(111, 13);
            this.label13.TabIndex = 1610;
            this.label13.Text = "Late EMI\'s Penalty";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label14.Location = new System.Drawing.Point(486, 260);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(158, 13);
            this.label14.TabIndex = 1611;
            this.label14.Text = "Late Balance EMI\'s Penalty";
            // 
            // dg_Balance
            // 
            this.dg_Balance.AllowUserToAddRows = false;
            this.dg_Balance.AllowUserToDeleteRows = false;
            this.dg_Balance.AllowUserToResizeRows = false;
            this.dg_Balance.BackgroundColor = System.Drawing.Color.White;
            this.dg_Balance.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_Balance.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dg_Balance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_Balance.DefaultCellStyle = dataGridViewCellStyle8;
            this.dg_Balance.EnableHeadersVisualStyles = false;
            this.dg_Balance.GridColor = System.Drawing.Color.Black;
            this.dg_Balance.Location = new System.Drawing.Point(485, 279);
            this.dg_Balance.Name = "dg_Balance";
            this.dg_Balance.RowHeadersVisible = false;
            this.dg_Balance.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_Balance.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dg_Balance.Size = new System.Drawing.Size(403, 102);
            this.dg_Balance.TabIndex = 1612;
            this.dg_Balance.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dg_Balance_EditingControlShowing);
            // 
            // lblLate
            // 
            this.lblLate.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblLate.Location = new System.Drawing.Point(670, 113);
            this.lblLate.Name = "lblLate";
            this.lblLate.Size = new System.Drawing.Size(216, 13);
            this.lblLate.TabIndex = 1613;
            this.lblLate.Text = "Late EMI\'s Penalty";
            this.lblLate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblbalance
            // 
            this.lblbalance.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblbalance.Location = new System.Drawing.Point(672, 259);
            this.lblbalance.Name = "lblbalance";
            this.lblbalance.Size = new System.Drawing.Size(216, 13);
            this.lblbalance.TabIndex = 1614;
            this.lblbalance.Text = "Late EMI\'s Penalty";
            this.lblbalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Frm_PenaltyReceived
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 439);
            this.Controls.Add(this.lblbalance);
            this.Controls.Add(this.lblLate);
            this.Controls.Add(this.dg_Balance);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.dg_Late);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.dtpdate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cboSanctionNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblHead);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.brnDelete);
            this.Controls.Add(this.btnPrint);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Frm_PenaltyReceived";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Penalty Received";
            this.Load += new System.EventHandler(this.Frm_CashBank_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm_CashBank_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Late)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Balance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblHead;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboSanctionNo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button brnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtParty;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.TextBox txtChequeDate;
        private System.Windows.Forms.TextBox txtChequeno;
        private System.Windows.Forms.TextBox txtAccountbalance;
        private System.Windows.Forms.TextBox txtContactNo;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Button btnLedger;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.TextBox txtPenaltyRate;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.DataGridView dg_Late;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.DataGridView dg_Balance;
        private System.Windows.Forms.Label lblLate;
        private System.Windows.Forms.Label lblbalance;
    }
}