using DataAccess.Core;
using DataAccess.Repository.DataService;
using Diss.Core.DataServices;
using Diss.Core.Models;

namespace Diss.Services.Data
{
    public class UsersDataService : DataServiceBase<User>, IUsersService
    {
        public UsersDataService(IRepository repo)
            : base(repo)
        { }
    }
}
