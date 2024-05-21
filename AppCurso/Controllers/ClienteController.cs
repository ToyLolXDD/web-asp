using AppCurso.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppCurso.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index(ClienteCLS oClienteCLS)
        {
            llenarSexo();
            ViewBag.lista = listaSexo;
            List<Models.ClienteCLS> listaClientes = null;
            int? idSexo = oClienteCLS.idsexo;
            using (var bd = new BDPasajeEntities())
            {
                if (oClienteCLS.idsexo == 0)
                {


                    listaClientes = (from cliente in bd.Cliente
                                     where cliente.BHABILITADO == 1
                                     select new ClienteCLS
                                     {
                                         idcliente = cliente.IIDCLIENTE,
                                         nombre = cliente.NOMBRE,
                                         appaterno = cliente.APPATERNO,
                                         apmaterno = cliente.APMATERNO,
                                         email = cliente.EMAIL,
                                         telefonoFijo = cliente.TELEFONOFIJO,
                                         direccion = cliente.DIRECCION,
                                         idsexo = cliente.IIDSEXO,
                                         telefonoCelular = cliente.TELEFONOCELULAR,

                                     }
                                    ).ToList();
                }
                else
                {
                    listaClientes = (from cliente in bd.Cliente
                                     where cliente.BHABILITADO == 1 &&
                                     cliente.IIDSEXO == idSexo
                                     select new ClienteCLS
                                     {
                                         idcliente = cliente.IIDCLIENTE,
                                         nombre = cliente.NOMBRE,
                                         appaterno = cliente.APPATERNO,
                                         apmaterno = cliente.APMATERNO,
                                         email = cliente.EMAIL,
                                         telefonoFijo = cliente.TELEFONOFIJO,
                                         direccion = cliente.DIRECCION,
                                         idsexo = cliente.IIDSEXO,
                                         telefonoCelular = cliente.TELEFONOCELULAR,

                                     }
                                    ).ToList();
                }
            }
            return View(listaClientes);
            
        }
        List<SelectListItem> listaSexo;
        private void llenarSexo()
        {
            using(var bd=new BDPasajeEntities())
            {
                listaSexo = (from sexo in bd.Sexo
                            where sexo.BHABILITADO==1
                            select new SelectListItem
                            {
                                Text=sexo.NOMBRE,
                                Value=sexo.IIDSEXO.ToString(),
                            }).ToList();
                listaSexo.Insert(0, new SelectListItem { Text = "--Seleccione--" });
            }
        }
        public ActionResult Agregar()
        {
            llenarSexo();
            ViewBag.lista = listaSexo;
            return View();
        }
        [HttpPost]
        public ActionResult Agregar(ClienteCLS oClienteCLS)
        {
            int nroRegistrosEncontrados = 0;
            string nombreCliente = oClienteCLS.nombre;
            string apPaterno = oClienteCLS.appaterno;
            string apMaterno = oClienteCLS.apmaterno;

            using (var bd = new BDPasajeEntities())
            {
                nroRegistrosEncontrados = bd.Cliente.Where(p => p.NOMBRE.Equals(nombreCliente) && p.APPATERNO.Equals(apPaterno) && p.APMATERNO.Equals(apMaterno)).Count();
            }
            if (!ModelState.IsValid || nroRegistrosEncontrados >= 1)
            {
                if (nroRegistrosEncontrados >= 1) oClienteCLS.mensajeError = "Ya existe cliente registrado";
                llenarSexo();
                ViewBag.lista = listaSexo;
                return View(oClienteCLS);
            }
            
            else
            {
                using (var bd = new BDPasajeEntities())
                {
                    Cliente oCliente = new Cliente();
                    oCliente.NOMBRE = oClienteCLS.nombre;
                    oCliente.APPATERNO = oClienteCLS.appaterno;
                    oCliente.APMATERNO = oClienteCLS.apmaterno;
                    oCliente.EMAIL = oClienteCLS.email;
                    oCliente.DIRECCION = oClienteCLS.direccion;
                    oCliente.BHABILITADO = 1;
                    oCliente.IIDSEXO = oClienteCLS.idsexo;
                    oCliente.TELEFONOCELULAR = oClienteCLS.telefonoCelular;
                    oCliente.TELEFONOFIJO = oClienteCLS.telefonoFijo;
                    bd.Cliente.Add(oCliente);
                    bd.SaveChanges();
                }
                
            }
            return RedirectToAction("Index");
        }
        public ActionResult Editar(int id)
        {
            ClienteCLS oClienteCLS = new ClienteCLS();
            
            using(var bd = new BDPasajeEntities())
            {
                llenarSexo();
                ViewBag.lista = listaSexo;
                Cliente oCliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(id)).First();
                oClienteCLS.idcliente = oCliente.IIDCLIENTE;
                oClienteCLS.nombre = oCliente.NOMBRE;
                oClienteCLS.appaterno = oCliente.APPATERNO;
                oClienteCLS.apmaterno = oCliente.APMATERNO;
                oClienteCLS.direccion = oCliente.DIRECCION;
                oClienteCLS.email = oCliente.EMAIL;
                oClienteCLS.idsexo = oCliente.IIDSEXO;
                oClienteCLS.telefonoCelular = oCliente.TELEFONOCELULAR;
                oClienteCLS.telefonoFijo = oCliente.TELEFONOFIJO;
            }
            return View(oClienteCLS);
        }
        [HttpPost]
        public ActionResult Editar(ClienteCLS oClienteCLS)
        {
            int idCliente = oClienteCLS.idcliente;
            int nroRegistrosEncontrados = 0;
            string nombre = oClienteCLS.nombre;
            string appaterno = oClienteCLS.appaterno;
            string apmaterno = oClienteCLS.apmaterno;
            using (var bd=new BDPasajeEntities())
            {
                nroRegistrosEncontrados= bd.Cliente.Where(p => p.NOMBRE.Equals(nombre) && p.APPATERNO.Equals(appaterno) && p.APMATERNO.Equals(apmaterno) && !p.IIDCLIENTE.Equals(idCliente)).Count();
            }
            if(!ModelState.IsValid || nroRegistrosEncontrados>=1)
            {
                if (nroRegistrosEncontrados >= 1) oClienteCLS.mensajeError = "Ya existe el registro a editar";
                return View(oClienteCLS);
            }
            using (var bd=new BDPasajeEntities())
            {
                Cliente oCliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(idCliente)).First();
                oCliente.NOMBRE = oClienteCLS.nombre;
                oCliente.APPATERNO = oClienteCLS.appaterno;
                oCliente.APMATERNO = oClienteCLS.apmaterno;
                oCliente.EMAIL = oClienteCLS.email;
                oCliente.DIRECCION = oClienteCLS    .direccion;
                oCliente.IIDSEXO = oClienteCLS.idsexo;
                oCliente.TELEFONOCELULAR = oClienteCLS.telefonoCelular;
                oCliente.TELEFONOFIJO = oClienteCLS.telefonoFijo;

                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar (int idCliente)
        {
            using(var bd=new BDPasajeEntities())
            {
                Cliente oCliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(idCliente)).First();
                oCliente.BHABILITADO = 0;
                bd.SaveChanges();
                
            }
            return RedirectToAction("Index");
        }
           
    }
}