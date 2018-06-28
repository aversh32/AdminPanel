using DataAccess.Repository.EFCore;
using Diss.Core;

namespace Diss.Services.Data
{
    public class DissRepository : RepositoryBase<DissContext>
    {
        public DissRepository(DissContext db) : base(db)
        {

        }
    }
}
