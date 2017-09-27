namespace Capstone_CharityDAL.Error
{
    using System;
    using System.IO;

    public class LogError
    {
        public static void Log(Exception e)
        {
            using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Katie Davis\Desktop\FinalCapstone_FosterCharity (2)\Capstone_FosterCharity\LogError DAL\Log.text", true))
            {
                fileWriter.WriteLine(e.Message); //Exception sent to file
            }
        }
    }
}
