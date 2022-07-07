using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL.Controllers
{
    public class UsuarioController : ApiController
    {
        [HttpGet]
        [Route("api/Usuario/GetAll")]
        public IHttpActionResult GetAll()
        {
          


            var result = BL.Usuario.GetAll();

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK,  result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }

        }

    }
}
