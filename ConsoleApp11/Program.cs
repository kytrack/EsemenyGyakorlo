using ConsoleApp11;

internal class Program
{
    private static void Main(string[] args)
    {
        var esemenyKezelo = new EsemenyKezelo();
        esemenyKezelo.LoadFromData("../../../bedat.txt");

        var elsoesutolso = esemenyKezelo.ElsoEsUtolsoTanulo();
        Console.WriteLine("2. feladat\n" +
            $"Az első tanuló {elsoesutolso[0]}-kor lépett be a főkapun." +
            $"\nAz utolsó tanuló {elsoesutolso[1]}-kor lépett ki a főkapun.");

        esemenyKezelo.KesokMentes();

        Console.WriteLine($"4. feladat\nA menzán aznap {esemenyKezelo.EbedelokSzama} tanuló ebédelt.");

        Console.WriteLine($"5. feladat\nAznap {esemenyKezelo.Kolcsonzokszama} tanuló kölcsönzött a könyvtárban.\n{esemenyKezelo.KolcsonzokVagyEbedelok}");

        Console.WriteLine($"6. feladat\nAz érintett tanulók:\n{esemenyKezelo.LogoDiakok()}");

        Console.Write("7. feladat\nEgy tanuló azonosítója=");
        string bekeretkod = Console.ReadLine().ToUpper();
        Console.WriteLine(esemenyKezelo.ErkezesEsTavzoasKozottIdo(bekeretkod));


    }
}