using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Email
    {

        public static async Task<ML.Result> Execute(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {

                var apiKey = "";
                var Client = new SendGridClient(apiKey);
                var from = new EmailAddress("marcocb1998gmail.com", "Marco");
                var subject = "Registro Exitoso";
                var to = new EmailAddress(usuario.Email, usuario.Nombre);
                var plainTextContent = "Registro Exitoso";
                var htmlContent = "C:/Users/digis/Documents/MCastaneda/Email/index.html";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = Client.SendEmailAsync(msg);

                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }



        public static ML.Result PopulateBody(string pathHTML, string userName, string password, string Nombre, string activateURL)
        {
            ML.Result result = new ML.Result();

            try
            {
                string body = string.Empty;

                using (StreamReader reader = new StreamReader(pathHTML))
                {
                    body = reader.ReadToEnd();
                }

                body = body.Replace("{UserName}", userName);
                body = body.Replace("{Password}", password);
                body = body.Replace("{Nombre}", Nombre);
                body = body.Replace("{Url}", activateURL);

                result.Correct = true;
                result.Object = body;

            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;

        }



        public static ML.Result SendEmail(ML.Email email)
        {
            ML.Result result = new ML.Result();

            try
            {
                // Create the message.
                MailMessage mailNew = new MailMessage();
                // Set the message properties.
                MailAddress from = new MailAddress(email.From, email.FromDisplayName);

                mailNew.From = from;
                mailNew.To.Add(email.To);
                mailNew.Subject = email.Subject;
                mailNew.IsBodyHtml = true;
                mailNew.Body = email.Body;

                SmtpClient smtp = new SmtpClient();

                smtp.Host = email.Host;
                smtp.Port = email.Port;
                smtp.EnableSsl = true;

                smtp.Credentials = new System.Net.NetworkCredential(email.User, email.Password);

                smtp.Send(mailNew);

                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}