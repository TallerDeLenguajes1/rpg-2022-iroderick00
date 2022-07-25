using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpopIdolRoad
{
    public class Menu
    {
        public static void Welcome()
        {
            Console.Write(@"
 _    __  ______   ______   ______    _____  _____    ______   _          ______   ______   ______   _____   
| |  / / | |  | \ / |  | \ | |  | \    | |  | | \ \  / |  | \ | |        | |  | \ / |  | \ | |  | | | | \ \  
| |-< <  | |__|_/ | |  | | | |__|_/    | |  | |  | | | |  | | | |   _    | |__| | | |  | | | |__| | | |  | | 
|_|  \_\ |_|      \_|__|_/ |_|        _|_|_ |_|_/_/  \_|__|_/ |_|__|_|   |_|  \_\ \_|__|_/ |_|  |_| |_|_/_/  
                                                                                                             
");
        }
        public static void ThanksForPlaying()
        {
            Console.Write(@"
_______ __    _     ______  ______   ______     ______   _        ______  __    _  _____  ______   ______  
  | |   \ \  | |   | |     / |  | \ | |  | \   | |  | \ | |      | |  | | \ \  | |  | |  | |  \ \ | | ____ 
  | |    \_\_| |   | |---- | |  | | | |__| |   | |__|_/ | |   _  | |__| |  \_\_| |  | |  | |  | | | |  | | 
  |_|    ____|_|   |_|     \_|__|_/ |_|  \_\   |_|      |_|__|_| |_|  |_|  ____|_| _|_|_ |_|  |_| |_|__|_| 
                                                                                                             
");
        }
        public static void Start()
        {
            string pathSave = "progreso.json";
            string pathWin = "ganadoras.json";
            var partida = new Game();
            bool run = true;
            int option;
            do
            {
                Console.Clear();
                Welcome();
                Console.WriteLine("[1]: EMPEZAR JUEGO");
                Console.WriteLine("[2]: CARGAR SAVE");
                Console.WriteLine("[3]: VER LISTA DE GANADORAS");
                Console.WriteLine("[0]:SALIR");
                option = Convert.ToInt32(Console.ReadLine());//TENGO PROBLEMAS CUANDO SALGO DEL LOOP ANTERIOR. FFLUSH?
                switch (option)
                {
                    case 0:
                        run = false;
                        Console.Clear();
                        ThanksForPlaying();
                        break;
                    case 1:
                        Game.GiftRandom(partida);
                        Game.Start(partida);
                        break;
                    case 2:
                        try
                        {
                            var lista = new List<Idol>();
                            lista = HelperJson.CargarArchivo(pathSave);
                            Game.ChooseIdol(partida, lista);
                            Game.Start(partida);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("No se encontró ninguna partida guardada. Comenzando juego...");
                            Game.GiftRandom(partida);
                            Game.Start(partida);
                            //throw;
                        }
                        break;
                    case 3:
                        try
                        {
                            HelperJson.CargarArchivo(pathWin);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("No se encontró registros de ganadores.");
                            Console.WriteLine("Presione una tecla para volver al menu...");
                            Console.ReadKey();
                            //throw;
                        }
                        break;
                }
            } while (run);

        }
    }
}
