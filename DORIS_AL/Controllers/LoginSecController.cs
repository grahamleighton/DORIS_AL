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
    public class LoginSecController : ApiController
    {
        private DDTrack_APIEntities db = new DDTrack_APIEntities();
        [ResponseType(typeof(bool))]
        [HttpPost]
        public IHttpActionResult SetPassword(UserClasses sp)
        {
            ObjectParameter op = new ObjectParameter("success", typeof(int));
            db.setPassword(sp.Hash, sp.UserID, sp.Password, op);
            if ((int)op.Value == 1)
                return Ok(true);
            else
                return Ok(false);
        }

        [ResponseType(typeof(bool))]
        [HttpPut]
        public IHttpActionResult SetPasswordAndSecurityQuestion(SetPasswordAndSecurity sp)

        {
            ObjectParameter op = new ObjectParameter("success", typeof(int));
            db.setPasswordandSecurityQuestion(sp.Hash, sp.UserID, sp.Password, sp.SecurityQuestionID, sp.SecurityAnswer, op);
            if ((int)op.Value == 1)
                return Ok(true);
            else
                return Ok(false);
        }

        [ResponseType(typeof(SecurityQuestionResponse))]
        [HttpGet]
        public IHttpActionResult GetSecurityQuestion(string Hash, long UserID)
        {
            UserAccess ua = new UserAccess();
            ua.UserID = UserID;
            ua.Hash = Hash;

            SecurityQuestionResponse sqr = new SecurityQuestionResponse();
            ObjectParameter question = new ObjectParameter("question", typeof(string));
            db.getSecurityQuestion( ua.UserID, question);

            if ((string)question.Value == "")
            {
                sqr.appendError("No question found");
            }
            sqr.SecurityQuestion = (string)question.Value;
            sqr.ReturnCode = sqr.ErrorMessage.Count;

            return Ok(sqr);
        }

        [ResponseType(typeof(ValidateRecoveryResponse))]
        [HttpGet]
        public IHttpActionResult ValidateRecovery(string Hash)
        {
            ObjectParameter userid = new ObjectParameter("userid", typeof(long));
            ObjectParameter valid = new ObjectParameter("valid", typeof(int));
            ObjectParameter suppliercode = new ObjectParameter("suppliercode", typeof(string));
            ValidateRecoveryResponse vcr = new ValidateRecoveryResponse();

            db.validateRecovery(Hash, userid, valid, suppliercode);
            if ((int)valid.Value == 1)
            {
                vcr.SupplierCode = (string)suppliercode.Value;
                vcr.UserID = (long)userid.Value;
                vcr.Valid = (int)valid.Value;
            }
             
            return Ok(vcr);
        }

        [ResponseType(typeof(bool))]
        [HttpGet]
        public IHttpActionResult IsEmailAvailable(string Hash , string UserName )
        {
            ObjectParameter exists = new ObjectParameter("exists", typeof(int));

            db.isEmailAvailable(Hash, UserName, exists);

            if ((int)exists.Value == 0)
            {
                return Ok(true);
            }

            return Ok(false);
        }
        [ResponseType(typeof(bool))]
        [HttpGet]
        public IHttpActionResult IsSupplierAvailable(string Hash , string SupplierCode )
        {
            ObjectParameter exists = new ObjectParameter("exists", typeof(int));

            db.isSupplierAvailable(Hash, SupplierCode, exists);

            if ((int)exists.Value == 0)
            {
                return Ok(false);
            }

            return Ok(true);
        }

        [ResponseType(typeof(bool))]
        [HttpPost]
        public IHttpActionResult CheckSecurityAnswer(CheckSecurity cs)
        {
            ObjectParameter correct = new ObjectParameter("correct", typeof(int));

            db.checkSecurityAnswer(cs.Hash, cs.UserID, cs.Answer, correct);

            if ((int)correct.Value == 1)
            {
                return Ok(true);
            }

            return Ok(false);
        }

        [ResponseType(typeof(SecurityQuestions))]
        [HttpGet]
        public IHttpActionResult GetSecurityQuestions( string hash )
        {
            if (hash == null)
                hash = "xxx";
            return Ok(db.getSecurityQuestions(hash).ToList());
        }

        [ResponseType(typeof(RecoveryUserResponse))]
        [HttpPost]
        public IHttpActionResult GetRecoveryUser(RecoveryUser ru)
        {
            ObjectParameter userid = new ObjectParameter("userid", typeof(long));

            db.getRecoveryUser(ru.UserName, ru.SupplierCode, userid);

            RecoveryUserResponse rur = new RecoveryUserResponse();
          
            rur.UserID = (long)userid.Value;

            return Ok(rur);
        }

        [ResponseType(typeof(RecoveryEmailResponse))]
        [HttpGet]
        public IHttpActionResult GetRecoveryEmail(int id)
        {
            ObjectParameter body = new ObjectParameter("body", typeof(string));
            ObjectParameter subject = new ObjectParameter("subject", typeof(string));
            ObjectParameter emailaddress = new ObjectParameter("emailaddress", typeof(string));

            db.getRecoveryEmail(id, body, subject, emailaddress);
            RecoveryEmailResponse rer = new RecoveryEmailResponse();
            if ( !String.IsNullOrEmpty((string)body.Value))
            {
                rer.EmailAddress = (string)emailaddress.Value;
                rer.Subject = (string)subject.Value;
                rer.Body = (string)body.Value;

            }

            return Ok(rer);
        }

    }
}
