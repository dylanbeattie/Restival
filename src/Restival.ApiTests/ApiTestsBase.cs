using System;
using NUnit.Framework;
using Restival.Api.Common.Resources;
using RestSharp;
using Shouldly;

namespace Restival.ApiTests {
    [TestFixture]
    public abstract class ApiTestsBase {
        protected abstract string BaseUri { get; }

        [Test]
        public void GET_Hello_Returns_Greeting() {
            var client = new RestClient(BaseUri);
            var request = new RestRequest("hello");
            Console.WriteLine(client.BuildUri(request));
            var status = client.Execute<Greeting>(request);
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
            var status = client.Execute<Greeting>(request);
            status.Data.Message.ShouldBe(String.Format("Hello, {0}!", name));
        }
    }
}