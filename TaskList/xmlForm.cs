using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        XPathNavigator nav;
        private static string path = "./Data/TaskList.xml";     // 文档默认的保存路径
        private static int count = 0;       // 任务列表中的任务个数

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

                //lblShow.Text = "文件已经创建";

                // 把 XML 文档加载到内存
                xDoc.Load(path);

                // 查询 XML 文档中任务的总数
                count = getTaskAmount();

                // 初始化列表内容
                btnDoing_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"程序加载时出现错误\n请重新打开");
            }       
        }

        // 查询 XML 文档中任务的总数
        private int getTaskAmount()
        {
            nav = xDoc.CreateNavigator();       // 创建文档浏览器
            nav.MoveToChild("TaskList", "");    // 移到到根节点
            count = int.Parse(nav.GetAttribute("count", ""));    // 获得根节点里的 count 属性值            
            return count;
        }

        // 创建新的 XML 文档，只包含声明和一个根节点
        private void createNewXmlFile()
        {
            if (!System.IO.Directory.Exists("./Data"))
                System.IO.Directory.CreateDirectory("./Data");

            xDoc = new XmlDocument();
            XmlDeclaration declare = xDoc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            xDoc.AppendChild(declare);

            root = xDoc.CreateElement("TaskList");      // 创建根节点 TaskList

            XmlAttribute attr = xDoc.CreateAttribute("count"); // 创建根节点的属性 count
            attr.Value = "0";       // 设置 count 的值为 “0”。
            root.Attributes.Append(attr);  // 把属性节点 (count)，添加到根节点 (TaskList)

            xDoc.AppendChild(root);     // 把根节点添加到文档中
            xDoc.Save(path);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string content = txtContent.Text.Trim();
            
            if (content == string.Empty)
            {
                MessageBox.Show("任务内容不能为空\n请重新输入", "输入错误", MessageBoxButtons.OK);
                txtContent.Focus();
                return;
            }

            DateTime time = DateTime.Now;
            
            // 隐藏彩蛋
            if ( content == "leo")
            {
                if (callMe() == DialogResult.Cancel)
                {
                    txtContent.Text = "";
                    txtContent.Focus();
                    return;
                }
            }

            // 创建 task 对象，任务的总数增加 1 
            Task task = new Task(++count, content, "doing", time, time);

            if (addXmlElement(task))
            {
                btnDoing_Click(sender, e);
                lblShow.Text = "任务添加成功";
                txtContent.Text = "";
                txtContent.Focus();
            }
            else
            {
                lblShow.Text = "任务添加失败";
                
                // 进行错误处理
                errorHandling(1);
            }
        }

        // 向 XML 文档添加一个新的任务
        private bool addXmlElement(Task task)
        {
            try
            {
                //xDoc.Load(path);      // 加载xml文件
                root = xDoc.DocumentElement;        // 获取xml文件的根元素

                // 创建 Task 节点
                XmlElement tElem = xDoc.CreateElement("Task");
                // 创建属性节点 id，并添加到 Task 中。
                XmlAttribute attr = xDoc.CreateAttribute("id");
                attr.Value = task.TId.ToString();
                tElem.SetAttributeNode(attr);

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

                root.SetAttribute("count", count.ToString());
                xDoc.Save(path);
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "添加任务节点出错");
                return false;
            }
        }

        // 错误处理的方法
        private void errorHandling(int e)
        {
            DialogResult dres;
            switch (e)
            {
                case 1:     // 添加新节点出错，或者刷新出错        
                    dres = MessageBox.Show("数据文件无法使用，可能存在错误。\n是否删除所有数据，建立新文件？", "数据文件损坏", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (dres == System.Windows.Forms.DialogResult.OK)
                    {
                        createNewXmlFile();
                        lblShow.Text += "  数据文件已重新建立";
                    }
                    else
                    {
                        lblShow.Text += "  请手动检查数据文件";
                    }
                    break;               
            }   
        }

        private void btnDoing_Click(object sender, EventArgs e)
        {
            
            btnDoing.BackColor = SystemColors.ActiveCaption;
            btnDone.BackColor = SystemColors.Control;
            if (refreshList("doing"))
                lblShow.Text = "正在进行中的任务已刷新";
            else
            {
                lblShow.Text = "刷新失败";
                
                // 进行错误处理
                errorHandling(1);
            }
        }

        // 刷新列表，根据 p 的值选择显示 doing 的任务，还是 done 的任务。
        private bool refreshList(string status)
        {
            cklShow.Items.Clear();
            try
            {
                // 创建一个存放任务对象的集合，用于排序
                //List<Task> tList = new List<Task>();    
                SortedList<DateTime, Task> sList = new SortedList<DateTime, Task>();
                Task task;

                //XPathDocument xpDoc = new XPathDocument(path);      // 创建 XPath 文档
                nav = xDoc.CreateNavigator();       // 创建文档浏览器
                string comm = string.Format("TaskList/Task[TaskStatus=\"{0}\"]", status);  // 命令字符串
                XPathExpression exp = nav.Compile(comm);        //  封装命令
                XPathNodeIterator ni = nav.Select(exp);         // 执行命令，返回一个迭代器
                while (ni.MoveNext()) 
                {
                    nav = ni.Current;    // nav 指向当前的 Task 节点

                    int id = int.Parse(nav.GetAttribute("id", "")); // 获得任务的 id
                    
                    // 获得任务的内容 content
                    XPathNodeIterator sni = ni.Current.SelectChildren("TaskContent", "");
                    sni.MoveNext();
                    string content = sni.Current.Value;

                    // 获得任务的添加时间
                    sni = nav.SelectChildren("TimeNew", "");
                    sni.MoveNext();
                    DateTime timeNew = DateTime.Parse(sni.Current.Value);

                    // 获取任务的完成时间
                    sni = nav.SelectChildren("TimeDone", "");
                    sni.MoveNext();
                    DateTime timeDone = DateTime.Parse(sni.Current.Value);

                    task = new Task(id, content, status, timeNew, timeDone);
                    // 把任务添加到集合 tList
                    //tList.Add(new Task(id, content, status, timeNew, timeDone));    

                    if (status == "doing")
                        sList.Add(task.TimeNew, task);  // 进行中的任务，按照添加的时间进行排序
                    else
                        sList.Add(task.TimeDone, task); // 已完成的任务，按照完成的时间进行排序
                }

                //IList<Task> tList = sList.Values;
                //IEnumerable<Task> iEnu = tList.Reverse();
                
                IEnumerator<KeyValuePair<DateTime,Task>> iEnu;
                if (status == "done")
                {
                    iEnu = sList.Reverse().GetEnumerator();   // 完成的任务的顺序是 最后完成的排在前边
                    lblAmount.Text = "已完成的任务共 " + sList.Count + " 个";
                }
                else
                {
                    iEnu = sList.GetEnumerator();             // 进行中的任务的顺序是 最后添加的排在后边
                    lblAmount.Text = "进行中的任务共 " + sList.Count + " 个";
                }
                // 将 sList 中的对象添加到 cklShow 控件中
                //foreach (Task t in sList.Values)
                //foreach (Task t in sList.Values)
                //{
                //    cklShow.Items.Add(t);
                //}

                while(iEnu.MoveNext())
                {
                    cklShow.Items.Add(iEnu.Current.Value);
                }
                
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "刷新失败");
                return false;
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            btnDone.BackColor = SystemColors.ActiveCaption;
            btnDoing.BackColor = SystemColors.Control;
            if (refreshList("done"))
                lblShow.Text = "完成的任务已刷新";
            else
            {
                lblShow.Text = "刷新失败";

                // 进行错误处理
                errorHandling(1);
            }
        }

        private void cklShow_DoubleClick(object sender, EventArgs e)
        {
            Task task = cklShow.SelectedItem as Task;
            try
            {
                if (task != null)
                {
                    // 如果btnDoing按键被激活，则双击事件完成“把任务标记为已完成”的任务。
                    if (btnDoing.BackColor == SystemColors.ActiveCaption)
                    {
                        if(updateTask(task.TId,"done"))
                        {
                            lblShow.Text = string.Format("恭喜！任务 \"{0}\"已完成！", task.ToString());
                            cklShow.Items.Remove(task);
                        }
                        else
                            lblShow.Text = "操作失败，没有改变任务状态";
                    }
                    else // 如果btnDone按键被激活，则双击事件完成“把任务标记为进行中”的任务。
                    {
                        if (updateTask(task.TId,"doing" ))
                        {
                            lblShow.Text = string.Format("任务 \"{0}\" 重新开始！", task.ToString());
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
                MessageBox.Show(ex.Message, "更新失败\n请检查数据文件", MessageBoxButtons.OK);
            }
        }

        private bool updateTask(int id, string status)
        {
            XPathNavigator nav = xDoc.CreateNavigator();       // 创建文档浏览器
            string comm = string.Format("TaskList/Task[@id=\"{0}\"]", id);  // 命令字符串
            
            nav = nav.SelectSingleNode(comm);         // 执行命令,返回一个 Task 节点
            
            // 修改任务的状态
            nav.MoveToChild("TaskStatus", "");
            nav.SetValue(status);
            nav.MoveToParent();

            // 修改对应的时间信息
            if (status == "done")
            {
                nav.MoveToChild("TimeDone", "");
                nav.SetValue(DateTime.Now.ToString());
            }
            else
            {
                nav.MoveToChild("TimeNew", "");
                nav.SetValue(DateTime.Now.ToString());
            }

            return true;
        }

        private void xmlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // 窗体关闭之前，保存文件。
                xDoc.Save(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "保存文件出错");
                
                DialogResult dres = MessageBox.Show("是否继续关闭窗口？\n并将数据保存到“failure.txt”中？", "即将关闭", MessageBoxButtons.OKCancel);
                if (dres == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter("failure.txt", true);
                    sw.Write(xDoc.OuterXml);
                    sw.Close();
                }
                else
                {
                    lblShow.Text = "不能正常关闭程序，请检查数据文件。";
                    e.Cancel = true;
                }
            }
        }

    }
}
