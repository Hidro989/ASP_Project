namespace ShareEbook_v1.Models
{   
    public enum TypeAccount
    {
        Admin, User
    }
    public class Account
    {
        private string _username;
        private string _password;
        private TypeAccount _type;

        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }
        public TypeAccount Type { get => _type; set => _type = value; }

        public int UserId;
        public User User;
    }
}
