using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Models
{
    [Table("TRABAJADORES")]
    public class Trabajador
    {
        [Key]
        [Column("IDEMPLEADO")]
        public int IdTrabajador { get; set; }
        [Column("APELLIDO")]
        public String Apellido { get; set; }
        [Column("TRABAJO")]
        public String Trabajo { get; set; }
        [Column("SALARIO")]
        public int Salario { get; set; }
    }
}
