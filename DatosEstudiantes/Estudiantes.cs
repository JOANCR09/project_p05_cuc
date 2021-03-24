using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DatosEstudiantes
{
   public class Estudiantes
    {
        [JsonProperty("ID")]
        public int id { get; set; }
        [JsonProperty("TipoID")]
        public string TipoId { get; set; }
        [JsonProperty("Nombre")]
        public string Nombre { get; set; }
        [JsonProperty("Apellidos")]
        public string Apellidos { get; set; }

        [JsonProperty("FechaNacimiento")]
        public string FechaNacimiento { get; set; }

        public string tojson()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
