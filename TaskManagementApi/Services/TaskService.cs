using System.Collections.Generic;
using System.Linq;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

public class TaskService : ITaskService
{
    private readonly List<TaskItem> _tasks;

    public TaskService()
    {
        _tasks = new List<TaskItem>();
        // Adding some initial tasks
        _tasks.Add(new TaskItem { Id = 1, Title = "Learn ASP.NET Core", Description = "Study the basics of ASP.NET Core framework and its components", IsCompleted = false });
        _tasks.Add(new TaskItem { Id = 2, Title = "Create a new project", Description = "Set up a new ASP.NET Core project using Visual Studio or Visual Studio Code", IsCompleted = false });
        _tasks.Add(new TaskItem { Id = 3, Title = "Add a new feature", Description = "Implement a new feature based on project requirements", IsCompleted = false });
        _tasks.Add(new TaskItem { Id = 4, Title = "Deploy the app", Description = "Deploy the application to a hosting service like Azure or AWS", IsCompleted = false });
    }

    public List<TaskItem> GetAllTasks() => _tasks;

    public TaskItem? GetTaskById(int? id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id) ?? null;
    }

    public void AddTask(TaskItem task)
    {
        task.Id = _tasks.Max(t => t.Id) + 1;
        _tasks.Add(task);
    }

    public void UpdateTask(TaskItem task)
    {
        var existingTask = _tasks.FirstOrDefault(t => t.Id == task.Id);
        if (existingTask != null)
        {
            existingTask.Title = task.Title;
            existingTask.IsCompleted = task.IsCompleted;
        }
    }

    public void DeleteTask(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            _tasks.Remove(task);
        }
    }
}
