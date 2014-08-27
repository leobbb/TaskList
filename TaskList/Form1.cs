using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;

namespace TaskList
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cklShow.Items.Add("first");
            cklShow.Items.Add("second");
            cklShow.Items.Add("three");
        }

        private void cklShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cklShow.SelectedIndex >= 0)
                {
                    int select = cklShow.SelectedIndex;
                    //lblShow.Text = Convert.ToString(select);

                    string selItem = cklShow.SelectedItem.ToString();
                    lblShow.Text = "项目 “"+ selItem + "” 已完成";

                    //CheckedListBox.ObjectCollection itmList = cklShow.Items;
                    //itmList.RemoveAt(select);
                    cklShow.Items.RemoveAt(select);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "出现异常",MessageBoxButtons.OK);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string strConn = "Data Source = (localdb)\Projects; Initial Catalog = myData; Integrated Security = True";
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection (strConn);
            string strSel = "select [taskContent] from [TaskList] where [taskStatus] = 0";
        }
    }
}
