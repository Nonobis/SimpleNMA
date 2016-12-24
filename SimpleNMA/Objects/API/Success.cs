using System.Xml.Serialization;

namespace SimpleNMA.Objects.API
{
    [XmlRoot(ElementName = "success")]
    public class Success
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the remaining.
        /// </summary>
        /// <value>
        /// The remaining.
        /// </value>
        [XmlAttribute(AttributeName = "remaining")]
        public string Remaining { get; set; }

        /// <summary>
        /// Gets or sets the resettimer.
        /// </summary>
        /// <value>
        /// The resettimer.
        /// </value>
        [XmlAttribute(AttributeName = "resettimer")]
        public string Resettimer { get; set; }
    }
}
