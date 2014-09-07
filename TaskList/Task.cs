using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList
{
    class Task
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


        public Task(string cont, string sta, DateTime n ,DateTime d )
        {
            this._content = cont;
            this._status = sta;
            this._timeNew = n;
            this._timeDone = d;
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
