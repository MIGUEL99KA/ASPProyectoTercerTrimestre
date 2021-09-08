using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPProyectoTercerTrimestre.Models;
using Rotativa;

namespace ASPProyectoTercerTrimestre.Controllers
{
    public class Producto_compraController : Controller
    {
        [Authorize]
        // GET: Producto_compra
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.producto_compra.ToList());
            }
        }

        public static string NombreProducto(int idProducto)
        {
            using (var db = new inventario2021Entities())
            {
                return db.producto.Find(idProducto).nombre;
            }
        }

        public ActionResult ListarCompras()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.compra.ToList());
            }
        }

        public ActionResult ListarProductos()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.producto.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(producto_compra producto_Compra)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.producto_compra.Add(producto_Compra);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.producto_compra.Find(id));
            }
        }

        public ActionResult Edit(int id)
        {
            using (var db = new inventario2021Entities())
            {
                producto_compra Producto_compraEdit = db.producto_compra.Where(a => a.id == id).FirstOrDefault();
                return View(Producto_compraEdit);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(producto_compra Producto_compraEdit)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var oldproduct = db.producto_compra.Find(Producto_compraEdit.id);
                    oldproduct.id_compra = Producto_compraEdit.id_compra;
                    oldproduct.id_producto = Producto_compraEdit.id_producto;
                    oldproduct.cantidad = Producto_compraEdit.cantidad;
                    db.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    producto_compra producto_Compra = db.producto_compra.Find(id);
                    db.producto_compra.Remove(producto_Compra);
                    db.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }

        public ActionResult Cuenta()
        {
            try
            {
                var db = new inventario2021Entities();
                var Query = from tabCliente in db.cliente
                            join tabCompra in db.compra on tabCliente.id equals tabCompra.id_cliente
                            join tabProducto_compra in db.producto_compra on tabCompra.id equals tabProducto_compra.id_compra
                            join tabProducto in db.producto on tabProducto_compra.id equals tabProducto.id
                            select new Cuenta
                            {
                                NombreCliente = tabCliente.nombre,
                                DocumentoCliente = tabCliente.documento,
                                NombreProducto = tabProducto.nombre,
                                PrecioProducto = tabProducto.percio_unitario,
                                CantidadProducto = tabProducto.cantidad
                            };
                return View(Query);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();
            }

        }
        public ActionResult PdfCuenta()
        {
            return new ActionAsPdf("Cuenta") { FileName = "Cuenta.pdf" };
        }
    }
}