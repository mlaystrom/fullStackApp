using System.Text.Json.Serialization;

namespace ExampleServer.Models;

public class ErrorResponse
{
        //Constructor
        // No return
        //The name of the constructor is the same as class
        public ErrorResponse(string message)
        {
            Message = message;
        }
        //Property
        [JsonPropertyName("message")]
        public string Message { get;}
}