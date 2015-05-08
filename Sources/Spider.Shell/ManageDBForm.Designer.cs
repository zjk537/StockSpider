namespace Spider.Shell
{
    partial class ManageDBForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageDBForm));
            this.gbClear = new System.Windows.Forms.GroupBox();
            this.lblClearMsg = new System.Windows.Forms.Label();
            this.btnExeClear = new System.Windows.Forms.Button();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExeUrl = new System.Windows.Forms.Button();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.gbClear.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbClear
            // 
            this.gbClear.Controls.Add(this.lblClearMsg);
            this.gbClear.Controls.Add(this.btnExeClear);
            this.gbClear.Controls.Add(this.checkBox5);
            this.gbClear.Controls.Add(this.checkBox4);
            this.gbClear.Controls.Add(this.checkBox3);
            this.gbClear.Controls.Add(this.checkBox2);
            this.gbClear.Controls.Add(this.checkBox1);
            this.gbClear.Location = new System.Drawing.Point(12, 12);
            this.gbClear.Name = "gbClear";
            this.gbClear.Size = new System.Drawing.Size(450, 66);
            this.gbClear.TabIndex = 1;
            this.gbClear.TabStop = false;
            this.gbClear.Text = "清理数据";
            // 
            // lblClearMsg
            // 
            this.lblClearMsg.AutoSize = true;
            this.lblClearMsg.ForeColor = System.Drawing.Color.Red;
            this.lblClearMsg.Location = new System.Drawing.Point(261, 50);
            this.lblClearMsg.Name = "lblClearMsg";
            this.lblClearMsg.Size = new System.Drawing.Size(0, 12);
            this.lblClearMsg.TabIndex = 5;
            // 
            // btnExeClear
            // 
            this.btnExeClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExeClear.Location = new System.Drawing.Point(406, 17);
            this.btnExeClear.Name = "btnExeClear";
            this.btnExeClear.Size = new System.Drawing.Size(41, 46);
            this.btnExeClear.TabIndex = 4;
            this.btnExeClear.Text = "执行";
            this.btnExeClear.UseVisualStyleBackColor = true;
            this.btnExeClear.Click += new System.EventHandler(this.btnExeClear_Click);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Location = new System.Drawing.Point(241, 20);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(96, 16);
            this.checkBox4.TabIndex = 1;
            this.checkBox4.Tag = "BigDealSum";
            this.checkBox4.Text = "大单汇总数据";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(163, 20);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(72, 16);
            this.checkBox3.TabIndex = 1;
            this.checkBox3.Tag = "BigDealRecord";
            this.checkBox3.Text = "大单数据";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(85, 20);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(72, 16);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Tag = "Notice";
            this.checkBox2.Text = "分红数据";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(7, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Tag = "StockCompany";
            this.checkBox1.Text = "上市公司";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnExeUrl);
            this.groupBox1.Location = new System.Drawing.Point(12, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(447, 56);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "重置状态";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "重置Url数据源状态，正在执行中的，重置为初始状态";
            // 
            // btnExeUrl
            // 
            this.btnExeUrl.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExeUrl.Location = new System.Drawing.Point(406, 17);
            this.btnExeUrl.Name = "btnExeUrl";
            this.btnExeUrl.Size = new System.Drawing.Size(38, 36);
            this.btnExeUrl.TabIndex = 4;
            this.btnExeUrl.Text = "执行";
            this.btnExeUrl.UseVisualStyleBackColor = true;
            this.btnExeUrl.Click += new System.EventHandler(this.btnExeUrl_Click);
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Checked = true;
            this.checkBox5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox5.Location = new System.Drawing.Point(8, 42);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(72, 16);
            this.checkBox5.TabIndex = 1;
            this.checkBox5.Tag = "DailyRecord";
            this.checkBox5.Text = "每日数据";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // ManageDBForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 342);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbClear);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManageDBForm";
            this.Text = "维护数据库";
            this.gbClear.ResumeLayout(false);
            this.gbClear.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbClear;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnExeClear;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExeUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblClearMsg;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox5;

    }
}