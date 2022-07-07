using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpopIdolRoad
{
    public class Menu
    {
        public static void start()
        {
            bool run = true;
            int option;
            string pathSave = "progreso.json";

            Console.WriteLine(@"
 _    __  ______   ______   ______    _____  _____    ______   _          ______   ______   ______   _____   
| |  / / | |  | \ / |  | \ | |  | \    | |  | | \ \  / |  | \ | |        | |  | \ / |  | \ | |  | | | | \ \  
| |-< <  | |__|_/ | |  | | | |__|_/    | |  | |  | | | |  | | | |   _    | |__| | | |  | | | |__| | | |  | | 
|_|  \_\ |_|      \_|__|_/ |_|        _|_|_ |_|_/_/  \_|__|_/ |_|__|_|   |_|  \_\ \_|__|_/ |_|  |_| |_|_/_/  
                                                                                                             
");
            do
            {
                Console.WriteLine("[1]: EMPEZAR JUEGO");
                Console.WriteLine("[2]: CARGAR SAVE");
                Console.WriteLine("[3]: VER LISTA DE GANADORAS");
                Console.WriteLine("[0]:SALIR");
                option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 0:
                        run = false;
                        break;
                    case 1:
                        Game.start();
                        break;
                    case 2:
                        try
                        {
                            var lista = new List<Idol>();
                            HelperJson.cargarArchivo(pathSave);
                            Game.startSave(lista);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("No se encontró ninguna partida guardada. Comenzando juego...");
                            Game.start();
                            throw;
                        }
                        break;
                    case 3:
                        try
                        {
                            HelperJson.cargarArchivo(pathSave);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("No se encontró registros de ganadores.");
                            throw;
                        }
                        break;
                }
            } while (run);

        }
    }
}
