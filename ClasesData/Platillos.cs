//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClasesData
{
    using System;
    using System.Collections.Generic;
    
    public partial class Platillos
    {
        public int PlatilloID { get; set; }
        public string Nombre { get; set; }
        public decimal Costo { get; set; }
        public Nullable<int> CategoriaID { get; set; }
        public Nullable<int> IDESTADO { get; set; }
    
        public virtual Categorias Categorias { get; set; }
        public virtual Estado Estado { get; set; }
    }
}