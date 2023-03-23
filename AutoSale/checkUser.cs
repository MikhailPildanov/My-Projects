using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSales
{
    public class checkUser
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public bool IsAdmin { get; }

        public string Status => IsAdmin ? "Admin" : "User";

        public checkUser(int id, string login, bool isAdmin)
        {
            Id = id;
            Login = login.Trim();
            IsAdmin = isAdmin;
        }


    }
}
