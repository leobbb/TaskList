using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace TaskList
{
    class Task : IComparable
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
        {
            this._tId = id;
            this._content = cont;
        }

        public Task(int id,string cont, string sta, DateTime n ,DateTime d )
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
                default :
                    throw new Exception("输入的参数错误");
            }
        }

    }
}
