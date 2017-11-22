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
        [HttpPost]
        public IHttpActionResult DeleteUserSupplier(DeleteUserSupplier dus)
        {
            ObjectParameter success = new ObjectParameter("success", typeof(int));
            db.deleteUserSupplier(dus.Hash, dus.UserID, dus.SupplierID,success );
            DeleteUserSupplierResponse dusr = new DeleteUserSupplierResponse();
            dusr.ReturnCode = (int)success.Value;


            switch (dusr.ReturnCode)
            {
                case 0: dusr.appendError("Success"); break;
                case -1: dusr.appendError("Not authorized"); break;
                case -2: dusr.appendError("Invalid User"); break;
                case -3: dusr.appendError("No match for supplier"); break;
                case -4: dusr.appendError("General error occurred"); break;
                default: break;
            }


            return Ok(dusr);
        }

        [ResponseType(typeof(PostUserSupplierResponse))]
        [HttpPost]
        public IHttpActionResult PostUserSupplier(PostSupplier ps)
        {
            ObjectParameter success = new ObjectParameter("success",typeof(int));

            db.addUserSupplier(ps.Hash, ps.UserID, ps.SupplierID, ps.SupplierCode, success);
            PostUserSupplierResponse pusr = new PostUserSupplierResponse();
            pusr.ReturnCode = (int)success.Value;
            switch ( pusr.ReturnCode)
            {
                case 0: pusr.appendError("Invalid parameters"); break;
                case 1: pusr.appendError("Success"); break;
                case 2: pusr.appendError("Success , already added");break;
                case 3:pusr.appendError("not authorized"); break;
                case -1: pusr.appendError("no such supplier");break;
                default: break;
            }

            return Ok(pusr);

        }




    }
}
