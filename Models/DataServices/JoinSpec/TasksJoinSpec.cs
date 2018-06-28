using System.Linq;
using DataAccess.Core.Specification.Join;
using Diss.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Diss.Core.DataServices.JoinSpec
{
    public class TasksJoinSpec : IQueryJoin<Task>
    {
        public IQueryable<Task> Include(IQueryable<Task> src)
        {
            return src
                .Include(x => x.Domain)
                .Include(x => x.Status)
                .Include(x => x.UserTasks).ThenInclude(x => x.User);
        }
    }
}
