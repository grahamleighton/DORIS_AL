using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DORIS_AL.Controllers;
using DORIS_AL.Models;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Collections.Generic;

namespace DORIS_AL_Test
{
    [TestClass]
    public class UnitTest1
    {
        private static LoginResponse _loginResponse;
        private static List<Users> userList = new List<Users>();
        private static List<Users> adminList = new List<Users>();
        private static AddUser AddedUser;

        private enum _adminlevel {  SystemAdmin=0,SupplierAdmin=1,User=2,ReadOnly=3,DDAdmin=4 };

        private void ClearTestData()
        {
            TestController tc = new TestController();
            IHttpActionResult resp = tc.ClearTestData("qzcaCh.z");

            Assert.AreNotEqual(resp, null);

            Assert.IsInstanceOfType(resp, typeof(OkNegotiatedContentResult<bool>));

            var lresp = resp as OkNegotiatedContentResult<bool>;
            Assert.AreNotEqual(lresp, null);
            bool res = lresp.Content;
            Console.WriteLine("Clearing test data");
            Assert.AreEqual(res, true);

        }

        [TestInitialize]
        public void Init()
        {

        }

        [TestMethod]
        public void aa_aa_Reset()
        {
            Console.WriteLine("Clearing test data");
            ClearTestData();
        }
        public void SetUp()
        {
//            _loginResponse.Token = "";
        //    Console.WriteLine("Setup() : hash cleared");
        }
        [TestMethod]
        public void ee_CheckUserCounts()
        {
            // login as admin and populate users
            ab_Login_Valid();
            Assert.AreNotEqual(_loginResponse.Token, "0");

            // valid sysadin login , get all users

            ac_GetAllUsers();
            Assert.AreNotEqual(userList, null );
            Assert.AreNotEqual(userList.Count, 0);

            // copy list to the admin list
            foreach ( Users u in userList )
            {
                Users nu = new Users();
                nu = u;
                adminList.Add(nu);
            }
            List<string> suppList = new List<string>();

            // create list of suppliers
            foreach ( Users u in adminList )
            {

            }


        }
        [TestMethod]
        public void aa_Login_InValid()
        {
            LoginResponse lr = LoginUser("sysadmin2@fgh-uk.com", "B4rc0de3", "T132");

                    
            Assert.AreNotEqual(lr, null);

            Assert.AreEqual(lr.Token, "0");


        }
        [TestMethod]
        public void ab_Login_Valid()
        {
            // log in a valid user , sysadmin2@fgh-uk.com

            LoginResponse lr = LoginUser("sysadmin2@fgh-uk.com", "B4rc0de2", "0000");
            Assert.AreNotEqual(lr, null);

            _loginResponse = lr;
            Assert.AreNotEqual(lr.Token, "0");


        }

        public LoginResponse LoginUser( string UserName , string Password , string SupplierCode)
        {
            /*
             * Log in with a valid user 
             * 
             */
            SetUp();

            LoginRequest lr = new LoginRequest();

            Assert.AreNotEqual(lr, null);

            lr.IPAddress = "0.0.0.0";
            lr.UserID = UserName;
            lr.Supplier = SupplierCode;
            lr.Password = Password ;
            LoginController lc = new LoginController();

            Assert.AreNotEqual(lc, null);

            Console.WriteLine("Logging in as " + lr._display());

            IHttpActionResult resp = lc.LoginUser(lr);

            Assert.AreNotEqual(resp, null);

            Assert.IsInstanceOfType(resp, typeof(OkNegotiatedContentResult<LoginResponse>));

            var lresp = resp as OkNegotiatedContentResult<LoginResponse>;
            Assert.AreNotEqual(lresp, null);

            LoginResponse rsp = lresp.Content;
            Assert.AreNotEqual(rsp, null);

          
            Console.WriteLine("Logging response = " + rsp._display());

            // user logged in save hash

            return rsp;
        }


        [TestMethod]
        public void ac_GetAllUsers()
        {
            if (String.IsNullOrEmpty(_loginResponse.Token))
                ab_Login_Valid();   // call the valid login test

            Assert.AreNotEqual(_loginResponse.Token, "");

            // get list of all users using hash

            UserController uc = new UserController();
            Assert.AreNotEqual(uc, null);
            Console.WriteLine("requesting all users with hash {" + _loginResponse.Token + "]");

            IHttpActionResult resp = uc.GetUsers(_loginResponse.Token, "");
            Assert.AreNotEqual(resp, null);

            Assert.IsInstanceOfType(resp, typeof(OkNegotiatedContentResult<List<Users>>));

            var lresp = resp as OkNegotiatedContentResult<List<Users>>;
            Assert.AreNotEqual(lresp, null);

            userList = lresp.Content;

            Assert.AreNotEqual(userList, null);    //checked returned an object
            Assert.AreNotEqual(userList.Count, 0);  // check at least one in the list

            Console.WriteLine(String.Format("Users list returned {0} users", userList.Count));



        }

