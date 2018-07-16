using System.Collections.Generic;
using Diss.Core.Models;

namespace ManagerPanel.Models
{
    public class TaskViewModel
    {
        public Diss.Core.Models.Task Task { get; set; }
        public List<User> CoordinatorList { get; set; }
        public List<User> AnalystList { get; set; }

        public int CoordinatorId { get; set; }
        public int AnalystId { get; set; }
    }
}
