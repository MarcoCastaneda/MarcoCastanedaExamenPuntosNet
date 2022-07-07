using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace PL.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Email = "Luisill@gmail.com";
            usuario.Password = "Master1";
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            ML.Result result = new ML.Result();
            result = BL.Usuario.GetByUsername(email);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                if (usuario.Email == email && usuario.Password == password)
                {
                    return RedirectToAction("GetAll", "Usuario");
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


        [HttpPost]
        public ActionResult ValidarUser(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            string userName = usuario.Username;
            string pathHTML = "C:/Users/digis/Documents/MCastaneda/MaCastaneda/MarcoCastañedaPuntos.Net/Email/Leo.html";
            string Nombre = usuario.Nombre;
            string emailTo = usuario.Email;
            string activateURL = "http://localhost:16605/Usuario/CambioContrase%C3%B1a";


            result.Object = usuario;
            result.Correct = true;

            if (result.Correct)
            {
                return EnviarEmail(pathHTML, userName, Nombre, activateURL, emailTo);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error, comprueba que el User Name sea correcto " + result.ErrorMessage;
            }
            return PartialView("Modal");
        }


        public ActionResult EnviarEmail(string pathHTML, string UserName, string Nombre, string activateURL, string emailTo)
        {
        
            ML.Result result = new ML.Result();
        
            try
            {

                result = BL.Email.PopulateBody(pathHTML, UserName, "", Nombre, activateURL);

                ML.Email emailModel = new ML.Email();

                emailModel.From = ConfigurationManager.AppSettings["From"];// "test@digis01.com";//web.config
                emailModel.FromDisplayName = ConfigurationManager.AppSettings["FromDisplayName"];// "Control de Asistencia Escolar";//web.config
                emailModel.Host = ConfigurationManager.AppSettings["Host"]; // "digis01.com";//web.config               
                emailModel.User = ConfigurationManager.AppSettings["User"]; // "test@digis01.com";//web.config
                emailModel.Password = ConfigurationManager.AppSettings["Password"]; // "Welcome01$$$#";//web.config
                emailModel.Port = int.Parse(ConfigurationManager.AppSettings["Port"]); //465;//web.config
                emailModel.Body = result.Object.ToString();
                emailModel.Subject = "Registro Exitoso";//web.config
                emailModel.To = emailTo;//Recuperar el correo de la BD

                result = BL.Email.SendEmail(emailModel);

                if (result.Correct)
                {
                    ViewBag.Message = "Se ha enviado un E-mail a la cuenta: ";
                }
                else
                {
                    ViewBag.Message = "No se ha podido realizar la petición: " + result.ErrorMessage;
                }


            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return PartialView("Modal");



        }



        }

    }


