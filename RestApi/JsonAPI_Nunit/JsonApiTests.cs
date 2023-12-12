using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonAPI_Nunit
{
    [TestFixture]
    internal class JsonApiTests
    {
        private RestClient client;
        private string baseUrl = "https://jsonplaceholder.typicode.com/";
        [SetUp] 
        public void SetUp() 
        {
            client= new RestClient(baseUrl);
        }
        [Test]
        public void GetSingleItemTest()
        {
            var getSingleUserRequest = new RestRequest("posts/1", Method.Get);
            var getSingeUserResponse = client.Execute(getSingleUserRequest);
            Assert.That(getSingeUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            var user=JsonConvert.DeserializeObject<UserData>(getSingeUserResponse.Content);
            Assert.NotNull(user);
            Assert.That(user.Id, Is.EqualTo(1));
            Assert.That(user.UserId, Is.EqualTo(1));

        }
        [Test]
        public void CreateItem()
        {
            var createItemRequest= new RestRequest("/posts", Method.Post);
            createItemRequest.AddHeader("Content-Type", "application/json");
            createItemRequest.AddJsonBody(new
            {
                title = "foo",
                body = "bar",
                userId = "100"
            });
            var createItemResponse= client.Execute(createItemRequest);
            Assert.That(createItemResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
            var user = JsonConvert.DeserializeObject<UserData>(createItemResponse.Content);
            Assert.NotNull(user);
            
            
        }
        [Test]
        public void UpdateItem()
        {
            var updateItemRequest = new RestRequest("/posts/1", Method.Put);
            updateItemRequest.AddHeader("Content-Type", "application/json");
            updateItemRequest.AddJsonBody(new
            {
                userId = "500",
                body = "Update item",

            });
            var updateItemResponse = client.Execute(updateItemRequest);
            Assert.That(updateItemResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            var item=JsonConvert.DeserializeObject(updateItemResponse.Content);
            Assert.NotNull(item);

        }
        [Test]
        public void DeleteItem()
        { 
            var deleteItemRequest = new RestRequest("/posts/1", Method.Delete);
            var deleteItemResponse= client.Execute(deleteItemRequest);
            Assert.That(deleteItemResponse.StatusCode,Is.EqualTo(System.Net.HttpStatusCode.OK));
        }
        [Test]
        public void GetAllItem()
        {
            var allItemRequest = new RestRequest("/posts", Method.Get);
            var allItemResponse= client.Execute(allItemRequest);
            Assert.That(allItemResponse.StatusCode,Is.EqualTo(System.Net.HttpStatusCode.OK));
            List<UserData>Item=JsonConvert.DeserializeObject<List<UserData>>(allItemResponse.Content);
            Assert.NotNull(Item);
        }
        [Test]
        public void GetNonExistingItem()
        {
            var nonExistingItemRequest = new RestRequest("/posts/1000",Method.Get);
            var nonExistingItemResponse=client.Execute(nonExistingItemRequest);
            Assert.That(nonExistingItemResponse.StatusCode,Is.EqualTo(System.Net.HttpStatusCode.NotFound));
        }
        
    }
}
