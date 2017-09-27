namespace Capstone_FosterCharity.Error
{
    using System;
    using System.IO;

    public class ErrorLog
    {
        public static void LogError(Exception e)
        {
            using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Katie Davis\Documents\Visual Studio 2015\Projects\Capstone_FosterCharity\ErrorLog\Log.text",true)) 
            {
                fileWriter.WriteLine(e.Message); //Exception sent to file
            }
        }
    }
}