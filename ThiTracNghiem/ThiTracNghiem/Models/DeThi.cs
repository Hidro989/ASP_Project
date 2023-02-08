using System.ComponentModel.DataAnnotations;

namespace ThiTracNghiem.Models
{
    public class DeThi
    {
        private int _iD;
        private string _tenDeThi;
        private int? _soLuongCauHoi = 0;
        private int? _thoiGian = 60;

      
        public int ID { get => _iD; set => _iD = value; }
        public string TenDeThi { get => _tenDeThi; set => _tenDeThi = value; }
        public int? SoLuongCauHoi { get => _soLuongCauHoi; set => _soLuongCauHoi = value; }
        public int? ThoiGian { get => _thoiGian; set => _thoiGian = value; }
        public MonThi MonThi { get; set; }
        public int MonThiID { get; set; }
        public ICollection<CauHoi> CauHois { get; set; }

        public DeThi()
        {
            CauHois = new List<CauHoi>();
        }
    }
}
