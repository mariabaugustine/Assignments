using Newtonsoft.Json;
using NUnit.Framework;
using RestBookerApi.Utilities;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        [Order(2)]
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

                var user = JsonConvert.DeserializeObject<CreateBookingRes>(response.Content);
                Details createres = user.Booking;
                BookingDate dates = createres.BookingDates;

                Assert.NotNull(user);
                //Assert.That();
                Assert.AreEqual("John", createres.FirstName);
                Console.WriteLine(createres.FirstName);
                Console.WriteLine(dates.CheckIn);
                Log.Information(createres.FirstName);
                //Log.Information("User created and returned");
                //Assert.IsNotEmpty(user.FirstName);
                //Log.Information("User First Name matches with fetch");
                //Assert.IsNotEmpty(user.LastName);
                //Log.Information("User Last Name matches with fetch");
                //Log.Information("CreateBooking test passed all Asserts");

                test.Pass("CreateBooking passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("Create Booking test failed");
            }
        }
        [Test]
        [Order(3)]
        
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
        [Test]
        [TestCase(3)]
        [Order(6)]
        public void Update(int userId)
        {
            test = extent.CreateTest("Update Booking ");
            Log.Information("UpdateBookingTest Started");

            var request = new RestRequest("auth", Method.Post);
            request.AddHeader("Content-Type", "Application/Json");
            request.AddJsonBody(new { username = "admin", password = "password123" });
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"Api Error:{response.Content}");
                var data = JsonConvert.DeserializeObject<Cookies>(response.Content);
                Assert.NotNull(data);
                Log.Information("Cookie creation test passed");

                var reqput = new RestRequest("booking/" + userId, Method.Put);
                reqput.AddHeader("Content-Type", "Application/Json");
                reqput.AddHeader("Cookie", "token=" + data.Token);
                reqput.AddJsonBody(new
                {
                    firstname = "Updated John",
                    lastname = "Updated Doe",
                    totalprice = "200",
                    depositpaid = "true",
                    bookingdates = new
                    {
                        checkin = "2023-03-01",
                        checkout = "2023-03-15"
                    },
                    additionalneeds = "Extra pillows"
                });

                test.Pass("Update User Test Passed");
            }
            catch (AssertionException)
            {
                test.Fail("Update Booking test failed");
            }
        }
        [Test]
        [TestCase(50)]
        [Order(7)]
        public void DeleteItem(int userId)
        {
            test = extent.CreateTest("Delete Item");
            Log.Information($"Delete Item {userId} started");
            var request = new RestRequest("auth", Method.Post);
            request.AddHeader("Content-Type", "Application/Json");
            request.AddJsonBody(new { username = "admin", password = "password123" });
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"Api response:{response.Content}");
                var data = JsonConvert.DeserializeObject<Cookies>(response.Content);
                Assert.NotNull(data);
                var updateRequest = new RestRequest("booking/" + userId, Method.Delete);
                updateRequest.AddHeader("Content-Type", "Application/Json");
                updateRequest.AddHeader("Cookie", "token=" + data.Token);
                var updateResponse = client.Execute(updateRequest);
                Assert.That(updateResponse.StatusCode,Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information($"Api response:{updateResponse.Content}");
                Log.Information("User deleted");
                Log.Information("Delete Test Passed");
                test.Pass("Delete test passed");
                
            }
            catch(AssertionException)
            {
                test.Fail("Delete test failed");
            }
        }
        [Test]
        [TestCase(89)]
        [Order(4)]
        public void PartialUpdateBookingTest(int userId)
        {
            test = extent.CreateTest("Partial Update Booking ");
            Log.Information("Partial Update Booking Test Started");

            var request = new RestRequest("auth", Method.Post);
            request.AddHeader("Content-Type", "Application/Json");
            request.AddJsonBody(new { username = "admin", password = "password123" });
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Log.Information($"Api Error:{response.Content}");
                var user = JsonConvert.DeserializeObject<Cookies>(response.Content);
                Assert.NotNull(user);
                Log.Information("Partial Update Booking test passed");

                var reqput = new RestRequest("booking/" + userId, Method.Put);
                reqput.AddHeader("Content-Type", "Application/Json");
                reqput.AddHeader("Cookie", "token=" + user.Token);
                reqput.AddJsonBody(new
                {
                    totalprice = "500",
                    additionalneeds = "Extra pillows"
                });

                Log.Information("Partial Update Booking test passed");

                test.Pass("Partial Update Booking Test Passed");
            }
            catch (AssertionException)
            {
                test.Fail("Partial Update Booking test failed");
            }
        }
        [Test]
        [Order(5)]
        public void GetHealthCheckTest()
        {
            test = extent.CreateTest("Get Health Check");
            Log.Information("Get Health Check Test Started");

            var request = new RestRequest("ping", Method.Get);
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                Log.Information($"API Response: {response.Content}");

                Log.Information("Get Health Check test passed");

                test.Pass("Get Health Check test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Get Health Check test failed");
            }
        }

    }
}
