// Copyright (c) Carlos Guzmán Álvarez. All rights reserved.
// Licensed under the New BSD License (BSD). See LICENSE file in the project root for full license information.

namespace Conversa.Net.Xmpp.Eventing
{
    using Conversa.Net.Xmpp.Shared;
    using System.Xml.Serialization;

    /// <summary>
    /// User Mood
    /// </summary>
    /// <remarks>
    /// XEP-0107: User Mood
    /// </remarks>
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://jabber.org/protocol/mood")]
    [XmlRootAttribute("mood", Namespace = "http://jabber.org/protocol/mood", IsNullable = false)]
    public sealed class Mood
    {
        /// <remarks/>
        [XmlElementAttribute("afraid", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("amazed", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("amorous", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("angry", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("annoyed", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("anxious", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("aroused", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("ashamed", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("bored", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("brave", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("calm", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("cautious", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("cold", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("confident", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("confused", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("contemplative", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("contented", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("cranky", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("crazy", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("creative", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("curious", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("dejected", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("depressed", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("disappointed", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("disgusted", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("dismayed", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("distracted", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("embarrassed", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("envious", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("excited", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("flirtatious", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("frustrated", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("grumpy", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("guilty", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("happy", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("hopeful", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("hot", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("humbled", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("humiliated", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("hungry", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("hurt", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("impressed", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("in_awe", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("in_love", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("indignant", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("interested", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("intoxicated", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("invincible", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("jealous", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("lonely", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("lucky", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("mean", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("moody", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("nervous", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("neutral", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("offended", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("outraged", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("playful", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("proud", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("relaxed", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("relieved", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("remorseful", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("restless", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("sad", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("sarcastic", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("serious", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("shocked", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("shy", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("sick", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("sleepy", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("spontaneous", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("stressed", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("strong", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("surprised", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("thankful", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("thirsty", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("tired", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("undefined", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("weak", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlElementAttribute("worried", typeof(Empty), Namespace = "http://jabber.org/protocol/mood")]
        [XmlChoiceIdentifierAttribute("MoodType")]
        public Empty Item
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlIgnoreAttribute]
        public MoodType MoodType
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElementAttribute("text")]
        public string Text
        {
            get;
            set;
        }

        public Mood()
        {
        }
    }
}
