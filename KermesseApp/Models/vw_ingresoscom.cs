//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KermesseApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class vw_ingresoscom
    {
        public int id_ingreso_comunidad { get; set; }
        public string Kermesse { get; set; }
        public string Comunidad { get; set; }
        public string Responsable_com { get; set; }
        public string Producto { get; set; }
        public int Cantidad_disponible { get; set; }
        public int Cantidad_vendida { get; set; }
        public int Total_Bonos { get; set; }
    }
}
