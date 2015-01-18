// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.GeoLocation
{
    using System;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    /// User Location
    /// </summary>
    /// <remarks>
    /// XEP-0080: User Location
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/geoloc")]
    [XmlRootAttribute("geoloc", Namespace = "http://jabber.org/protocol/geoloc", IsNullable = false)]
    public partial class UserLocation
    {
        [XmlElementAttribute("accuracy")]
        public decimal Accuracy
        {
            get;
            set;
        }

        [XmlIgnoreAttribute]
        public bool AccuracySpecified
        {
            get;
            set;
        }

        [XmlElementAttribute("alt")]
        public decimal Alt
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool AltSpecified
        {
            get;
            set;
        }

        [XmlElementAttribute("area")]
        public string Area
        {
            get;
            set;
        }

        [XmlElementAttribute("bearing")]
        public decimal Bearing
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool BearingSpecified
        {
            get;
            set;
        }

        [XmlElementAttribute("building")]
        public string Building
        {
            get;
            set;
        }

        [XmlElementAttribute("country")]
        public string Country
        {
            get;
            set;
        }

        [XmlElementAttribute("countrycode")]
        public string CountryCode
        {
            get;
            set;
        }

        [XmlElementAttribute("datum")]
        public string Datum
        {
            get;
            set;
        }

        [XmlElementAttribute("description")]
        public string Description
        {
            get;
            set;
        }

        [XmlElementAttribute("error")]
        public decimal Error
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool ErrorSpecified
        {
            get;
            set;
        }

        [XmlElementAttribute("floor")]
        public string Floor
        {
            get;
            set;
        }

        [XmlElementAttribute("lat")]
        public decimal Lat
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool LatSpecified
        {
            get;
            set;
        }

        [XmlElementAttribute("locality")]
        public string Locality
        {
            get;
            set;
        }

        [XmlElementAttribute("lon")]
        public decimal Lon
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool LonSpecified
        {
            get;
            set;
        }

        [XmlElementAttribute("postalcode")]
        public string PostalCode
        {
            get;
            set;
        }

        [XmlElementAttribute("region")]
        public string Region
        {
            get;
            set;
        }

        [XmlElementAttribute("room")]
        public string Room
        {
            get;
            set;
        }

        [XmlElementAttribute("speed")]
        public decimal Speed
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool SpeedSpecified
        {
            get;
            set;
        }

        [XmlElementAttribute("street")]
        public string Street
        {
            get;
            set;
        }

        [XmlElementAttribute("text")]
        public string Text
        {
            get;
            set;
        }

        [XmlElementAttribute("timestamp")]
        public DateTime Timestamp
        {
            get;
            set;
        }

        [XmlIgnore]
        public bool TimestampSpecified
        {
            get;
            set;
        }

        [XmlElementAttribute("uri", DataType = "anyURI")]
        public string Uri
        {
            get;
            set;
        }

        [XmlAttributeAttribute("lang", Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Lang
        {
            get;
            set;
        }

        public UserLocation()
        {
        }
    }
}
