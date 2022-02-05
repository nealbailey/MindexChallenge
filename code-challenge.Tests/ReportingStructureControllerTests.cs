using challenge.Models;
using code_challenge.Tests.Integration.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureControllerTests
    {
        protected static HttpClient _httpClient;
        protected static TestServer _testServer;

        #region Test Scaffolding

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestServerStartup>()
                .UseEnvironment("Development"));

            _httpClient = _testServer.CreateClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        #endregion

        [TestMethod]
        public void GetReportingStructure_ChallengeTask_1()
        {
            // Arrange
            var lennonUid = "16a596ae-edd3-4847-99fe-c4518e82c86f";

            // Act
            var requestTask = _httpClient.GetAsync($"api/reportingstructure/{lennonUid}");
            var response = requestTask.Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(reportingStructure.Employee.FirstName, "John");
            Assert.AreEqual(reportingStructure.Employee.LastName, "Lennon");
            Assert.AreEqual(reportingStructure.Employee.Position, "Development Manager");
            Assert.AreEqual(reportingStructure.Employee.Department, "Engineering");
            Assert.IsNotNull(reportingStructure.Employee.DirectReports);
            Assert.AreEqual(2, reportingStructure.Employee.DirectReports.Count);
            Assert.AreEqual(4, reportingStructure.NumberOfReports);
        }

        [TestMethod]
        public void GetReportingStructure_ChallengeTask_1_VerifyCounts()
        {
            // Arrange
            var uids = new List<string> {
                "16a596ae-edd3-4847-99fe-c4518e82c86f", // John
                "b7839309-3348-463b-a7e3-5de1c168beb3", // Paul
                "03aa1462-ffa9-4978-901b-7c001562cf6f", // Ringo
                "62c1084e-6e34-4630-93fd-9153afb65309", // Pete
                "c0c2293d-16bd-4603-8e08-638a9d18b22c"  // George
            };

            // Act
            for (int i = 0; i < uids.Count; i++)
            {
                var requestTask = _httpClient.GetAsync($"api/reportingstructure/{uids[i]}");
                var response = requestTask.Result;
                var reportingStructure = response.DeserializeContent<ReportingStructure>();

                // Assert
                switch (i)
                {
                    case 0: Assert.AreEqual(4, reportingStructure.NumberOfReports); break;
                    case 1: Assert.AreEqual(0, reportingStructure.NumberOfReports); break;
                    case 2: Assert.AreEqual(2, reportingStructure.NumberOfReports); break;
                    case 3: Assert.AreEqual(0, reportingStructure.NumberOfReports); break;
                    case 4: Assert.AreEqual(0, reportingStructure.NumberOfReports); break;
                }
            }
        }

        [TestMethod]
        public void GetReportingStructure_ChallengeTask_1_NotFound()
        {
            // Arrange
            var fakeUid = "this_is_an_injection_;DELETE * FROM USERS";

            // Act
            var requestTask = _httpClient.GetAsync($"api/reportingstructure/{fakeUid}");
            var response = requestTask.Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);            
        }
    }
}
