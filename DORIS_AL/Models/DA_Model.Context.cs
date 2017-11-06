﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DORIS_AL.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DDTrack_APIEntities : DbContext
    {
        public DDTrack_APIEntities()
            : base("name=DDTrack_APIEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual int activateUser(Nullable<long> userID, string hash, ObjectParameter success)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            var hashParameter = hash != null ?
                new ObjectParameter("hash", hash) :
                new ObjectParameter("hash", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("activateUser", userIDParameter, hashParameter, success);
        }
    
        public virtual int addUser(string hash, string userName, string supplierCode, string fullName, string telephone, Nullable<int> adminLevel, ObjectParameter rC, ObjectParameter errorMessage)
        {
            var hashParameter = hash != null ?
                new ObjectParameter("hash", hash) :
                new ObjectParameter("hash", typeof(string));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var supplierCodeParameter = supplierCode != null ?
                new ObjectParameter("SupplierCode", supplierCode) :
                new ObjectParameter("SupplierCode", typeof(string));
    
            var fullNameParameter = fullName != null ?
                new ObjectParameter("FullName", fullName) :
                new ObjectParameter("FullName", typeof(string));
    
            var telephoneParameter = telephone != null ?
                new ObjectParameter("Telephone", telephone) :
                new ObjectParameter("Telephone", typeof(string));
    
            var adminLevelParameter = adminLevel.HasValue ?
                new ObjectParameter("AdminLevel", adminLevel) :
                new ObjectParameter("AdminLevel", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("addUser", hashParameter, userNameParameter, supplierCodeParameter, fullNameParameter, telephoneParameter, adminLevelParameter, rC, errorMessage);
        }
    
        public virtual int addUserSupplier(string hash, Nullable<long> userID, Nullable<long> supplierID, string supplierCode, ObjectParameter success)
        {
            var hashParameter = hash != null ?
                new ObjectParameter("hash", hash) :
                new ObjectParameter("hash", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            var supplierIDParameter = supplierID.HasValue ?
                new ObjectParameter("SupplierID", supplierID) :
                new ObjectParameter("SupplierID", typeof(long));
    
            var supplierCodeParameter = supplierCode != null ?
                new ObjectParameter("SupplierCode", supplierCode) :
                new ObjectParameter("SupplierCode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("addUserSupplier", hashParameter, userIDParameter, supplierIDParameter, supplierCodeParameter, success);
        }
    
        public virtual int checkSecurityAnswer(string hash, Nullable<long> userid, string answer, ObjectParameter correct)
        {
            var hashParameter = hash != null ?
                new ObjectParameter("hash", hash) :
                new ObjectParameter("hash", typeof(string));
    
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(long));
    
            var answerParameter = answer != null ?
                new ObjectParameter("answer", answer) :
                new ObjectParameter("answer", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("checkSecurityAnswer", hashParameter, useridParameter, answerParameter, correct);
        }
    
        public virtual int deactivateUser(Nullable<long> userID, string hash, ObjectParameter success)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            var hashParameter = hash != null ?
                new ObjectParameter("hash", hash) :
                new ObjectParameter("hash", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("deactivateUser", userIDParameter, hashParameter, success);
        }
    
        public virtual int deleteUserSupplier(string hash, Nullable<long> userID, Nullable<long> supplierID, ObjectParameter success)
        {
            var hashParameter = hash != null ?
                new ObjectParameter("hash", hash) :
                new ObjectParameter("hash", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            var supplierIDParameter = supplierID.HasValue ?
                new ObjectParameter("SupplierID", supplierID) :
                new ObjectParameter("SupplierID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("deleteUserSupplier", hashParameter, userIDParameter, supplierIDParameter, success);
        }
    
        public virtual ObjectResult<MyUserDetails> getMyUserDetails(string hashValue, ObjectParameter validUser)
        {
            var hashValueParameter = hashValue != null ?
                new ObjectParameter("HashValue", hashValue) :
                new ObjectParameter("HashValue", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MyUserDetails>("getMyUserDetails", hashValueParameter, validUser);
        }
    
        public virtual int getRecoveryEmail(Nullable<long> userid, ObjectParameter body, ObjectParameter subject, ObjectParameter emailaddress)
        {
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("getRecoveryEmail", useridParameter, body, subject, emailaddress);
        }
    
        public virtual ObjectResult<User> getUser(string hash, Nullable<long> userID)
        {
            var hashParameter = hash != null ?
                new ObjectParameter("hash", hash) :
                new ObjectParameter("hash", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<User>("getUser", hashParameter, userIDParameter);
        }
    
        public virtual ObjectResult<Users> getUsers(string hash, ObjectParameter count)
        {
            var hashParameter = hash != null ?
                new ObjectParameter("hash", hash) :
                new ObjectParameter("hash", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Users>("getUsers", hashParameter, count);
        }
    
        public virtual ObjectResult<UserSuppliers> UserSuppliers(string hash, Nullable<long> userID)
        {
            var hashParameter = hash != null ?
                new ObjectParameter("hash", hash) :
                new ObjectParameter("hash", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UserSuppliers>("UserSuppliers", hashParameter, userIDParameter);
        }
    
        public virtual int getWelcomeEmail(Nullable<long> userid, ObjectParameter body, ObjectParameter subject, ObjectParameter emailaddress)
        {
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("getWelcomeEmail", useridParameter, body, subject, emailaddress);
        }
    
        public virtual int isEmailAvailable(string hash, string username, ObjectParameter exists)
        {
            var hashParameter = hash != null ?
                new ObjectParameter("hash", hash) :
                new ObjectParameter("hash", typeof(string));
    
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("isEmailAvailable", hashParameter, usernameParameter, exists);
        }
    
        public virtual int isSupplierAvailable(string hash, string suppliercode, ObjectParameter exists)
        {
            var hashParameter = hash != null ?
                new ObjectParameter("hash", hash) :
                new ObjectParameter("hash", typeof(string));
    
            var suppliercodeParameter = suppliercode != null ?
                new ObjectParameter("suppliercode", suppliercode) :
                new ObjectParameter("suppliercode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("isSupplierAvailable", hashParameter, suppliercodeParameter, exists);
        }
    
        public virtual int loginUser(string userID, string password, string supplier, ObjectParameter token, string ipaddress, ObjectParameter change)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            var supplierParameter = supplier != null ?
                new ObjectParameter("Supplier", supplier) :
                new ObjectParameter("Supplier", typeof(string));
    
            var ipaddressParameter = ipaddress != null ?
                new ObjectParameter("ipaddress", ipaddress) :
                new ObjectParameter("ipaddress", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("loginUser", userIDParameter, passwordParameter, supplierParameter, token, ipaddressParameter, change);
        }
    
        public virtual int setPassword(string hash, Nullable<long> userID, string password, ObjectParameter success)
        {
            var hashParameter = hash != null ?
                new ObjectParameter("hash", hash) :
                new ObjectParameter("hash", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("setPassword", hashParameter, userIDParameter, passwordParameter, success);
        }
    
        public virtual int setPasswordandSecurityQuestion(string hash, Nullable<long> userID, string password, Nullable<long> securityQuestionID, string securityAnswer, ObjectParameter success)
        {
            var hashParameter = hash != null ?
                new ObjectParameter("hash", hash) :
                new ObjectParameter("hash", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var securityQuestionIDParameter = securityQuestionID.HasValue ?
                new ObjectParameter("SecurityQuestionID", securityQuestionID) :
                new ObjectParameter("SecurityQuestionID", typeof(long));
    
            var securityAnswerParameter = securityAnswer != null ?
                new ObjectParameter("SecurityAnswer", securityAnswer) :
                new ObjectParameter("SecurityAnswer", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("setPasswordandSecurityQuestion", hashParameter, userIDParameter, passwordParameter, securityQuestionIDParameter, securityAnswerParameter, success);
        }
    
        public virtual int updateUser(string hash, Nullable<long> userID, string userName, string fullName, string telephone, Nullable<int> adminLevel, ObjectParameter rC, ObjectParameter errorMessage)
        {
            var hashParameter = hash != null ?
                new ObjectParameter("hash", hash) :
                new ObjectParameter("hash", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var fullNameParameter = fullName != null ?
                new ObjectParameter("FullName", fullName) :
                new ObjectParameter("FullName", typeof(string));
    
            var telephoneParameter = telephone != null ?
                new ObjectParameter("Telephone", telephone) :
                new ObjectParameter("Telephone", typeof(string));
    
            var adminLevelParameter = adminLevel.HasValue ?
                new ObjectParameter("AdminLevel", adminLevel) :
                new ObjectParameter("AdminLevel", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("updateUser", hashParameter, userIDParameter, userNameParameter, fullNameParameter, telephoneParameter, adminLevelParameter, rC, errorMessage);
        }
    
        public virtual int validateRecovery(string hash, ObjectParameter userid, ObjectParameter valid, ObjectParameter suppliercode)
        {
            var hashParameter = hash != null ?
                new ObjectParameter("hash", hash) :
                new ObjectParameter("hash", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("validateRecovery", hashParameter, userid, valid, suppliercode);
        }
    
        public virtual int getRecoveryUser(string username, string supplier, ObjectParameter userid)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var supplierParameter = supplier != null ?
                new ObjectParameter("supplier", supplier) :
                new ObjectParameter("supplier", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("getRecoveryUser", usernameParameter, supplierParameter, userid);
        }
    
        public virtual int getSecurityQuestion(Nullable<long> userid, ObjectParameter question)
        {
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("getSecurityQuestion", useridParameter, question);
        }
    }
}
