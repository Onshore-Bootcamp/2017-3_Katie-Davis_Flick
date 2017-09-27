namespace Capstone_FosterCharity.Controllers
{
    using Capstone_CharityDAL;
    using Capstone_CharityDAL.Interfaces;
    using Capstone_CharityDAL.Models;
    using Custom;
    using Error;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ViewModels;

    public class UserController : Controller
    {
        UserDataAccessLayer UserAccess = new UserDataAccessLayer(); //Creating new instance off the DAL

        [HttpGet]
        public ActionResult CreateUser()
        {
            //Creating new instance of UserVM
            UserVM newVM = new UserVM();

            ActionResult oResponse = null;

            if (Session["Username"] == null || (Int16)Session["Role"] != 1)
            {
                //Guest,User,Power User
                oResponse = RedirectToAction("Index", "Home");
            }
            else
            { //Admin
                oResponse = View(newVM);
            }
            return oResponse;
        }

        [HttpPost]
        public ActionResult CreateUser(UserVM iUser)
        {
            ActionResult oResponse = null;

            if (Session["Username"] == null || (Int16)Session["Role"] != 1)
            {
                //Guest,Power User, User
                oResponse = RedirectToAction("Index", "Home");
            }
            else
            { //Admin
                if (ModelState.IsValid) //if info correct
                {
                    try
                    {
                        //Mapping assigned to variable
                        IUserDO Userform = UserMap.MapPOtoDO(iUser.User);
                        //Use of method from DAL
                        UserAccess.CreateUser(Userform);
                        //Redirect to list of users
                        oResponse = RedirectToAction("ViewUsers", "User", new { UserID = Session["UserID"] });
                    }
                    catch (Exception e)
                    {
                        iUser.ErrorMessage = "Sorry we can preform that task at the moment, try again later";
                        ErrorLog.LogError(e);
                        oResponse = View(iUser);
                    }
                    finally
                    {
                        //Onshore standards
                    }
                }
                else //if info incorrect
                {
                    oResponse = View(iUser);
                }
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult ViewUsers()
        {
            ActionResult oResponse = null;
            if (Session["Username"] == null || (Int16)Session["Role"] != 1)
            {
                //Guest, User, Power User
                oResponse = RedirectToAction("Index", "Home");
            }
            else //Admin
            {
                UserVM newVM = new UserVM(); //create new instance
                try
                {
                    //Use of method from DAL assigned in a variable
                    List<IUserDO> userInfo = UserAccess.ViewAllUsers();
                    //Mapping assigned in a variable
                    newVM.UserList = UserMap.MapDOtoPO(userInfo);
                    //return view
                    oResponse = View(newVM);
                }
                catch (Exception e)
                {
                    newVM.ErrorMessage = "Sorry we cannot process your request at this time";
                    ErrorLog.LogError(e);
                    oResponse = View(newVM);

                }
                finally
                {
                    //Onshore standards
                }
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult UpdateUser(long UserID)
        {
            ActionResult oResponse = null;
            if (Session["Username"] == null || (Int16)Session["Role"] == 3)
            {
                //Guest, User
                oResponse = RedirectToAction("Index", "Home");
            }
            else //Admin
            {
                //Create a new instance of the object 
                UserVM newVM = new UserVM();
                //set the method to a variable to be used
                IUserDO user = UserAccess.ViewUsersByID(UserID);
                //set mapping to a variable
                newVM.User = UserMap.MapDOtoPO(user);
                //return view 
                oResponse = View(newVM);
            }
            return oResponse;
        }

        [HttpPost]
        public ActionResult UpdateUser(UserVM iUser)
        {
            ActionResult oResponse = null;
            if (Session["Username"] == null || (Int16)Session["Role"] == 3)
            {
                //Guest, User
                oResponse = RedirectToAction("Index", "Home");
            }
            else //Admin
            {
                if (ModelState.IsValid) //if correct info
                {
                    try
                    {
                        //give mapping a variable to be used
                        IUserDO update = UserMap.MapPOtoDO(iUser.User);
                        //Call the method to be used
                        UserAccess.UpdateUser(update);

                        if ((Int16)Session["Role"] != 1)
                        {
                            //if not admin
                            //redirect to profile
                            oResponse = RedirectToAction("ViewUserbyID", "User", new { UserID = (long)Session["UserID"] }); //Return to the view to see changes
                        }
                        else
                        {
                            //admin views all users
                            oResponse = RedirectToAction("ViewUsers", "User");
                        }
                    }
                    catch (Exception e)
                    {           //for the errors thrown below
                        iUser.ErrorMessage = "Sorry your request cannot be processed";
                        ErrorLog.LogError(e);
                        oResponse = View(iUser);
                    }
                    finally
                    {
                        //Onshore standards
                    }
                }
                else //if it isn't valid information
                {
                    oResponse = View(iUser); //Return to the Update page to fill out info again
                }
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult DeleteUser(long UserID)
        {
            IUserDO user = UserAccess.ViewUsersByID(UserID); //Use of method from DAL assigned to variable  

            ActionResult oResponse = null;
            if (Session["Username"] == null || (Int16)Session["Role"] != 1)
            {
                //Everyone, but admin
                oResponse = RedirectToAction("Index", "Home");
            }
            else if (user.Role != 1) //stops an admin from deleting them self
            {
                UserVM newVM = new UserVM(); //creating a new instance 
                try
                {
                    //Uses method from DAL
                    UserAccess.DeleteUsers(UserID);
                    //Return view to see the change
                    oResponse = RedirectToAction("ViewUsers", "User");
                }
                catch (Exception e)
                {
                    newVM.ErrorMessage = "Sorry we can not process your request at this time";
                    ErrorLog.LogError(e);
                    oResponse = RedirectToAction("ViewUsers", "User");
                }
                finally
                {
                    //Onshore standards
                }
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult Register()
        {
            UserVM newVM = new UserVM(); //create a new instance
            return View(newVM); //return the view
        }

        [HttpPost]
        public ActionResult Register(UserVM iUser)
        {
            ActionResult oResponse = null;

            if (ModelState.IsValid) //if info correct
            {
                try
                {
                    //Maping assigned into a variable
                    IUserDO Userform = UserMap.MapPOtoDO(iUser.User);
                    //Method used from DAL
                    UserAccess.Register(Userform);
                    //Return to login view
                    oResponse = RedirectToAction("Login", "User");
                }
                catch (Exception e)
                {
                    iUser.ErrorMessage = "Sorry we can preform that task at the moment, try again later";
                    ErrorLog.LogError(e);
                    oResponse = View(iUser);
                }
                finally
                {
                    //Onshore standards
                }
            }
            else //if incorrect info
            {
                oResponse = View(iUser);
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult Login()
        {
            LoginVM newVM = new LoginVM(); //create new instance
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM iForm)
        {
            ActionResult oResponse = null;

            if (ModelState.IsValid) //if info is format correct
            {
                //Uses method from DAl assigned in a variable
                UserDO user = UserAccess.GetUserByUserName(iForm.User.Username);

                //finds username and password and makes sure input matches database
                if (user != null && iForm.User.Password.Equals(user.Password))
                {
                    //setting session
                    Session["UserName"] = user.Username;
                    Session["Role"] = user.Role;
                    Session["UserID"] = user.UserID;
                    Session.Timeout = 10;
                    oResponse = RedirectToAction("Index", "Home");
                }
                else
                {
                    iForm.ErrorMessage = "Your username or password is incorrect";
                    oResponse = View(iForm);
                }
            }
            else
            {
                oResponse = View(iForm);
            }
            return oResponse;
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            //return to login view
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public ActionResult ViewItemsByUserID(long UserID)
        {
            ActionResult oResponse = null;
            if (Session["Username"] == null) //Guest
            {
                oResponse = RedirectToAction("Index", "Home");
            }
            else //EVeryone else 
            {
                ItemVM newVM = new ItemVM(); //creating new instance
                try
                {
                    //Uses method from DAL assigned to a variable
                    List<IItemDO> userItemInfo = UserAccess.ViewItemsbyUserID(UserID);
                    //Mapping assigned to a variable
                    newVM.ItemList = ItemMap.MapDOtoPO(userItemInfo);
                    //Return View
                    oResponse = View(newVM);
                }
                catch (Exception e)
                {
                    newVM.ErrorMessage = "Sorry we cannot process your request at this time";
                    ErrorLog.LogError(e);
                    oResponse = View(newVM);
                }
                finally
                {
                    //Onshore standards
                }
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult ViewDonationsbyUserID(long UserID)
        {
            ActionResult oResponse = null;
            if (Session["Username"] == null) //Guest
            {
                oResponse = RedirectToAction("Index", "Home");
            }
            else //Everyone else
            {
                DonationVM newVM = new DonationVM();//Creating new instance
                try
                {
                    //Uses method form DAL then assigns to variable
                    List<IDonationDO> userDonationInfo = UserAccess.ViewDonationsbyUserID(UserID);
                    //Mapping assigned to a variable
                    newVM.DonationList = DonationMap.MapDOtoPO(userDonationInfo);
                    //Return view
                    oResponse = View(newVM);
                }
                catch (Exception e)
                {
                    newVM.ErrorMessage = "Sorry we cannot process your request at this time";
                    ErrorLog.LogError(e);
                    oResponse = View(newVM);
                }
                finally
                {
                    //Onshore standards
                }
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult ViewUserbyID(long UserID)
        {
            ActionResult oResponse = null;
            if (Session["Username"] == null) //Guest
            {
                oResponse = RedirectToAction("Index", "Home");
            }
            else
            {
                UserVM newVM = new UserVM(); //creating new instance
                try
                {
                    //Uses method from DAL then assigns to variable 
                    IUserDO userInfo = UserAccess.ViewUsersByID(UserID);
                    //Mapping assigned to variable
                    newVM.User = UserMap.MapDOtoPO(userInfo);
                    //Return this view
                    oResponse = View(newVM);
                }
                catch (Exception e)
                {
                    newVM.ErrorMessage = "Sorry we cannot process your request at this time";
                    ErrorLog.LogError(e);
                    oResponse = View(newVM);
                }
                finally
                {
                    //Onshore standards
                }
            }
            return oResponse;
        }
    }
}
