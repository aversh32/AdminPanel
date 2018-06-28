using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
