using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KpopIdolRoad
{
    public class HelperJson
    {
        public HelperJson()
        {
        }

        public static void GenerarJson(string pathJson, List<Idol> lista)
        {
            string archivoGenerado;
            using (var fs = new FileStream(pathJson, FileMode.OpenOrCreate))
            {
                using (var sw = new StreamWriter(fs))
                {
                    archivoGenerado = JsonSerializer.Serialize(lista);
                    sw.WriteLine(archivoGenerado);
                }
            }
        }
        public static List<Idol>? CargarArchivo(string pathJson) //?//
        {
            var lista = new List<Idol>();
            try
            {
                string archivoCargado;
                using (var fs = new FileStream(pathJson, FileMode.OpenOrCreate))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        archivoCargado = sr.ReadToEnd();
                    }
                }
                lista = JsonSerializer.Deserialize<List<Idol>>(archivoCargado);
                return lista;
            }
            catch (System.Text.Json.JsonException)
            {
                return lista;
            }
        }
    }
}
