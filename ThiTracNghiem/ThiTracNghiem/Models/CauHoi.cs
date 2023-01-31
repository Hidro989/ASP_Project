using System.ComponentModel.DataAnnotations;

namespace ThiTracNghiem.Models
{   

    public enum DapAnDung
    {
        A = 1, B = 2, C = 3, D = 4
    }
    public class CauHoi
    {   
        private int _iD;
        private string _noiDung;
        private string _a;
        private string _b;
        private string _c;
        private string _d;
        private DapAnDung _dapAnDung;

        
        public int ID { get => _iD; set => _iD = value; }
        public string NoiDung { get => _noiDung; set => _noiDung = value; }
        public string A { get => _a; set => _a = value; }
        public string B { get => _b; set => _b = value; }
        public string C { get => _c; set => _c = value; }
        public string D { get => _d; set => _d = value; }
        public DapAnDung DapAnDung { get => _dapAnDung; set => _dapAnDung = value; }

        public int DeThiID { get; set; }
        public DeThi DeThi { get; set; }
    }
}
