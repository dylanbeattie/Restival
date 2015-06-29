using System;
using NUnit.Framework;
using Restival.Api.Common.Resources;
using RestSharp;
using Shouldly;

namespace Restival.ApiTests {
    public abstract class ProfilesApiTestBase<TApi> : ApiTestBase<TApi> where TApi : IApiUnderTest, new() {
        [Test]
        public void GET_Profiles_Returns_Profiles() {
            var client = new RestClient(BaseUri);
            var request = new RestRequest("profiles");
            Console.WriteLine(client.BuildUri(request));
            var result = client.Execute<ProfileListResponse>(request);
            Console.WriteLine(result.Content);
            result.Data.ShouldBeOfType(typeof(ProfileListResponse));
        }

        [Test]
        public void GET_Profiles_With_Pagination_Returns_All_Profiles() {
            var client = new RestClient(BaseUri);
            var request = new RestRequest("profiles");
            Console.WriteLine(client.BuildUri(request));
            var result = client.Execute<ProfileListResponse>(request);
        }
    }
}