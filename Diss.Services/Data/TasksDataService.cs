using System.Threading.Tasks;
using DataAccess.Core;
using DataAccess.Repository.DataService;
using Diss.Core;
using Diss.Core.DataServices;
using Diss.Core.Models;
using Task = Diss.Core.Models.Task;

namespace Diss.Services.Data
{
    public class TasksDataService : DataServiceBase<Core.Models.Task>, ITasksDataService
    {
        public TasksDataService(IRepository repo)
            : base(repo)
        { }

        public async Task<Task> UpsertTask(Core.Models.Task task)
        {
            var ctx = Repository.GetDatabaseContext<DissContext>();
           // ctx.AttachObjectGraph(task, null);

            return await UpsertAndSave(task);
        }
    }
}
