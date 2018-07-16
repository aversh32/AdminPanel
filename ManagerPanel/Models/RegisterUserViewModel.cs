using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diss.Core.Models;

namespace ManagerPanel.Models
{
    public class RegisterUserViewModel
    {
        public User User { get; set; }
        public List<Role> Roles { get; set; }
        public List<string> SelectedRoles { get; set; }
    }
}
