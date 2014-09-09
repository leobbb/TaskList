using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace TaskList
{
    // 可复用的枚举，表示排序的方向
    public enum SortDirection
    {
        Ascending = 0,
        Descending = 1
    }

    class Task : IComparer<Task>
    {
        private int _tId;           // 任务的编号
        private string _content;    // 任务的内容
        private string _status;     // 任务的状态    
        private DateTime _timeNew;      // 任务的创建时间
        private DateTime _timeDone;     // 任务的完成时间

        public int TId
        {
            get { return _tId; }
        }
        public string Content
        {
            get { return _content; }
        }
        public string Status
        {
            get { return _status; }
        }

        public DateTime TimeNew
        {
            get { return _timeNew; }
        }

        public DateTime TimeDone
        {
            get { return _timeDone; }
        }

        public Task(int id, string cont)
            : this(id, cont, string.Empty, new DateTime(), new DateTime())
        {
            this._tId = id;
            this._content = cont;
        }

        public Task(int id, string cont, string sta, DateTime n, DateTime d)
        {
            this._tId = id;
            this._content = cont;
            this._status = sta;
            this._timeNew = n;
            this._timeDone = d;
        }

        public override string ToString()
        {
            return Content;
        }

        // 按照日期从小到大排序
        public int CompareTo(object obj)
        {
            Task t = (Task)obj;
            if (this.TimeNew.CompareTo(t.TimeNew) < 0)
                return -1;
            else if (this.TimeNew.CompareTo(t.TimeNew) == 0)
                return 0;
            else
                return 1;
        }

        public int CompareTo(Task obj, string done)
        {
            switch (done)
            {
                case "New":
                    return this.CompareTo(obj);
                case "Done":
                    if (this.TimeDone.CompareTo(obj.TimeDone) < 0)
                        return -1;
                    else if (this.TimeDone.CompareTo(obj.TimeDone) == 0)
                        return 0;
                    else
                        return 1;
                default:
                    throw new Exception("输入的参数错误");
            }
        }

        // Task 使用哪一个字段进行排序
        public enum SortField
        {
            TaskId,
            TaskContent,
            TimeNew,
            TimeDone
        }

        // 用于排序的两个私有变量
        private SortField sortField;
        private SortDirection sortDirection;

        // 初始化排序变量的构造方法
        public Task(SortField sf, SortDirection sd)
        {
            this.sortField = sf;
            this.sortDirection = sd;
        }


        // 实现 IComparer 接口
        int IComparer<Task>.Compare(Task x, Task y)
        {           
            return this.Compare(x, y, this.sortField, this.sortDirection);
        }

        // 对单个属性按某种方式进行排序
        int Compare(Task x, Task y, SortField field, SortDirection direction)
        {
            int result = 0;
            switch (field)
            {
                case SortField.TimeNew:
                    if (direction == SortDirection.Ascending)
                        return x.TimeNew.CompareTo(y.TimeNew);
                    else
                        return y.TimeNew.CompareTo(x.TimeNew);
                    //break;
                case SortField.TimeDone:
                    if (direction == SortDirection.Ascending)
                        return x.TimeDone.CompareTo(y.TimeDone);
                    else
                        return y.TimeDone.CompareTo(x.TimeDone);
                    //break;
                case SortField.TaskId:
                    if (direction == SortDirection.Ascending)
                        return x.TId.CompareTo(y.TId);
                    else 
                        return y.TId.CompareTo(x.TId);
                    //break;
                case SortField.TaskContent:
                    if (direction == SortDirection.Ascending)
                        return x.Content.CompareTo(y.Content);
                    else
                        return y.Content.CompareTo(x.Content);
                    //break;

            }
            return result;
        }
    }
}
