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
    using System.ComponentModel.DataAnnotations;

    public partial class tbl_tasacambio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_tasacambio()
        {
            this.tbl_tasacambio_det = new HashSet<tbl_tasacambio_det>();
        }
    
        public int id_tasacambio { get; set; }
        public int id_monedaO { get; set; }
        public int id_monedaC { get; set; }

        [Display(Name = "Mes: ")]
        [Required(ErrorMessage = "Escriba el nombre de la moneda")]
        [StringLength(20, ErrorMessage = "La cantidad de caracteres permitida es de 20")]
        public int mes { get; set; }

        public int anio { get; set; }
        public int estado { get; set; }
    
        public virtual tbl_moneda tbl_moneda { get; set; }
        public virtual tbl_moneda tbl_moneda1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_tasacambio_det> tbl_tasacambio_det { get; set; }
    }
}