        public User GetUser( long UserID)
        {
            User u = new User();

            Assert.AreNotEqual(UserID, 0);

            Assert.AreNotEqual(_loginResponse.Token, "");



            return u;
        }


        [TestMethod]
        public void bb_User_AddUser()
        {
            AddAUser();
        }

        [TestMethod]
        public void bc_User_AddExistingUser()
        {

            Console.WriteLine("Re-adding user { " + AddedUser._display() + " }");


            UserController uc = new UserController();
            Assert.AreNotEqual(uc, null);
            IHttpActionResult resp = uc.AddUser(AddedUser);
            Assert.AreNotEqual(resp, null);

            AddUserResponse aur = new AddUserResponse();
            Assert.AreNotEqual(aur, null);

            Assert.IsInstanceOfType(resp, typeof(OkNegotiatedContentResult<AddUserResponse>));

            var lresp = resp as OkNegotiatedContentResult<AddUserResponse>;
            Assert.AreNotEqual(lresp, null);

            aur = lresp.Content;

            Assert.AreNotEqual(aur, null);    //checked returned an object

            Console.WriteLine("Response : { " + aur._display() + " }");
            Console.WriteLine(String.Format("Result of add is {0}", aur.ReturnCode));
            if (aur.ErrorMessage.Count > 0)
            {
                foreach (string msg in aur.ErrorMessage)
                {
                    Console.WriteLine(msg);
                }
            }
            Assert.AreNotEqual(aur.ReturnCode, 0);
        }

        [TestMethod]
        public void bd_User_AddUserWithInvalidHash()
        {
            AddUser _AddedUser = new AddUser();
            _AddedUser = AddedUser;
            _AddedUser.Hash = "32781462738648";

            Console.WriteLine("Re-adding user { " + _AddedUser._display() + " } with incorrect hash");

             

            UserController uc = new UserController();
            Assert.AreNotEqual(uc, null);
            IHttpActionResult resp = uc.AddUser(_AddedUser);
            Assert.AreNotEqual(resp, null);

            AddUserResponse aur = new AddUserResponse();
            Assert.AreNotEqual(aur, null);

            Assert.IsInstanceOfType(resp, typeof(OkNegotiatedContentResult<AddUserResponse>));

            var lresp = resp as OkNegotiatedContentResult<AddUserResponse>;
            Assert.AreNotEqual(lresp, null);

            aur = lresp.Content;

            Assert.AreNotEqual(aur, null);    //checked returned an object

            Console.WriteLine("Response : { " + aur._display() + " }");
            Console.WriteLine(String.Format("Result of add is {0}", aur.ReturnCode));
            if (aur.ErrorMessage.Count > 0)
            {
                foreach (string msg in aur.ErrorMessage)
                {
                    Console.WriteLine(msg);
                }
            }
            Assert.AreNotEqual(aur.ReturnCode, 0);

        }

