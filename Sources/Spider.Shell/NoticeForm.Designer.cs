namespace Spider.Shell
{
    partial class NoticeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoticeForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridNotice = new System.Windows.Forms.DataGridView();
            this.clmStockCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNoticeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridNotice)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(719, 51);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gridNotice);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 51);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(719, 462);
            this.panel2.TabIndex = 1;
            // 
            // gridNotice
            // 
            this.gridNotice.AllowUserToAddRows = false;
            this.gridNotice.AllowUserToDeleteRows = false;
            this.gridNotice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridNotice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmStockCode,
            this.clmTitle,
            this.clmDate,
            this.clmNoticeId,
            this.clmId});
            this.gridNotice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridNotice.Location = new System.Drawing.Point(0, 0);
            this.gridNotice.Name = "gridNotice";
            this.gridNotice.ReadOnly = true;
            this.gridNotice.Size = new System.Drawing.Size(719, 462);
            this.gridNotice.TabIndex = 0;
            // 
            // clmStockCode
            // 
            this.clmStockCode.DataPropertyName = "StockCode";
            this.clmStockCode.HeaderText = "股票代码";
            this.clmStockCode.Name = "clmStockCode";
            this.clmStockCode.ReadOnly = true;
            // 
            // clmTitle
            // 
            this.clmTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmTitle.DataPropertyName = "Title";
            this.clmTitle.HeaderText = "公告信息概况";
            this.clmTitle.Name = "clmTitle";
            this.clmTitle.ReadOnly = true;
            // 
            // clmDate
            // 
            this.clmDate.DataPropertyName = "Date";
            this.clmDate.HeaderText = "日期";
            this.clmDate.Name = "clmDate";
            this.clmDate.ReadOnly = true;
            // 
            // clmNoticeId
            // 
            this.clmNoticeId.DataPropertyName = "NoticeId";
            this.clmNoticeId.HeaderText = "NoticeId";
            this.clmNoticeId.Name = "clmNoticeId";
            this.clmNoticeId.ReadOnly = true;
            this.clmNoticeId.Visible = false;
            // 
            // clmId
            // 
            this.clmId.DataPropertyName = "id";
            this.clmId.HeaderText = "id";
            this.clmId.Name = "clmId";
            this.clmId.ReadOnly = true;
            this.clmId.Visible = false;
            // 
            // NoticeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 513);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NoticeForm";
            this.Text = "公司分红信息";
            this.Load += new System.EventHandler(this.NoticeForm_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridNotice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView gridNotice;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStockCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNoticeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmId;
    }
}