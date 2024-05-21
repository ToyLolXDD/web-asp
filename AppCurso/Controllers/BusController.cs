using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppCurso.Models;

namespace AppCurso.Controllers
{
    public class BusController : Controller
    {
        // GET: Bus
        public ActionResult Index(BusCLS oBusCLS)
        {
            listarCombos();
            List<BusCLS> listaBus = null;
            List<BusCLS> listaRpta = new List<BusCLS>(  );
            using (var bd = new BDPasajeEntities())
            {
                
                listaBus = (from bus in bd.Bus
                            join sucursal in bd.Sucursal
                            on bus.IIDSUCURSAL equals sucursal.IIDSUCURSAL
                            join tipoBus in bd.TipoBus
                            on bus.IIDTIPOBUS equals tipoBus.IIDTIPOBUS
                            join tipoModelo in bd.Modelo
                            on bus.IIDMODELO equals tipoModelo.IIDMODELO
                            where bus.BHABILITADO == 1
                            select new BusCLS
                            {
                                idBus = bus.IIDBUS,
                                placa = bus.PLACA,
                                nombreModelo = tipoModelo.NOMBRE,
                                nombreSucursal = sucursal.NOMBRE,
                                nombreTipoBus = tipoBus.NOMBRE,
                                idModelo = tipoModelo.IIDMODELO,
                                idSucursal = sucursal.IIDSUCURSAL,
                                idTipobus = tipoBus.IIDTIPOBUS

                            }).ToList();

                if (oBusCLS.idBus == 0 && oBusCLS.placa == null && oBusCLS.idModelo == 0 && oBusCLS.idSucursal == 0 && oBusCLS.idTipobus == 0)
                {
                    listaRpta = listaBus;
                }
                else
                {
                    //Filtros
                    if (oBusCLS.idBus != 0)
                    {
                        listaBus = listaBus.Where(p => p.idBus.ToString().Contains(oBusCLS.idBus.ToString())).ToList();
                    }
                    if (oBusCLS.placa != null)
                    {
                        listaBus = listaBus.Where(p => p.placa.ToString().Contains(oBusCLS.placa.ToString())).ToList();
                    }
                    if (oBusCLS.idModelo != 0)
                    {
                        listaBus = listaBus.Where(p => p.idModelo.ToString().Contains(oBusCLS.idModelo.ToString())).ToList();
                    }
                    if (oBusCLS.idSucursal != 0)
                    {
                        listaBus = listaBus.Where(p => p.idSucursal.ToString().Contains(oBusCLS.idSucursal.ToString())).ToList();
                    }
                    if(oBusCLS.idTipobus != 0)
                    {
                        listaBus = listaBus.Where(p => p.idTipobus.ToString().Contains(oBusCLS.idTipobus.ToString())).ToList();
                    }
                }listaRpta = listaBus;
            }
            return View(listaRpta);
        }
        public ActionResult Agregar()
        {
            listarCombos();
            return View();
        }
        [HttpPost]
        public ActionResult Agregar(BusCLS oBusCLS)
        {
            int nregistroencontrados = 0;
            string placa = oBusCLS.placa;
            
            using(var bd =new BDPasajeEntities())
            {
                nregistroencontrados = bd.Bus.Where(p => p.PLACA.Equals(placa)).Count();
                if (!ModelState.IsValid || nregistroencontrados>=1)
                {
                    if (nregistroencontrados >= 1) oBusCLS.mensajeError = "El bus ya existe";
                    listarCombos();
                    return View();
                }
                Bus oBus = new Bus();
                
                oBus.IIDSUCURSAL = oBusCLS.idSucursal;
                oBus.IIDTIPOBUS = oBusCLS.idTipobus;
                oBus.PLACA = oBusCLS.placa;
                oBus.FECHACOMPRA = oBusCLS.fechaCompra;
                oBus.IIDMODELO = oBusCLS.idModelo;
                oBus.NUMEROCOLUMNAS = oBusCLS.numeroColumnas;
                oBus.NUMEROFILAS = oBusCLS.numeroFilas;
                oBus.DESCRIPCION = oBusCLS.descripcion;
                oBus.OBSERVACION = oBusCLS.observacion;
                oBus.IIDMARCA = oBusCLS.idMarca;
                oBus.BHABILITADO = 1;
                bd.Bus.Add(oBus);
                bd.SaveChanges();
                


            }
            return RedirectToAction("Index");
        }
        public void listarTipoBus()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.TipoBus
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDTIPOBUS.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaTipoBus = lista;
            }

        }
        public void listarMarca()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.Marca
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDMARCA.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaMarca = lista;
            }
        }
        public void listarModelo()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.Modelo
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDMODELO.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaModelo = lista;
            }
        }
        public void listarSucursal()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.Sucursal
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDSUCURSAL.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaSucursal = lista;
            }
        }
        public void listarCombos()
        {
            listarModelo();
            listarMarca();
            listarTipoBus();
            listarSucursal();
            
        }

        public ActionResult Edit(int id)
        {
            BusCLS oBusCLS = new BusCLS();
            listarCombos();
            using(var bd= new BDPasajeEntities())
            {
                Bus oBus = bd.Bus.Where(p => p.IIDBUS.Equals(id)).First();
                oBusCLS.idBus = oBus.IIDBUS;
                oBusCLS.idSucursal = (int)oBus.IIDSUCURSAL;
                oBusCLS.idTipobus = (int)oBus.IIDTIPOBUS;
                oBusCLS.placa = oBus.PLACA;
                oBusCLS.fechaCompra = (DateTime)oBus.FECHACOMPRA;
                oBusCLS.idModelo = (int)oBus.IIDMODELO;
                oBusCLS.numeroColumnas = (int)oBus.NUMEROCOLUMNAS;
                oBusCLS.numeroFilas = (int)oBus.NUMEROFILAS;
                oBusCLS.observacion = oBus.OBSERVACION;
                oBusCLS.descripcion = oBus.DESCRIPCION;
                oBusCLS.idMarca = (int)oBus.IIDMARCA;

            }
            return View(oBusCLS);
        }
        [HttpPost]
        public ActionResult Edit (BusCLS oBusCLS)
        {
            string placa = oBusCLS.placa;
            int idBus = oBusCLS.idBus;
            int nregistrosEncontrados = 0;
            using(var bd= new BDPasajeEntities())
            {
                nregistrosEncontrados = bd.Bus.Where(p => p.PLACA.Equals(placa) && !p.IIDBUS.Equals(idBus)).Count();
            }
            if (!ModelState.IsValid || nregistrosEncontrados>=1)
            {
                if (nregistrosEncontrados >= 1) oBusCLS.mensajeError = "El bus ya existe";
                listarCombos();
                return View(oBusCLS);
            }
            using (var bd=new BDPasajeEntities())
            {
                Bus oBus = bd.Bus.Where(p => p.IIDBUS.Equals(idBus)).First();
                oBus.IIDBUS = oBusCLS.idBus;
                oBus.IIDSUCURSAL = oBusCLS.idSucursal;
                oBus.IIDTIPOBUS = oBusCLS.idTipobus;
                oBus.PLACA = oBusCLS.placa;
                oBus.FECHACOMPRA = oBusCLS.fechaCompra;
                oBus.NUMEROCOLUMNAS = oBusCLS.numeroColumnas;
                oBus.NUMEROFILAS = oBusCLS.numeroFilas;
                oBus.DESCRIPCION = oBusCLS.descripcion;
                oBus.OBSERVACION = oBusCLS.observacion;
                oBus.NUMEROFILAS = oBusCLS.numeroFilas;
                oBus.IIDMODELO = oBusCLS.idModelo;
                oBus.IIDMARCA = oBusCLS.idMarca;
                bd.SaveChanges();



            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Eliminar(int idBus)
        {
            using(var bd = new BDPasajeEntities())
            {
                Bus oBus = bd.Bus.Where(p => p.IIDBUS.Equals(idBus)).First();
                oBus.BHABILITADO = 0;
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }

    
}
