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

    public class Task 
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
            : this(id, cont, "doing", DateTime.Now, DateTime.Now)
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
        //public int CompareTo(object obj)
        //{
        //    Task t = (Task)obj;
        //    if (this.TimeNew.CompareTo(t.TimeNew) < 0)
        //        return -1;
        //    else if (this.TimeNew.CompareTo(t.TimeNew) == 0)
        //        return 0;
        //    else
        //        return 1;
        //}

        //public int CompareTo(Task obj, string done)
        //{
        //    switch (done)
        //    {
        //        case "New":
        //            return this.CompareTo(obj);
        //        case "Done":
        //            if (this.TimeDone.CompareTo(obj.TimeDone) < 0)
        //                return -1;
        //            else if (this.TimeDone.CompareTo(obj.TimeDone) == 0)
        //                return 0;
        //            else
        //                return 1;
        //        default:
        //            throw new Exception("输入的参数错误");
        //    }
        //}

        // Task 使用哪一个字段进行排序
                
        public enum SortField
        {
            TaskId,
            TaskContent,
            TimeNew,
            TimeDone
        }
        
        // 用于存储排序的字段和方法
        public struct Sorter
        {
            public SortField field;
            public SortDirection direction;

            public Sorter(SortField f, SortDirection dir)
            {
                this.field = f;
                this.direction = dir;
            }

            public Sorter(SortField f)
                : this(f, SortDirection.Ascending)
            { 
            }
        }

        // 嵌套类，实现 IComparer<Task> 接口
        public class TaskComparer : IComparer<Task>
        {
            // 存储排序属性的集合
            private List<Sorter> list;

            public TaskComparer(List<Sorter> ls)
            {
                this.list = ls;
            }

            public TaskComparer(SortField sf, SortDirection sd)
            {
                list = new List<Sorter>();
                list.Add(new Sorter(sf, sd));
            }

            public TaskComparer(SortField sf) : this(sf, SortDirection.Ascending) { }
            public TaskComparer() : this(SortField.TaskId, SortDirection.Ascending) { }


            //// 用于排序的两个私有变量
            //private SortField sortField;
            //private SortDirection sortDirection;

            //// 初始化排序变量的构造方法
            //public Task(SortField sf, SortDirection sd)
            //{
            //    this.sortField = sf;
            //    this.sortDirection = sd;
            //}


            // 实现 IComparer 接口
            int IComparer<Task>.Compare(Task x, Task y)
            {
                int result = 0;
                foreach (Sorter item in list)
                {
                    result = Compare(x, y, item.field, item.direction);
                    if (result != 0)        // 一旦 result 不为0 ，则已经区分出位置的大小，跳出循环
                        break;
                }
                return result;
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
}
