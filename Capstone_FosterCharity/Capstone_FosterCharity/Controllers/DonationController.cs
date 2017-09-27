namespace Capstone_FosterCharity.Controllers
{
    using Capstone_CharityDAL;
    using Capstone_CharityDAL.Interfaces;
    using Custom;
    using Error;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ViewModels;

    public class DonationController : Controller
    {

        DonationDataAccessLayer DonationAccess = new DonationDataAccessLayer();

        // GET: Donation
        [HttpGet]
        public ActionResult CreateDonations()
        {
            ActionResult oResponse = null;

            if (Session["Username"] == null) //Guest can not see anything past the homepage
            {
                oResponse = RedirectToAction("Index", "Home");
            }
            else
            {
                DonationVM newVM = new DonationVM(); //Create a new instance of DonationVM
                newVM.Donation.UserID = (long)Session["UserID"]; //Pull UserID from Session to add to list of donations

                oResponse = View(newVM);
            }


            return oResponse;

        }

        [HttpPost]
        public ActionResult CreateDonations(DonationVM iDonation)
        {
            ActionResult oResponse = null; //defining our varivale

            if (Session["Username"] == null) //Guest
            {
                oResponse = RedirectToAction("Index", "Home");
            }

            else  //Users,Power User, Admin
            {
                if (ModelState.IsValid)//If info was entered correctly 
                {
                    try
                    {
                        //set mapping to a variable to be used to convert POtoDO
                        IDonationDO DonationForm = DonationMap.MapPOtoDO(iDonation.Donation);
                        //Use the method from DAL to preform action
                        DonationAccess.CreateDonation(DonationForm);
                        //Redirect to view using UserID from database
                        oResponse = RedirectToAction("ViewDonationsbyUserID", "User", new { UserID = (long)Session["UserID"] });
                    }
                    catch (Exception e) //If problem with sql connection 
                    {
                        iDonation.ErrorMessage = "We are sorry, We cannot process your request at this time";
                        ErrorLog.LogError(e); //Log to file
                        oResponse = View(iDonation); //return view
                    }
                    finally
                    {
                        //Onshore standards
                    }
                }
                else //If info was incorrect 
                {
                    oResponse = View(iDonation);
                }
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult ViewDonations()
        {
            ActionResult oResponse = null;

            DonationVM newVM = new DonationVM();

            if (Session["Username"] == null || (Int16)Session["Role"] != 1)
            {
                //Power User & User are redirected
                oResponse = RedirectToAction("Index", "Home");
            }
            else
            {
                try
                {
                    //Call method from DAL to be put into variable 
                    List<IDonationDO> donationInfo = DonationAccess.ViewAllDonations();
                    //put mapping into a variable
                    newVM.DonationList = DonationMap.MapDOtoPO(donationInfo);
                    //Return view
                    oResponse = View(newVM);
                }
                catch (Exception e)
                {
                    newVM.ErrorMessage = "Sorry, We couldn't obtain the list of donations list";
                    ErrorLog.LogError(e); //log error
                }
                finally
                {
                    //Onshore standards
                }
            }

            return oResponse;
        }

        [HttpGet]
        public ActionResult UpdateDonation(long DonationID)
        {
            ActionResult oResponse = null;

            if (Session["Username"] == null || (Int16)Session["Role"] != 1)
            {
                //Power User & User redirected
                oResponse = RedirectToAction("Index", "Home");
            }
            else //Admin
            {
                //Create a new instance of the object
                DonationVM newVM = new DonationVM();
                //set the method to a variable to be used
                IDonationDO donation = DonationAccess.ViewDonationsByID(DonationID);
                //set mapping to a variable
                newVM.Donation = DonationMap.MapDOtoPO(donation);
                //return view
                oResponse = View(newVM);
            }

            return oResponse;
        }

        [HttpPost]
        public ActionResult UpdateDonation(DonationVM iDonation)
        {
            ActionResult oResponse = null;

            if (Session["Username"] == null || (Int16)Session["Role"] != 1)
            {
                //Power User & User redirected
                oResponse = RedirectToAction("Index", "Home");
            }
            else //Admin
            {
                if (ModelState.IsValid) //Info correct
                {
                    try
                    {
                        //put mapping into a variable
                        IDonationDO update = DonationMap.MapPOtoDO(iDonation.Donation);
                        //Use of method from DAL
                        DonationAccess.UpdateDonation(update);
                        //Return the list of donations
                        oResponse = RedirectToAction("ViewDonations", "Donation");
                    }
                    catch (Exception e)
                    {
                        iDonation.ErrorMessage = "Sorry we are unable to handle your request";
                        ErrorLog.LogError(e); //Log into file
                        oResponse = RedirectToAction("UpdateDonation", "Donation");
                    }
                    finally
                    {
                        //Onshore standards
                    }
                }
                else //Info incorrect
                {
                    oResponse = View(iDonation);
                }
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult DeleteDonation(long DonationID)
        {
            ActionResult oResponse = null;
            if (Session["Username"] == null || (Int16)Session["Role"] != 1)
            {
                //Power User & User redirected
                oResponse = RedirectToAction("Index", "Home");
            }
            else //Admin
            {
                try
                {
                    //Pull method from DAL
                    DonationAccess.DeleteDonation(DonationID);
                }
                catch (Exception e) //bad connection to sql
                {
                    //new instance of VM
                    DonationVM newVM = new DonationVM();
                    newVM.ErrorMessage = "Sorry we could not process your request at this time";
                    ErrorLog.LogError(e);
                }
                finally
                {
                    //Onshore standards
                }
            }
            return RedirectToAction("ViewDonations", "Donation");
        }
    }
}