using System.Threading.Tasks;
using DataAccess.Core;
using Diss.Core.Models;
using Task = Diss.Core.Models.Task;

namespace Diss.Core.DataServices
{
    public interface ITasksDataService : IDataService<Task>
    {
        Task<Task> UpsertTask(Models.Task task);
    }
}
