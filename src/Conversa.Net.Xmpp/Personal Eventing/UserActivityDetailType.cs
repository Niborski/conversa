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
    public enum UserActivityDetailType
    {
        /// <remarks/>
        [XmlEnumAttribute("##any:")]
        Item,

        /// <remarks/>
        [XmlEnumAttribute("at_the_spa")]
        AtTheSpa,

        /// <remarks/>
        [XmlEnumAttribute("brushing_teeth")]
        BrushingTeeth,

        /// <remarks/>
        [XmlEnumAttribute("buying_groceries")]
        BuyingGroceries,

        /// <remarks/>
        [XmlEnumAttribute("cleaning")]
        Cleaning,

        /// <remarks/>
        [XmlEnumAttribute("coding")]
        Coding,

        /// <remarks/>
        [XmlEnumAttribute("commuting")]
        Commuting,

        /// <remarks/>
        [XmlEnumAttribute("cooking")]
        Cooking,

        /// <remarks/>
        [XmlEnumAttribute("cycling")]
        Cycling,

        /// <remarks/>
        [XmlEnumAttribute("day_off")]
        DayOff,

        /// <remarks/>
        [XmlEnumAttribute("doing_maintenance")]
        DoingMaintenance,

        /// <remarks/>
        [XmlEnumAttribute("doing_the_dishes")]
        DoingTheDishes,

        /// <remarks/>
        [XmlEnumAttribute("doing_the_laundry")]
        DoingTheLaundry,

        /// <remarks/>
        [XmlEnumAttribute("driving")]
        Driving,

        /// <remarks/>
        [XmlEnumAttribute("gaming")]
        Gaming,

        /// <remarks/>
        [XmlEnumAttribute("gardening")]
        Gardening,

        /// <remarks/>
        [XmlEnumAttribute("getting_a_haircut")]
        GettingAHaircut,

        /// <remarks/>
        [XmlEnumAttribute("going_out")]
        GoingOut,

        /// <remarks/>
        [XmlEnumAttribute("hanging_out")]
        HangingOut,

        /// <remarks/>
        [XmlEnumAttribute("having_a_beer")]
        HavingABeer,

        /// <remarks/>
        [XmlEnumAttribute("having_a_snack")]
        HavingASnack,

        /// <remarks/>
        [XmlEnumAttribute("having_breakfast")]
        HavingBreakfast,

        /// <remarks/>
        [XmlEnumAttribute("having_coffee")]
        HavingCoffee,

        /// <remarks/>
        [XmlEnumAttribute("having_dinner")]
        HavingDinner,

        /// <remarks/>
        [XmlEnumAttribute("having_lunch")]
        HavingLunch,

        /// <remarks/>
        [XmlEnumAttribute("having_tea")]
        HavingTea,

        /// <remarks/>
        [XmlEnumAttribute("hiking")]
        Hiking,

        /// <remarks/>
        [XmlEnumAttribute("in_a_car")]
        InACar,

        /// <remarks/>
        [XmlEnumAttribute("in_a_meeting")]
        InAMeeting,

        /// <remarks/>
        [XmlEnumAttribute("in_real_life")]
        InRealLife,

        /// <remarks/>
        [XmlEnumAttribute("jogging")]
        Jogging,

        /// <remarks/>
        [XmlEnumAttribute("on_a_bus")]
        OnABus,

        /// <remarks/>
        [XmlEnumAttribute("on_a_plane")]
        OnAPlane,

        /// <remarks/>
        [XmlEnumAttribute("on_a_train")]
        OnATrain,

        /// <remarks/>
        [XmlEnumAttribute("on_a_trip")]
        OnATrip,

        /// <remarks/>
        [XmlEnumAttribute("on_the_phone")]
        OnThePhone,

        /// <remarks/>
        [XmlEnumAttribute("on_vacation")]
        OnVacation,

        /// <remarks/>
        [XmlEnumAttribute("other")]
        Other,

        /// <remarks/>
        [XmlEnumAttribute("partying")]
        Partying,

        /// <remarks/>
        [XmlEnumAttribute("playing_sports")]
        PlayingSports,

        /// <remarks/>
        [XmlEnumAttribute("reading")]
        Reading,

        /// <remarks/>
        [XmlEnumAttribute("rehearsing")]
        Rehearsing,

        /// <remarks/>
        [XmlEnumAttribute("running")]
        Running,

        /// <remarks/>
        [XmlEnumAttribute("running_an_errand")]
        RunningAnErrand,

        /// <remarks/>
        [XmlEnumAttribute("scheduled_holiday")]
        ScheduledHoliday,

        /// <remarks/>
        [XmlEnumAttribute("shaving")]
        Shaving,

        /// <remarks/>
        [XmlEnumAttribute("shopping")]
        Shopping,

        /// <remarks/>
        [XmlEnumAttribute("skiing")]
        Skiing,

        /// <remarks/>
        [XmlEnumAttribute("sleeping")]
        Sleeping,

        /// <remarks/>
        [XmlEnumAttribute("socializing")]
        Socializing,

        /// <remarks/>
        [XmlEnumAttribute("studying")]
        Studying,

        /// <remarks/>
        [XmlEnumAttribute("sunbathing")]
        Sunbathing,

        /// <remarks/>
        [XmlEnumAttribute("swimming")]
        Swimming,

        /// <remarks/>
        [XmlEnumAttribute("taking_a_bath")]
        TakingABath,

        /// <remarks/>
        [XmlEnumAttribute("taking_a_shower")]
        TakingAShower,

        /// <remarks/>
        [XmlEnumAttribute("walking")]
        Walking,

        /// <remarks/>
        [XmlEnumAttribute("walking_the_dog")]
        WalkingTheDog,

        /// <remarks/>
        [XmlEnumAttribute("watching_a_movie")]
        WatchingAMovie,

        /// <remarks/>
        [XmlEnumAttribute("watching_tv")]
        WatchingTv,

        /// <remarks/>
        [XmlEnumAttribute("working_out")]
        WorkingOut,

        /// <remarks/>
        [XmlEnumAttribute("writing")]
        Writing,
    }
}
