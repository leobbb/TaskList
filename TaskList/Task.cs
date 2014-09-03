using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList
{
    class Task
    {
        private int _tId;
        private string _content;

        public int TId
        {
            get { return _tId; }
        }
        public string Content
        {
            get { return _content; }
        }

        public Task(int id, string content)
        {
            this._tId = id;
            this._content = content;
        }
    }
}
