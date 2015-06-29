namespace Restival.Data.Entities {
    public class Profile {
        public string Id { get; set; }
        public string Name { get; set; }
        public Profile() { }

        public Profile(string id, string name) {
            this.Id = id;
            this.Name = name;
        }
    }
}
