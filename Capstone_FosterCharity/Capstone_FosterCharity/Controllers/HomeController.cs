namespace Capstone_FosterCharity.Controllers
{
    using Capstone_CharityBLL;
    using Capstone_CharityBLL.Interfaces;
    using Capstone_CharityDAL;
    using Capstone_CharityDAL.Interfaces;
    using Capstone_FosterCharity.Custom;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        DonationBusinessLogic DonationAverage = new DonationBusinessLogic(); //use of BLL
        DonationDataAccessLayer DonationAccess = new DonationDataAccessLayer();//use of DAL
        ItemDataAccessLayer ItemAccess = new ItemDataAccessLayer(); //use of DAL
        ItemBusinessLogicLayer ItemTotal = new ItemBusinessLogicLayer(); //use of BLL

        public ActionResult Index()
        {
            //define variable
            decimal average = 0;
            

            //assigned method into variable
            List<IDonationDO> donationInfo = DonationAccess.ViewAllDonations();
            //assigned mapping into variable
            List<IDonationBO> DonationList = DonationMap.MapDOtoBO(donationInfo);
            //use BLL method assigned into variable
            average = DonationAverage.AverageDonation(DonationList);
            //setting average to a Viewbag to be able to see on view
            ViewBag.AverageDonation = average;

            


            return View();
        }
        public ActionResult Statistics()
        {
            //View full of info
            return View();
        }
    }
}