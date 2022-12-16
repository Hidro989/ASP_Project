using ShareEbook_v1.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ShareEbook_v1.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên người dùng"), Display(Name = "Tên người dùng")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu"), DataType(DataType.Password), StringLength(16, ErrorMessage = "Mật khẩu có độ dài tối đa 16 ký tự")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Mật khẩu có độ dài tối thiểu là 8 ký tự, tồn tại ít nhất một ký tự viết hoa, một ký tự viết thường, một ký tự đặc biệt")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ và tên"), StringLength(50), Display(Name = "Họ và tên")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email"), DataType(DataType.EmailAddress),]
        [RegularExpression("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "Vui lòng nhập email hợp lệ!!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại"), DataType(DataType.PhoneNumber)]
        public string Phonenumber { get; set; }

        public Gender Gender { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { get; set; }

    }
}
