using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SimpleNMA.Objects.API
{
    [XmlRoot(ElementName = "nma")]
    public class Nma
    {
        /// <summary>
        /// Gets or sets the success.
        /// </summary>
        /// <value>
        /// The success.
        /// </value>
        [XmlElement(ElementName = "success")]
        public Success Success { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        [XmlElement(ElementName = "error")]
        public Error Error { get; set; }
    }
}
