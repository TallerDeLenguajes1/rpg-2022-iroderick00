using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpopIdolRoad
{
    public class Idol
    {
        const string dataJson = "idolsList.json";
        public enum enumPosition
        {
            //Center,
            //Leader,
            //Visual,
            Vocalist,
            Rapper,
            Dancer,
        }
        public enum enumAgency
        {
            YG,
            JYP,
            SM,
            Cube
        }
        public string StageName { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public string Country { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public enumPosition Position  { get; set; }
        public enumAgency Agency { get; set; }
        private int vocal;
        public int Vocal
        {
            get { return vocal; }
            set
            {
                if (value > 0 && value < 100)
                {
                    vocal = value;
                }
            }
        }
        private int rap;
        public int Rap
        {
            get { return rap; }
            set
            {
                if (value > 0 && value < 100)
                {
                    rap = value;
                }
            }
        }
        private int dance;
        public int Dance
        {
            get { return dance; }
            set
            {
                if (value > 0 && value < 100)
                {
                    dance = value;
                }
            }
        }
        public int Energy { get; set; }
        public Idol() { }
        public Idol(string name, string stageName, DateTime birth, string country, int position, double height, double weight, int agency)
        {
            StageName = stageName;
            Name = name;
            Birth = birth;
            Country = country;
            Position = (enumPosition)position;
            Height = height;
            Weight = weight;
            Agency = (enumAgency)agency;
            InitializeStats(this);
        }
        public static void InitializeStats(Idol idol)
        {
            int number = (int)idol.Position;
            switch (number)
            {
                case 0:
                    idol.Vocal = new Random().Next(70,100);
                    idol.Rap = new Random().Next(0, 70);
                    idol.Dance = new Random().Next(0, 70);
                    break;
                case 1:
                    idol.Vocal = new Random().Next(0, 70);
                    idol.Rap = new Random().Next(70, 100);
                    idol.Dance = new Random().Next(0, 70);
                    break;
                case 2:
                    idol.Vocal = new Random().Next(0, 70);
                    idol.Rap = new Random().Next(0, 70);
                    idol.Dance = new Random().Next(70, 100);
                    break;
            }
            idol.Energy = 100;
        }
        public static Idol GenerateRandom(Idol idol) // POR QUE ACA TENGO QUE RETURNEAR IDOL Y EN CREATELINEUP NO TENGO QUE RETURNEAR NEWGAME
        {
            var lista = HelperJson.CargarArchivo(dataJson);
            var random = new Random().Next(0, lista.Count);
            idol = lista[random];
            InitializeStats(idol);
            return idol;
        }
        public static void PrintIdolData(Idol idol)
        {
            Console.WriteLine($"Stage name: {idol.StageName}");
            Console.WriteLine($"Name: {idol.Name}");
            Console.WriteLine($"Birth: {idol.Birth}");
            Console.WriteLine($"Country: {idol.Country}");
            Console.WriteLine($"Height: {idol.Height}");
            Console.WriteLine($"Weight: {idol.Weight}");
            Console.WriteLine($"Position: {idol.Position}");
            Console.WriteLine($"Agency: {idol.Agency}");
        }
        public static void PrintIdolStats(Idol idol)
        {
            Console.WriteLine($"Vocal: {idol.Vocal}");
            Console.WriteLine($"Rap: {idol.Rap}");
            Console.WriteLine($"Dance: {idol.Dance}");
        }

    }
}
