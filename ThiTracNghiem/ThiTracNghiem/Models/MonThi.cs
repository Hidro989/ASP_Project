using System.ComponentModel.DataAnnotations;

namespace ThiTracNghiem.Models
{
    public class MonThi
    {
        private int _iD;
        private string _tenMonThi;
        private int? _soLuongDe = 0;

   
        public int ID { get => _iD; set => _iD = value; }
        public string TenMonThi { get => _tenMonThi; set => _tenMonThi = value; }
        public int? SoLuongDe { get => _soLuongDe; set => _soLuongDe = value; }
        public ICollection<DeThi> DeThis { get; set; }

        public MonThi()
        {
            DeThis = new List<DeThi>();
        }
    }
}
