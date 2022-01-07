using System.Collections.Generic;
using System.Threading.Tasks;
using S4_E3.Models;

namespace S4_E3.Data
{
    public interface ITodosData
    {
        Task<IList<Todo>> GetTodosAsync();
        Task<Todo>   AddTodoAsync(Todo todo);
        Task   RemoveTodoAsync(int todoId);
        Task<Todo>   UpdateAsync(Todo todo);
    }
}