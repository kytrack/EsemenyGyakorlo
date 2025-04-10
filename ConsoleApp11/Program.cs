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


    }
}