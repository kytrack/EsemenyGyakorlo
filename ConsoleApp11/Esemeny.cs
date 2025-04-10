using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    public class Esemeny
    {
        string tanuloKod;
        TimeOnly esemenyIdopont;
        int esemenyKod;

        public Esemeny(string sor)
        {
            string[] adatok = sor.Split(' ');
            this.tanuloKod = adatok[0];
            // this.esemenyIdopont =TimeSpan.ParseExact(adatok[1],"hh\\:mm",null);
            this.esemenyIdopont = TimeOnly.Parse(adatok[1]);
            this.esemenyKod =int.Parse(adatok[2]);
        }

        public string TanuloKod { get => tanuloKod; set => tanuloKod = value; }
        public TimeOnly EsemenyIdopont { get => esemenyIdopont; set => esemenyIdopont = value; }
        public int EsemenyKod { get => esemenyKod; set => esemenyKod = value; }
    }
}
