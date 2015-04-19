using System;

namespace Restival.Api.Common.Resources {
    public class Greeting {
        public string Message { get; set; }

        public Greeting() {
            Message = "Hello, World!";
        }

        public Greeting(string name) {
            Message = String.Format("Hello, {0}!", name);
        }
    }
}