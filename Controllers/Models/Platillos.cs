using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Controllers.Models
{
    public class Platilloclass
    {
        public string Nombre { get; set; }
        public decimal Costo { get; set; }
        public int CategoriaID { get; set; }
        public int IDESTADO { get; set; }
    }
    public class PlatilloEstadoModel
    {
        public int PlatilloID { get; set; }
        public string Estado { get; set; }
    }
}