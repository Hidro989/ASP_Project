using System;

namespace ShareEbook_v1.Models
{   
    public enum Gender
    {
        Male, Female, Other
    }
    public class User
    {
        private int _id;
        private string _name;
        private string _email;
        private string _phoneNumber;
        private Gender _gender;
        private DateTime _birthday;

        // Xử lý trạng thái sau
        //private int _status; 

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Email { get => _email; set => _email = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public Gender Gender { get => _gender; set => _gender = value; }
        public DateTime Birthday { get => _birthday; set => _birthday = value; }
        //public int Status { get => _status; set => _status = value; }
    }
}
