using ASPProyectoTercerTrimestre.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using System.Web.Security;
using System.Web;
//using ASPProyectoTercerTrimestre.Filtros;

namespace ASPProyectoTercerTrimestre.Controllers
{
    public class Producto_imagenController : Controller
    {
        [Authorize]
        // GET: Producto_imagen
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.producto_imagen.ToList());
            }
        }

        public static string NombreProducto(int idproducto)
        {
            using (var db = new inventario2021Entities())
            {
                return db.producto.Find(idproducto).nombre;
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

        public ActionResult Create(producto_imagen producto_imagen)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.producto_imagen.Add(producto_imagen);
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
                return View(db.producto_imagen.Find(id));
            }
        }

        public ActionResult Edit(int id)
        {
            using (var db = new inventario2021Entities())
            {
                producto_imagen producto_imagenEdit = db.producto_imagen.Where(a => a.id == id).FirstOrDefault();
                return View(producto_imagenEdit);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(producto_imagen producto_imagenEdit)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var oldproduct = db.producto_imagen.Find(producto_imagenEdit.id);
                    oldproduct.imagen = producto_imagenEdit.imagen;
                    oldproduct.id_producto = producto_imagenEdit.id_producto;
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
                    producto_imagen producto_imagen = db.producto_imagen.Find(id);
                    db.producto_imagen.Remove(producto_imagen);
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

        public ActionResult cargarImagen()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult cargarImagen(int id_producto, HttpPostedFileBase imagen)
        {
            try
            {
                //string para guardar la ruta
                string filePath = string.Empty;
                string nameFile = "";

                //condicion para saber si el archivo llego
                if (imagen != null)
                {
                    //ruta de la carpeta que guardara el archivo
                    string path = Server.MapPath("~/Uploads/Imagenes/");

                    //condicion para saber si la carpeta uploads existe
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    nameFile = Path.GetFileName(imagen.FileName);

                    //obtener el nombre del archivo
                    filePath = path + Path.GetFileName(imagen.FileName);

                    //obtener la extension del archivo
                    string extension = Path.GetExtension(imagen.FileName);

                    //guardar el archivo
                    imagen.SaveAs(filePath);
                }

                using (var db = new inventario2021Entities())
                {
                    var imagenProducto = new producto_imagen();
                    imagenProducto.id_producto = id_producto;
                    imagenProducto.imagen = "/Uploads/Imagenes/" + nameFile;
                    db.producto_imagen.Add(imagenProducto);
                    db.SaveChanges();

                }

                return View();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }

        }

    }

}