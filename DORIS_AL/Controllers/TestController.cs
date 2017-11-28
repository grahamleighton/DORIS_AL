using DORIS_AL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace DORIS_AL.Controllers
{
    public class TestController : ApiController
    {
        private DDTrack_APIEntities db = new DDTrack_APIEntities();

        [ResponseType(typeof(bool))]
        public IHttpActionResult ClearTestData(string AuthKey)
        {
            if (AuthKey != "qzcaCh.z")
                return Ok(false);
            db.ClearTestData();
            return Ok(true);
        }
    }
}
