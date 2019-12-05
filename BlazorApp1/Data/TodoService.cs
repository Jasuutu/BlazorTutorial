using System.Collections.Generic;
using System.Linq;

namespace BlazorApp1.Data
{
    public class TodoService
    {
        private List<TodoItem> todos;
        private int currentId;

        public TodoService()
        {
            todos = new List<TodoItem> { new TodoItem {Id = 1, Title = "Test" } };
            currentId = 1;
        }

        public IEnumerable<TodoItem> Todos => todos;

        public void AddTodoItem(string todoTitle)
        {
            var todo = new TodoItem{Id = ++currentId, Title = todoTitle};
            todos.Add(todo);
        }

        public void ClearDoneTodos()
        {
            if (todos.Any())
            {
                todos.ForEach(todo =>
                {
                    if (todo.IsDone)
                    {
                        todo.IsCleared = true;
                    }
                });
            }
        }

        public void ResetAllTodos()
        {
            if (todos.Any())
            {
                todos.ForEach(todo => todo.IsCleared = false);
            }
        }
    }
}