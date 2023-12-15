
using JSonPlaceHolderApiModularizedCode.Utilities;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSonPlaceHolderApiModularizedCode
{
    public class JsonApiTests:CoreCodes
    {


        [Test]
        [TestCase(1)]
        public void GetSingleItemTest(int number)
        {
            test = extent.CreateTest("Get Single Item");
            Log.Information("GetSingleItem Test Started");

            var getSingleUserRequest = new RestRequest("posts/" +number, Method.Get);
            var getSingeUserResponse = client.Execute(getSingleUserRequest);
            try
            {
                Assert.That(getSingeUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"Api Response:{getSingeUserResponse.Content}");
                var item = JsonConvert.DeserializeObject<UserData>(getSingeUserResponse.Content);
                Assert.NotNull(item);
                Log.Information("User returned");
                Assert.That(item.Id, Is.EqualTo(1));
                Log.Information(" id match with fetch");
                Assert.That(item.UserId, Is.EqualTo(1));
                Log.Information("User id match with fetch");
                Log.Information("Get single item test passed");
                test.Pass("Get single item test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Get single user test failed");
            }
        }
        [Test]
        [Order(2)]
        public void CreateItem()
        {
            test = extent.CreateTest("Create Item");
            Log.Information("Create Item Test Started");
            var createItemRequest = new RestRequest("/posts", Method.Post);
            createItemRequest.AddHeader("Content-Type", "application/json");
            createItemRequest.AddJsonBody(new
            {
                title = "foo",
                body = "bar",
                userId = "100"
            });
            var createItemResponse = client.Execute(createItemRequest);
            try
            {
                Assert.That(createItemResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                var item = JsonConvert.DeserializeObject<UserData>(createItemResponse.Content);
                Assert.NotNull(item);
                
                Log.Information($"Api response:{createItemResponse.Content}");
                Log.Information("Item created and returned");
                test.Pass("Create item test passed");

            }
            catch (AssertionException) 
            {
                test.Fail("create item test failed");
            }


        }

        [Test]
        [Order(3)]
        [TestCase(1)]
        public void UpdateItem(int number)
        {
            test = extent.CreateTest("Update Item");
            Log.Information("Update Item Test Started");
            var updateItemRequest = new RestRequest("/posts/"+number, Method.Put);
            updateItemRequest.AddHeader("Content-Type", "application/json");
            updateItemRequest.AddJsonBody(new
            {
                userId = "500",
                body = "Update item",

            });
            var updateItemResponse = client.Execute(updateItemRequest);
            try
            {
               Assert.That(updateItemResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"Api Response:{updateItemResponse.Content}");
                var item = JsonConvert.DeserializeObject(updateItemResponse.Content);
                Assert.NotNull(item);
                Log.Information("Item updated and returned");
                Log.Information("Update item test passed");
                test.Pass("Update item test passed");
            }
            catch(AssertionException) 
            {
                test.Fail("Update item test failed");
            }

        }
        [Test]
        [Order(4)]
        [TestCase(1)]
        public void DeleteItem(int number)
        {
            extent.CreateTest("Delete item ");
            Log.Information("Delete item test started");
            var deleteItemRequest = new RestRequest("/posts/"+number, Method.Delete);
            var deleteItemResponse = client.Execute(deleteItemRequest);
            try
            {
                Assert.That(deleteItemResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"{number} deleted");
                Log.Information("Delete item test passed");
                test.Pass("Delete item test passed");
            }
            catch(AssertionException) 
            {
                test.Fail("Delete item test failed");
            }
        }
        [Test]
        public void GetAllItem()
        {
            extent.CreateTest("Get all item ");
            Log.Information("Get all  item test started");
            var allItemRequest = new RestRequest("/posts", Method.Get);
            var allItemResponse = client.Execute(allItemRequest);
            try
            {
                Assert.That(allItemResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"Api Response:{allItemResponse.Content}");
                List<UserData> Item = JsonConvert.DeserializeObject<List<UserData>>(allItemResponse.Content);
                Assert.NotNull(Item);
                Log.Information("All item returned");
                Log.Information("Get all item test passed");
                test.Pass("Get all item test passed");
            }
            catch (AssertionException) 
            {
                test.Fail("Get all item test failed");
            }
        }
        [Test]
        public void GetNonExistingItem()
        {
            extent.CreateTest("Get Non-Existing Item ");
            Log.Information("Get Non-Existing item test started");
            var nonExistingItemRequest = new RestRequest("/posts/1000", Method.Get);
            var nonExistingItemResponse = client.Execute(nonExistingItemRequest);
            try
            {
                Assert.That(nonExistingItemResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
                Log.Information("Get non existing item test passed");
                test.Pass("Get Non-Existing item test passed");

            }
            catch(AssertionException) 
            {
                test.Fail("Get Non-Existing item test failed ");
            }
        }


    }
}
