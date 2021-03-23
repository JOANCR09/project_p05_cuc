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
    public class AsistenciasController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Asistencias
        public IQueryable<Asistencia> GetAsistencias()
        {
            return db.Asistencias;
        }

        // GET: api/Asistencias/5
        [ResponseType(typeof(Asistencia))]
        public IHttpActionResult GetAsistencia(int id)
        {
            Asistencia asistencia = db.Asistencias.Find(id);
            if (asistencia == null)
            {
                return NotFound();
            }

            return Ok(asistencia);
        }

        // PUT: api/Asistencias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAsistencia(int id, Asistencia asistencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asistencia.IDAsistencia)
            {
                return BadRequest();
            }

            db.Entry(asistencia).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsistenciaExists(id))
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

        // POST: api/Asistencias
        [ResponseType(typeof(Asistencia))]
        public IHttpActionResult PostAsistencia(Asistencia asistencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Asistencias.Add(asistencia);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AsistenciaExists(asistencia.IDAsistencia))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = asistencia.IDAsistencia }, asistencia);
        }

        // DELETE: api/Asistencias/5
        [ResponseType(typeof(Asistencia))]
        public IHttpActionResult DeleteAsistencia(int id)
        {
            Asistencia asistencia = db.Asistencias.Find(id);
            if (asistencia == null)
            {
                return NotFound();
            }

            db.Asistencias.Remove(asistencia);
            db.SaveChanges();

            return Ok(asistencia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AsistenciaExists(int id)
        {
            return db.Asistencias.Count(e => e.IDAsistencia == id) > 0;
        }
    }
}