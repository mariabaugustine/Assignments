using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestBookerApi.Utilities
{
    public class CoreCodes
    {
        public RestClient client;
        public ExtentReports extent;
        public ExtentTest? test;
        public ExtentSparkReporter sparkReporter;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            string currentDirectory = Directory.GetParent(@"../../../").FullName;
            extent = new ExtentReports();
            sparkReporter = new ExtentSparkReporter(currentDirectory + "/ExtentReports/extent-report-"
                + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".html");
            extent.AttachReporter(sparkReporter);
            string logFilePath = currentDirectory + "/Logs/log_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            Log.Logger = new LoggerConfiguration().WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day).CreateLogger();
        }
        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://restful-booker.herokuapp.com/booking");
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            extent.Flush();
        }
    }
}
