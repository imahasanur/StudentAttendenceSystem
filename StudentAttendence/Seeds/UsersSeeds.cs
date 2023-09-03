using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttendence.Seeds
{
    public static class UsersSeeds
    {
        public static List<Users> Users
        {
            get
            {
                return new List<Users>()
                {
                    new Users {Name="Mr Admin", UserName="admin",UserPassword="12345",Type="admin" }
                };
            }
        }
    }
}
