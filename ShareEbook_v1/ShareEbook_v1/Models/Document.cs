using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareEbook_v1.Models
{
    public class Document
    {
        private int _id;
        private string _name;
        private string _category;
        private string _author;
        private IFormFile _picture;
        private string _pictureUrl;
        private IFormFile _fileDocument;
        private string _fileUrl;
        private string _description;
        public int Id { get => _id; set => _id = value; }

        [Required(ErrorMessage = "Vui lòng nhập tên tài liệu"), StringLength(200), Display(Name = "Tên tài liệu")]
        public string Name { get => _name; set => _name = value; }

        [Display(Name = "Danh mục")]
        public string Category { get => _category; set => _category = value; }

        [Required(ErrorMessage = "Vui lòng nhập tên tác giả"), Display(Name = "Tác giả")]
        public string Author { get => _author; set => _author = value; }

        [Required(ErrorMessage = "Vui lòng chọn ảnh"), Display(Name = "Hình ảnh")]
        [NotMapped]
        public IFormFile Picture { get => _picture; set => _picture = value; }

        [Required(ErrorMessage = "Vui lòng chọn tệp tài liệu"), Display(Name = "Tệp tài liệu")]
        [NotMapped]
        public IFormFile FileDocument { get => _fileDocument; set => _fileDocument = value; }
        [Display(Name = "Mô tả")]
        public string Description { get => _description; set => _description = value; }
        public string PictureUrl { get => _pictureUrl; set => _pictureUrl = value; }
        public string FileUrl { get => _fileUrl; set => _fileUrl = value; }
    }
}
