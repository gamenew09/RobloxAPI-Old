using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobloxAPI
{
    public class ShowcasePlace
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Places
    {
        /// <summary>
        /// Has an Error occured when getting the places.
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// How many places are in the users account
        /// </summary>
        public string Count { get; set; }
        /// <summary>
        /// Has the user maxed out the place number?
        /// </summary>
        public string UserIsAtMaxShowcases { get; set; }
        /// <summary>
        /// Does the user have any places?
        /// </summary>
        public bool HasAnyPlaces { get { return Showcase != null; } }
        /// <summary>
        /// The places the user has.
        /// </summary>
        public List<ShowcasePlace> Showcase { get; set; }
    }

    /// <summary>
    /// Class used for the place search. Can be created but will be at their null state.
    /// </summary>
    public class Place
    {

        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// The userid of the creator.
        /// </summary>
        public int CreatorID { get; set; }
        /// <summary>
        /// The username of the creator.
        /// </summary>
        public string CreatorName { get; set; }
        /// <summary>
        /// The roblox.com relative path of the user, usually started with /User.aspx?ID=.
        /// </summary>
        public string CreatorUrl { get; set; }
        /// <summary>
        /// How many times did the place get played.
        /// </summary>
        public int Plays { get; set; }
        /// <summary>
        /// How much, in robux, does the place cost.
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// Can you thumbs up or down?
        /// </summary>
        public bool IsVotingEnabled { get; set; }
        /// <summary>
        /// How many times did people thumbs up the place.
        /// </summary>
        public int TotalUpVotes { get; set; }
        /// <summary>
        /// How many times did people thumbs down the place.
        /// </summary>
        public int TotalDownVotes { get; set; }
        /// <summary>
        /// How many people have bought the place, only set when the price is above 0.
        /// </summary>
        public int TotalBought { get; set; }
        /// <summary>
        /// Did an error occur while getting this place?
        /// </summary>
        public bool HasErrorOcurred { get; set; }
        /// <summary>
        /// The name of the place.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The id of the place.
        /// </summary>
        public int PlaceID { get; set; }
        /// <summary>
        /// How many people are playing the place when this place was searched.
        /// </summary>
        public int PlayerCount { get; set; }
    }
}
