using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetaLabUnidad3.Singleton;
using BetaLabUnidad3.Models;

namespace BetaLabUnidad3.Controllers
{
    public class MedController : Controller
    {
        // GET: Med
        public ActionResult Index()
        {
            bool n = false;
            if (n == false)
            {
                DataAlmacenada.Instancia.LecturaArchivo();
                n = true;
            }
            
            return View(DataAlmacenada.Instancia.ListaMed); //Cargamos la lista 
        }


        // GET: Med/Create
        public ActionResult Create()
        {
            return RedirectToAction("CrearPedido", "Pedidos");
        }

        // POST: Med/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                return RedirectToAction("CrearPedido", "Pedidos");
            }
            catch
            {
                return View();
            }
        }



        public ActionResult Buscar(string nombre)
        {
            var MedBuscado = new Med();
            MedBuscado.Nombre = nombre;
            int indice = DataAlmacenada.Instancia.ArbolMed.CrearNodo(MedBuscado.Nombre, MedBuscado.id);

            foreach (var item in DataAlmacenada.Instancia.ListaMed)
            {
                if (indice == item.id)
                {
                    DataAlmacenada.Instancia.MedBuscados.Add(item);
                }
            }

            return View(DataAlmacenada.Instancia.MedBuscados);
        }

        // POST: Med/Create
        [HttpPost]
        public ActionResult Buscar(FormCollection collection)
        {
            try
            {
                return RedirectToAction("CrearPedido", "Pedidos");
            }
            catch
            {
                return View();
            }
        }


    }
}