        public void AddAUser()
        {
            /* 
             * Add a new user 
             */ 
            if ( userList == null || String.IsNullOrEmpty(_loginResponse.Token) )
            {
                ab_Login_Valid();
                ac_GetAllUsers();
            }
            Assert.AreNotEqual(userList.Count, 0);
            if ( String.IsNullOrEmpty(_loginResponse.Token))
            {
                Console.WriteLine("hash is missing");
                Assert.AreNotEqual(_loginResponse.Token, "");
                return;
            }
            // ok find a user we can add

            string baseUserName = "test.user";

            string fullName = "";
            int start = 1;
            bool Added = false;
            while ( ! Added )
            { 
                fullName = baseUserName + Convert.ToString(start) +  "@fgh-uk.com";
                Users foundUser  = userList.Find(m => m.UserName == fullName);
                if (foundUser == null)
                {
                    Added = true;
                    Console.WriteLine(fullName + " is a free user account");
                    AddedUser  = new AddUser();
                    AddedUser.AdminLevel = 1;
                    AddedUser.FullName = "mvct " + fullName;
                    AddedUser.SupplierCode = "D048";
                    AddedUser.UserName = fullName;
                    AddedUser.Telephone = "01274888888";
                    AddedUser.Hash = _loginResponse.Token;

                    Console.WriteLine("New user { " + AddedUser._display() + " }");


                    UserController uc = new UserController();
                    Assert.AreNotEqual(uc, null);
                    IHttpActionResult resp = uc.AddUser(AddedUser);
                    Assert.AreNotEqual(resp, null);

                    AddUserResponse aur = new AddUserResponse();
                    Assert.AreNotEqual(aur, null);

                    Assert.IsInstanceOfType(resp, typeof(OkNegotiatedContentResult<AddUserResponse>));

                    var lresp = resp as OkNegotiatedContentResult<AddUserResponse>;
                    Assert.AreNotEqual(lresp, null);

                    aur = lresp.Content;

                    Assert.AreNotEqual(aur, null);    //checked returned an object

                    Console.WriteLine("Response : { " + aur._display() + " }" );
                    Console.WriteLine(String.Format("Result of add is {0}", aur.ReturnCode));
                    if ( aur.ErrorMessage.Count > 0  )
                    {
                        foreach ( string msg in aur.ErrorMessage )
                        {
                            Console.WriteLine(msg);
                        }
                    }
                    Assert.AreEqual(aur.ReturnCode, 0);
                    


                }
                start++;

            }
        }


        public long CreateUser(string UserName , string Password , string FullName , string Telephone, string Supplier , string hash , int AdminLevel, bool Fail = true  )
        {
            /* 
             * Add a new user 
             */


            if ( String.IsNullOrEmpty(UserName))
            {
                Assert.Fail("username parameter is missing");
            }
            if (String.IsNullOrEmpty(Supplier))
            {
                Assert.Fail("supplier parameter is missing");
            }
            if ( String.IsNullOrEmpty(hash))
            {
                Console.WriteLine("Caution : hash is empty on CreateUser()");
            }

            AddedUser = new AddUser();
            AddedUser.AdminLevel = AdminLevel;
            AddedUser.FullName = FullName;
            AddedUser.SupplierCode = Supplier;
            AddedUser.UserName = UserName;
            AddedUser.Telephone = Telephone;
            AddedUser.Hash = hash;

            Console.WriteLine("New user { " + AddedUser._display() + " }");


            UserController uc = new UserController();
            Assert.AreNotEqual(uc, null);
            IHttpActionResult resp = uc.AddUser(AddedUser);
            Assert.AreNotEqual(resp, null);

            AddUserResponse aur = new AddUserResponse();
            Assert.AreNotEqual(aur, null);

            Assert.IsInstanceOfType(resp, typeof(OkNegotiatedContentResult<AddUserResponse>));

            var lresp = resp as OkNegotiatedContentResult<AddUserResponse>;
            Assert.AreNotEqual(lresp, null);

            aur = lresp.Content;

            Assert.AreNotEqual(aur, null);    //checked returned an object

            Console.WriteLine("Response : { " + aur._display() + " }");
            Console.WriteLine(String.Format("Result of add is {0}", aur.ReturnCode));
            if (aur.ErrorMessage.Count > 0)
            {
                foreach (string msg in aur.ErrorMessage)
                {
                    Console.WriteLine(msg);
                }
            }
            if ( Fail )
                Assert.AreEqual(aur.ReturnCode, 0);


            if (aur.ReturnCode == 0)
            {
                // need to find userid 

                ab_Login_Valid();
                ac_GetAllUsers();
                Assert.AreNotEqual(userList, null);
                Assert.AreNotEqual(userList.Count, 0);
                foreach (Users u in userList)
                {
                    if (u.UserName == AddedUser.UserName)
                    {
                        AddedUser.UserID = u.UserID;
                        Supplier = u.SupplierCode;
                    }
                }

                Console.WriteLine("Setting password to " + Password);

                UserClasses x = new UserClasses();
                x.Hash = _loginResponse.Token;
                x.UserID = (long)AddedUser.UserID;
                x.Password = Password;
                LoginResponse savelr = _loginResponse;
                bool result = SetPassword(x, AddedUser.UserID);
                _loginResponse = savelr;

                Assert.AreEqual(result, true);
            }

            
            return AddedUser.UserID;


        }




