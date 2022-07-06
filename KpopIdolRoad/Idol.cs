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
            Console.ReadKey();
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
        }
        public static void GenerateRandom()
        {
            var lista = HelperJson.cargarArchivo(dataJson);
            var random = new Random().Next(0, lista.Count);
            var idol = new Idol();
            idol = lista[random];
            InitializeStats(idol);
        }
    }
}
