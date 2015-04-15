namespace Spider.Shell
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnShowNotice = new System.Windows.Forms.Button();
            this.btnAddUrl = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listLog = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gridSourceUrl = new System.Windows.Forms.DataGridView();
            this.clmUrlId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmUrlType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmOperate = new System.Windows.Forms.DataGridViewButtonColumn();
            this.clmState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSourceUrl)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnShowNotice);
            this.panel1.Controls.Add(this.btnAddUrl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(711, 62);
            this.panel1.TabIndex = 6;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(20, 11);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnShowNotice
            // 
            this.btnShowNotice.Location = new System.Drawing.Point(182, 12);
            this.btnShowNotice.Name = "btnShowNotice";
            this.btnShowNotice.Size = new System.Drawing.Size(93, 21);
            this.btnShowNotice.TabIndex = 5;
            this.btnShowNotice.Text = "查看分红信息";
            this.btnShowNotice.UseVisualStyleBackColor = true;
            this.btnShowNotice.Click += new System.EventHandler(this.btnShowNotice_Click);
            // 
            // btnAddUrl
            // 
            this.btnAddUrl.Location = new System.Drawing.Point(101, 10);
            this.btnAddUrl.Name = "btnAddUrl";
            this.btnAddUrl.Size = new System.Drawing.Size(75, 23);
            this.btnAddUrl.TabIndex = 4;
            this.btnAddUrl.Text = "添加Url";
            this.btnAddUrl.UseVisualStyleBackColor = true;
            this.btnAddUrl.Click += new System.EventHandler(this.btnAddUrl_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 581);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(711, 21);
            this.progressBar1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listLog);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 442);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(711, 139);
            this.panel2.TabIndex = 8;
            // 
            // listLog
            // 
            this.listLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listLog.FormattingEnabled = true;
            this.listLog.ItemHeight = 12;
            this.listLog.Location = new System.Drawing.Point(0, 0);
            this.listLog.Name = "listLog";
            this.listLog.Size = new System.Drawing.Size(711, 139);
            this.listLog.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gridSourceUrl);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 62);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(711, 380);
            this.panel3.TabIndex = 9;
            // 
            // gridSourceUrl
            // 
            this.gridSourceUrl.AllowUserToAddRows = false;
            this.gridSourceUrl.AllowUserToDeleteRows = false;
            this.gridSourceUrl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSourceUrl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmUrlId,
            this.clmUrl,
            this.clmUrlType,
            this.clmRemark,
            this.clmOperate,
            this.clmState});
            this.gridSourceUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSourceUrl.Location = new System.Drawing.Point(0, 0);
            this.gridSourceUrl.Name = "gridSourceUrl";
            this.gridSourceUrl.ReadOnly = true;
            this.gridSourceUrl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gridSourceUrl.Size = new System.Drawing.Size(711, 380);
            this.gridSourceUrl.TabIndex = 6;
            this.gridSourceUrl.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSourceUrl_CellContentClick);
            this.gridSourceUrl.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridSourceUrl_CellFormatting);
            // 
            // clmUrlId
            // 
            this.clmUrlId.DataPropertyName = "UrlId";
            this.clmUrlId.Frozen = true;
            this.clmUrlId.HeaderText = "UrlId";
            this.clmUrlId.Name = "clmUrlId";
            this.clmUrlId.ReadOnly = true;
            this.clmUrlId.Width = 50;
            // 
            // clmUrl
            // 
            this.clmUrl.DataPropertyName = "Url";
            this.clmUrl.HeaderText = "Url";
            this.clmUrl.Name = "clmUrl";
            this.clmUrl.ReadOnly = true;
            this.clmUrl.Width = 200;
            // 
            // clmUrlType
            // 
            this.clmUrlType.DataPropertyName = "UrlType";
            this.clmUrlType.HeaderText = "UrlType";
            this.clmUrlType.Name = "clmUrlType";
            this.clmUrlType.ReadOnly = true;
            // 
            // clmRemark
            // 
            this.clmRemark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmRemark.DataPropertyName = "Remark";
            this.clmRemark.HeaderText = "Remark";
            this.clmRemark.Name = "clmRemark";
            this.clmRemark.ReadOnly = true;
            // 
            // clmOperate
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "获取";
            this.clmOperate.DefaultCellStyle = dataGridViewCellStyle1;
            this.clmOperate.HeaderText = "操作";
            this.clmOperate.Name = "clmOperate";
            this.clmOperate.ReadOnly = true;
            this.clmOperate.Text = "获取";
            // 
            // clmState
            // 
            this.clmState.DataPropertyName = "State";
            this.clmState.HeaderText = "状态";
            this.clmState.Name = "clmState";
            this.clmState.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 602);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSourceUrl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAddUrl;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView gridSourceUrl;
        private System.Windows.Forms.ListBox listLog;
        private System.Windows.Forms.Button btnShowNotice;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmUrlId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmUrlType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRemark;
        private System.Windows.Forms.DataGridViewButtonColumn clmOperate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmState;

    }
}

