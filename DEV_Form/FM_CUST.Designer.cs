
namespace DEV_Form
{
    partial class FM_CUST
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.BizType = new System.Windows.Forms.GroupBox();
            this.rdo4 = new System.Windows.Forms.RadioButton();
            this.rdo3 = new System.Windows.Forms.RadioButton();
            this.rdo2 = new System.Windows.Forms.RadioButton();
            this.rdo1 = new System.Windows.Forms.RadioButton();
            this.CustType = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CustEnd = new System.Windows.Forms.DateTimePicker();
            this.CustStart = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCustName = new System.Windows.Forms.TextBox();
            this.txtCustCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvGrid = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.BizType.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.BizType);
            this.groupBox1.Controls.Add(this.CustType);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.CustEnd);
            this.groupBox1.Controls.Add(this.CustStart);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCustName);
            this.groupBox1.Controls.Add(this.txtCustCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(980, 150);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "거래처 조회";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(798, 100);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(103, 38);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "조회";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // BizType
            // 
            this.BizType.Controls.Add(this.rdo4);
            this.BizType.Controls.Add(this.rdo3);
            this.BizType.Controls.Add(this.rdo2);
            this.BizType.Controls.Add(this.rdo1);
            this.BizType.Location = new System.Drawing.Point(223, 83);
            this.BizType.Name = "BizType";
            this.BizType.Size = new System.Drawing.Size(428, 56);
            this.BizType.TabIndex = 9;
            this.BizType.TabStop = false;
            this.BizType.Text = "종목";
            // 
            // rdo4
            // 
            this.rdo4.AutoSize = true;
            this.rdo4.Location = new System.Drawing.Point(316, 26);
            this.rdo4.Name = "rdo4";
            this.rdo4.Size = new System.Drawing.Size(105, 24);
            this.rdo4.TabIndex = 3;
            this.rdo4.TabStop = true;
            this.rdo4.Text = "펌프압축기";
            this.rdo4.UseVisualStyleBackColor = true;
            // 
            // rdo3
            // 
            this.rdo3.AutoSize = true;
            this.rdo3.Location = new System.Drawing.Point(220, 26);
            this.rdo3.Name = "rdo3";
            this.rdo3.Size = new System.Drawing.Size(90, 24);
            this.rdo3.TabIndex = 2;
            this.rdo3.TabStop = true;
            this.rdo3.Text = "절삭가공";
            this.rdo3.UseVisualStyleBackColor = true;
            // 
            // rdo2
            // 
            this.rdo2.AutoSize = true;
            this.rdo2.Location = new System.Drawing.Point(109, 26);
            this.rdo2.Name = "rdo2";
            this.rdo2.Size = new System.Drawing.Size(105, 24);
            this.rdo2.TabIndex = 1;
            this.rdo2.TabStop = true;
            this.rdo2.Text = "자동차부품";
            this.rdo2.UseVisualStyleBackColor = true;
            // 
            // rdo1
            // 
            this.rdo1.AutoSize = true;
            this.rdo1.Location = new System.Drawing.Point(6, 26);
            this.rdo1.Name = "rdo1";
            this.rdo1.Size = new System.Drawing.Size(105, 24);
            this.rdo1.TabIndex = 0;
            this.rdo1.TabStop = true;
            this.rdo1.Text = "상용차부품";
            this.rdo1.UseVisualStyleBackColor = true;
            // 
            // CustType
            // 
            this.CustType.AutoSize = true;
            this.CustType.Location = new System.Drawing.Point(21, 96);
            this.CustType.Name = "CustType";
            this.CustType.Size = new System.Drawing.Size(126, 24);
            this.CustType.TabIndex = 8;
            this.CustType.Text = "고객사만 검색";
            this.CustType.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(794, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "~";
            // 
            // CustEnd
            // 
            this.CustEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.CustEnd.Location = new System.Drawing.Point(820, 32);
            this.CustEnd.Name = "CustEnd";
            this.CustEnd.Size = new System.Drawing.Size(128, 27);
            this.CustEnd.TabIndex = 6;
            // 
            // CustStart
            // 
            this.CustStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.CustStart.Location = new System.Drawing.Point(657, 32);
            this.CustStart.Name = "CustStart";
            this.CustStart.Size = new System.Drawing.Size(131, 27);
            this.CustStart.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(582, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "거래일자";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(267, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "거래처 명";
            // 
            // txtCustName
            // 
            this.txtCustName.Location = new System.Drawing.Point(338, 33);
            this.txtCustName.Name = "txtCustName";
            this.txtCustName.Size = new System.Drawing.Size(160, 27);
            this.txtCustName.TabIndex = 2;
            // 
            // txtCustCode
            // 
            this.txtCustCode.Location = new System.Drawing.Point(101, 33);
            this.txtCustCode.Name = "txtCustCode";
            this.txtCustCode.Size = new System.Drawing.Size(160, 27);
            this.txtCustCode.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "거래처 코드";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvGrid);
            this.groupBox3.Controls.Add(this.btnAdd);
            this.groupBox3.Controls.Add(this.btnDelete);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 150);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(980, 384);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "거래처 정보";
            // 
            // dgvGrid
            // 
            this.dgvGrid.AllowUserToAddRows = false;
            this.dgvGrid.AllowUserToDeleteRows = false;
            this.dgvGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrid.Location = new System.Drawing.Point(3, 70);
            this.dgvGrid.Name = "dgvGrid";
            this.dgvGrid.RowHeadersWidth = 51;
            this.dgvGrid.RowTemplate.Height = 29;
            this.dgvGrid.Size = new System.Drawing.Size(974, 311);
            this.dgvGrid.TabIndex = 14;
            this.dgvGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGrid_CellContentClick);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(21, 26);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(103, 38);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "추가";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(239, 26);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(103, 38);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "저장";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(130, 26);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(103, 38);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "삭제";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // FM_CUST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 534);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FM_CUST";
            this.Text = "FM_CUST";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.BizType.ResumeLayout(false);
            this.BizType.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnS;
        private System.Windows.Forms.GroupBox BizType;
        private System.Windows.Forms.RadioButton rdo4;
        private System.Windows.Forms.RadioButton rdo3;
        private System.Windows.Forms.RadioButton rdo2;
        private System.Windows.Forms.RadioButton rdo1;
        private System.Windows.Forms.CheckBox CustType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker CustEnd;
        private System.Windows.Forms.DateTimePicker CustStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCustName;
        private System.Windows.Forms.TextBox txtCustCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvGrid;
        private System.Windows.Forms.Button btnSearch;
    }
}