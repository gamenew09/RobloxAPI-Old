using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobloxAPI
{
    public class Place
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
        public List<Place> Showcase { get; set; }
    }
}
