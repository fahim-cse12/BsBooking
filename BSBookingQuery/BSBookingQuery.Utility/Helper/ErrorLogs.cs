namespace BSBookingQuery.Utility.Helper
{
    public class ErrorLogs
    {
        public static void PrintError(string className
         , string methodName
         , string msg)
        {
            string layerName = "BSBookingQuery.BL";
            Error.PrintError(layerName
                , className
                , methodName
                , msg);
        }
    }
}
