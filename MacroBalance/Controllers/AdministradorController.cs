using System.Linq;
using System.Net;
using System.Web.Mvc;
using MacroBalance.Database;
using MacroBalance.Service;
using Macrobalance.Constants;


namespace MacroBalance.Controllers
{
    public class AdministradorController : Controller
    {
        private readonly MacroBalanceEntities1 db = new MacroBalanceEntities1();

        public ActionResult Index()
        {
            if (Session[SessionAtributes.Rol]?.ToString() != UserRoles.Admin)
                return RedirectToAction("Index", "Home");

            var usuarios = db.Usuario.ToList();
            return View(usuarios);
        }

        // GET: Administrador/Details/5
        public ActionResult Details(int? id)
        {
            if (Session[SessionAtributes.Rol]?.ToString() != UserRoles.Admin)
                return RedirectToAction("Index", "Home");

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var usuario = db.Usuario.Find(id);
            if (usuario == null)
                return HttpNotFound();

            return View(usuario);
        }

        // GET: Administrador/Create
        public ActionResult Create()
        {
            if (Session[SessionAtributes.Rol]?.ToString() != UserRoles.Admin)
                return RedirectToAction("Index", "Home");

            return View();
        }

        // POST: Administrador/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nombre,Apellidos,CorreoElectronico,Contrasena,Rol")] Usuario usuario)
        {
            if (Session[SessionAtributes.Rol]?.ToString() != UserRoles.Admin)
                return RedirectToAction("Index", "Home");

            // Validar campos vacíos manualmente
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                ModelState.AddModelError("Nombre", "Por favor ingrese un nombre.");

            if (string.IsNullOrWhiteSpace(usuario.Apellidos))
                ModelState.AddModelError("Apellidos", "Por favor ingrese los apellidos.");

            if (string.IsNullOrWhiteSpace(usuario.CorreoElectronico))
                ModelState.AddModelError("CorreoElectronico", "Por favor ingrese un correo electrónico.");

            if (string.IsNullOrWhiteSpace(usuario.Contrasena))
                ModelState.AddModelError("Contrasena", "Por favor ingrese una contraseña.");

            if (string.IsNullOrWhiteSpace(usuario.Rol))
                ModelState.AddModelError("Rol", "Por favor seleccione un rol.");

            // Validar correo duplicado
            if (db.Usuario.Any(u => u.CorreoElectronico == usuario.CorreoElectronico))
            {
                ModelState.AddModelError("CorreoElectronico", "El correo electrónico ya está en uso.");
            }

            if (ModelState.IsValid)
            {
                usuario.Contrasena = PasswordHasher.Hash(usuario.Contrasena);
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Roles = new SelectList(new[] { "User", UserRoles.Admin }, usuario.Rol);
            return View(usuario);
        }



        // GET: Administrador/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session[SessionAtributes.Rol]?.ToString() != UserRoles.Admin)
                return RedirectToAction("Index", "Home");

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var usuario = db.Usuario.Find(id);
            if (usuario == null)
                return HttpNotFound();

            ViewBag.Roles = new SelectList(new[] { "User", UserRoles.Admin }, usuario.Rol);
            return View(usuario);
        }

        // POST: Administrador/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Apellidos,CorreoElectronico,Rol")] Usuario usuario)
        {
            if (Session[SessionAtributes.Rol]?.ToString() != UserRoles.Admin)
                return RedirectToAction("Index", "Home");

            var originalUsuario = db.Usuario.Find(usuario.Id);
            if (originalUsuario == null)
                return HttpNotFound();

            // Validar campos vacíos
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                ModelState.AddModelError("Nombre", "Por favor ingrese un nombre.");

            if (string.IsNullOrWhiteSpace(usuario.Apellidos))
                ModelState.AddModelError("Apellidos", "Por favor ingrese los apellidos.");

            if (string.IsNullOrWhiteSpace(usuario.CorreoElectronico))
                ModelState.AddModelError("CorreoElectronico", "Por favor ingrese un correo electrónico.");

            if (string.IsNullOrWhiteSpace(usuario.Rol))
                ModelState.AddModelError("Rol", "Por favor seleccione un rol.");

            // Validar correo duplicado (excepto si es el mismo usuario)
            if (db.Usuario.Any(u => u.CorreoElectronico == usuario.CorreoElectronico && u.Id != usuario.Id))
            {
                ModelState.AddModelError("CorreoElectronico", "El correo electrónico ya está en uso.");
            }

            if (ModelState.IsValid)
            {
                originalUsuario.Nombre = usuario.Nombre;
                originalUsuario.Apellidos = usuario.Apellidos;
                originalUsuario.CorreoElectronico = usuario.CorreoElectronico;
                originalUsuario.Rol = usuario.Rol;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Roles = new SelectList(new[] { "User", UserRoles.Admin }, usuario.Rol);
            return View(usuario);
        }

        // GET: Administrador/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Rol"]?.ToString() != UserRoles.Admin)
                return RedirectToAction("Index", "Home");

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var usuario = db.Usuario.Find(id);
            if (usuario == null)
                return HttpNotFound();

            return View(usuario);
        }

        // POST: Administrador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Rol"]?.ToString() != UserRoles.Admin)
                return RedirectToAction("Index", "Home");

            var usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