        [TestMethod]
        public void cc_LoginSec_SetPasswordAndLogin()
        {
            UserClasses uc = new UserClasses();

            // need to find userid of added user


            Assert.AreNotEqual(AddedUser, null);

            ac_GetAllUsers();

            Assert.AreNotEqual(userList, null);
            Assert.AreNotEqual(userList.Count, 0);
            string Supplier = "";
            foreach ( Users u in userList )
            {
                if ( u.UserName == AddedUser.UserName )
                {
                    AddedUser.UserID = u.UserID;
                    Supplier = u.SupplierCode;
                }
            }

            Assert.AreNotEqual(AddedUser.UserID, 0);



            uc.Hash = _loginResponse.Token;
            uc.UserID = (long)AddedUser.UserID;
            uc.Password = "B4rc0de1";

            bool resp = SetPassword(uc,AddedUser.UserID);

            Console.WriteLine("Password set to [B4rc0de1] for UserID [" + Convert.ToString(uc.UserID) + "] using hash [" + _loginResponse.Token + "]");

            Assert.AreEqual(resp, true);

            LoginResponse lr = LoginUser(AddedUser.UserName, "B4rc0de1", Supplier);
            Assert.AreNotEqual(lr, null);
            Assert.AreNotEqual(lr.Token, "0");
            Console.Write("Logged in Successfully");




        }


        public bool SetPassword(UserClasses uc, long UserID)
        {
            LoginSecController lc = new LoginSecController();

            Assert.AreNotEqual(lc, null);
            Console.WriteLine("Setting Password for user " + Convert.ToString(UserID)  );

            uc.UserID = UserID;

            IHttpActionResult resp = lc.SetPassword(uc);
            Assert.AreNotEqual(resp, null);

            Assert.IsInstanceOfType(resp, typeof(OkNegotiatedContentResult<bool>));

            var lresp = resp as OkNegotiatedContentResult<bool>;
            Assert.AreNotEqual(lresp, null);

            bool passwordSet = false;
            passwordSet = lresp.Content;

            return passwordSet;
        }
        [TestMethod]
        public void ef_LoginAsSupplierAdmin()
        {
            // create a user for supplier L014

            Console.WriteLine(">> Clearing test data");
            ClearTestData();
            Console.WriteLine("------------------------------------------------");

            ab_Login_Valid();
            ac_GetAllUsers();
            long UserID = 0;
            foreach ( Users u in userList )
            {
                if ( u.UserName == "test_l014@l014.co.uk")
                {
                    UserID = u.UserID;
                }
            }

            if (UserID == 0)
            {
                UserID = CreateUser("test_l014@l014.co.uk", "B4rc0de1", "mvct Test L014", "01274 530244", "L014", _loginResponse.Token, (int)_adminlevel.SupplierAdmin);
                Assert.AreNotEqual(0,UserID);
                Assert.AreNotEqual(_loginResponse.Token, "0");
            }

            Console.WriteLine(">> Logging in as newly created supplier admin");
            LoginResponse SA_lr =  LoginUser("test_l014@l014.co.uk", "B4rc0de1", "L014");
            Assert.AreNotEqual("0",SA_lr.Token);
            Console.WriteLine("------------------------------------------------");
            long SA_UserID = UserID;
            // Create some other L014 users
            Console.WriteLine(">> Creating a new user for L014 as a supplier admin");
            long user = CreateUser("test_l014_user@l014.co.uk", "B4rc0de1", "mvct Test User L014", "01274 530244", "L014", SA_lr.Token, (int)_adminlevel.User);
            Assert.AreNotEqual(0,user);
            Console.WriteLine("------------------------------------------------");

            //         _loginResponse = LoginUser("test_l014@l014.co.uk", "B4rc0de1", "L014");

            Console.WriteLine(">. Creating a new supplier admin for L014 as a supplier admin");
            long sa = CreateUser("test_l014_sa@l014.co.uk", "B4rc0de1", "mvct Test SA L014", "01274 530244", "L014", SA_lr.Token, (int)_adminlevel.SupplierAdmin );
            Assert.AreNotEqual(0,sa);
            Console.WriteLine("------------------------------------------------");

            //         _loginResponse = LoginUser("test_l014@l014.co.uk", "B4rc0de1", "L014");

            long sysa = CreateUser("test_l014_sys@l014.co.uk", "B4rc0de1", "mvct Test Sys L014", "01274 530244", "L014", SA_lr.Token, (int)_adminlevel.SystemAdmin,false);

            Assert.AreEqual(0,sysa); // should come back with 0 
            long ID = 0;
            Console.WriteLine(">. Creating a new supplier admin for D048 as a supplier admin for L014");
            ID = CreateUser("test_l015_sa@D048.co.uk", "B4rc0de1", "mvct Test Sa D048", "01274 530244", "D048", SA_lr.Token, (int)_adminlevel.SupplierAdmin, false);
            if (ID > 0)
                Console.WriteLine("**** Allowed creation of supplier admin for a different supplier ****");
            Assert.AreEqual(0, ID); // should come back with 0 
            Console.WriteLine("------------------------------------------------");

            Console.WriteLine(">. Creating a new user for D048 as a supplier admin for L014");
            ID = CreateUser("test_l015_user@D048.co.uk", "B4rc0de1", "mvct Test User D048", "01274 530244", "D048", SA_lr.Token, (int)_adminlevel.User , false);
            if (ID > 0)
                Console.WriteLine("**** Allowed creation of user for a different supplier ****");
            Assert.AreEqual(0, ID); // should come back with 0 
            Console.WriteLine("------------------------------------------------");

            Console.WriteLine(">. Creating a new sa for D048 as a supplier admin for L014");
            ID = CreateUser("test_l015_sa@D048.co.uk", "B4rc0de1", "mvct Test User D048", "01274 530244", "D048", SA_lr.Token, (int)_adminlevel.SystemAdmin, false);
            if (ID > 0)
                Console.WriteLine("**** Allowed creation of supplier admin for a different supplier ****");
            Assert.AreEqual(0, ID); // should come back with 0 
            Console.WriteLine("------------------------------------------------");

            Console.WriteLine(">. Creating a new sa for 0000 as a supplier admin for L104");
            ID = CreateUser("test_l015_sa2@D048.co.uk", "B4rc0de1", "mvct Test User D048", "01274 530244", "0000", SA_lr.Token, (int)_adminlevel.SystemAdmin, false);
            if (ID > 0)
                Console.WriteLine("**** Allowed creation of system admin when signed on as user ****");
            Assert.AreEqual(0, ID); // should come back with 0 
            Console.WriteLine("------------------------------------------------");

            // ok we are logged in as a supplier admin
            Console.WriteLine(">> Checking all users match supplier L014");
            LoginUser("test_l014_sa@l014.co.uk", "B4rc0de1", "L104");
            ac_GetAllUsers();
            Assert.AreNotEqual(userList.Count, 0);
            foreach (Users u in userList)
            {
                if (u.SupplierCode != "L014")
                {
                    Console.WriteLine("User " + u.UserName + "(" + u.SupplierCode + ") is not from L014");
                    Assert.AreNotEqual(u.SupplierCode, "L014");
                }
            }

            Console.WriteLine("------------------------------------------------");


        }

