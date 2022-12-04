using System;
using System.ComponentModel.DataAnnotations;

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

        [Required, StringLength(50)]
        public string Name { get => _name; set => _name = value; }

        [Required,DataType(DataType.EmailAddress),]
        [RegularExpression("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "Vui lòng nhập email hợp lệ!!")]
        public string Email { get => _email; set => _email = value; }

        [Required, DataType(DataType.PhoneNumber)]
       // [RegularExpression(@"(((^\+|)84)|0)(3|5|7|8|9)+([0-9]{10})$", ErrorMessage = "Vui lòng nhập số điện thoại hợp lệ!!")]
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }


        public Gender Gender { get => _gender; set => _gender = value; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get => _birthday; set => _birthday = value; }
        //public int Status { get => _status; set => _status = value; }
    }
}
