namespace Restival.Data.Entities {
    public class Profile {
        public Profile() {}

        public Profile(string name) {
            Name = name;
        }

        public User User { get; set; }
        public string Name { get; set; }
    }
}