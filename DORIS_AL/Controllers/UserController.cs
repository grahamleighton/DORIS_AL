using DORIS_AL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity.Core.Objects;

namespace DORIS_AL.Controllers
{
    public class UserController : ApiController
    {
        private DDTrack_APIEntities db = new DDTrack_APIEntities();

        private enum ActivationStatus { OK, FAIL, NOTAUTH, USERNOTEXIST, INVPARAM };



        [ResponseType(typeof(bool))]
        [HttpGet]
        public IHttpActionResult DeActivate(string hash, long UserID)
        {
            ObjectParameter success = new ObjectParameter("success", typeof(int));
            db.deactivateUser(UserID, hash, success);

            if (success == null || (ActivationStatus)success.Value != ActivationStatus.OK)
            {
                switch ((ActivationStatus)success.Value)
                {
                    case ActivationStatus.FAIL:
                        {
                            return setError("General Failure");
                        }
                    case ActivationStatus.INVPARAM:
                        {
                            return setError("Invalid parameters");
                        }
                    case ActivationStatus.NOTAUTH:
                        {
                            return UserNotAuthorized();
                        }
                    case ActivationStatus.USERNOTEXIST:
                        {
                            return setError("User does not exist");
                        }
                    default: break;
                }
            }

            return Ok(true);
        }

        [ResponseType(typeof(bool))]
        [HttpGet]
        public IHttpActionResult Activate(string hash,long UserID)
        {
            ObjectParameter success = new ObjectParameter("success", typeof(int));
            db.activateUser(UserID, hash, success);
            
            if (success == null || (ActivationStatus)success.Value != ActivationStatus.OK )
            {
                switch ((ActivationStatus)success.Value)
                {
                    case ActivationStatus.FAIL:
                        {
                            return setError("General Failure");
                        }
                    case ActivationStatus.INVPARAM:
                        {
                            return setError("Invalid parameters");
                        }
                    case ActivationStatus.NOTAUTH:
                        {
                            return UserNotAuthorized();
                        }
                    case ActivationStatus.USERNOTEXIST:
                        {
                            return setError("User does not exist");
                        }
                    default: break;
                }
            }

            return Ok(true);
        }


        [ResponseType(typeof(Users))]
        [HttpGet]
        public IHttpActionResult GetUsers(string hash)
        {
            ObjectParameter count = new ObjectParameter("count", typeof(int));
            List<Users> u = db.getUsers(hash, count).ToList();
            if (u == null || (int)count.Value == -1 )
            {
                return UserNotAuthorized();
            }

            return Ok(u);
        }

        [ResponseType(typeof(string))]
        private IHttpActionResult UserNotAuthorized()
        {
            return Ok (new InvalidUser("User not authorized"));
        }

        [ResponseType(typeof(string))]
        private IHttpActionResult setError(string msg)
        {
            return Ok(new InvalidUser(msg));
        }

        [ResponseType(typeof(string))]
        private IHttpActionResult UnknownError()
        {
            return Ok(new InvalidUser("Unknown error"));
        }

        [ResponseType(typeof(User))]
        [HttpGet]
        public IHttpActionResult GetUser(string hash, long UserID)
        {
            User u = db.getUser(hash,UserID).SingleOrDefault();
            if ( u == null || u.UserID == -1 )
            {
                return UserNotAuthorized();
            }

            return Ok(u);
        }
        [ResponseType(typeof(MyUserDetails))]
        [HttpGet]
        public IHttpActionResult GetMyDetails(string hash)
        {
            ObjectParameter op = new ObjectParameter("ValidUser", typeof(int));
            MyUserDetails u = db.getMyUserDetails(hash, op).SingleOrDefault();
            if (u == null || (int)op.Value == 0)
            {
                return UserNotAuthorized();
            }

            return Ok(u);
        }

        [ResponseType(typeof(AddUserResponse))]
        [HttpPost]
        public IHttpActionResult AddUser(AddUser AUser)
        {
            ObjectParameter rc = new ObjectParameter("rc", typeof(int));
            ObjectParameter em = new ObjectParameter("errormessage", typeof(string));

            db.addUser(AUser.Hash, AUser.UserName, AUser.SupplierCode, AUser.FullName, AUser.Telephone, AUser.AdminLevel, rc, em);
            AddUserResponse aur = new AddUserResponse();
            aur.ReturnCode = (int)rc.Value;
            aur.appendError((string)em.Value);
            return Ok(aur);
        }


        [ResponseType(typeof(UpdateUserResponse))]
        [HttpPut]
        public IHttpActionResult UpdateUser(UpdateUser uu)
        {
            ObjectParameter rc = new ObjectParameter("rc", typeof(int));
            ObjectParameter em = new ObjectParameter("errormessage", typeof(string));

            db.updateUser(uu.Hash, uu.UserID, uu.UserName, uu.FullName, uu.Telephone, uu.AdminLevel, rc, em);


            UpdateUserResponse uur = new UpdateUserResponse();
            uur.ReturnCode = (int)rc.Value;
            if ( ! String.IsNullOrEmpty((string)em.Value))
            { 
                uur.appendError((string)em.Value);
            }

            return Ok(uur);
        }



    }
}
