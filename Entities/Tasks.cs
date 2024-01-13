using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List.Entities
{
    public class Tasks
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Content { get; set; }
        public User user { get; set; }
        public int userId { get; set; }
    }
}
