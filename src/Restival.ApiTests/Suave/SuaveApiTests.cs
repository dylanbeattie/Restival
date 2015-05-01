namespace Restival.ApiTests.Suave {
    public class SuaveApiTests : ApiTestsBase {
        protected override string BaseUri {
            get { return ("http://restival.local:8080"); }
        }
    }
}