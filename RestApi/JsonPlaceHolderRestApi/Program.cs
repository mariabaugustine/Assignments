using RestSharp;
string baseUrl = "https://jsonplaceholder.typicode.com/";
var client=new RestClient(baseUrl);

GetAllUsers(client);
CreateUser(client);
updateUser(client);
DeleteUser(client);

static void GetAllUsers(RestClient client)
{
    var getAllUserRequest = new RestRequest("posts", Method.Get);
    var getUserReponse=client.Execute(getAllUserRequest);
    Console.WriteLine("GET Response:"+getUserReponse.Content);
}

static void CreateUser(RestClient client)
{
    var createUserRequest = new RestRequest("posts", Method.Post);
    createUserRequest.AddHeader("Content-Type", "application/json");
    createUserRequest.AddJsonBody(new
    {
        title = "foo",
        body = "bar",
        userId = "100"
    });
    var createUserResponse=client.Execute(createUserRequest);
    Console.WriteLine("POST Response:"+createUserResponse.Content);
}

static void updateUser(RestClient client)
{
    var updateUserRequest = new RestRequest("posts/1", Method.Put);
    updateUserRequest.AddHeader("Content-Type", "application/json");
    updateUserRequest.AddJsonBody(new
    {
        userId = "500",
        body = "Update user",

    });
    var updateUserResponse=client.Execute(updateUserRequest);
    Console.WriteLine("PUT Response:"+updateUserResponse.Content);
}

static void DeleteUser(RestClient client)
{
    var deleteUserRequest = new RestRequest("posts/1", Method.Delete);
    var deleteUserResponse=client.Execute(deleteUserRequest);
    if (deleteUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        Console.WriteLine("DELETE Response:Item Deleted Successfully");
    }
    else
    {
        Console.WriteLine($"Error:{deleteUserResponse.ErrorMessage}");
    }


}

