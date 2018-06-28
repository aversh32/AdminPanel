using System.Linq;
using DataAccess.Core.Specification.Join;
using Diss.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Diss.Core.DataServices.JoinSpec
{
    public class UserJoinSpec : IQueryJoin<User>
    {
        public IQueryable<User> Include(IQueryable<User> src)
        {
            return src
                .Include(x => x.UserRoles).ThenInclude(ur => ur.Role)
                .Include(x => x.UserDomains).ThenInclude(ud => ud.Domain)
                .Include(x => x.UserTasks).ThenInclude(ut => ut.Task);
        }
    }
}
