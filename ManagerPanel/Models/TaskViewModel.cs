using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diss.Core.Models;

namespace WebService.Models
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
