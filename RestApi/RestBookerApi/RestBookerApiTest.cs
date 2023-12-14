using Newtonsoft.Json;
using RestBookerApi.Utilities;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestBookerApi
{
    internal class RestBookerApiTest:CoreCodes
    {
        [Test]
        [Order(0)]
        [TestCase(3)]
        public void GetSingleBookingIdTest(int userId)
        {
            test = extent.CreateTest("Get Single Booking Id");
            Log.Information("GetSingleBookingId Test Started");

            var request = new RestRequest("booking/" + userId, Method.Get);
            request.AddHeader("Accept", "application/json");
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var user = JsonConvert.DeserializeObject<Details>(response.Content);

                Assert.NotNull(user);
                Log.Information("User returned");
                Assert.IsNotEmpty(user.FirstName);
                Log.Information("User FirstName matches with fetch");
                Assert.IsNotEmpty(user.LastName);
                Log.Information("User lastName matches with fetch");
                Assert.IsNotEmpty(user.DepositPaid);
                Log.Information("User DepositPaid matches with fetch");


                test.Pass("GetSingleBookingIdTest passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("GetSingleBookingId test failed");
            }
        }

        [Test]
        [Order(1)]
        public void GetAllBookingTest()
        {
            test = extent.CreateTest("Get All Booking");
            Log.Information("Get All Booking Test Started");

            var request = new RestRequest("/booking", Method.Get);
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                List<Details> users = JsonConvert.DeserializeObject<List<Details>>(response.Content);

                Assert.NotNull(users);
                Log.Information("Get All Booking  test passed");

                test.Pass("Get All Booking  test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Get All Booking  test failed");
            }
        }
        [Test]
        [Order(3)]
        public void CreateBookingTest()
        {
            test = extent.CreateTest("Create Booking");
            Log.Information("CreateBooking Test Started");

            var request = new RestRequest("/booking", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(new
            {
                firstname = "John",
                lastname = "Doe",
                totalprice = "200",
                depositpaid = "true",
                bookingdates = new
                {
                    checkin = "2023-03-01",
                    checkout = "2023-03-15"
                },
                additionalneeds = "Extra pillows"
            });

            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var user = JsonConvert.DeserializeObject<Details>(response.Content);
                Assert.NotNull(user);
                Log.Information("User created and returned");
                Assert.IsNotEmpty(user.FirstName);
                Log.Information("User First Name matches with fetch");
                Assert.IsNotEmpty(user.LastName);
                Log.Information("User Last Name matches with fetch");
                Log.Information("CreateBooking test passed all Asserts");

                test.Pass("CreateBooking passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("Create Booking test failed");
            }
        }
        [Test]
        public void CreateToken()
        {
            test = extent.CreateTest("Create Token");
            Log.Information("Create Token started");
            var request = new RestRequest("/auth", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                username = "admin",
                password ="password123"

            });
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var user = JsonConvert.DeserializeObject<Cookies>(response.Content);
                Assert.NotNull(user);
                Log.Information("Create Token Test Passed");
                test.Pass("Create Token Test Passed");
            }
            catch (AssertionException) 
            {
                test.Fail("Create token test failed");
            }
        }


    }
}
/////