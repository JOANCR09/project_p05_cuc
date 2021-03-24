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
    public class PeriodosController : ApiController
    {
        private Entities1 db = new Entities1();

        // GET: api/Periodoes
        public IQueryable<Periodo> GetPeriodoes()
        {
            return db.Periodoes;
        }

        // GET: api/Periodoes/5
        [ResponseType(typeof(Periodo))]
        public IHttpActionResult GetPeriodo(int id)
        {
            Periodo periodo = db.Periodoes.Find(id);
            if (periodo == null)
            {
                return NotFound();
            }

            return Ok(periodo);
        }

        // PUT: api/Periodoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPeriodo(int id, Periodo periodo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != periodo.IDPeriodo)
            {
                return BadRequest();
            }

            db.Entry(periodo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeriodoExists(id))
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

        // POST: api/Periodoes
        [ResponseType(typeof(Periodo))]
        public IHttpActionResult PostPeriodo(Periodo periodo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Periodoes.Add(periodo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PeriodoExists(periodo.IDPeriodo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = periodo.IDPeriodo }, periodo);
        }

        // DELETE: api/Periodoes/5
        [ResponseType(typeof(Periodo))]
        public IHttpActionResult DeletePeriodo(int id)
        {
            Periodo periodo = db.Periodoes.Find(id);
            if (periodo == null)
            {
                return NotFound();
            }

            db.Periodoes.Remove(periodo);
            db.SaveChanges();

            return Ok(periodo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PeriodoExists(int id)
        {
            return db.Periodoes.Count(e => e.IDPeriodo == id) > 0;
        }
    }
}