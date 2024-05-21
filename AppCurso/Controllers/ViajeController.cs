using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppCurso.Models;
using System.Web.Mvc;

namespace AppCurso.Controllers
{
    public class ViajeController : Controller
    {
        // GET: Viaje
        public ActionResult Index()
        {
            List<ViajeCLS> listaViajes = null;
            using (var bd = new BDPasajeEntities())
            {
                listaViajes = (from viaje in bd.Viaje
                               join lugar in bd.Lugar
                               on viaje.IIDLUGARORIGEN equals lugar.IIDLUGAR
                               join lugarDestino in bd.Lugar
                               on viaje.IIDLUGARDESTINO equals lugarDestino.IIDLUGAR
                               join bus in bd.Bus
                               on viaje.IIDBUS equals bus.IIDBUS
                               where viaje.BHABILITADO == 1
                               select new ViajeCLS
                               {
                                   idViaje = viaje.IIDVIAJE,
                                   placa = bus.PLACA,
                                   nombreLugarOrigen = lugar.NOMBRE,
                                   nombreLugarDestino = lugarDestino.NOMBRE
                               }).ToList();

            }
            return View(listaViajes);
        }
        public ActionResult Agregar()
        {
            listarCombos();
            return View();
        }
        public void listarLugarOrigen()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.Lugar
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDLUGAR.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaLugar = lista;
            }
        }
        public void listarBus()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.Bus
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.PLACA,
                             Value = item.IIDBUS.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaBus = lista;
            }
        }
        public void listarCombos()
        {
            listarBus();
            listarLugarOrigen();
        }
    
        public ActionResult Eliminar(int id)
        {
            using(var bd=new BDPasajeEntities())
            {
                Viaje oViaje = bd.Viaje.Where(p => p.IIDVIAJE.Equals(id)).First();
                oViaje.BHABILITADO = 0;
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}