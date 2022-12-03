using System;

namespace ShareEbook_v1.Models
{   
    
    public class Post
    {
        private int _id;
        private DateTime _dateSubmitted;
        private bool _pending;
        
        public int Id { get => _id; set => _id = value; }

        public DateTime DateSubmitted { get => _dateSubmitted; set => _dateSubmitted = value; }
        public bool Pending { get => _pending; set => _pending = value; }

        public int DocumentId;
        public int UserId;
        public Document DocumentInfor;
        public User UserInfor;
    }
}
