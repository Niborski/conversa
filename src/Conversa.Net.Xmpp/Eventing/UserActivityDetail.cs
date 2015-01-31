// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Eventing
{
    using System.Xml.Serialization;
    using Windows.Data.Xml.Dom;

    /// <summary>
    /// User Activity
    /// </summary>
    /// <remarks>
    /// XEP-0108: User Activity
    /// </remarks>
    [XmlTypeAttribute("general", Namespace = "http://jabber.org/protocol/activity")]
    public partial class UserActivityDetail
    {
        [XmlAnyElementAttribute()]
        [XmlElementAttribute("at_the_spa", typeof(XmlElement))]
        [XmlElementAttribute("brushing_teeth", typeof(XmlElement))]
        [XmlElementAttribute("buying_groceries", typeof(XmlElement))]
        [XmlElementAttribute("cleaning", typeof(XmlElement))]
        [XmlElementAttribute("coding", typeof(XmlElement))]
        [XmlElementAttribute("commuting", typeof(XmlElement))]
        [XmlElementAttribute("cooking", typeof(XmlElement))]
        [XmlElementAttribute("cycling", typeof(XmlElement))]
        [XmlElementAttribute("day_off", typeof(XmlElement))]
        [XmlElementAttribute("doing_maintenance", typeof(XmlElement))]
        [XmlElementAttribute("doing_the_dishes", typeof(XmlElement))]
        [XmlElementAttribute("doing_the_laundry", typeof(XmlElement))]
        [XmlElementAttribute("driving", typeof(XmlElement))]
        [XmlElementAttribute("gaming", typeof(XmlElement))]
        [XmlElementAttribute("gardening", typeof(XmlElement))]
        [XmlElementAttribute("getting_a_haircut", typeof(XmlElement))]
        [XmlElementAttribute("going_out", typeof(XmlElement))]
        [XmlElementAttribute("hanging_out", typeof(XmlElement))]
        [XmlElementAttribute("having_a_beer", typeof(XmlElement))]
        [XmlElementAttribute("having_a_snack", typeof(XmlElement))]
        [XmlElementAttribute("having_breakfast", typeof(XmlElement))]
        [XmlElementAttribute("having_coffee", typeof(XmlElement))]
        [XmlElementAttribute("having_dinner", typeof(XmlElement))]
        [XmlElementAttribute("having_lunch", typeof(XmlElement))]
        [XmlElementAttribute("having_tea", typeof(XmlElement))]
        [XmlElementAttribute("hiking", typeof(XmlElement))]
        [XmlElementAttribute("in_a_car", typeof(XmlElement))]
        [XmlElementAttribute("in_a_meeting", typeof(XmlElement))]
        [XmlElementAttribute("in_real_life", typeof(XmlElement))]
        [XmlElementAttribute("jogging", typeof(XmlElement))]
        [XmlElementAttribute("on_a_bus", typeof(XmlElement))]
        [XmlElementAttribute("on_a_plane", typeof(XmlElement))]
        [XmlElementAttribute("on_a_train", typeof(XmlElement))]
        [XmlElementAttribute("on_a_trip", typeof(XmlElement))]
        [XmlElementAttribute("on_the_phone", typeof(XmlElement))]
        [XmlElementAttribute("on_vacation", typeof(XmlElement))]
        [XmlElementAttribute("other", typeof(XmlElement))]
        [XmlElementAttribute("partying", typeof(XmlElement))]
        [XmlElementAttribute("playing_sports", typeof(XmlElement))]
        [XmlElementAttribute("reading", typeof(XmlElement))]
        [XmlElementAttribute("rehearsing", typeof(XmlElement))]
        [XmlElementAttribute("running", typeof(XmlElement))]
        [XmlElementAttribute("running_an_errand", typeof(XmlElement))]
        [XmlElementAttribute("scheduled_holiday", typeof(XmlElement))]
        [XmlElementAttribute("shaving", typeof(XmlElement))]
        [XmlElementAttribute("shopping", typeof(XmlElement))]
        [XmlElementAttribute("skiing", typeof(XmlElement))]
        [XmlElementAttribute("sleeping", typeof(XmlElement))]
        [XmlElementAttribute("socializing", typeof(XmlElement))]
        [XmlElementAttribute("studying", typeof(XmlElement))]
        [XmlElementAttribute("sunbathing", typeof(XmlElement))]
        [XmlElementAttribute("swimming", typeof(XmlElement))]
        [XmlElementAttribute("taking_a_bath", typeof(XmlElement))]
        [XmlElementAttribute("taking_a_shower", typeof(XmlElement))]
        [XmlElementAttribute("walking", typeof(XmlElement))]
        [XmlElementAttribute("walking_the_dog", typeof(XmlElement))]
        [XmlElementAttribute("watching_a_movie", typeof(XmlElement))]
        [XmlElementAttribute("watching_tv", typeof(XmlElement))]
        [XmlElementAttribute("working_out", typeof(XmlElement))]
        [XmlElementAttribute("writing", typeof(XmlElement))]
        [XmlChoiceIdentifierAttribute("Type")]
        public XmlElement Item
        {
            get;
            set;
        }

        [XmlIgnore]
        public UserActivityDetailType Type
        {
            get;
            set;
        }

        public UserActivityDetail()
        {
        }
    }
}
