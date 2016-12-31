using SimpleNMA.Properties;
using System;
using System.Net;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using SimpleExtension;
using SimpleNMA.Objects.API;

namespace SimpleNMA
{
    public class NMAManager
    {
        #region Gestion du singleton

        /// <summary>
        /// Instance
        /// </summary>
        private static NMAManager _instance;

        /// <summary>
        /// Lock
        /// </summary>
        private static readonly object Padlock = new object();

        /// <summary>
        /// Initialisation d'une instance de la classe <see cref="NMAManager" />.
        /// </summary>
        private NMAManager()
        {
        }

        /// <summary>
        /// Permet de récupérer l'instance courante
        /// </summary>        
        public static NMAManager Instance
        {
            get
            {
                lock (Padlock)
                {
                    if (_instance == null)
                        _instance = new NMAManager();

                    return _instance;
                }
            }
        }

        #endregion

        #region Private Constants

        /// <summary>
        /// The post notification base method
        /// </summary>
        private const string POST_NOTIFICATION_BASE_METHOD = "notify?apikey={0}&application={1}&description={2}&event={3}&priority={4}";

        /// <summary>
        /// The verifiy key base method
        /// </summary>
        private const string VERIFIY_KEY_BASE_METHOD = "verify?apikey={0}";

        /// <summary>
        /// The post notification URL parameter
        /// </summary>
        private const string POST_NOTIFICATION_URL_PARAMETER = "&url={0}";

        /// <summary>
        /// The post notification content type parameter
        /// </summary>
        private const string POST_NOTIFICATION_CONTENT_TYPE_PARAMETER = "&content-type={0}";

        /// <summary>
        /// The post notification provider parameter
        /// </summary>
        private const string POST_NOTIFICATION_PROVIDER_PARAMETER = "&developerkey={0}";

        /// <summary>
        /// The request content type
        /// </summary>
        private const string REQUEST_CONTENT_TYPE = "application/x-www-form-urlencoded";

        /// <summary>
        /// The request post method type
        /// </summary>
        private const string REQUEST_POST_METHOD_TYPE = "POST";

        /// <summary>
        /// The request get method type
        /// </summary>
        private const string REQUEST_GET_METHOD_TYPE = "GET";

        /// <summary>
        /// The default base URL
        /// </summary>
        private const string DEFAULT_BASE_URL = @"http://www.notifymyandroid.com/publicapi/";

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        /// <value>
        /// The API key.
        /// </value
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the developper key.
        /// </summary>
        /// <value>
        /// The developper key.
        /// </value>
        public string DevelopperKey { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow HTML].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow HTML]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowHtml { get; set; }

        #endregion

        #region Private Methods

        /// <summary>
        /// Builds the notification request URL.
        /// </summary>
        /// <param name="notification_">The notification.</param>
        /// <returns></returns>
        private string BuildNotificationRequestUrl(NMANotification pNotification)
        {
            var UrlRequest = DEFAULT_BASE_URL;
            var nmaUrlSb = new StringBuilder(UrlRequest);
            nmaUrlSb.AppendFormat(
                POST_NOTIFICATION_BASE_METHOD,
                HttpUtility.UrlEncode(ApiKey),
                HttpUtility.UrlEncode(ApplicationName),
                HttpUtility.UrlEncode(pNotification.Description),
                HttpUtility.UrlEncode(pNotification.Event),
                ((sbyte)(pNotification.Priority)));

            if (!string.IsNullOrEmpty(pNotification.Url))
                nmaUrlSb.AppendFormat(
                    POST_NOTIFICATION_URL_PARAMETER,
                    HttpUtility.UrlEncode(pNotification.Url));

            if (!string.IsNullOrEmpty(DevelopperKey))
                nmaUrlSb.AppendFormat(
                    POST_NOTIFICATION_PROVIDER_PARAMETER,
                    HttpUtility.UrlEncode(DevelopperKey));

            if (AllowHtml)
                nmaUrlSb.AppendFormat(
                    POST_NOTIFICATION_CONTENT_TYPE_PARAMETER,
                    HttpUtility.UrlEncode("text/html"));

            return nmaUrlSb.ToString();
        }

        /// <summary>
        /// Builds the API verify request URL.
        /// </summary>
        /// <param name="pNotification">The p notification.</param>
        /// <returns></returns>
        private string BuildApiVerifyRequestUrl()
        {
            var UrlRequest = DEFAULT_BASE_URL;
            var nmaUrlSb = new StringBuilder(UrlRequest);
            nmaUrlSb.AppendFormat(
                VERIFIY_KEY_BASE_METHOD,
                HttpUtility.UrlEncode(ApiKey));

            if (!string.IsNullOrEmpty(DevelopperKey))
                nmaUrlSb.AppendFormat(
                    POST_NOTIFICATION_PROVIDER_PARAMETER,
                    HttpUtility.UrlEncode(DevelopperKey));
            return nmaUrlSb.ToString();
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Checks the API key.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SimpleNMAException"></exception>
        public bool CheckApiKey()
        {
            if (string.IsNullOrEmpty(ApiKey))
                throw new SimpleNMAException(Resources.ApiKeyNotSpecified);

            using (var webClient = new WebClient())
            {
                var oResponse = webClient.DownloadString(BuildApiVerifyRequestUrl());
                if (!string.IsNullOrEmpty(oResponse))
                {
                    var oNma = (Nma)oResponse.DeSerializeObject(typeof(Nma));
                    if (oNma != null)
                    {
                        if (oNma.Success != null)
                        {
                            return true;
                        }

                        throw new SimpleNMAException($"{oNma.Error.Code} : {oNma.Error.Text}");
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Sends the specified p notification.
        /// </summary>
        /// <param name="pNotification">The p notification.</param>
        /// <returns></returns>
        /// <exception cref="SimpleNMAException"></exception>
        public bool Send(NMANotification pNotification)
        {
            if (pNotification == null)
                throw new SimpleNMAException(Resources.NotificationNotSpecified);

            if (pNotification.IsValid)
            {
                using (var webClient = new WebClient())
                {
                    var oResponse = webClient.DownloadString(BuildNotificationRequestUrl(pNotification));
                    if (!string.IsNullOrEmpty(oResponse))
                    {
                        var oNma = (Nma)oResponse.DeSerializeObject(typeof(Nma));
                        if (oNma != null)
                        {
                            if (oNma.Success != null)
                            {
                                return true;
                            }

                            throw new SimpleNMAException($"{oNma.Error.Code} : {oNma.Error.Text}");
                        }
                    }
                }
            }
            return false;
        }

        #endregion

    }
}
