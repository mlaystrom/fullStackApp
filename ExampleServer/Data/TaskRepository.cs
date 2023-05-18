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

    //Update method
    public bool MarkTaskAsComplete (int taskId)
    {   //.FirstOrDefault returns the first element of the sequence(_taskList) that satisfies
    //a condition or a default value if no such element is found.
        TaskModel? task = _taskList.FirstOrDefault(t => t.Id == taskId);

        // Check if the task doesn't exist OR if the task is already complete
        if (task == null || task.IsComplete)
        {
            // Return false to indicate it didn't change anythings
            return false;
        }
        task.IsComplete = true;
        return true;
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