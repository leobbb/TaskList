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
        System.Data.SqlClient.SqlConnection conn;
        SqlCommand command;
        SqlDataReader dataReader;

        public Form1()
        {
            InitializeComponent();
            //cklShow.Items.Add("first");
            //cklShow.Items.Add("second");
            //cklShow.Items.Add("three");
            // 使用@ 忽略字符串中的转义字符 (\)
            string strConn = @"Data Source = (localdb)\Projects; Initial Catalog = Task_List ; Integrated Security = True";
            conn = new System.Data.SqlClient.SqlConnection(strConn);
            command = new SqlCommand();
            command.Connection = conn;
            
            lblShow.Text = "";
        }

        //private void cklShow_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (cklShow.SelectedIndex >= 0)
        //        {
        //            int select = cklShow.SelectedIndex;
        //            //lblShow.Text = Convert.ToString(select);

        //            string selItem = cklShow.SelectedItem.ToString();
        //            lblShow.Text = "项目 “"+ selItem + "” 已完成";

        //            cklShow.Items.RemoveAt(select);
        //        }
        //    }
        //    catch (Exception x)
        //    {
        //        MessageBox.Show(x.Message, "出现异常",MessageBoxButtons.OK);
        //    }
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            refreshCheckList(cklShow,0);
        }

        // 更新指定列表的内容
        private void refreshCheckList(CheckedListBox ckl,int status)
        {
            ckl.Items.Clear();
            try
            {
                string sql = "select [taskId],[taskContent] from [TaskList] where [taskStatus] = " + status;
                command.CommandText = sql;
                conn.Open();
                dataReader = command.ExecuteReader();
                int id;
                string content;
                while (dataReader.Read())
                {
                    id = Convert.ToInt32(dataReader[0]);
                    content = dataReader[1].ToString().Trim();
                    ckl.Items.Add(new Task(id, content));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "数据库操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dataReader.Close();
                conn.Close();
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text.Trim();
            if (content == string.Empty)
            {
                lblShow.Text = "任务信息不能为空";
                txtContent.Focus();
                return;
            }
            // 如果数据插入成功，则刷新列表；如果失败，则提示错误。 
            if (addTask(content))
            {
                lblShow.Text = "任务已添加";
                refreshCheckList(cklShow,0);
            }
            else
                lblShow.Text = "任务添加失败";

        }


        // 把指定数据插入数据库
        private bool addTask(string content)
        {
            string sql = "INSERT INTO [TaskList] ([taskContent], [taskStatus]) VALUES( N'"+ content +
                "', 0)";
            int count = 0;
            try
            {
                command.CommandText = sql;
                conn.Open();
                count = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "数据库操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            // 如果插入成功，则只影响到一行记录，所以count为1，则函数返回true。
            if (count == 1)
                return true;
            else
                return false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cklShow_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            lblShow.Text = e.CurrentValue.ToString();
            //if (e.CurrentValue == CheckState.Checked)
            //    return;
            try
            {
                lblShow.Text = "选中状态改变";
                int index = e.Index;
                Task task = cklShow.Items[index] as Task;
                if (task != null)
                {
                    e.NewValue = CheckState.Unchecked;
                    if (finishTask(task.TId))
                    {
                        lblShow.Text = string.Format("恭喜！任务 \"{0}\"已完成！", task.Content);
                        //cklShow.Items.RemoveAt(index);
                        refreshCheckList(cklShow, 0);
                    }
                    else
                        lblShow.Text = "操作失败，没有改变任务状态";
                }
                else
                    lblShow.Text = "没有选中任务";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "删除出错", MessageBoxButtons.OK);
            }
        }

        private bool finishTask(int p)
        {
            string sql = "UPDATE [TaskList] " +
                        "SET [taskStatus]= 1 " +
                        "WHERE [taskId] = " + p;
            int count = 0;
            try
            {
                command.CommandText = sql;
                conn.Open();
                count = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "操作数据库出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

            if (count == 1)
                return true;
            else
                return false;
        }
        private void btnDoing_Click(object sender, EventArgs e)
        {
            refreshCheckList(cklShow, 0);
            lblShow.Text = "进行中的任务列表刷新成功";
            btnDoing.BackColor = SystemColors.ActiveCaption;
            btnDone.BackColor = SystemColors.Control;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            refreshCheckList(cklShow, 1);
            lblShow.Text = "已完成的任务列表刷新成功";
            btnDone.BackColor = SystemColors.ActiveCaption;
            btnDoing.BackColor = SystemColors.Control;
        }

    }
}
