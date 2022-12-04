using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Username { get => _username; set => _username = value; }

        [Required, DataType(DataType.Password), StringLength(16, ErrorMessage = "Mật khẩu có độ dài tối đa 16 ký tự")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Mật khẩu có độ dài tối thiểu là 8 ký tự, tồn tại ít nhất một ký tự viết hoa, một ký tự viết thường, một ký tự đặc biệt")]
        public string Password { get => _password; set => _password = value; }
        public TypeAccount Type { get => _type; set => _type = value; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
