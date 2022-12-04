using System.ComponentModel.DataAnnotations;

namespace ShareEbook_v1.Models
{
    public class Document
    {
        private int _id;
        private string _name;
        private string _category;
        private string _author;
        private byte[] _picture;
        private byte[] _fileDocument;
        private string _description;
        public int Id { get => _id; set => _id = value; }

        [Required, StringLength(200)]
        public string Name { get => _name; set => _name = value; }
        public string Category { get => _category; set => _category = value; }

        [Required]
        public string Author { get => _author; set => _author = value; }

        [Required]
        public byte[] Picture { get => _picture; set => _picture = value; }

        [Required]
        public byte[] FileDocument { get => _fileDocument; set => _fileDocument = value; }
        public string Description { get => _description; set => _description = value; }
    }
}
