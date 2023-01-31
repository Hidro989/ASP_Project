﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThiTracNghiem.Models
{
    public class Admin
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserName { get; set; }
        public string Password { get; set; }    
    }
}
