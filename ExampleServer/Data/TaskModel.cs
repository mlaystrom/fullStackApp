// TaskModel is our POCO (Plain Old C# Object)
// The class is going to represent the data object

namespace ExampleServer.Data;


//Task or ToDo is something we want to get done
//4 parts: Identifier, Title, Description, a completion status
        //same as file name
public class TaskModel
{

    public static int TotalTasks = 0;

    //Constructor
    public TaskModel(string title, string description)
        {
            TotalTasks ++; //anytime increment a new task model increase
            Id = TotalTasks; //assigning new task to ID property

            Title = title;
            Description = description;
        }
    

    public int Id { get; } //will look like, row 1, row 2, etc.  //Read Only without set
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsComplete { get; set; }  //will help us grab by ID



    public void WriteTotalTasks()
{
         Console.WriteLine($"Task {Id}/{TotalTasks}"); //these values will be dynamic and change over time, example 3/10
}
}