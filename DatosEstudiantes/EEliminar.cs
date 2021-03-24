using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DatosEstudiantes
{
    public class EEliminar
    {
        private const string url_Estudiante = "http://localhost/Proyecto.PrograV/api/Estudiantes";
        private List<Estudiantes> Prueba { get; set; }
        public void Eliminar(int cod)
        {
            Estudiantes b = new Estudiantes();
            b.id = cod;
            string json = JsonConvert.SerializeObject(cod, Formatting.None);

            using (var client = new HttpClient())
            {

                var task = Task.Run(async () =>
                {
                    return await client.DeleteAsync(url_Estudiante + "/" + cod);
                }

                );
                HttpResponseMessage message = task.Result;
                if (message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var task2 = Task<string>.Run(async () =>
                    {
                        return await message.Content.ReadAsStringAsync();
                    });
                    string resultstr = task2.Result;
                    Estudiantes result = JsonConvert.DeserializeObject<Estudiantes>(resultstr);
                    Console.WriteLine("Cliente {0} eliminado con el codigo {1}", result.Nombre, result.id);
                }
                else
                {
                    Console.WriteLine("error intente mas tarde");
                }
            }

        }
    }
}
