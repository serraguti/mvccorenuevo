using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
//---------------------------------------------------
//PROCEDIMIENTO PAGINACION INDIVIDUAL
//CREATE PROCEDURE PAGINARREGISTRODEPARTAMENTO
//(@POSICION INT, @REGISTROS INT OUT)
//AS
//    SELECT @REGISTROS = COUNT(DEPT_NO) FROM VISTADEPT
//	SELECT DEPT_NO, DNOMBRE, LOC FROM VISTADEPT
//	WHERE POSICION = @POSICION
//GO
//----------------------------------------------------------
//---------------PROCEDURE PAGINACION GRUPO DEPARTAMENTO-----------
//CREATE PROCEDURE PAGINARGRUPODEPARTAMENTOS
//(@POSICION INT, @REGISTROS INT OUT)
//AS
//    SELECT @REGISTROS = COUNT(DEPT_NO) FROM VISTADEPT
//	SELECT DEPT_NO, DNOMBRE, LOC FROM VISTADEPT
//	WHERE POSICION >= @POSICION AND
//	POSICION < (@POSICION + 2)
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

        public Departamento GetDepartamentoPosicion(int posicion
            , ref int salida)
        {
            String sql = "PAGINARREGISTRODEPARTAMENTO @POSICION"
                + ", @REGISTROS OUT";
            SqlParameter pampos =
                new SqlParameter("@POSICION", posicion);
            SqlParameter pamreg =
                new SqlParameter("@REGISTROS", -1);
            pamreg.Direction = System.Data.ParameterDirection.Output;
            Departamento departamento =
this.context.Departamentos.FromSqlRaw<Departamento>(sql, pampos, pamreg)
.AsEnumerable()
.FirstOrDefault();
            int numeroregistros = Convert.ToInt32(pamreg.Value);
            salida = numeroregistros;
            return departamento;
        }

        public List<Departamento> GetGrupoDepartamentosSQL
            (int posicion, ref int numeroregistros)
        {
            String sql = "PAGINARGRUPODEPARTAMENTOS @POSICION"
                + ", @REGISTROS OUT";
            SqlParameter pamposicion =
                new SqlParameter("@POSICION", posicion);
            SqlParameter pamregistros =
                new SqlParameter("@REGISTROS", -1);
            pamregistros.Direction = System.Data.ParameterDirection.Output;
            List<Departamento> departamentos =
                this.context.Departamentos
                .FromSqlRaw(sql, pamposicion, pamregistros).ToList();
            numeroregistros = Convert.ToInt32(pamregistros.Value);
            return departamentos;
        }
    }
}
