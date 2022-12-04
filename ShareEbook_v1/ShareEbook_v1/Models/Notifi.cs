namespace ShareEbook_v1.Models
{
    public class Notifi
    {
        private int _id;
        private string _content;
        private int _type;

        public int Id { get => _id; set => _id = value; }
        public string Content { get => _content; set => _content = value; }
        public int Type { get => _type; set => _type = value; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
