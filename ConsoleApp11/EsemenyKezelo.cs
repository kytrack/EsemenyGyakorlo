using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    public class EsemenyKezelo
    {
        List<Esemeny> esemenyLista;
        //        Ennek legyenek az alábbi metódusai:
        //void LoadFromData(String path)
        //List<String> EbedelokKodjai()
        //List<Esemeny> GetEsemenyek(int esemenyFajta)
        //List<String> KikVoltakIskolaban(int kezdoOra, int kezdoPerc, int zaroOra, int zaroPerc)
        //- A megadott intervallumban iskolában tartózkódok listája

        public void LoadFromData(String path)
        {
            esemenyLista = File.ReadLines(path).Select(x => new Esemeny(x)).ToList();
        }

        public TimeOnly[] ElsoEsUtolsoTanulo()
        {
            TimeOnly[] idopontok = new TimeOnly[2];
            idopontok[0] = esemenyLista.First().EsemenyIdopont;
            idopontok[1] = esemenyLista.Last().EsemenyIdopont;
            return idopontok;
        }


    }
}
