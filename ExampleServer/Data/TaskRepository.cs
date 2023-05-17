// TaskRepository is responsible for storing and manipulating
// Our collection of data, in this case TaskModels

//not creating an instance, creating an entry in the list of repository

namespace ExampleServer.Data;

public class TaskRepository
{
    //Data storage (All of our tasks)
    private readonly List<TaskModel> _taskList = new List<TaskModel>();

    //Create method
                    // paramater(type-TaskModel called-task)
    public void AddTask(TaskModel task)
    {
        _taskList.Add(task);
        //_taskList.Contains(task);  //.Contains determines whether an element is in the list
    }

    //Read method

    public List<TaskModel> GetTasks()//no parameter because getting all tasks
    {
        //return _taskList;
        return new List<TaskModel>(_taskList);//a "photo copy"
    }
                                    
    public List<TaskModel> GetTasksByStatus(bool isComplete)
    {
        // Start a new list
        List<TaskModel> tasks = new List<TaskModel>();
        // Iterate through all tasks and check the status

        //type is TaskModel
        foreach (TaskModel task in _taskList)
        {
        // Add a task to the new list if its status matches the parameter
        if (task.IsComplete == isComplete)
        {
            tasks.Add(task);
        }

        }
        // return the new list
        return tasks;
    }

    //Delete method

    public bool DeleteTaskById (int id)
    {
            //loop through each task
        foreach (var task in _taskList)
        {   //Check the task Id against our parameter
            if (task.Id == id)
            {// if we find it remove the task and return true/false
                return _taskList.Remove(task);
            }
        }
        //Return false if we don't find the Id in the loop
        return false;
    }
}