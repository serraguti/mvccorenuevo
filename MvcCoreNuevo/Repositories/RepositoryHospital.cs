using MvcCoreNuevo.Data;
using MvcCoreNuevo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#region VISTA DEPARTAMENTOS PAGINACION
//CREATE VIEW VISTADEPT AS
//SELECT 
//ISNULL(ROW_NUMBER() OVER(ORDER BY DEPT_NO), 0)
//AS POSICION
//, ISNULL(DEPT.DEPT_NO, 0) AS DEPT_NO
//, DEPT.DNOMBRE, DEPT.LOC FROM DEPT
//GO
//VISTA CON CASTING A INT32
//ALTER VIEW VISTADEPT AS
//SELECT 
//CAST(
//ISNULL(ROW_NUMBER() OVER(ORDER BY DEPT_NO), 0) AS INT
//)
//AS POSICION
//, ISNULL(DEPT.DEPT_NO, 0) AS DEPT_NO
//, DEPT.DNOMBRE, DEPT.LOC FROM DEPT
//GO
#endregion

namespace MvcCoreNuevo.Repositories
{
    public class RepositoryHospital : IRepositoryHospital
    {
        private HospitalContext context;
        public RepositoryHospital(HospitalContext context)
        {
            this.context = context;
        }
        public List<Departamento> GetDepartamentos()
        {
            return this.context.Departamentos.ToList();
        }

        public Departamento BuscarDepartamento(int id)
        {
            return this.context.Departamentos
                .SingleOrDefault(x => x.Numero == id);
        }

        public VistaDept GetRegistroDepartamento(int posicion)
        {
            return
                this.context.VistaDepartamentos.Where
                (x => x.Posicion == posicion).FirstOrDefault();
        }

        public int GetNumeroRegistrosVistaDepartamento()
        {
            return this.context.VistaDepartamentos.Count();
        }

        public List<VistaDept> GetGrupoDepartamentos(int posicion)
        {
            //SELECT * FROM VISTADEPT WHERE POSICION >= 1 AND
            //POSICION < (1 + 2)
            var consulta = from datos in this.context.VistaDepartamentos
                           where datos.Posicion >= posicion
                           && datos.Posicion < (posicion + 2)
                           select datos;
            return consulta.ToList();
        }
    }
}
