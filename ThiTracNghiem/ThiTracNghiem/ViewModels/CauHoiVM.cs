using ThiTracNghiem.Models;

namespace ThiTracNghiem.ViewModels
{
    public class CauHoiVM
    {
        public int ID { get; set; }
        public string NoiDung { get;set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }

        public DapAnDung DapAnDung { get; set; }
        public int DethiID { get; set; }
    }
}
