namespace SHARED.Models
{
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DtCreated { get; set; }

        public User() { }

        public User(string name, int age, DateTime dtCreated)
        {
            Name = name;
            Age = age;
            DtCreated = dtCreated;
        }
    }
}