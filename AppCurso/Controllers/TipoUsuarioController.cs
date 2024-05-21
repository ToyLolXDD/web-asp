using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppCurso.Models;

namespace AppCurso.Controllers
{
    public class TipoUsuarioController : Controller
    {
        // GET: TipoUsuario
        private TipoUsuarioCLS oTipoVal;
        private bool buscarTipoUser(TipoUsuarioCLS oTipoUser)
        {
            bool busquedaId = true;
            bool busdaNom = true;
            bool busquedaDescrip = true;
            if (oTipoVal.iidTipoUsuario > 0)
                busquedaId = oTipoUser.iidTipoUsuario.ToString().Contains(oTipoVal.iidTipoUsuario.ToString());
            if (oTipoVal.nombre != null)
                busdaNom=oTipoUser.nombre.ToString().Contains(oTipoVal.nombre);
            if (oTipoVal.descripcion != null)
                busquedaDescrip=oTipoUser.descripcion.ToString().Contains(oTipoVal.descripcion);
            return (busquedaId && busdaNom && busquedaDescrip);
        }
        public ActionResult Index(TipoUsuarioCLS oTipoUser)
        {
            oTipoVal = oTipoUser;
            List<TipoUsuarioCLS> listaTipoUsuario = null;
            //pongo variable
            List<TipoUsuarioCLS> listaFiltrado;
            using (var bd= new BDPasajeEntities())
            {
                listaTipoUsuario = (from tipousuario in bd.TipoUsuario
                                    where tipousuario.BHABILITADO == 1
                                    select new TipoUsuarioCLS
                                    {
                                        iidTipoUsuario = tipousuario.IIDTIPOUSUARIO,
                                        nombre = tipousuario.NOMBRE,
                                        descripcion = tipousuario.DESCRIPCION
                                    }).ToList();
                if (oTipoUser.iidTipoUsuario == 0 && oTipoUser.nombre == null && oTipoUser.descripcion ==null) listaFiltrado = listaTipoUsuario;
                else
                {
                    Predicate<TipoUsuarioCLS> pred = new Predicate<TipoUsuarioCLS>(buscarTipoUser);
                    listaFiltrado = listaTipoUsuario.FindAll(pred);
                }

            }
            return View(listaFiltrado);
        }


    }
}