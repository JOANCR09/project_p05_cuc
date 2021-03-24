using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Proyecto.PrograV.Models;

namespace Proyecto.PrograV.Controllers
{
    public class MatriculasController : ApiController
    {
        private Entities1 db = new Entities1();

        // GET: api/Matriculas
        public IQueryable<Matricula> GetMatriculas()
        {
            return db.Matriculas;
        }

        // GET: api/Matriculas/5
        [ResponseType(typeof(Matricula))]
        public IHttpActionResult GetMatricula(int id)
        {
            Matricula matricula = db.Matriculas.Find(id);
            if (matricula == null)
            {
                return NotFound();
            }

            return Ok(matricula);
        }

        // PUT: api/Matriculas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMatricula(int id, Matricula matricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != matricula.IDMatricula)
            {
                return BadRequest();
            }

            db.Entry(matricula).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatriculaExists(id))
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

        // POST: api/Matriculas
        [ResponseType(typeof(Matricula))]
        public IHttpActionResult PostMatricula(Matricula matricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Matriculas.Add(matricula);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MatriculaExists(matricula.IDMatricula))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = matricula.IDMatricula }, matricula);
        }

        // DELETE: api/Matriculas/5
        [ResponseType(typeof(Matricula))]
        public IHttpActionResult DeleteMatricula(int id)
        {
            Matricula matricula = db.Matriculas.Find(id);
            if (matricula == null)
            {
                return NotFound();
            }

            db.Matriculas.Remove(matricula);
            db.SaveChanges();

            return Ok(matricula);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MatriculaExists(int id)
        {
            return db.Matriculas.Count(e => e.IDMatricula == id) > 0;
        }
    }
}