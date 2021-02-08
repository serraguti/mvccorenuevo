using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Models
{
    [Table("VISTADEPT")]
    public class VistaDept
    {
        [Column("POSICION")]
        public int Posicion { get; set; }
        [Key]
        [Column("DEPT_NO")]
        public int Numero { get; set; }
        [Column("DNOMBRE")]
        public String Nombre {get;set;}
        [Column("LOC")]
        public String Localidad { get; set; }
    }
}
