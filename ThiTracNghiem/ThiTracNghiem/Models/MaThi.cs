using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ThiTracNghiem.Models
{
    public class MaThi
    {
        [Key]
        public string Ma { get => _ma; set => _ma = value; }
        [Required]
        [Range(0, 5)]
        public int SLSD { get => _sLSD; set => _sLSD = value; }
        
        private string _ma;

        private int _sLSD = 5; // Số lần sử dụng
    }
}
