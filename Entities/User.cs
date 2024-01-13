using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List.Entities
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int userId;
        private int _age;
        public int Age
        {
            get => _age;
            set
            {
                _age = value;
            }
        }
        
        public string Mail { get; set; }
        private int _password;

        public int Password
        {
            get => _password;
            set => _password = value;
        }
        public List<Tasks> Tasks { get; set; }

    }
}
