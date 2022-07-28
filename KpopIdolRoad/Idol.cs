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
        //Traits
        public string StageName { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public string Group { get; set; }
        public string Country { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public enumPosition Position  { get; set; }
        public enumAgency Agency { get; set; }
        private int vocal;
        //Stats
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
        private double energy;
        public double Energy 
        {
            get { return energy; }
            set
            {
                if ((value >= 0) && (value <= 100))
                {
                    energy = value;
                }
                else
                {
                    if (value < 0 )
                    {
                        energy = 0;
                    }
                    else
                    {
                        if (value > 100)
                        {
                            energy = 100;
                        }
                    }
                }
            }
        }
        private int cd;
        private double eff;
        public double Eff
        {
            get { return eff; }
            set
            {
                if ((value >= 0) && (value <= 100))
                {
                    eff = value;
                }
                else
                {
                    if (value > 100)
                    {
                        eff = 100;
                    }
                }
            }
        }
        public int Motivation { get; set; }
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
                    idol.Vocal = new Random().Next(70, 100);
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
            idol.Motivation = new Random().Next(10, 30);
            idol.Energy = 100;
            idol.Eff = new Random().Next(0, 50);
            idol.cd = 3;
        }
        public static Idol GenerateRandom() // POR QUE ACA TENGO QUE RETURNEAR IDOL Y EN CREATELINEUP NO TENGO QUE RETURNEAR NEWGAME
        {
            var newIdol = new Idol();
            var lista = HelperJson.CargarArchivo(dataJson);
            var random = new Random().Next(0, lista.Count);
            newIdol = lista[random];
            InitializeStats(newIdol);
            return newIdol;
        }
        public static void PrintIdolData(Idol idol)
        {
            Console.WriteLine($"Stage name: {idol.StageName}");
            Console.WriteLine($"Name: {idol.Name}");
            Console.WriteLine($"Birth: {idol.Birth}");
            Console.WriteLine($"Group: {idol.Group}");
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
            Console.WriteLine($"Motivation: {idol.Motivation}");
        }
        //Skills
        public static double Skill(Idol idol)
        {
            double dmg = 0;
            double eff = idol.Eff / (double)100;
            var playlist = new Playlist();
            playlist = SpotifyAPI.get();
            int random = new Random().Next(0, playlist.items.Count()-1);
            if (Game.SongBuff(playlist.items[random].track.artists[0].name, idol));
            {
                eff = Math.Min(1,eff*2);
            }
            int number = (int)idol.Position;
            if (idol.cd == 0)
            {
                switch (number)
                {
                    case 0:
                        dmg = idol.Vocal;
                        Console.WriteLine($"{idol.StageName} HIGH NOTE!");
                        break;
                    case 1:
                        dmg = idol.Rap;
                        Console.WriteLine($"{idol.StageName} FAST RAP!");
                        break;
                    case 2:
                        dmg = idol.Dance;
                        Console.WriteLine($"{idol.StageName} DANCE BREAK!");
                        break;
                }
                idol.cd = 3;
            }
            else
            {
                dmg = ((idol.Vocal + idol.Rap + idol.Dance)/3)* eff; 
                idol.cd--;
            }
            return dmg;
        }
        public static void BuffStats(Idol idol, int choice)
        {
            if (choice == 1)
            {
                idol.Motivation += 5;
            }
            else
            {
                idol.eff += 10;
            }
        }
    }
}
