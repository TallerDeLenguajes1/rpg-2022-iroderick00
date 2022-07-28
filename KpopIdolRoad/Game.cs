using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpopIdolRoad
{
    public class Game
    {
        string pathWin = "ganadoras.json";
        private const int TURNOS = 3;
        private const int LINEUP = 7;
        public Idol Player { get; set; }
        public List<Idol> LineUp { get; set; }
        private bool run;
        public Game () {  }
        public static void CreateLineUp(Game partida)
        {
            partida.LineUp = new List<Idol>();
            partida.Player.Energy = 100;
            for (int i = 0; i < LINEUP; i++)
            {
                var aux = new Idol();
                aux = Idol.GenerateRandom();
                partida.LineUp.Add(aux);
            }
        }
        public static void GiftRandom(Game partida)
        {
            Console.WriteLine("SE VA A ASIGNAR UNA IDOL ALEATORIA PARA COMENZAR");
            partida.Player = Idol.GenerateRandom();
            Console.WriteLine("IDOL GENERADA: ");
            Idol.PrintIdolData(partida.Player);
            Idol.PrintIdolStats(partida.Player);
        }
        public static void ChooseIdol(Game partida, List<Idol> list)
        {
            int cont = 0, eleccion;
            Console.WriteLine("LISTA DE IDOLS OBTENIDAS");
            foreach (var idol in list)
            {
                Console.WriteLine($"Posición en la lista: {cont}");
                Idol.PrintIdolData(idol);
                Idol.PrintIdolStats(idol);
                Console.WriteLine("---------");
            }
            Console.WriteLine("Ingrese la posición de la idol que desea utilizar: ");
            eleccion = Convert.ToInt32(Console.ReadLine());
            partida.LineUp[0] = list[eleccion];
        }
        public static bool SongBuff(string songArtist, Idol idol)
        {
            return songArtist == idol.Group;
        }
        private static void DecreaseEnergy(Idol idol, double energy)
        {
            idol.Energy -= energy;
        }
        private static void Benefit(Idol idol)
        {
            int choice;
            Console.WriteLine("Como premio por ganar la ronda hay 2 posibles beneficios: ");
            Console.WriteLine("[1]: Mas motivacion");
            Console.WriteLine("[2]: Mas efectividad");
            choice = Convert.ToInt32(Console.ReadLine());
            Idol.BuffStats(idol,choice);
            idol.Energy += 50;
        }
        public static Idol Round(Game partida)
        {
            if (partida.LineUp.Count > 0)
            {
                double dmg1, dmg2, turns = 0;
                while (partida.LineUp[0].Energy >= 0 && partida.Player.Energy >= 0 && turns <= TURNOS)
                {
                    Console.WriteLine($"turno {turns}");
                    dmg1 = Math.Max(0, Idol.Skill(partida.Player) - partida.LineUp[0].Motivation);
                    dmg2 = Math.Max(0, Idol.Skill(partida.LineUp[0]) - partida.Player.Motivation);
                    Console.WriteLine($"DMG RECIBIDO PLAYER: {dmg2}");
                    Console.WriteLine($"DMG RECIBIDO CPU: {dmg1}");
                    DecreaseEnergy(partida.Player, dmg2);
                    DecreaseEnergy(partida.LineUp[0], dmg1);
                    Console.WriteLine($"PLAYER Energía restante: {partida.Player.Energy}");
                    Console.WriteLine($"CPU Energía restante: {partida.LineUp[0].Energy}");
                    turns++;
                }
                if (partida.Player.Energy < partida.LineUp[0].Energy)
                {
                    partida.run = false;
                    return partida.LineUp[0];
                }
                partida.LineUp.Remove(partida.LineUp[0]);
                return partida.Player;
            }
            else
            {
                partida.run = false;
                return partida.Player;
            }
        }
        public static void Start(Game partida)
        {
            int end = 1;
            do
            {
                var control = new Idol();
                partida.run = true;
                CreateLineUp(partida);  
                int cont = 1;
                var winners = new List<Idol>();
                while (partida.run/* && partida.LineUp.Count > 0*/)
                {
                    Console.WriteLine($"RONDA NUMERO {cont}");
                    control = Round(partida);
                    cont++;
                    Console.WriteLine($"Ganadora: {control.StageName} de {control.Group}");
                    if (control == partida.Player && partida.LineUp.Count > 0)
                    {
                        Benefit(partida.Player);
                    }
                }
                if (partida.LineUp.Count == 0)
                {
                    Console.WriteLine("WIN");
                    winners = HelperJson.CargarArchivo(partida.pathWin);
                    if (!winners.Contains(partida.Player))
                    {
                        winners.Add(partida.Player);
                    }
                    HelperJson.GenerarJson(partida.pathWin, winners);
                }
                else
                {
                    Console.WriteLine("LOSE");
                }
                Console.WriteLine($"¿Volver a jugar con {partida.Player.StageName} de {partida.Player.Group}? [0]:NO - [1]:SI");
                end = Convert.ToInt32(Console.ReadLine());
            } while (end == 1);
        }
    }
}
