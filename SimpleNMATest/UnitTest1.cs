using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleNMA;

namespace SimpleNMATest
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Checks the API success.
        /// </summary>
        [TestMethod]
        public void CheckApiSuccess()
        {
            try
            {
                NMAManager.Instance.ApiKey = ""; // Set Valid API
                var bResult = NMAManager.Instance.CheckApiKey();
                if (bResult)
                {
                    Console.WriteLine("API OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Checks the API fail.
        /// </summary>
        [TestMethod]
        public void CheckApiFail()
        {
            try
            {
                NMAManager.Instance.ApiKey = ""; // Set Invalid API
                var bResult = NMAManager.Instance.CheckApiKey();
                if (bResult)
                {
                    Console.WriteLine("API OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Sends the test notification.
        /// </summary>
        [TestMethod]
        public void SendTestNotification()
        {
            try
            {
                NMAManager.Instance.ApiKey = ""; // Set Valid API
                NMAManager.Instance.ApplicationName = ""; // Set ApplicationName
                var oNotif = new NMANotification("Test", "Test Notification", PriorityNotification.Normal);
                var bResult = NMAManager.Instance.Send(oNotif);
                if (bResult)
                {
                    Console.WriteLine("Notification Send with Success");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
