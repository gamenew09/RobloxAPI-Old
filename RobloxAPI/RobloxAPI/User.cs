using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Net;
using HtmlAgilityPack;
using System.IO;
using System.ComponentModel;

namespace RobloxAPI
{

    public class MyWebClient : WebClient
    {
        Uri _responseUri;

        public Uri ResponseUri
        {
            get { return _responseUri; }
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = base.GetWebResponse(request);
            _responseUri = response.ResponseUri;
            return response;
        }
    }

    public class User
    {
        /// <summary>
        /// The user id of the user.
        /// </summary>
        [Description("The user id of the user.")]
        public int Id { get; set; }
        /// <summary>
        /// The user's username.
        /// </summary>
        [Description("The user's username.")]
        public string Username { get; set; }

        /// <summary>
        /// Get's the user's Builder's Club type.
        /// </summary>
        [Description("Get's the user's Builder's Club type.")]
        public RobloxMembershipType Membership
        {
            get 
            {
                RobloxMembershipType type = RobloxMembershipType.None;
                MyWebClient client = new MyWebClient();
                client.OpenRead(new Uri("http://www.roblox.com/Thumbs/BCOverlay.ashx?username=" + Username));
                switch (client.ResponseUri.AbsolutePath)
                {
                    case "images/icons/overlay_obcOnly.png":
                        type = RobloxMembershipType.OutrageousBuildersClub;
                        break;
                    case "images/icons/overlay_tbcOnly.png":
                        type = RobloxMembershipType.TurboBuildersClub;
                        break;
                    case "images/icons/overlay_bcOnly.png":
                        type = RobloxMembershipType.BuildersClub;
                        break;
                }
                return type; 
            }
        }

        /// <summary>
        /// How many places the user has. Might be broken.
        /// </summary>
        [Description("How many places the user has. Might be broken.")]
        public int PlaceCount
        {
            get
            {
                return ShowcasePlaces.Count();
            }
        }

        /// <summary>
        /// Get's the user's places. Might be broken.
        /// </summary>
        [Description("Get's the user's places. Might be broken.")]
        public ShowcasePlace[] ShowcasePlaces
        {
            get
            {
                Places plces = RobloxApi.GetPlacesFrom(Id);
                if (plces.HasAnyPlaces)
                {
                    return plces.Showcase.ToArray();
                }
                else
                {
                    return new ShowcasePlace[]{
                        new ShowcasePlace()
                        {
                            ID = 0
                        }
                    };
                }
            }
        }

        /// <summary>
        /// The user's friends.
        /// </summary>
        [Description("The user's friends.")]
        public User[] Friends
        {
            get
            {
                return RobloxApi.GetUserFriends(Id);
            }
        }

        public bool HasAsset(int assetId)
        {
            try
            {
                WebClient client = new WebClient();
                using (StreamReader reader = new StreamReader(client.OpenRead("https://api.roblox.com/ownership/hasasset?userId=" + Id + "&assetId=" + assetId)))
                {
                    return bool.Parse(reader.ReadToEnd());
                }
            }
            catch { return false; }
        }

        /// <summary>
        /// The user's blurb. Basicly a desciption.
        /// </summary>
        [Description("The user's blurb. Basicly a desciption.")]
        public string Blurb
        {
            get
            {
                return RobloxApi.GetUserBlurb(Id);
            }
        }

        /// <summary>
        /// The image of the user.
        /// </summary>
        [Description("The image of the user.")]
        public Image Thumbnail
        {
            get
            {
                return RobloxApi.GetUserThumbnail(this, 110, 110);
            }
        }

        public override string ToString()
        {
            return Username;
        }

        /// <summary>
        /// Roblox introduced on 4/16/2014, Points. You earn them by people programming their places to award them.
        /// For now it is a beta test for everyone but will go away for fixing then come back. So this isn't going to be completely good to use yet.
        /// </summary>
        [Description("Roblox introduced on 4/16/2014, Points. You earn them by people programming their places to award them. For now it is a beta test for everyone but will go away for fixing then come back. So this isn't going to be completely good to use yet.")]
        public int Points
        {
            get
            {
                WebClient c = new WebClient();
                HtmlDocument doc = new HtmlDocument();
                doc.LoadFromUri("http://web.roblox.com/User.aspx?ID=" + Id);
                try
                {
                    HtmlNode node = doc.GetElementByClass("user-points", "span");
                    Console.WriteLine(node.InnerText);
                    string text = node.InnerText.Replace("Player Points: ", "");
                    int i = 0;
                    Console.WriteLine(text);
                    int.TryParse(text, out i);
                    return i;
                }
                catch { return 0; }
            }
        }

    }
}
