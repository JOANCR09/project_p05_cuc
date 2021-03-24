using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DatosEstudiantes
{
     
    public class EMostrar
    {
        private const string url_Estudiante = "http://localhost/Proyecto.PrograV/api/Estudiantes";
        private List<Estudiantes> Prueba { get; set; }

        public List<Estudiantes> Mostrar()
        {
            IEnumerable<Estudiantes> A;
            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {
                    return await client.GetAsync(url_Estudiante);
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
                    Prueba = JsonConvert.DeserializeObject<List<Estudiantes>>(resultstr);
            //         Prueba= JsonConvert.DeserializeObject<List<Estudiantes>>(resultstr);

                }

            }
            return Prueba;


        }
    }
}
