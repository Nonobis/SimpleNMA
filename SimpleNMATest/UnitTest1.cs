﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleNMA;

namespace SimpleNMATest
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// The valid API key
        /// </summary>
        private string _ValidAPIKey = "0f4dba0bba3f9116c2ffc63064340fb6ca9baba393b727b5";

        /// <summary>
        /// The invalid API key
        /// </summary>
        private string _InvalidApiKey = "0f4dbafsba3f9116c2ffc63064340fb6ca9b42a393b727b5";

        /// <summary>
        /// Checks the API success.
        /// </summary>
        [TestMethod]
        public void CheckApiSuccess()
        {
            try
            {
                NMAManager.Instance.ApiKey = _ValidAPIKey; // Valid Test API
                var bResult = NMAManager.Instance.CheckApiKey();
                Assert.IsTrue(bResult, "API Key Valid");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
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
                NMAManager.Instance.ApiKey = _InvalidApiKey; // Random API for test
                NMAManager.Instance.CheckApiKey();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
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
                NMAManager.Instance.ApiKey = _ValidAPIKey; // Set Valid API
                NMAManager.Instance.ApplicationName = "SimpleNMA"; // Set ApplicationName
                var oNotif = new NMANotification("Test", "Test Notification", PriorityNotification.Normal);
                var bResult = NMAManager.Instance.Send(oNotif);
                Assert.IsTrue(bResult, "Notification Send with Success");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void SendTestNotificationWithUrl()
        {
            try
            {
                NMAManager.Instance.ApiKey = _ValidAPIKey; // Set Valid API
                NMAManager.Instance.ApplicationName = "SimpleNMA"; // Set ApplicationName
                var oNotif = new NMANotification("Test", "Test Notification", "http://www.google.fr", PriorityNotification.Normal);
                var bResult = NMAManager.Instance.Send(oNotif);
                Assert.IsTrue(bResult, "Notification Send with Success");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void SendTestNotificationWithHtml()
        {
            try
            {
                NMAManager.Instance.ApiKey = _ValidAPIKey; // Set Valid API
                NMAManager.Instance.ApplicationName = "SimpleNMA"; // Set ApplicationName
                NMAManager.Instance.AllowHtml = true;

                /* Html Tags Supported
                  * 
                  * <a href="...">, <b>, <big>, <blockquote>, <br>, <cite>
                  * <dfn>, <div align="...">, <em>, <font size="..." color="..." face="...">
                  * <h1>, <h2>, <h3>, <h4>, <h5>, <h6>
                  *  <i>, <p>, <small>, <strike>, <strong>
                  * <sub>, <sup>, <tt>, <u>
                */
                var sHtml = "<cite>Test Notification</cite>";
                var oNotif = new NMANotification("Test", sHtml, "http://www.google.fr", PriorityNotification.Normal);
                var bResult = NMAManager.Instance.Send(oNotif);
                Assert.IsTrue(bResult, "Notification Send with Success");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
