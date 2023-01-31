using System.Security.Cryptography;
using ThiTracNghiem.Models;

namespace ThiTracNghiem.ViewModels
{
    public class DeThiVM
    {
        public int ID { get; set; }
        public string TenDeThi { get; set; }
        public int? SoLuongCauHoi { get ; set; }
        public int? ThoiGian { get; set; }
        public int MonThiID { get; set; }
    }
}
