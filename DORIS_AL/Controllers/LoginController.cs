using DORIS_AL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace DORIS_AL.Controllers
{
    public class LoginController : ApiController
    {
        private DDTrack_APIEntities db = new DDTrack_APIEntities();

     

        [ResponseType(typeof(LoginResponse))]
        [HttpPost]
        public IHttpActionResult LoginUser( LoginRequest req)
        {
            ObjectParameter token = new ObjectParameter("token", typeof(string));
            ObjectParameter change = new ObjectParameter("change", typeof(string));

          
            if ( ! ModelState.IsValid )
            {
                return BadRequest();
            }
            
            LoginResponse lr = new LoginResponse();
            int rc = db.loginUser(req.UserID, req.Password, req.Supplier, token, req.IPAddress, change);

            lr.CopyRequest(req);
            lr.Token = (string)token.Value;
            lr.Change = (string)change.Value;
            lr.Password = "";

            return Ok(lr);
        }

    }
}
