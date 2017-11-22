using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DORIS_AL.Models
{
    public class BaseAccess
    {
        [Required]
        public string Hash { get; set; }
    }
    public class UserAccess : BaseAccess
    { 
        public long UserID { get; set; }
    }
    public class UserClasses : UserAccess
    {
        public string Password { get; set; } 
    }

    public class CheckSecurity : UserAccess
    {
        public string Answer { get; set; }
    }

    public class SetPasswordAndSecurity : UserClasses
    {
        public long SecurityQuestionID { get; set; }
        public string SecurityAnswer { get; set; }

    }

    public class RecoveryUser : UserAccess
    {
        public string UserName { get; set;  }
        public string SupplierCode { get; set; }
    }
    public class RecoveryUserResponse 
    {
        public long UserID { get; set;  }
      
    }
    public class RecoveryEmailResponse 
    {
        public string Body { get; set; }
      
        public string Subject { get; set; }
        public string EmailAddress { get; set; }
    }
    public class LoginRequest
    {
        [Required]
        [MaxLength(250)]
        [RegularExpression(@"^[^\s\,]*$")]
        public string UserID { get; set; }
        public string Password { get; set; }
        [RegularExpression(@"^[^\s\,]*$")]
        public string Supplier { get; set; }
        public string IPAddress { get; set; }
    }

    public class LoginResponse : LoginRequest
    {
        public string Token { get; set; }
        public string Change { get; set; }
        public void CopyRequest(LoginRequest r)
        {
            this.UserID = r.UserID;
            this.Password = r.Password;
            this.Supplier = r.Supplier;
            this.IPAddress = r.IPAddress;
            
        }
    }
    public class ErrorMessages
    {
        public List<string> ErrorMessage;
        public ErrorMessages()
        {
            ErrorMessage = new List<string>();
        }
        public void appendError(string msg)
        {
            ErrorMessage.Add(msg);
        }
        public List<string> getMessages()
        {
            return ErrorMessage;
        }

    }

    public class InvalidUser : ErrorMessages
    {

        public InvalidUser(string msg)
        {
            appendError(msg);
        }
    }
    public class SecurityQuestionResponse : ErrorMessages
    {
        public string SecurityQuestion { get; set; }
        public int ReturnCode { get; set; }
    }
    public class UpdateUser : UserAccess
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Telephone { get; set; }
        public int AdminLevel { get; set; }
    }
    public class AddUser : UpdateUser
    {
        [MaxLength(4)]
        public string SupplierCode { get; set; }
    }

    public class AddUserResponse : ErrorMessages
    {
        public int ReturnCode { get; set; }
    }

    public class ValidateRecoveryResponse
    {
        public long UserID { get; set; }
        public int Valid { get; set; }
        public string SupplierCode { get; set; }
        public ValidateRecoveryResponse()
        {
            UserID = 0;
            Valid = 0;
            SupplierCode = "";
        }
    }

  

    public class UpdateUserResponse : ErrorMessages
    {
        public int ReturnCode { get; set;  }
    }

    public class EmailAvailable : BaseAccess
    {
        public string UserName { get; set;  } 
    }
    public class SupplierAvailable : BaseAccess
    {
        public string SupplierCode { get; set; }
    }

    public class PostSupplier : UserAccess
    {
        public long SupplierID { get; set;  }
        public string SupplierCode { get; set;  }
    }
    public class DeleteUserSupplier : UserAccess
    {
        public long SupplierID { get; set; }
    }
    public class DeleteUserSupplierResponse : ErrorMessages
    {
        public int ReturnCode { get; set;  }
    }
    public class PostUserSupplierResponse : ErrorMessages
    {
        public int ReturnCode { get; set; }
    }
    public class UserSupplierList
    {
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public long SupplierID { get; set; }
        public long UserSupplierID { get; set; }
        public void Copy(UserSuppliers us)
        {
            this.SupplierCode = us.SupplierCode;
            this.SupplierName = us.SupplierName;
            this.SupplierID = (long)us.SupplierID;
            this.UserSupplierID = (long)us.UserSupplierID;
        }
    }
    public class AdminLevels
    {
        public int AdminNumber { get; set; }
        public string AdminName { get; set;  }
        public void Copy(getAdminLevelsCreate_Result par)
        {
            this.AdminName = par.AdminLevelName;
            this.AdminNumber = (int)par.AdminLevelNumber;
        }
    }
}