        [TestMethod]
        public void ef_LoginAsUser()
        {

            Console.WriteLine(">> Logging in as newly created user for L014");
            LoginResponse User_lr = LoginUser("test_l014_user@l014.co.uk", "B4rc0de1", "L014");
            if (User_lr.Token == "0")
            {
                Console.WriteLine("cannot log in as user test_l014_user@l104.co.uk");
                Assert.AreEqual("0",User_lr.Token);
            }

            Console.WriteLine("------------------------------------------------");
         //   long SA_UserID = UserID;
            // Create some other L014 users
            Console.WriteLine(">> Creating a new user for L014 ");
            long user = CreateUser("test_l014_user2@l014.co.uk", "B4rc0de1", "mvct Test User L014", "01274 530244", "L014", User_lr.Token, (int)_adminlevel.User,false);
            Assert.AreEqual(0,user);
            Console.WriteLine("------------------------------------------------");

            Console.WriteLine(">> Creating a new user for D048 ");
            user = CreateUser("test_l014_user2@l014.co.uk", "B4rc0de1", "mvct Test User L014", "01274 530244", "D048", User_lr.Token, (int)_adminlevel.User, false);
            Assert.AreEqual(0, user);
            Console.WriteLine("------------------------------------------------");

            //         _loginResponse = LoginUser("test_l014@l014.co.uk", "B4rc0de1", "L014");

            Console.WriteLine(">. Creating a new supplier admin for L014");
            long sa = CreateUser("test_l014_sa2@l014.co.uk", "B4rc0de1", "mvct Test SA L014", "01274 530244", "L014", User_lr.Token, (int)_adminlevel.SupplierAdmin,false);
            Assert.AreEqual(0,sa);
            Console.WriteLine("------------------------------------------------");

            Console.WriteLine(">. Creating a new supplier admin for D048");
            sa = CreateUser("test_l014_sa2@l014.co.uk", "B4rc0de1", "mvct Test SA L014", "01274 530244", "D048", User_lr.Token, (int)_adminlevel.SupplierAdmin, false);
            Assert.AreEqual(0, sa);
            Console.WriteLine("------------------------------------------------");

            //         _loginResponse = LoginUser("test_l014@l014.co.uk", "B4rc0de1", "L014");

            long sysa = CreateUser("test_l014_sys2@l014.co.uk", "B4rc0de1", "mvct Test Sys L014", "01274 530244", "L014", User_lr.Token, (int)_adminlevel.SystemAdmin, false);
            Assert.AreEqual(0,sysa); // should come back with 0 
            sysa = CreateUser("test_l014_sys2@l014.co.uk", "B4rc0de1", "mvct Test Sys L014", "01274 530244", "D048", User_lr.Token, (int)_adminlevel.SystemAdmin, false);
            Assert.AreEqual(0, sysa); // should come back with 0 
            sysa = CreateUser("test_l014_sys2@l014.co.uk", "B4rc0de1", "mvct Test Sys L014", "01274 530244", "0000", User_lr.Token, (int)_adminlevel.SystemAdmin, false);
            Assert.AreEqual(0, sysa); // should come back with 0 

            // ok we are logged in as a supplier admin
            Console.WriteLine(">> Checking all users match supplier L014");
            LoginUser("test_l014_user@l014.co.uk", "B4rc0de1", "L014");
            ac_GetAllUsers();
            Assert.AreNotEqual(userList.Count, 0);
            foreach (Users u in userList)
            {
                if (u.SupplierCode != "L014")
                {
                    Console.WriteLine("User " + u.UserName + "(" + u.SupplierCode + ") is not from L014");
                    Assert.AreNotEqual(u.SupplierCode, "L014");
                }
            }

        }

     
        public void ff_UpdateUser()
        {
            // update the last added user to have a different username

            UpdateUser uu = new UpdateUser();
            // log back on as administrator

            //            ab_Login_Valid();

            LoginResponse sa  = LoginUser("sysadmin2@fgh-uk.com", "B4rc0de1", "0000");
            if (sa.Token == "0")
            {
                Console.WriteLine("Failed to log in as user sysadmin2@fgh-uk.com");
                Assert.AreEqual(sa.Token, "0");
            }
         //   long UserID
            ac_GetAllUsers();
            Assert.AreNotEqual(userList.Count, 0);
            foreach (Users u in userList)
            {
                if (u.UserName == "test_l014_user@l014.co.uk")
                {
                    Console.WriteLine("User " + u.UserName + "(" + u.SupplierCode + ")");
            
                }
            }

            // find user id for test_l014_user@l014.co.uk




            LoginResponse lr = LoginUser("test_l014_user@l014.co.uk", "B4rc0de1", "L014");
            if (lr.Token == "0")
            {
                Console.WriteLine("Failed to log in as user test_l014_user@l104.co.uk");
                Assert.AreEqual(lr.Token, "0");
            }



            uu.Hash = _loginResponse.Token;
            uu.FullName = AddedUser.FullName + " altered";
            uu.Telephone = AddedUser.Telephone;
            uu.UserID = AddedUser.UserID;
            uu.AdminLevel = AddedUser.AdminLevel;
            uu.UserName = AddedUser.UserName;

            Assert.AreNotEqual(uu.Hash, "0");
            Assert.AreNotEqual(uu.Hash, "");
            Assert.AreNotEqual(uu.UserID, 0);

            UserController uc = new UserController();

            Assert.AreNotEqual(uc, null);
            IHttpActionResult resp = uc.UpdateUser(uu);
            Assert.AreNotEqual(resp, null);

            Assert.IsInstanceOfType(resp, typeof(OkNegotiatedContentResult<UpdateUserResponse>));

            var lresp = resp as OkNegotiatedContentResult<UpdateUserResponse>;
            Assert.AreNotEqual(lresp, null);

            UpdateUserResponse uur = lresp.Content;

            Console.WriteLine(String.Format(" Update user return code : {0}", uur.ReturnCode));
            if (uur.ErrorMessage.Count > 0)
            {
                foreach (string msg in uur.ErrorMessage)
                {
                    Console.WriteLine(msg);
                }
            }
            Assert.AreEqual(uur.ReturnCode, 0);
            

        }


    }
}
