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
    public class GruposController : ApiController
    {
        private Entities1 db = new Entities1();

        // GET: api/Grupoes
        public IQueryable<Grupo> GetGrupoes(int type, int data)
        {
            // periodo, profesor, curso o carrera.
            switch (type)
            {
                case 1:// periodo
                    return db.Grupoes.Where(a => a.IDPeriodo == data);
                    break;
                case 2: // profesor
                    return db.Grupoes.Where(a => a.IDProfesor == data);
                    break;
                case 3: // curso
                    return db.Grupoes.Where(a => a.IDCurso == data);
                    break;
                case 4: // carrera
                    //var query1 = db.Cursoes.Where(t1 => t1.idCarrera == 1);
                    //var query2 = db.carreras.Where(t2 => t2.id != 1);
                    //var join = query1.Join(query2, x => x.idCarrera, y => y.id, (db.Cursoes.Where(t1 => t1.idCarrera == 1), db.carreras.Where(t2 => t2.id != 1))
                    //            => new { query1, query2 }).Where(o => o.query1.idCarrera == o.query2.id );
                    //return db.Grupoes.Where(a => a. == data);
                    return db.Grupoes;
                    break;
                default: // mostrar todo
                    return db.Grupoes;
                    break;
            }
        }

        // GET: api/Grupoes/5
        [ResponseType(typeof(Grupo))]
        public IHttpActionResult GetGrupo(int id)
        {
            Grupo grupo = db.Grupoes.Find(id);
            if (grupo == null)
            {
                return NotFound();
            }

            return Ok(grupo);
        }

        // PUT: api/Grupoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGrupo(int id, Grupo grupo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grupo.IDGrupo)
            {
                return BadRequest();
            }

            db.Entry(grupo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GrupoExists(id))
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

        // POST: api/Grupoes
        [ResponseType(typeof(Grupo))]
        public IHttpActionResult PostGrupo(Grupo grupo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Grupoes.Add(grupo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GrupoExists(grupo.IDGrupo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = grupo.IDGrupo }, grupo);
        }

        // DELETE: api/Grupoes/5
        [ResponseType(typeof(Grupo))]
        public IHttpActionResult DeleteGrupo(int id)
        {
            Grupo grupo = db.Grupoes.Find(id);
            if (grupo == null)
            {
                return NotFound();
            }

            db.Grupoes.Remove(grupo);
            db.SaveChanges();

            return Ok(grupo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GrupoExists(int id)
        {
            return db.Grupoes.Count(e => e.IDGrupo == id) > 0;
        }
    }
}