using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Description;
using Proyecto.PrograV.Models;

namespace Proyecto.PrograV.Controllers
{
    public class EstudiantesController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Estudiantes
        public IQueryable<Estudiante> GetEstudiantes()
        {
            return db.Estudiantes;
        }

        // GET: api/Estudiantes/5
        [ResponseType(typeof(Estudiante))]
        public IHttpActionResult GetEstudiante(int id)
        {
            Estudiante estudiante = db.Estudiantes.Find(id);

            if (estudiante == null)
            {
                return NotFound();
            }

            return Ok(estudiante);
        }

        // PUT: api/Estudiantes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEstudiante(int id, Estudiante estudiante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != estudiante.ID)
            {
                return BadRequest();
            }

            Boolean valid = true;

            switch (estudiante.TipoID)
            {
                case "cedula":
                case "Cedula":
                    /* Cédula de identidad */
                    if (estudiante.ID.ToString().Length != 9) // tamaño de 9 digitos
                    {
                        valid = false;
                        return BadRequest("El tamaño debe ser de 9 digitos.");
                    }
                    break;
                case "dimex":
                case "DIMEX":
                    /* DIMEX */
                    // formato 00000000000 ó 000000000000
                    if (estudiante.ID.ToString().Length >= 11 && estudiante.ID.ToString().Length <= 12) // tamaño entre 11 y 12
                    {
                        if (estudiante.ID.ToString().StartsWith("0")) // NO debe iniciar en 0
                        {
                            valid = false;
                            return BadRequest("No debe iniciar con 0.");
                        }
                    }
                    else
                    {
                        valid = false;
                        return BadRequest("El tamaño debe ser de 11 o 12 digitos.");
                    }
                    break;
                case "cedula de resisdencia":
                    /* Cédula de Residencia */

                    if (!estudiante.ID.ToString().StartsWith("1")) // debe iniciar en 1
                    {
                        valid = false;
                        return BadRequest("Debe iniciar con 1.");
                    }

                    break;
                case "pasaporte":
                case "Pasaporte":
                    /* Pasaporte */
                    if (estudiante.ID.ToString().Length != 9) // tamaño de 9 digitos
                    {
                        valid = false;
                        return BadRequest("El tamaño debe ser de 9 digitos.");
                    }
                    break;
                default:
                    valid = false;
                    break;
            }

            if (valid)
            {
                db.Entry(estudiante).State = EntityState.Modified;
            }

            //db.Entry(estudiante).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstudianteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Estudiantes
        [ResponseType(typeof(Estudiante))]
        public IHttpActionResult PostEstudiante(int type, Estudiante estudiante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Boolean valid = true;

            switch (type)
            {
                case 1:
                    /* Cédula de identidad */
                    if (estudiante.ID.ToString().Length != 9) // tamaño de 9 digitos
                    {
                        valid = false;
                        return BadRequest("El tamaño debe ser de 9 digitos.");
                    }
                    break;
                case 2:
                    /* DIMEX */
                    // formato 00000000000 ó 000000000000
                    if (estudiante.ID.ToString().Length >= 11 && estudiante.ID.ToString().Length <= 12) // tamaño entre 11 y 12
                    {
                        if (estudiante.ID.ToString().StartsWith("0")) // NO debe iniciar en 0
                        {
                            valid = false;
                            return BadRequest("No debe iniciar con 0.");
                        }
                    }
                    else
                    {
                        valid = false;
                        return BadRequest("El tamaño debe ser de 11 o 12 digitos.");
                    }
                    break;
                case 3:
                    /* Cédula de Residencia */
                    if (!estudiante.ID.ToString().StartsWith("1")) // debe iniciar en 1
                    {
                        valid = false;
                        return BadRequest("Debe iniciar con 1.");
                    }

                    break;
                case 4:
                    /* Pasaporte */
                    if (estudiante.ID.ToString().Length != 9) // tamaño de 9 digitos
                    {
                        valid = false;
                        return BadRequest("El tamaño debe ser de 9 digitos.");
                    }
                    break;
                default:
                    valid = false;
                    break;
            }

            //db.Estudiantes.Add(estudiante);

            if (valid)
            {
                db.Estudiantes.Add(estudiante);
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EstudianteExists(estudiante.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = estudiante.ID }, estudiante);
        }

        // DELETE: api/Estudiantes/5
        [ResponseType(typeof(Estudiante))]
        public IHttpActionResult DeleteEstudiante(int id)
        {
            Estudiante estudiante = db.Estudiantes.Find(id);
            if (estudiante == null)
            {
                return NotFound();
            }

            db.Estudiantes.Remove(estudiante);
            db.SaveChanges();

            return Ok(estudiante);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EstudianteExists(int id)
        {
            return db.Estudiantes.Count(e => e.ID == id) > 0;
        }
    }
}