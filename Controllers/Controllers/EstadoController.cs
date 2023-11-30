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

    public class EstadoController : ApiController
    {
        private tiusr30pl_RestauranteEntities Estado = new tiusr30pl_RestauranteEntities();
        public EstadoController()
        {
            Estado.Configuration.LazyLoadingEnabled = false;
            Estado.Configuration.ProxyCreationEnabled = false;


        }
        [HttpGet]
        [Route("api/Estados/ObtenerEstados")]
        public IHttpActionResult ObtenerEstados()
        {
            try
            {
                var todosLosEstados = Estado.Estado.ToList();
                return Ok(todosLosEstados);
            }
            catch (Exception)
            {
                return InternalServerError();
            }



        }
    }
}
