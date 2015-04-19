namespace Restival.ApiTests.ServiceStack {
    public class ServiceStackApiTests : ApiTestsBase {
        protected override string BaseUri {
            get { return ("http://restival.local/api.servicestack"); }
        }
    }
}
