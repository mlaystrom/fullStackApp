using ExampleServer.Data; 

TaskModel.TotalTasks = 0;  //terminal-->0
Console.WriteLine(TaskModel.TotalTasks);


//Instance of our class
TaskModel task1 = new TaskModel("Task 1", "The first task");

task1.WriteTotalTasks();  //can write into instances because not static //terminal--> 1/1

TaskModel task2 = new TaskModel("Task 2", "The second task");
task1.WriteTotalTasks();
task2.WriteTotalTasks();

Console.WriteLine(task2.Id); //comes back as 2

// Implicit Types
// var assumes the type from the righthand side of the expression
var task3 = new TaskModel("Task 3", "The third task");

//Target-typed new
//Implicit new will assume the type from the lefthand side
//declaring type on lefthand side
TaskModel task4 = new("Task 4", "The fourth task");


task1.IsComplete = true; //the bool in TaskModel.cs and then the var task = repo.GetTasksByStatus(true) returns those that are true
task4.IsComplete = true;

//initialize task repo
TaskRepository repo = new TaskRepository();
// var repo = new TaskRepository();
//TaskRepository repo=new();

repo.AddTask(task1);
repo.AddTask(task2);
repo.AddTask(task3);
repo.AddTask(task4);

//deleting a task
repo.DeleteTaskById(3);

//capture the tasks
// tasks gets its type from the GetTasks() return type
//var tasks = repo.GetTasks();
var tasks = repo.GetTasksByStatus(true); //return tasks that are true
foreach (var task in tasks)
{
    Console.WriteLine(task.Description);
}