using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class ProfesorsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Profesors
        public IQueryable<Profesor> GetProfesors()
        {
            return db.Profesors;
        }

        // GET: api/Profesors/5
        [ResponseType(typeof(Profesor))]
        public IHttpActionResult GetProfesor(int id)
        {
            Profesor profesor = db.Profesors.Find(id);
            if (profesor == null)
            {
                return NotFound();
            }

            return Ok(profesor);
        }

        // PUT: api/Profesors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProfesor(int id, Profesor profesor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != profesor.IDProfesor)
            {
                return BadRequest();
            }

            Boolean valid = true;

            switch (profesor.TipoID)
            {
                case "cedula":
                case "Cedula":
                    /* Cédula de identidad */
                    if (profesor.IDProfesor.ToString().Length != 9) // tamaño de 9 digitos
                    {
                        valid = false;
                        return BadRequest("El tamaño debe ser de 9 digitos.");
                    }
                    break;
                case "dimex":
                case "DIMEX":
                    /* DIMEX */
                    // formato 00000000000 ó 000000000000
                    if (profesor.IDProfesor.ToString().Length >= 11 && profesor.IDProfesor.ToString().Length <= 12) // tamaño entre 11 y 12
                    {
                        if (profesor.IDProfesor.ToString().StartsWith("0")) // NO debe iniciar en 0
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

                    if (!profesor.IDProfesor.ToString().StartsWith("1")) // debe iniciar en 1
                    {
                        valid = false;
                        return BadRequest("Debe iniciar con 1.");
                    }

                    break;
                case "pasaporte":
                case "Pasaporte":
                    /* Pasaporte */
                    if (profesor.IDProfesor.ToString().Length != 9) // tamaño de 9 digitos
                    {
                        valid = false;
                        return BadRequest("El tamaño debe ser de 9 digitos.");
                    }
                    break;
                default:
                    valid = false;
                    break;
            }

            // Validar email
            if (!new EmailAddressAttribute().IsValid(profesor.Correo))
            {
                valid = false;
                return BadRequest("Email incorrecto.");
            }

            // validar numero telefonico
            if (profesor.Telefono.ToString().Length != 8) // tamaño de 8 digitos
            {
                valid = false;
                return BadRequest("Telefono incorrecto, tamaño max: 8 digitos.");
            }

            if (valid)
            {
                db.Entry(profesor).State = EntityState.Modified;
            }

            //db.Entry(profesor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesorExists(id))
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

        // POST: api/Profesors
        [ResponseType(typeof(Profesor))]
        public IHttpActionResult PostProfesor(int type, Profesor profesor)
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
                    if (profesor.IDProfesor.ToString().Length != 9) // tamaño de 9 digitos
                    {
                        valid = false;
                        return BadRequest("El tamaño debe ser de 9 digitos.");
                    }
                    break;
                case 2:
                    /* DIMEX */
                    // formato 00000000000 ó 000000000000
                    if (profesor.IDProfesor.ToString().Length >= 11 && profesor.IDProfesor.ToString().Length <= 12) // tamaño entre 11 y 12
                    {
                        if (profesor.IDProfesor.ToString().StartsWith("0")) // NO debe iniciar en 0
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
                    if (!profesor.IDProfesor.ToString().StartsWith("1")) // debe iniciar en 1
                    {
                        valid = false;
                        return BadRequest("Debe iniciar con 1.");
                    }

                    break;
                case 4:
                    /* Pasaporte */
                    if (profesor.IDProfesor.ToString().Length != 9) // tamaño de 9 digitos
                    {
                        valid = false;
                        return BadRequest("El tamaño debe ser de 9 digitos.");
                    }
                    break;
                default:
                    valid = false;
                    break;
            }

            // Validar email
            if (!new EmailAddressAttribute().IsValid(profesor.Correo))
            {
                valid = false;
                return BadRequest("Email incorrecto.");
            }

            // validar numero telefonico
            if (profesor.Telefono.ToString().Length != 8) // tamaño de 8 digitos
            {
                valid = false;
                return BadRequest("Telefono incorrecto, tamaño max: 8 digitos.");
            }

            if (valid)
            {
                db.Profesors.Add(profesor);
            }


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProfesorExists(profesor.IDProfesor))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = profesor.IDProfesor }, profesor);
        }

        // DELETE: api/Profesors/5
        [ResponseType(typeof(Profesor))]
        public IHttpActionResult DeleteProfesor(int id)
        {
            Profesor profesor = db.Profesors.Find(id);
            if (profesor == null)
            {
                return NotFound();
            }

            db.Profesors.Remove(profesor);
            db.SaveChanges();

            return Ok(profesor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfesorExists(int id)
        {
            return db.Profesors.Count(e => e.IDProfesor == id) > 0;
        }
    }
}