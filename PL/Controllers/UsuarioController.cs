using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            //ML.Usuario usuario = new ML.Usuario();
            //ML.Result result = BL.Usuario.GetAll(usuario);

            //   usuario.Usuarios = result.Objects;
            ML.Usuario resultUsuario = new ML.Usuario();
            resultUsuario.Usuarios = new List<object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:22468");

                var responseTask = client.GetAsync("/api/Usuario/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                        resultUsuario.Usuarios.Add(resultItemList);
                    }

                }
            }

            return View(resultUsuario);
        }

        [HttpGet]
        public ActionResult Form(int? Idusuario)
        {

            ML.Usuario usuario = new ML.Usuario();
            if (Idusuario == null)
            {
                return View(usuario);
            }
            else
            {
                ML.Result result = new ML.Result();
                result = BL.Usuario.GetById(Idusuario.Value);

                if (result.Correct)
                {
                    usuario = ((ML.Usuario)result.Object);
                }

            }
            return View(usuario);
        }


        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

         


            if (usuario.IdUsuario == 0)
            {
                usuario.Password = ConfigurationManager.AppSettings["PasswordDefault"];
                result = BL.Usuario.Add(usuario);


                if (result.Correct)
                {

                    LoginController email = new LoginController();
                    email.ValidarUser(usuario);

                    //BL.Email.Execute(usuario).Wait();

                   

                    ViewBag.Mensaje = "El usuario se ha agregado";
                }
                else
                {
                    ViewBag.Mensaje = "El usuario no se ha agregado";
                }
            }
            else
            {
                result = BL.Usuario.Update(usuario);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "El usuario se actualizo correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error al realizar la actualizacion" + result.ErrorMessage;
                }
            }



            return PartialView("Modal");
        }


        [HttpGet]
        public ActionResult Delete(ML.Usuario Usuario)
        {
            ML.Result result = BL.Usuario.Delete(Usuario);

            if (result.Correct)
            {
                ViewBag.Mensaje = "El usuario se ha eliminado";
            }
            else
            {
                ViewBag.Mensaje = "error al eliminar";
            }
            return PartialView("modal");
        }
       

            [HttpGet]
            public ActionResult CambioContraseña()
           
            {
            ML.Usuario usuario = new ML.Usuario();
           
            return View(usuario);
            }

            [HttpPost]
            public ActionResult CambioContraseña(string email, string password)
            {
            ML.Result result = new ML.Result();
            result = BL.Usuario.GetByUsername(email);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                if (usuario.Email == email && usuario.Password == password)
                {
                   
                    return RedirectToAction("ConfirmarContraseña", "Usuario", new { IdUsuario = usuario.IdUsuario });
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult ConfirmarContraseña(int? Idusuario)
        {

            ML.Usuario usuario = new ML.Usuario();
            if (Idusuario == null)
            {
                return View(usuario);
            }
            else
            {
                ML.Result result = new ML.Result();
                result = BL.Usuario.GetById(Idusuario.Value);

                if (result.Correct)
                {
                    usuario = ((ML.Usuario)result.Object);
                }

            }
            return View(usuario);
        }

        [HttpPost]
        public ActionResult ConfirmarContraseña(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            result.Correct = true;



           
                

                if (result.Correct)
                {
                    result = BL.Usuario.UpdatePassword(usuario);

                    if (result.Correct)
                    {
                        ViewBag.Mensaje = "El usuario se actualizo correctamente";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un error al realizar la actualizacion" + result.ErrorMessage;
                    }

                }
                
            
   



            return PartialView("Modal");
        }






    }



}
    
 
