﻿using System.ComponentModel.DataAnnotations;

namespace ShareEbook_v1.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu hiện tại của bạn")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu hiện tại")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }

   
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu mới")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu mới và mật khẩu xác nhận không khớp")]
        public string ConfirmPassword { get; set; }
    }
}
