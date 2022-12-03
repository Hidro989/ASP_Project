namespace ShareEbook_v1.Models
{
    public class Document
    {
        private string _id;
        private string _name;
        private string _category;
        private string _author;
        private byte[] _picture;
        private byte[] _fileDocument;
        private string _description;
        public string Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Category { get => _category; set => _category = value; }
        public string Author { get => _author; set => _author = value; }
        public byte[] Picture { get => _picture; set => _picture = value; }
        public byte[] FileDocument { get => _fileDocument; set => _fileDocument = value; }
        public string Description { get => _description; set => _description = value; }
    }
}
