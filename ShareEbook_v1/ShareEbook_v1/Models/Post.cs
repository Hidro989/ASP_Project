using System;
using System.ComponentModel.DataAnnotations;

namespace ShareEbook_v1.Models
{   
    
    public class Post
    {
        private int _id;
        private DateTime _dateSubmitted = DateTime.Now;
        private bool _pending = false;
        
        public int Id { get => _id; set => _id = value; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Ngày đăng")]
        public DateTime DateSubmitted { get => _dateSubmitted; set => _dateSubmitted = value; }
        public bool Pending { get => _pending; set => _pending = value; }

        public int DocumentId { get; set; }
        public int UserId { get; set; }
        public Document DocumentInfor { get; set; }
        public User UserInfor { get; set; }
    }
}
