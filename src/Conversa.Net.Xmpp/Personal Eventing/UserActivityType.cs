// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.PersonalEventing
{
    using System.Xml.Serialization;

    /// <summary>
    /// User Activity
    /// </summary>
    /// <remarks>
    /// XEP-0108: User Activity
    /// </remarks>
    [XmlTypeAttribute(Namespace = "http://jabber.org/protocol/activity", IncludeInSchema = false)]
    public enum UserActivityType
    {
        /// <remarks/>
        [XmlEnumAttribute("doing_chores")]
        DoingChores,

        /// <remarks/>
        [XmlEnumAttribute("drinking")]
        Drinking,

        /// <remarks/>
        [XmlEnumAttribute("eating")]
        Eating,

        /// <remarks/>
        [XmlEnumAttribute("exercising")]
        Exercising,

        /// <remarks/>
        [XmlEnumAttribute("grooming")]
        Grooming,

        /// <remarks/>
        [XmlEnumAttribute("having_appointment")]
        HavingAppointment,

        /// <remarks/>
        [XmlEnumAttribute("inactive")]
        Inactive,

        /// <remarks/>
        [XmlEnumAttribute("relaxing")]
        Relaxing,

        /// <remarks/>
        [XmlEnumAttribute("talking")]
        Talking,

        /// <remarks/>
        [XmlEnumAttribute("traveling")]
        Traveling,

        /// <remarks/>
        [XmlEnumAttribute("working")]
        Working,
    }
}
