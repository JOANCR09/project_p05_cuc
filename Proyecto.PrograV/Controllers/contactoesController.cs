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
    public class contactoesController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/contactoes
        public IQueryable<contacto> Getcontactoes()
        {
            return db.contactoes;
        }

        // GET: api/contactoes/5
        [ResponseType(typeof(contacto))]
        public IHttpActionResult Getcontacto(int id)
        {
            contacto contacto = db.contactoes.Find(id);
            if (contacto == null)
            {
                return NotFound();
            }

            return Ok(contacto);
        }

        // PUT: api/contactoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcontacto(int id, contacto contacto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contacto.ID_Estudiante)
            {
                return BadRequest();
            }

            db.Entry(contacto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!contactoExists(id))
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

        // POST: api/contactoes
        [ResponseType(typeof(contacto))]
        public IHttpActionResult Postcontacto(int id, contacto contacto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Boolean valid = true;
            Estudiante estudiante = db.Estudiantes.Find(id);

            if (estudiante == null)
            {
                return NotFound();
            }

            // Validar email
            if (!new EmailAddressAttribute().IsValid(contacto.email))
            {
                valid = false;
                return BadRequest("Email incorrecto.");
            }

            // validar numero telefonico
            if (contacto.telefono.Length != 8) // tamaño de 8 digitos
            {
                valid = false;
                return BadRequest("Telefono incorrecto, tamaño max: 8 digitos.");
            }

            db.contactoes.Add(contacto);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (contactoExists(contacto.ID_Estudiante))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = contacto.ID_Estudiante }, contacto);
        }

        // DELETE: api/contactoes/5
        [ResponseType(typeof(contacto))]
        public IHttpActionResult Deletecontacto(string telefono, string email)
        {
            contacto contacto = db.contactoes.Find(telefono);
            if (contacto == null)
            {
                contacto = db.contactoes.Find(email);
                if (contacto == null)
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }


            db.contactoes.Remove(contacto);
            db.SaveChanges();

            return Ok(contacto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool contactoExists(int id)
        {
            return db.contactoes.Count(e => e.ID_Estudiante == id) > 0;
        }
    }
}