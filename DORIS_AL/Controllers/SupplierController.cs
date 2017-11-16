using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DORIS_AL.Models;
using System.Web.Http.Description;
using System.Data.Entity.Core.Objects;

namespace DORIS_AL.Controllers
{
    public class SupplierController : ApiController
    {
        private DDTrack_APIEntities db = new DDTrack_APIEntities();

        [ResponseType(typeof(UserSupplierList))]
        [HttpGet]
        public IHttpActionResult GetUserSuppliers(string hash , long UserID )
        {
            List<UserSuppliers> us = db.UserSuppliers(hash, UserID).ToList();

            List<UserSupplierList> usl  = new List<UserSupplierList>();

            foreach ( UserSuppliers u in us )
            {
                UserSupplierList ul = new UserSupplierList();
                ul.Copy(u);
                usl.Add(ul);

            }
            return Ok(usl);
        }

        [ResponseType(typeof(DeleteUserSupplierResponse))]
        [HttpDelete]
        public IHttpActionResult DeleteUserSupplier(DeleteUserSupplier dus)
        {
            ObjectParameter success = new ObjectParameter("success", typeof(int));
            db.deleteUserSupplier(dus.Hash, dus.UserID, dus.SupplierID,success );
            DeleteUserSupplierResponse dusr = new DeleteUserSupplierResponse();
            dusr.ReturnCode = (int)success.Value;
            return Ok(dusr);
        }

        [ResponseType(typeof(bool))]
        [HttpPost]
        public IHttpActionResult PostUserSupplier(PostSupplier ps)
        {
            ObjectParameter success = new ObjectParameter("success",typeof(int));

            db.addUserSupplier(ps.Hash, ps.UserID, ps.SupplierID, ps.SupplierCode, success);
            if ((int)success.Value == 0)
                return Ok(false);
            return Ok(true);

        }




    }
}
