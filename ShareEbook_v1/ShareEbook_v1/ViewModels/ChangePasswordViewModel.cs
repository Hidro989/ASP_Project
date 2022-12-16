using System.ComponentModel.DataAnnotations;

namespace ShareEbook_v1.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu hiện tại của bạn")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu hiện tại")]
        public string CurrentPassword { get; set; }


        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới"), DataType(DataType.Password), StringLength(16, ErrorMessage = "Mật khẩu có độ dài tối đa 16 ký tự")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Mật khẩu có độ dài tối thiểu là 8 ký tự, tồn tại ít nhất một ký tự viết hoa, một ký tự viết thường, một ký tự đặc biệt")]
        public string NewPassword { get; set; }

   
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu mới")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu mới và mật khẩu xác nhận không khớp")]
        public string ConfirmPassword { get; set; }
    }
}
