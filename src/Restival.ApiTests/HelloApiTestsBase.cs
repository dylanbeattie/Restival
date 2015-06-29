using System;
using NUnit.Framework;
using Restival.Api.Common.Resources;
using RestSharp;
using Shouldly;

namespace Restival.ApiTests {
    public abstract class HelloApiTestsBase<TApi> : ApiTestBase<TApi> where TApi : IApiUnderTest, new() {
        [Test]
        public void GET_Hello_Returns_Greeting() {
            var client = new RestClient(BaseUri);
            var request = new RestRequest("hello");
            Console.WriteLine(client.BuildUri(request));
            var status = client.Execute<HelloResponse>(request);
            Console.WriteLine(status.Content);
            status.Data.Message.ShouldBe("Hello, World!");
        }

        [Test]
        [TestCase("Alice")]
        [TestCase("Bryan")]
        [TestCase("Carol")]
        public void Get_Hello_Name_In_Path_Returns_Greeting(string name) {
            var client = new RestClient(BaseUri);
            var request = new RestRequest("hello/{name}");
            request.AddUrlSegment("name", name);
            Console.WriteLine(client.BuildUri(request));
            var status = client.Execute<HelloResponse>(request);
            status.Data.Message.ShouldBe(String.Format("Hello, {0}!", name));
        }
    }
}