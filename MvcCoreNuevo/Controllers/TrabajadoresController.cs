using Microsoft.AspNetCore.Mvc;
using MvcCoreNuevo.Models;
using MvcCoreNuevo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Controllers
{
    public class TrabajadoresController : Controller
    {
        IRepositoryHospital repo;
        public TrabajadoresController(IRepositoryHospital repo)
        {
            this.repo = repo;
        }
        public IActionResult 
            PaginarTrabajadores(int? posicion
            , int? salario)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int registros = 0;
            List<Trabajador> trabajadores;
            if (salario == null)
            {
                trabajadores =
                this.repo.GetTrabajadores(posicion.Value
                , ref registros);
            }
            else
            {
                trabajadores =
                    this.repo.GetTrabajadores(posicion.Value
                    , ref registros, salario.Value);
                ViewData["SALARIO"] = salario.Value;
            }
            ViewData["NUMEROREGISTROS"] = registros;
            return View(trabajadores);
        }

        [HttpPost]
        public IActionResult 
            PaginarTrabajadores(int salario)
        {
            int posicion = 1;
            int registros = 0;
            List<Trabajador> trabajadores =
                this.repo.GetTrabajadores(posicion
                , ref registros, salario);
            ViewData["NUMEROREGISTROS"] = registros;
            ViewData["SALARIO"] = salario;
            return View(trabajadores);
        }
    }
}
