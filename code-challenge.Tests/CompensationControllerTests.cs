using challenge.Models;
using code_challenge.Tests.Integration.Extensions;
using code_challenge.Tests.Integration.Helpers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Text;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
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
        public void Compensation_ChallengeTask_2_CreateCompensation()
        {
            var uid = "16a596ae-edd3-4847-99fe-c4518e82c86f";

            // Arrange
            var compensation = new Compensation()
            {
                EmployeeId = uid,
                EffectiveDate = System.DateTime.Parse("2015-08-15T09:35:00"),
                Salary = 165000.00
            };
            
            var requestContent = new JsonSerialization().ToJson(compensation);

            // Act
            var postRequestTask = _httpClient.PatchAsync(
                $"api/compensation/{uid}",
                new StringContent(requestContent, Encoding.UTF8, "application/json"));

            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var employee = response.DeserializeContent<Employee>();
            Assert.IsNotNull(employee.Compensation);
            Assert.AreEqual(employee.Compensation.EmployeeId, uid);
            Assert.AreEqual(employee.Compensation.Salary, 165000.00);            
        }

        [TestMethod]
        public void Compensation_ChallengeTask_2_CreateCompensation_IdOptions()
        {
            var requestContent = new JsonSerialization();

            //
            // Test #1: Id sent on Url but not the body
            //
            var compensation = new Compensation() {
                EffectiveDate = System.DateTime.Parse("2021-02-15T10:35:00"),
                Salary = 365000.00
            };
            var content = requestContent.ToJson(compensation);
            var requestTask = _httpClient.PatchAsync($"api/compensation/16a596ae-edd3-4847-99fe-c4518e82c86f", new StringContent(content, Encoding.UTF8, "application/json"));
            var response = requestTask.Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var employee = response.DeserializeContent<Employee>();
            Assert.AreEqual(employee.Compensation.EmployeeId, "16a596ae-edd3-4847-99fe-c4518e82c86f");

            //
            // Test #2: Id sent on body doesn't match the url id
            //
            compensation = new Compensation()  {
                EmployeeId = "b7839309-3348-463b-a7e3-5de1c168beb3",
                EffectiveDate = System.DateTime.Parse("2021-02-15T10:35:00"),
                Salary = 365000.00
            };
            content = requestContent.ToJson(compensation);
            requestTask = _httpClient.PatchAsync($"api/compensation/16a596ae-edd3-4847-99fe-c4518e82c86f", new StringContent(content, Encoding.UTF8, "application/json"));
            response = requestTask.Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

            //
            // Test #3: Id is not valid
            //
            var uid = System.Guid.NewGuid().ToString();
            compensation = new Compensation() {
                EmployeeId = uid,
                EffectiveDate = System.DateTime.Parse("2021-02-15T10:35:00"),
                Salary = 365000.00
            };
            content = requestContent.ToJson(compensation);
            requestTask = _httpClient.PatchAsync($"api/compensation/{uid}", new StringContent(content, Encoding.UTF8, "application/json"));
            response = requestTask.Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
