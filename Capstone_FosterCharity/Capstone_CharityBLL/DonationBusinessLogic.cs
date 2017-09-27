namespace Capstone_CharityBLL
{
    using Capstone_CharityBLL.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DonationBusinessLogic
    {
        public decimal AverageDonation(List<IDonationBO> DonationList)
        {
            //define variables
            decimal average = 0;
            decimal sum = 0;

            if (DonationList == null || DonationList.Count == 0)
            {

            }
            else
            {
                try
                {
                    foreach (IDonationBO num in DonationList) //goes through IDonationBO's 
                    {
                        sum = sum + num.Amount;
                    }
                    average = sum / DonationList.Count;
                }
                catch (Exception e)
                {
                    throw (e);
                }
                finally
                {
                    //Onshore standards
                }
            }
            return average;
        }
    }
}
