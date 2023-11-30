using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Controllers.Models;

namespace Controllers.Controllers
{
    public class PlatillosController : ApiController
    {
        private tiusr30pl_RestauranteEntities Plati = new tiusr30pl_RestauranteEntities();

        public PlatillosController()
        {
            Plati.Configuration.LazyLoadingEnabled = false;
            Plati.Configuration.ProxyCreationEnabled = false;
        }

        [HttpPost]
        [Route("api/Platillos/RegistrarPlatillo")]
        public IHttpActionResult RegistrarPlatillo(Platilloclass platilloNuevo)
        {
            try
            {
                var platilloExistente = Plati.Platillos.FirstOrDefault(p => p.Nombre == platilloNuevo.Nombre);

                if (platilloExistente != null)
                {
                    return Conflict();
                }

                var nuevoPlatillo = new Platillos
                {
                    Nombre = platilloNuevo.Nombre,
                    Costo = platilloNuevo.Costo,
                    CategoriaID = platilloNuevo.CategoriaID,
                    IDESTADO = platilloNuevo.IDESTADO
                };

                Plati.Platillos.Add(nuevoPlatillo);
                Plati.SaveChanges();

                return StatusCode(HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }


        [HttpPut]
        [Route("api/Platillos/EditarPlatillo")]
        public IHttpActionResult EditarPlatillo(string nombreActual, Platilloclass platilloEditado)
        {
            try
            {
                // Buscar el platillo por nombre
                var platilloExistente = Plati.Platillos.FirstOrDefault(p => p.Nombre == nombreActual);

                if (platilloExistente == null)
                {
                    return NotFound();
                }

                platilloExistente.Nombre = platilloEditado.Nombre;
                platilloExistente.Costo = platilloEditado.Costo;
                platilloExistente.CategoriaID = platilloEditado.CategoriaID;
                platilloExistente.IDESTADO = platilloEditado.IDESTADO;

                Plati.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("api/Platillos/EliminarPlatillo/{nombre}")]
        public IHttpActionResult EliminarPlatillo(string nombre)
        {
            try
            {
                var platilloExistente = Plati.Platillos.FirstOrDefault(p => p.Nombre == nombre);

                if (platilloExistente == null)
                {
                    return NotFound();
                }

                Plati.Platillos.Remove(platilloExistente);
                Plati.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
            [HttpGet]
            [Route("api/Platillos/ListarPlatillos")]
            // En el controlador de Platillos
            public IHttpActionResult ListarPlatillos()
            {
                try
                {
                    var platillos = from p in Plati.Platillos
                                    join c in Plati.Categorias on p.CategoriaID equals c.CategoriaID
                                    join e in Plati.Estado on p.IDESTADO equals e.EstadoID
                                    select new
                                    {
                                        PlatilloID = p.PlatilloID,
                                        Nombre = p.Nombre,
                                        Costo = p.Costo,
                                        CategoriaNombre = c.Nombre,
                                        EstadoDescripcion = e.Descripcion
                                    };

                    return Ok(platillos);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }

        [HttpGet]
        [Route("api/Listado/ListarPlatillosPorCategoria")]
        public IHttpActionResult ListarPlatillosPorCategoria(string categoriaSeleccionada)
        {
            try
            {
                var platillos = from p in Plati.Platillos
                                join c in Plati.Categorias on p.CategoriaID equals c.CategoriaID
                                join e in Plati.Estado on p.IDESTADO equals e.EstadoID
                                where c.Nombre == categoriaSeleccionada
                                select new
                                {
                                    PlatilloID = p.PlatilloID,
                                    Nombre = p.Nombre,
                                    Costo = p.Costo,
                                    CategoriaNombre = c.Nombre,
                                    EstadoDescripcion = e.Descripcion
                                };

                return Ok(platillos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPut]
        [Route("api/Platillos/ActualizarEstadoPlatillo")]
        public IHttpActionResult ActualizarEstadoPlatillo(PlatilloEstadoModel model)
        {
            try
            {
                var platilloExistente = Plati.Platillos.FirstOrDefault(p => p.PlatilloID == model.PlatilloID);

                if (platilloExistente == null)
                {
                    return NotFound();
                }
                var estado = Plati.Estado.FirstOrDefault(e => e.Descripcion == model.Estado);

                if (estado == null)
                {
                    return BadRequest("Estado no válido");
                }

                platilloExistente.IDESTADO = estado.EstadoID;
                Plati.SaveChanges();

                return Ok("El estado del platillo se actualizó correctamente");
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
