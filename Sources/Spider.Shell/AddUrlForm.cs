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
    public partial class AddUrlForm : Form
    {
        public AddUrlForm()
        {
            InitializeComponent();

        }
        private void AddUrlForm_Load(object sender, EventArgs e)
        {
            BindUrlType();
        }

        private void btnAddUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtUrl.Text))
            {
                MessageBox.Show("URL 不能为空！");
            }


            SourceUrlModel urlModel = new SourceUrlModel()
            {
                Url = this.txtUrl.Text,
                UrlType = (UrlType)this.comboBox1.SelectedValue,
                Remark = this.txtRemark.Text
            };
            try
            {
                UrlBusiness business = new UrlBusiness();
                business.AddUrl(urlModel);
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

            this.comboBox1.DataSource = new BindingSource() { DataSource = urlTypes };
            this.comboBox1.ValueMember = "Key";
            this.comboBox1.DisplayMember = "Value";

        }


    }
}
