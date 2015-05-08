using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spider.Business.Stock;
using Spider.Models.Stock;

namespace Spider.Shell
{
    public partial class UrlForm : Form
    {
        private SourceUrlModel urlModel = null;

        public UrlForm()
        {
            InitializeComponent();
            this.Text = "新增获取数据Url";
        }

        public UrlForm(SourceUrlModel model)
        {
            InitializeComponent();
            this.urlModel = model;
            this.Text = "修改获取数据Url";
        }

        private void bindFormComponent()
        {
            this.txtUrl.Text = this.urlModel.Url;
            this.cbUrlType.SelectedValue = this.urlModel.UrlType;
            this.txtRemark.Text = this.urlModel.Remark;
            this.txtHandle.Text = this.urlModel.Handle;
        }

        private void AddUrlForm_Load(object sender, EventArgs e)
        {
            BindUrlType();
            if (this.urlModel != null)
            {
                bindFormComponent();
            }
        }

        private void btnAddUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtUrl.Text))
            {
                MessageBox.Show("URL 不能为空！");
            }



            try
            {
                UrlBusiness business = new UrlBusiness();
                if (this.urlModel == null)
                {
                    SourceUrlModel urlModel = new SourceUrlModel()
                    {
                        Url = this.txtUrl.Text.Trim(),
                        UrlType = (UrlType)this.cbUrlType.SelectedValue,
                        Remark = this.txtRemark.Text.Trim(),
                        Handle = this.txtHandle.Text.Trim()
                    };
                    business.AddUrl(urlModel);
                }
                else
                {
                    this.urlModel.Url = this.txtUrl.Text.Trim();
                    this.urlModel.UrlType = (UrlType)this.cbUrlType.SelectedValue;
                    this.urlModel.Remark = txtRemark.Text.Trim();
                    this.urlModel.Handle = this.txtHandle.Text.Trim();
                    business.UpdateUrl(urlModel);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void BindUrlType()
        {
            Dictionary<UrlType, string> urlTypes = new Dictionary<UrlType, string>();

            FieldInfo[] fields = typeof(UrlType).GetFields();
            object enumInstance = typeof(UrlType).Assembly.CreateInstance(typeof(UrlType).FullName);
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    string desc = ((UrlDescAttribute)field.GetCustomAttribute(typeof(UrlDescAttribute))).Description;
                    urlTypes.Add((UrlType)field.GetValue(enumInstance), desc);
                }
            }

            this.cbUrlType.DataSource = new BindingSource() { DataSource = urlTypes };
            this.cbUrlType.ValueMember = "Key";
            this.cbUrlType.DisplayMember = "Value";

        }


    }
}
