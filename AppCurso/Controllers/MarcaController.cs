using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppCurso.Models;

namespace AppCurso.Controllers
{
    public class MarcaController : Controller
    {
        // GET: Marca
        public ActionResult Index(MarcaCLS oMarcaCLS)
        {
            string nombreMarca = oMarcaCLS.nombre;
            List<MarcaCLS> listaMarca = null;
            using (var bd = new BDPasajeEntities())
            {
                if (oMarcaCLS.nombre == null)
                {
                    listaMarca = (from marca in bd.Marca
                                  where marca.BHABILITADO == 1
                                  select new MarcaCLS
                                  {
                                      id_marca = marca.IIDMARCA,
                                      nombre = marca.NOMBRE,
                                      descripcion = marca.DESCRIPCION,
                                  }).ToList();
                }
                else
                {
                    listaMarca = (from marca in bd.Marca
                                  where marca.BHABILITADO == 1 && marca.NOMBRE.Contains(nombreMarca)
                                  select new MarcaCLS
                                  {
                                      id_marca = marca.IIDMARCA,
                                      nombre = marca.NOMBRE,
                                      descripcion = marca.DESCRIPCION,
                                  }).ToList();
                }
                
            }
            return View(listaMarca);
        }
        public ActionResult Agregar()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Agregar(MarcaCLS oMarcaCLS)
        {
            int nroRegistrosEncontrados = 0;
            string nombreMarca = oMarcaCLS.nombre;
            using (var bd=new BDPasajeEntities())
            {
                nroRegistrosEncontrados = bd.Marca.Where(p => p.NOMBRE.Equals(nombreMarca)).Count();
            }

                if (!ModelState.IsValid || nroRegistrosEncontrados>=1 )
                {
                if (nroRegistrosEncontrados >= 1) oMarcaCLS.mensajeError = "El nombre marca ya existe";
                    return View(oMarcaCLS);
                }
                else
                {
                    using (var bd = new BDPasajeEntities())
                    {
                        Marca oMarca = new Marca();
                        oMarca.NOMBRE = oMarcaCLS.nombre;
                        oMarca.DESCRIPCION = oMarcaCLS.descripcion;
                        oMarca.BHABILITADO = 1;
                        bd.Marca.Add(oMarca);
                        bd.SaveChanges();
                    }
                }
            return RedirectToAction("Index");
        }
        public ActionResult Editar(int id)
        {
            MarcaCLS oMarcaCLS = new MarcaCLS();
            using (var bd=new BDPasajeEntities())
            {
                Marca oMarca = bd.Marca.Where(p => p.IIDMARCA.Equals(id)).First();
                oMarcaCLS.id_marca = oMarca.IIDMARCA;
                oMarcaCLS.nombre = oMarca.NOMBRE;
                oMarcaCLS.descripcion = oMarca.DESCRIPCION;

            }
            return View(oMarcaCLS);
        }
        [HttpPost]
        public ActionResult Editar(MarcaCLS oMarcaCLS)
        {
            int nroRegistrosEncontrados = 0;
            string nombreMarca = oMarcaCLS.nombre;
            int idMarca = oMarcaCLS.id_marca;
            using (var bd = new BDPasajeEntities())
            {
                nroRegistrosEncontrados = bd.Marca.Where(p => p.NOMBRE.Equals(nombreMarca) && !p.IIDMARCA.Equals(idMarca)).Count();
            }
            if (!ModelState.IsValid || nroRegistrosEncontrados>=1)
            {
                if (nroRegistrosEncontrados >= 1) oMarcaCLS.mensajeError = "Ya se encuentra registrada la marca";
                return View(oMarcaCLS);
            }
            
            using(var bd=new BDPasajeEntities())
            {
                Marca oMarca = bd.Marca.Where(p => p.IIDMARCA.Equals(idMarca)).First();
                oMarca.NOMBRE = oMarcaCLS.nombre;
                oMarca.DESCRIPCION = oMarcaCLS.descripcion;
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar (int id_marca)
        {
            using (var bd=new BDPasajeEntities())
            {
                Marca oMarca = bd.Marca.Where(p => p.IIDMARCA.Equals(id_marca)).First();
                oMarca.BHABILITADO = 0;
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}