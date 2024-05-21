using AppCurso.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppCurso.Controllers
{
    public class SucursalController : Controller
    {
        // GET: Sucursal
        public ActionResult Index(SucursalCLS oSucursalCLS)
        {
            
            List<SucursalCLS> listaSucursal = null;
            string nombreSucrusal = oSucursalCLS.nombre;
                using (var bd = new BDPasajeEntities())
                {
                    if (oSucursalCLS.nombre == null)
                    {
                        listaSucursal = (from sucursal in bd.Sucursal
                                         where sucursal.BHABILITADO == 1
                                         select new SucursalCLS
                                         {
                                             id_sucursal = sucursal.IIDSUCURSAL,
                                             nombre = sucursal.NOMBRE,
                                             telefono = sucursal.TELEFONO,
                                             email = sucursal.EMAIL
                                         }
                                        ).ToList();
                    }
                    else
                    {
                        listaSucursal = (from sucursal in bd.Sucursal
                                         where sucursal.BHABILITADO == 1 && sucursal.NOMBRE.Contains(nombreSucrusal)
                                         select new SucursalCLS
                                         {
                                             id_sucursal = sucursal.IIDSUCURSAL,
                                             nombre = sucursal.NOMBRE,
                                             telefono = sucursal.TELEFONO,
                                             email = sucursal.EMAIL
                                         }
                                        ).ToList();
                }
                }
            
            
            return View(listaSucursal);
        }
        public ActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Agregar(SucursalCLS oSucursalCLS)
        {
            int nroRegistrosEncontrados = 0;
            string nombreSucursal = oSucursalCLS.nombre;
            using (var bd = new BDPasajeEntities())
            {
                nroRegistrosEncontrados = bd.Sucursal.Where(p => p.NOMBRE.Equals(nombreSucursal)).Count();
            }
            if (!ModelState.IsValid || nroRegistrosEncontrados>=1)
            {
                if (nroRegistrosEncontrados >= 1) oSucursalCLS.mensajeError = "Ya existe la sucursal a agreagar";
                return View(oSucursalCLS);
            }
            using(var bd = new BDPasajeEntities())
            {
                Sucursal oSucursal = new Sucursal();
                oSucursal.NOMBRE = oSucursalCLS.nombre;
                oSucursal.DIRECCION = oSucursalCLS.direccion;
                oSucursal.TELEFONO = oSucursalCLS.telefono;
                oSucursal.EMAIL = oSucursalCLS.email;
                oSucursal.FECHAAPERTURA = oSucursalCLS.fechaApertura;
                oSucursal.BHABILITADO = 1;
                bd.Sucursal.Add(oSucursal);
                bd.SaveChanges();


            }
            return RedirectToAction("Index");
        }
        public ActionResult Editar(int id)
        {
            SucursalCLS oSucursalCLS = new SucursalCLS();
            using (var bd = new BDPasajeEntities())
            {
                Sucursal oSucursal = bd.Sucursal.Where(p => p.IIDSUCURSAL.Equals(id)).First();
                oSucursalCLS.id_sucursal = oSucursal.IIDSUCURSAL;
                oSucursalCLS.nombre = oSucursal.NOMBRE;
                oSucursalCLS.direccion = oSucursal.DIRECCION;
                oSucursalCLS.telefono = oSucursal.TELEFONO;
                oSucursalCLS.email = oSucursal.EMAIL;
                oSucursalCLS.fechaApertura = (DateTime)oSucursal.FECHAAPERTURA;

            }
            return View(oSucursalCLS);
        }
        [HttpPost]
        public ActionResult Editar(SucursalCLS oSucursalCLS)
        {
            int idSucursal = oSucursalCLS.id_sucursal;
            int nroRegistrosEncontrados = 0;
            string nombreSucursal = oSucursalCLS.nombre;
            using (var bd = new BDPasajeEntities())
            {
                nroRegistrosEncontrados = bd.Sucursal.Where(p => p.NOMBRE.Equals(nombreSucursal) && !p.IIDSUCURSAL.Equals(idSucursal)).Count();
            }
            
            if(!ModelState.IsValid || nroRegistrosEncontrados>= 1)
            {
                if (nroRegistrosEncontrados >= 1) oSucursalCLS.mensajeError = "Ya existe la sucursal.";
                
                    return View(oSucursalCLS);
                
                
            }
            using(var bd=new BDPasajeEntities())
            {
                Sucursal oSucursal = bd.Sucursal.Where(p => p.IIDSUCURSAL.Equals(idSucursal)).First();
                oSucursal.NOMBRE = oSucursalCLS.nombre;

                oSucursal.DIRECCION = oSucursalCLS.direccion;
                oSucursal.TELEFONO = oSucursalCLS.telefono;
                oSucursal.EMAIL = oSucursalCLS.email;
                oSucursal.FECHAAPERTURA = oSucursalCLS.fechaApertura;

                bd.SaveChanges();

            }
            
            return RedirectToAction("Index");
        }
        public ActionResult Eliminar(int id)
        {
            using (var bd=new BDPasajeEntities())
            {
                Sucursal oSucursal = bd.Sucursal.Where(p => p.IIDSUCURSAL.Equals(id)).First();
                oSucursal.BHABILITADO = 0;
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}