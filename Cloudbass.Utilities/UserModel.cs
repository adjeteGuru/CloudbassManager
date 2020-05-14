namespace Cloudbass.Utilities
{
    public class UserModel
    {
        public string Name { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public int TokenVersion { get; set; }

        public bool? Active { get; set; }

        public string Salt { get; set; }
    }
}