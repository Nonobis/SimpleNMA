using SimpleNMA.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNMA
{
    public class NMANotification
    {
        #region Property

        /// <summary>
        /// Gets or sets the event.
        /// </summary>
        /// <value>
        /// The event.
        /// </value>
        public string Event { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        public PriorityNotification Priority { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        /// <exception cref="SimpleNMAException">
        /// </exception>
        public bool IsValid
        {
            get
            {
                if (String.IsNullOrEmpty(Description))
                    throw new SimpleNMAException(Resources.DescriptionNotProvided);

                if (Description.Length > DESCRIPTION_MAX_LENGTH)
                    throw new SimpleNMAException(String.Format(Resources.DescriptionTooLong, DESCRIPTION_MAX_LENGTH));

                if (String.IsNullOrEmpty(Event))
                    throw new SimpleNMAException(Resources.EventNotProvided);

                if (Event.Length > EVENT_MAX_LENGTH)
                    throw new SimpleNMAException(string.Format(Resources.EventTooLong, EVENT_MAX_LENGTH));

                if (!String.IsNullOrEmpty(Url) && (Url.Length > URL_MAX_LENGTH))
                    throw new SimpleNMAException(string.Format(Resources.UrlTooLong, URL_MAX_LENGTH));

                return true;
            }
        }
        #endregion

        #region Private Constants

        /// <summary>
        /// The description maximum length
        /// </summary>
        private const int DESCRIPTION_MAX_LENGTH = 10000;

        /// <summary>
        /// The event maximum length
        /// </summary>
        private const int EVENT_MAX_LENGTH = 1000;

        /// <summary>
        /// The URL maximum length
        /// </summary>
        private const int URL_MAX_LENGTH = 1000;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NMANotification"/> class.
        /// </summary>
        public NMANotification()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NMANotification"/> class.
        /// </summary>
        /// <param name="pEvent">The p event.</param>
        /// <param name="pDescription">The p description.</param>
        /// <param name="pUrl">The p URL.</param>
        /// <param name="pPriority">The p priority.</param>
        public NMANotification(string pEvent, string pDescription, string pUrl, PriorityNotification pPriority)
        {
            Event = pEvent;
            Description = pDescription;
            Url = pUrl;
            Priority = pPriority;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NMANotification"/> class.
        /// </summary>
        /// <param name="pEvent">The p event.</param>
        /// <param name="pDescription">The p description.</param>
        /// <param name="pPriority">The p priority.</param>
        public NMANotification(string pEvent, string pDescription, PriorityNotification pPriority)
        {
            Event = pEvent;
            Description = pDescription;
            Priority = pPriority;
        }

        #endregion
    }
}
