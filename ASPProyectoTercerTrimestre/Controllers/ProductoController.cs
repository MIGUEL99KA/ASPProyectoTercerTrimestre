using ASPProyectoTercerTrimestre.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Routing;

namespace ASPProyectoTercerTrimestre.Controllers
{
    public class ProductoController : Controller
    {
        [Authorize]
        // GET: Producto
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.producto.ToList());
            }

        }

        public static string NombreProveedor(int idProveedor)
        {
            using (var db = new inventario2021Entities())
            {
                return db.proveedor.Find(idProveedor).nombre;
            }
        }


        public ActionResult ListarProveedores()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.proveedor.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(producto producto)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.producto.Add(producto);
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
                var producto = db.producto.Find(id);

                //conu+sultando de la tabla producto_imagen las imagenes del producto
                var imagen = db.producto_imagen.Where(e => e.id_producto == producto.id).FirstOrDefault();
                //pasando la ruta a la vista
                ViewBag.imagen = imagen.imagen;

                return View(producto);
            }
        }

        public ActionResult Edit(int id)
        {
            using (var db = new inventario2021Entities())
            {
                producto productoEdit = db.producto.Where(a => a.id == id).FirstOrDefault();
                return View(productoEdit);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(producto productoEdit)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var oldproduct = db.producto.Find(productoEdit.id);
                    oldproduct.nombre = productoEdit.nombre;
                    oldproduct.cantidad = productoEdit.cantidad;
                    oldproduct.descripcion = productoEdit.descripcion;
                    oldproduct.percio_unitario = productoEdit.percio_unitario;
                    oldproduct.id_proveedor = productoEdit.id_proveedor;
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
                    producto producto = db.producto.Find(id);
                    db.producto.Remove(producto);
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

        public ActionResult PaginadorIndex(int pagina = 1)
        {
            try
            {
                var CantidadRegistros = 5;

                using (var db = new inventario2021Entities())
                {
                    var producto = db.producto.OrderBy(x => x.id).Skip((pagina - 1) * CantidadRegistros).Take(CantidadRegistros).ToList();

                    var totalRegistros = db.producto.Count();
                    var modelo = new ProductoIndex();
                    modelo.Producto = producto;
                    modelo.Actualpage = pagina;
                    modelo.Total = totalRegistros;
                    modelo.Recordspage = CantidadRegistros;
                    modelo.valueQueryString = new RouteValueDictionary();

                    return View(modelo);

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }
    }


}