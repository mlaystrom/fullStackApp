// Define our actual class that serves and handles web requests
// Listen to HTTP requests, handle back and forth with our client
// Client being our web page (HTML, CSS, JS, etc)

using System.Net;//HttpListener (ctrl + . wrote this using statement)
using System.Text; //Encoding.UTF8
using System.Text.Json;
using ExampleServer.Data; //where we are getting _taskRepository from

namespace ExampleServer.Server;

public class WebServer
{   //private fields
    private readonly TaskRepository _taskRepository;
    private readonly HttpListener _httpListener = new();


    //constructor
    public WebServer(TaskRepository repository, string url)
    {
            //Dependency Injection(injecting something we're dependent on)
            //Passing the TaskRepository into our class
            //rather than making a new repository
        _taskRepository = repository;
        _httpListener.Prefixes.Add(url); //go into and grab one
    }
    public void Run()
    {
        // start the server ( http listener)
        _httpListener.Start();

        // add some debug feedback (console WriteLine)

        Console.WriteLine($"Listening for connections on {_httpListener.Prefixes.First()}");

        // Handle our incoming connections/requests
        // Bulk of our logic

        HandleIncomingRequests();//"invoking" here

        //Stop the server
        _httpListener.Stop();
    }

    private void HandleIncomingRequests()
    {   
        while(true)
        {
             //Have the server sit and wait for a connection request
             //Once there is a connection request it will return the context
             HttpListenerContext context = _httpListener.GetContext();

             //Get the Request and Response Objects from the context
             HttpListenerRequest request = context.Request;
             HttpListenerResponse response = context.Response;

                Console.WriteLine($"{request.HttpMethod} {request.Url}");

             switch (request.HttpMethod)
             {
                case "Get":
                // Handle Get Requests
                HandleGetRequests(request, response);
                break;
             }
        }
    }

    private void HandleGetRequests(HttpListenerRequest request, HttpListenerResponse response)
    {
       
        if (request.Url?.AbsolutePath == "/")
        {
            var tasks = _taskRepository.GetTasks();
             SendResponse(response, HttpStatusCode.OK, tasks);
        }
        else
        {
            SendResponse(response, HttpStatusCode.NotFound, null);
        }
    }




    private void SendResponse(HttpListenerResponse response, HttpStatusCode statusCode, object? data)
    {   
        //convert our c# object to JSON, which allows our browser to understand it
        //We need to also tell our response the content is JSON
        string json = JsonSerializer.Serialize(data);
        response.ContentType = "Application/json";

        // Convert our JSON to a byte[] -> basic numbers we can send over the internet
        // Breaking down JSON to a stream of numbers
        byte[] buffer = Encoding.UTF8.GetBytes(json);
        // We need to tell the response how much content to listen for and then stop
        // Tells the recipient(browser) how much of the data stream is the content
        response.ContentLength64 = buffer.Length;

        // Setting our response status code (OK, Bad, Good, etc.)
        // Casting our statusCode variable from type enum to type int
        response.StatusCode =(int)statusCode;

        // Simply here because CORS sucks
        response.AddHeader("Access-Control-Allow-Origin", "*");

        // Writing or sending our response
        response.OutputStream.Write(buffer, 0, buffer.Length);
        response.Close();
    }
    
}
