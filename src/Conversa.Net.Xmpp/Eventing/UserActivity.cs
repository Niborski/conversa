// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Eventing
{
    using System.Xml.Serialization;

    /// <summary>
    /// User Activity
    /// </summary>
    /// <remarks>
    /// XEP-0108: User Activity
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/activity")]
    [XmlRootAttribute("activity", Namespace = "http://jabber.org/protocol/activity", IsNullable = false)]
    public partial class UserActivity
    {
        [XmlElementAttribute("doing_chores", typeof(UserActivityDetail))]
        [XmlElementAttribute("drinking", typeof(UserActivityDetail))]
        [XmlElementAttribute("eating", typeof(UserActivityDetail))]
        [XmlElementAttribute("exercising", typeof(UserActivityDetail))]
        [XmlElementAttribute("grooming", typeof(UserActivityDetail))]
        [XmlElementAttribute("having_appointment", typeof(UserActivityDetail))]
        [XmlElementAttribute("inactive", typeof(UserActivityDetail))]
        [XmlElementAttribute("relaxing", typeof(UserActivityDetail))]
        [XmlElementAttribute("talking", typeof(UserActivityDetail))]
        [XmlElementAttribute("traveling", typeof(UserActivityDetail))]
        [XmlElementAttribute("working", typeof(UserActivityDetail))]
        [XmlChoiceIdentifierAttribute("Type")]
        public UserActivityDetail Item
        {
            get;
            set;
        }

        [XmlIgnore]
        public UserActivityType Type
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

        public UserActivity()
        {
        }
    }
}
