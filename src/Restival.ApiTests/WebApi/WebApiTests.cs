namespace Restival.ApiTests.WebApi {
    public class WebApiTests : ApiTestsBase {
        protected override string BaseUri {
            get { return ("http://restival.local/api.webapi"); }
        }
    }
}