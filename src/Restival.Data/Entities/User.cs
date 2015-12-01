using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restival.Data.Entities {
    public class User {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public User() { }

        public User(string id, string username, string password, string name) {
            this.Id = Guid.Parse(id);
            this.Username = username;
            this.Password = password;
            this.Name = name;
        }
    }
}
