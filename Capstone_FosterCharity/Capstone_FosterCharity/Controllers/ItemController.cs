namespace Capstone_FosterCharity.Controllers
{
    using Capstone_CharityDAL;
    using Capstone_CharityDAL.Interfaces;
    using Capstone_CharityDAL.Models;
    using Custom;
    using Error;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ViewModels;

    public class ItemController : Controller
    {
        ItemDataAccessLayer ItemAccess = new ItemDataAccessLayer(); //create new instance of data access layer

        [HttpGet]
        public ActionResult CreateItem()
        {
            ActionResult oResponse = null;

            if (Session["Username"] == null) //Guest 
            {
                oResponse = RedirectToAction("Index", "Home");
            }
            else //Everyone else
            {
                //creating a new instance of ItemVM
                ItemVM newVM = new ItemVM();
                //using the PO from a user by user ID
                newVM.Item.UserID = (long)Session["UserID"];

                oResponse = View(newVM);
            }
            return oResponse;
        }

        [HttpPost]
        public ActionResult CreateItem(ItemVM iItem)
        {
            ActionResult oResponse = null; //set to null to determine if valid or not
            if (Session["Username"] == null)
            {
                oResponse = RedirectToAction("Index", "Home");
            }
            else
            {
                if (ModelState.IsValid)
                {  //if info correct
                    try
                    {
                        //Mapping into a variable
                        IItemDO Itemform = ItemMap.MapPOtoDO(iItem.Item);
                        //Using the mehtod in DAL to create item
                        ItemAccess.CreateItem(Itemform);
                        //Return the view
                        oResponse = RedirectToAction("ViewItemsByUserID", "User", new { UserID = (long)Session["UserID"] });
                    }
                    catch (Exception e)
                    {
                        iItem.ErrorMessage = "We are sorry, we can not process your request";
                        ErrorLog.LogError(e); //Log error in file
                        oResponse = View(iItem); //return view
                    }

                    finally
                    {
                        //Onshore Standards
                    }
                }
                else //if info incorrect
                {
                    oResponse = View(iItem); //return to view once on
                }
            }
            return oResponse; //return view
        }

        [HttpGet]
        public ActionResult ViewItems()
        {
            ActionResult oResponse = null;
            if (Session["Username"] == null) //Guest
            {
                oResponse = RedirectToAction("Index", "Home");
            }
            else //Everyone else
            {
                //Creating a new instance of VM
                ItemVM newVM = new ItemVM();
                try
                {
                    //Call for method from DAL and set to variable
                    List<IItemDO> itemInfo = ItemAccess.ViewAllItems();
                    //Call mapping and assign in variable
                    newVM.ItemList = ItemMap.MapDOtoPO(itemInfo);
                    //return view
                    oResponse = View(newVM);
                }
                catch (Exception e)
                {
                    newVM.ErrorMessage = "Sorry, We couldn't obtain the list of items avaliable";
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
        public ActionResult UpdateItem(long ItemID)
        {
            ActionResult oResponse = null;
            if (Session["Username"] == null || (Int16)Session["Role"] == 3)
            {    //Guest & User
                oResponse = RedirectToAction("Index", "Home");
            }
            else
            {
                //Create a new instance of the object 
                ItemVM newVM = new ItemVM();
                //set the method to a variable to be used
                IItemDO item = ItemAccess.ViewItemsByID(ItemID);
                //set mapping to a variable 
                newVM.Item = ItemMap.MapDOtoPO(item);
                //return view
                oResponse = View(newVM);
            }
            return oResponse;
        }

        [HttpPost]
        public ActionResult UpdateItem(ItemVM iItem)
        {
            ActionResult oResponse = null;
            if (Session["Username"] == null || (Int16)Session["Role"] == 3)
            {
                //Guest & User
                oResponse = RedirectToAction("Index", "Home");
            }
            else
            {
                if (ModelState.IsValid) //if correct info
                {
                    try
                    {
                        //give mapping a variable to be used
                        IItemDO update = ItemMap.MapPOtoDO(iItem.Item);
                        //Call the method to be used
                        ItemAccess.UpdateItem(update);
                        //Return to the view to see changes
                        oResponse = RedirectToAction("ViewItems", "Item");
                    }
                    catch (Exception e)
                    {           //for the errors thrown below
                        iItem.ErrorMessage = "Sorry your request cannot be processed";
                        ErrorLog.LogError(e);
                        oResponse = RedirectToAction("UpdateItem", "Item");
                    }
                    finally
                    {
                        //Onshore standards
                    }
                }
                else //if it isn't valid information
                {
                    oResponse = View(iItem); //Return to the Update page to fill out info again
                }
            }

            return oResponse;
        }

        [HttpGet]
        public ActionResult DeleteItem(long ItemID)
        {
            if (Session["Username"] == null || (Int16)Session["Role"] == 3)
            {
               //go to the returned redirect
            }
            else
            {
                ItemVM newVM = new ItemVM();

                try
                {
                    ItemAccess.DeleteItem(ItemID);
                }
                catch (Exception e)
                {
                    newVM.ErrorMessage = "Sorry we couldn't handle your request";
                    ErrorLog.LogError(e);
                }
                finally
                {
                    //Onshore standards
                }
            }
            return RedirectToAction("ViewItems", "Item");
        }
    }
}
