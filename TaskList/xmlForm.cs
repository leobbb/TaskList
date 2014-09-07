using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;


namespace TaskList
{
    public partial class xmlForm : Form
    {
        public xmlForm()
        {
            InitializeComponent();
            xDoc = new XmlDocument();
        }

        XmlDocument xDoc;
        XmlElement root;
        
        private static string path = "./Data/TaskList.xml";

        private void xmlForm_Load(object sender, EventArgs e)
        {
            // 如果xml文件不存在，则在Data目录下创建TaskList.xml文件
            try
            {
                if (!System.IO.Directory.Exists("./Data"))
                    System.IO.Directory.CreateDirectory("./Data");

                //lblShow.Text = "Data文件夹已经创建。";

                if (!System.IO.File.Exists(path))
                    createNewXmlFile();

                lblShow.Text = "文件已经创建";

                xDoc.Load(path);

                btnDoing_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"文件夹创建出错");
            }
            

        }

        private void createNewXmlFile()
        {
            xDoc = new XmlDocument();
            XmlDeclaration declare = xDoc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            xDoc.AppendChild(declare);
            root = xDoc.CreateElement("TaskList");
            xDoc.AppendChild(root);
            xDoc.Save(path);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text;
            DateTime time = DateTime.Now;
            Task task = new Task(content, "doing", time, time);

            if (addXmlElement(task))
            {
                lblShow.Text = "任务添加成功";
                cklShow.Items.Add(task);
            }
            else
                lblShow.Text = "任务添加失败";

        }

        private bool addXmlElement(Task task)
        {
            try
            {
                //xDoc.Load(path);      // 加载xml文件
                root = xDoc.DocumentElement;        // 获取xml文件的根元素

                // 简单的判断根元素的名称
                if (root.Name != "TaskList")
                {
                    createNewXmlFile();
                    lblShow.Text = "数据文件出现错误，已建立新文件。";
                }

                // 创建 Task 节点
                XmlElement tElem = xDoc.CreateElement("Task");

                // 创建 TaskContent 节点，并添加到 Task 中
                XmlElement elem = xDoc.CreateElement("TaskContent");
                XmlText text = xDoc.CreateTextNode(task.Content);
                elem.AppendChild(text);
                tElem.AppendChild(elem);

                // 创建 TaskStatus 节点，并添加到 Task 中
                elem = xDoc.CreateElement("TaskStatus");
                text = xDoc.CreateTextNode(task.Status);
                elem.AppendChild(text);
                tElem.AppendChild(elem);

                // 创建 TimeNew 节点，并添加到 Task 中
                elem = xDoc.CreateElement("TimeNew");
                text = xDoc.CreateTextNode(task.TimeNew.ToString());
                elem.AppendChild(text);
                tElem.AppendChild(elem);

                // 创建 TimeDone 节点，并添加到 Task 中
                elem = xDoc.CreateElement("TimeDone");
                text = xDoc.CreateTextNode(task.TimeDone.ToString());
                elem.AppendChild(text);
                tElem.AppendChild(elem);

                // 把 Task 节点添加到 TaskList 根节点
                root.AppendChild(tElem);

                xDoc.Save(path);
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "添加任务节点出错");
                return false;
            }
        }

        private void btnDoing_Click(object sender, EventArgs e)
        {
            btnDoing.BackColor = SystemColors.ActiveCaption;
            btnDone.BackColor = SystemColors.Control;
            if (refreshList("doing"))
                lblShow.Text = "正在进行中的任务已刷新";
        }

        // 刷新列表，根据 p 的值选择显示 doing 的任务，还是 done 的任务。
        private bool refreshList(string status)
        {
            cklShow.Items.Clear();
            try
            {
                //XPathDocument xpDoc = new XPathDocument(path);      // 创建 XPath 文档
                XPathNavigator nav = xDoc.CreateNavigator();       // 创建文档浏览器
                string comm = string.Format("TaskList/Task[TaskStatus=\"{0}\"]", status);  // 命令字符串
                XPathExpression exp = nav.Compile(comm);        //  封装命令
                XPathNodeIterator ni = nav.Select(exp);         // 执行命令，返回一个迭代器
                while (ni.MoveNext()) 
                {
                    XPathNodeIterator sni = ni.Current.SelectChildren("TaskContent", "");
                    sni.MoveNext();
                    string content = sni.Current.Value;

                    cklShow.Items.Add(content);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "刷新出错");
                return false;
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            btnDone.BackColor = SystemColors.ActiveCaption;
            btnDoing.BackColor = SystemColors.Control;
            if (refreshList("done"))
                lblShow.Text = "完成的任务已刷新";

        }

        private void cklShow_DoubleClick(object sender, EventArgs e)
        {
            string task = cklShow.SelectedItem.ToString();
            try
            {
                if (task != null)
                {
                    // 如果btnDoing按键被激活，则双击事件完成“把任务标记为已完成”的任务。
                    if (btnDoing.BackColor == SystemColors.ActiveCaption)
                    {
                        if(updateTask(task,"done"))
                        {
                            lblShow.Text = string.Format("恭喜！任务 \"{0}\"已完成！", task);
                            cklShow.Items.Remove(task);
                        }
                        else
                            lblShow.Text = "操作失败，没有改变任务状态";
                    }
                    else // 如果btnDone按键被激活，则双击事件完成“把任务标记为进行中”的任务。
                    {
                        if (updateTask(task,"doing" ))
                        {
                            lblShow.Text = string.Format("任务 \"{0}\" 重新开始！", task);
                            cklShow.Items.Remove(task);
                        }
                        else
                            lblShow.Text = "操作失败，没有改变任务状态";
                    }
               }
                else
                    lblShow.Text = "没有选中任务";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "删除出错", MessageBoxButtons.OK);
            }
        }

        private bool updateTask(string content, string status)
        {
            XPathNavigator nav = xDoc.CreateNavigator();       // 创建文档浏览器
            string comm = string.Format("TaskList/Task[TaskContent=\"{0}\"]", content);  // 命令字符串
            
            XPathNavigator ni = nav.SelectSingleNode(comm);         // 执行命令，返回一个迭代器
            ni.MoveToChild("TaskStatus", "");
            ni.SetValue(status);
            return true;
        }

        private void xmlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            xDoc.Save(path);
        }

    }
}
