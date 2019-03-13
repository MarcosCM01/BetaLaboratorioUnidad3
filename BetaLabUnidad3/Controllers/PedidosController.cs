using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetaLabUnidad3.Models;
using BetaLabUnidad3.Singleton;

namespace BetaLabUnidad3.Controllers
{
    public class PedidosController : Controller
    {
        // GET: Pedidos
        public ActionResult Index()
        {
            return View(DataAlmacenada.Instancia.ListaPedidos);
        }

        // GET: Pedidos/CrearPedido
        public ActionResult CrearPedido()
        {
            return View();
        }

        // POST: Pedidos/CrearPedido
        [HttpPost]
        public ActionResult CrearPedido(Pedidos pedido)
        {
            try
            {

                if(string.IsNullOrEmpty(pedido.ClientName) || string.IsNullOrEmpty(pedido.direccion) || string.IsNullOrEmpty(pedido.nit))
                {
                    ViewBag.Error = "Se necesita llenar todos los campos vacios";
                    return View(pedido);
                }
                DataAlmacenada.Instancia.ListaPedidos.Add(pedido);
                return RedirectToAction("AgregarMed");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pedidos/AgregarMed
        public ActionResult AgregarMed()
        {
            return View(DataAlmacenada.Instancia.ListaMed);
        }

        // POST: Pedidos/Create
        [HttpPost]
        public ActionResult AgregarMed(FormCollection collection, Pedidos pedido)
        {
            try
            {
               foreach(var item in DataAlmacenada.Instancia.ListaMed)
                {
                    item.pedido = int.Parse(collection["pedido"]);
                }

               foreach(var item in DataAlmacenada.Instancia.ListaMed)
                {
                    if(item.pedido > 0 && item.pedido < item.existencia)
                    {
                        item.existencia = item.existencia - item.pedido;
                        Med agregado = new Med();
                        agregado.Nombre = item.Nombre;
                        agregado.id = item.id;
                        agregado.descripcion = item.descripcion;
                        agregado.casa = item.casa;
                        agregado.precio = item.precio;
                        agregado.existencia = item.existencia;

                        Pedidos pedidoAgregado = new Pedidos();
                        pedidoAgregado.ListaMedPedido.Add(agregado);

                        DataAlmacenada.Instancia.ListaPedidos.Add(pedidoAgregado);
                    }
                    else
                    {
                        ViewBag.Error = "No hay cantidad necesaria.";
                        return View(pedido);
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pedidos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pedidos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
