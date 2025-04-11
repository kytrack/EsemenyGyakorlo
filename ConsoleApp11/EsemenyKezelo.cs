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

        public List<String> EbedelokKodjai()
        {
            return esemenyLista.Where(x=>x.EsemenyKod==3).Select(x=>x.TanuloKod).ToList();
        }

        public List<Esemeny> GetEsemenyek(int esemenyFajta) 
        {
            return esemenyLista.Where(x => x.EsemenyKod == esemenyFajta).ToList();
        }

        public List<String> KikVoltakIskolaban(int kezdoOra, int kezdoPerc, int zaroOra, int zaroPerc)
        {
            var elsoerkezesek = esemenyLista.Where(x => x.EsemenyKod == 1).GroupBy(x => x.TanuloKod).Select(x=>x.First());    
            var utolsotavozasok = esemenyLista.Where(x => x.EsemenyKod == 2).GroupBy(x => x.TanuloKod).Select(x => x.Last());

            var erkezeskodok =elsoerkezesek.Where(x => x.EsemenyIdopont <= new TimeOnly(kezdoOra, kezdoPerc)).Select(x=>x.TanuloKod);
            var tavozaskodok = utolsotavozasok.Where(x => x.EsemenyIdopont >= new TimeOnly(zaroOra, zaroPerc)).Select(x => x.TanuloKod);

            var ittvoltak = erkezeskodok.Intersect(tavozaskodok);

            return ittvoltak.ToList();
        }

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

        public void KesokMentes()
        {
            var kesok=esemenyLista.Where(x => x.EsemenyIdopont > new TimeOnly(7, 50) &&
            x.EsemenyIdopont<new TimeOnly(8,16)).Select(x=>$"{x.EsemenyIdopont} {x.TanuloKod}");

            File.WriteAllLines("kesok.txt", kesok);
        }

        public int EbedelokSzama{ get => esemenyLista.Count(x => x.EsemenyKod == 3); }
        public int Kolcsonzokszama{get=> esemenyLista.Where(x => x.EsemenyKod == 4).DistinctBy(x=>x.TanuloKod).Count();}

        public string KolcsonzokVagyEbedelok { get => Kolcsonzokszama > EbedelokSzama ? "Többen voltak, mint a menzán" : "Nem voltak többen, mint a menzán"; }

        public string LogoDiakok()
        {
            var aznapKorabbanMarBement = esemenyLista
                .Where(x => x.EsemenyKod == 1 && x.EsemenyIdopont < new TimeOnly(10, 45))
                .Select(x => x.TanuloKod)
                .Distinct();

            var visszajottek = esemenyLista
                .Where(x => x.EsemenyKod == 1 && x.EsemenyIdopont >= new TimeOnly(10, 45) && x.EsemenyIdopont <= new TimeOnly(11, 0))
                .Select(x => x.TanuloKod)
                .Distinct();

            var foKapusKilepok = esemenyLista
                .Where(x => x.EsemenyKod == 2 && x.EsemenyIdopont >= new TimeOnly(10, 45) && x.EsemenyIdopont < new TimeOnly(10, 50))
                .Select(x => x.TanuloKod)
                .Distinct();

            var sunnyogok = visszajottek.Except(foKapusKilepok).Intersect(aznapKorabbanMarBement);

            return string.Join(" ", sunnyogok);
        }

        public string ErkezesEsTavzoasKozottIdo(string kod)
        {
            if (Letezokod(kod))
            {
                var erkezes = esemenyLista.Where(x => x.TanuloKod == kod && x.EsemenyKod == 1).First().EsemenyIdopont;
                var tavozas = esemenyLista.Where(x => x.TanuloKod == kod && x.EsemenyKod == 2).Last().EsemenyIdopont;
                var elteltido = tavozas - erkezes;
                return $"A tanuló érkezése és távozása között {elteltido.Hours} óra {elteltido.Minutes} perc telt el.";
            }
            return "Ilyen azonosítójú tanuló aznap nem volt az iskolában.";
        }

        public bool Letezokod(string kod)
        {
            return esemenyLista.Where(x => x.TanuloKod == kod).Any() ? true : false;
        }

    }
}
