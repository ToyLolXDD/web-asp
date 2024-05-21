using AppCurso.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppCurso.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult Index(EmpleadoCLS oEmpleadoCLS)
        {
            int idTipoUsuario = oEmpleadoCLS.idtipoUsuario;
            List<EmpleadoCLS> listaEmpleados = null;
            listarCombos();
            using (var bd = new BDPasajeEntities())
            {
                if (idTipoUsuario == 0)
                {
                    listaEmpleados = (from empleado in bd.Empleado
                                      join tipousuario in bd.TipoUsuario
                                      on empleado.IIDTIPOUSUARIO equals tipousuario.IIDTIPOUSUARIO
                                      join tipocontrato in bd.TipoContrato
                                      on empleado.IIDTIPOCONTRATO equals tipocontrato.IIDTIPOCONTRATO // Corregir la clave de unión aquí
                                      where empleado.BHABILITADO == 1
                                      select new EmpleadoCLS
                                      {
                                          id_empleado = empleado.IIDEMPLEADO,
                                          nombre = empleado.NOMBRE,
                                          appaterno = empleado.APPATERNO,
                                          nombreTipousuario = tipousuario.NOMBRE,
                                          nombreTipocontrato = tipocontrato.NOMBRE,

                                      }).ToList();
                }
                else
                {
                    listaEmpleados = (from empleado in bd.Empleado
                                      join tipousuario in bd.TipoUsuario
                                      on empleado.IIDTIPOUSUARIO equals tipousuario.IIDTIPOUSUARIO
                                      join tipocontrato in bd.TipoContrato
                                      on empleado.IIDTIPOCONTRATO equals tipocontrato.IIDTIPOCONTRATO // Corregir la clave de unión aquí
                                      where empleado.BHABILITADO == 1 && empleado.IIDTIPOUSUARIO == idTipoUsuario 
                                      select new EmpleadoCLS
                                      {
                                          id_empleado = empleado.IIDEMPLEADO,
                                          nombre = empleado.NOMBRE,
                                          appaterno = empleado.APPATERNO,
                                          nombreTipousuario = tipousuario.NOMBRE,
                                          nombreTipocontrato = tipocontrato.NOMBRE,

                                      }).ToList();
                }

            }

            return View(listaEmpleados);
        }
        public ActionResult Agregar()
        {
            listarCombos();
            return View();
        }
        public void llenarComboSex()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from sexo in bd.Sexo
                         where sexo.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = sexo.NOMBRE,
                             Value = sexo.IIDSEXO.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaSexo = lista;
            }
        }
        public void listarTipoContrato()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.TipoContrato
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDTIPOCONTRATO.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaTipoContrato = lista;
            }
        }
        public void listarTipoUsuario()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.TipoUsuario
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDTIPOUSUARIO.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaTipoUsuario = lista;
            }
        }
        public void listarCombos()
        {
            listarTipoContrato();
            listarTipoUsuario();
            llenarComboSex();
        }
        [HttpPost]
        public ActionResult Agregar(EmpleadoCLS oEmpleadoCLS)
        {
            int nregistrosAfectados = 0;
            string nombre = oEmpleadoCLS.nombre;
            string appaterno = oEmpleadoCLS.appaterno;
            string apmaterno = oEmpleadoCLS.apmaterno;
            using (var bd=new BDPasajeEntities())
            {
                nregistrosAfectados = bd.Empleado.Where(p => p.NOMBRE.Equals(nombre) && p.APPATERNO.Equals(appaterno) && p.APMATERNO.Equals(apmaterno)).Count();
            }
            if (!ModelState.IsValid || nregistrosAfectados>=1)
            {
                if (nregistrosAfectados >= 1) oEmpleadoCLS.mensajeError = "El empleado ya existe";
                listarCombos();
                return View(oEmpleadoCLS);
            }
            using (var bd = new BDPasajeEntities())
            {
                Empleado oEmpleado = new Empleado();
                oEmpleado.NOMBRE = oEmpleadoCLS.nombre;
                oEmpleado.APPATERNO = oEmpleadoCLS.appaterno;
                oEmpleado.APMATERNO = oEmpleadoCLS.apmaterno;
                oEmpleado.FECHACONTRATO = oEmpleadoCLS.fechaContrato;
                oEmpleado.SUELDO = oEmpleadoCLS.sueldo;
                oEmpleado.IIDTIPOUSUARIO = oEmpleadoCLS.idtipoUsuario;
                oEmpleado.IIDTIPOCONTRATO = oEmpleadoCLS.idtipoContrato;
                oEmpleado.IIDSEXO = oEmpleadoCLS.idSexo;
                oEmpleado.BHABILITADO = 1;
                bd.Empleado.Add(oEmpleado);
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            listarCombos();
            EmpleadoCLS oEmpleadoCLS = new EmpleadoCLS();
            using (var bd = new BDPasajeEntities())
            {
                Empleado oEmpleado = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(id)).First();
                oEmpleadoCLS.id_empleado = oEmpleado.IIDEMPLEADO;
                oEmpleadoCLS.nombre = oEmpleado.NOMBRE;
                oEmpleadoCLS.appaterno = oEmpleado.APPATERNO;
                oEmpleadoCLS.apmaterno = oEmpleado.APMATERNO;
                oEmpleadoCLS.fechaContrato = (DateTime)oEmpleado.FECHACONTRATO;
                oEmpleadoCLS.sueldo = (decimal)oEmpleado.SUELDO;
                oEmpleadoCLS.idtipoUsuario = (int)oEmpleado.IIDTIPOUSUARIO;
                oEmpleadoCLS.idtipoContrato = (int)oEmpleado.IIDTIPOCONTRATO;
                oEmpleadoCLS.idSexo = (int)oEmpleado.IIDSEXO;

            }
            return View(oEmpleadoCLS);
        }

        [HttpPost]
        public ActionResult Editar(EmpleadoCLS oEmpleadoCLS)
        {
            int nregistrosAfectados = 0;
            int idEmpleado = oEmpleadoCLS.id_empleado;
            string nombre = oEmpleadoCLS.nombre;
            string appaterno = oEmpleadoCLS.appaterno;
            string apmaterno = oEmpleadoCLS.apmaterno;
            using (var bd = new BDPasajeEntities())
            {
                nregistrosAfectados = bd.Empleado.Where(p => p.NOMBRE.Equals(nombre) && p.APPATERNO.Equals(appaterno) && p.APMATERNO.Equals(apmaterno) && !p.IIDEMPLEADO.Equals(idEmpleado)).Count();
            }
            
            if (!ModelState.IsValid || nregistrosAfectados>=1)
            {
                listarCombos();
                if (nregistrosAfectados >= 1) oEmpleadoCLS.mensajeError = "El empleado ya existe";
                return View(oEmpleadoCLS);
            }
            using (var bd = new BDPasajeEntities())
            {
                Empleado oEmpleado = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(idEmpleado)).First();
                oEmpleado.IIDEMPLEADO = oEmpleadoCLS.id_empleado;
                oEmpleado.NOMBRE = oEmpleadoCLS.nombre;
                oEmpleado.APPATERNO = oEmpleadoCLS.appaterno;
                oEmpleado.APMATERNO = oEmpleadoCLS.apmaterno;

                oEmpleado.FECHACONTRATO = oEmpleadoCLS.fechaContrato;
                oEmpleado.SUELDO = oEmpleadoCLS.sueldo;

                oEmpleado.IIDTIPOCONTRATO = oEmpleadoCLS.idtipoContrato;
                oEmpleado.IIDTIPOUSUARIO = oEmpleadoCLS.idtipoUsuario;
                oEmpleado.IIDSEXO = oEmpleadoCLS.idSexo;
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id_empleado)
        {
            using (var bd = new BDPasajeEntities())
            {
                Empleado oEmpleado = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(id_empleado)).First();
                oEmpleado.BHABILITADO = 0;
                bd.SaveChanges();
                
            }
            return RedirectToAction("Index");

        }
       
    }
    
}