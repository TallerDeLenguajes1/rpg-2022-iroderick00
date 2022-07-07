using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpopIdolRoad
{
    public class Game
    {
        public Idol Player { get; set; }
        public List<Idol> LineUp { get; set; }
        public Game () { }
        public static void CreateLineUp(Game partida, int cantidad)
        {
            partida.LineUp = new List<Idol>();
            for (int i = 0; i < cantidad; i++)
            {
                var aux = new Idol();
                aux = Idol.GenerateRandom(aux);
                partida.LineUp.Add(aux);
            }
        }
        public static void Launch()
        {
            var newGame = new Game ();
            CreateLineUp(newGame, 8);
            Start(newGame);
        }
        public static void LaunchSave(List<Idol> list)
        {
            var newGame = new Game();
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
            newGame.LineUp[0] = list[eleccion];
            CreateLineUp(newGame, 7);
        }
        public static void Start(Game partida)
        {

        }
    }
}
