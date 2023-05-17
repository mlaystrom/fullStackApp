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