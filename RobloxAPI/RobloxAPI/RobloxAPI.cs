using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Drawing;

namespace RobloxAPI
{

    #region Enumerations
    
        public enum RobloxMembershipType
        {
            /// <summary>
            /// Not a BC member
            /// </summary>
            None = 0,
            /// <summary>
            /// Builder's Club or BC
            /// </summary>
            BuildersClub = 1,
            /// <summary>
            /// Turbo Builders Club or TBC
            /// </summary>
            TurboBuildersClub = 2,
            /// <summary>
            /// Outrageous Builders Club or OBC
            /// </summary>
            OutrageousBuildersClub = 3
        }

        public enum RobloxAssetType
        {
            TShirt = 2,
            Audio = 3,
            Hat = 8,
            Model = 10,
            Shirt = 11,
            Pants = 12,
            Decal = 13,
            Head = 17,
            Face = 18,
            Gear = 19,
            Animation = 24,
            Package = 32,
            Plugin = 38
        }
    #endregion

    /// <summary>
    /// Credit: Roblox for making it and ZMidnight for making part of the web api.
    /// </summary>
    public static class RobloxApi
    {

        private static string ROBLOXAPI_SITE = "http://api.roblox.com/";

        #region API Methods

        /// <summary>
        /// Returns the friends based on the user id.
        /// </summary>
        /// <param name="userId">The user id to look up friends</param>
        /// <returns>The friends based on the user.</returns>
        public static User[] GetUserFriends(int userId)
        {
            try
            {
                using (StreamReader reader = new StreamReader(new WebClient().OpenRead(ROBLOXAPI_SITE + "users/" + userId + "/friends")))
                {
                    return JsonConvert.DeserializeObject<User[]>(reader.ReadToEnd());
                }
            }
            catch 
            {
                return new User[1]; 
            }
        }

        /// <summary>
        /// Gets the Asset Info by id
        /// </summary>
        /// <param name="prodId">The id of the asset.</param>
        /// <returns>The info of the item.</returns>
        public static ProductInfo GetAssetInfo(int prodId)
        {
            WebClient client = new WebClient();
            using (StreamReader reader = new StreamReader(client.OpenRead(ROBLOXAPI_SITE+"marketplace/productinfo?assetId="+prodId)))
            {
                return JsonConvert.DeserializeObject<ProductInfo>(reader.ReadToEnd());
            }
        }

        /// <summary>
        /// Gets the Developer Product info by id
        /// </summary>
        /// <param name="prodId">The id of the product.</param>
        /// <returns>The info of the item.</returns>
        public static ProductInfo GetProductInfo(int prodId)
        {
            WebClient client = new WebClient();
            using (StreamReader reader = new StreamReader(client.OpenRead(ROBLOXAPI_SITE + "marketplace/productdetails?productId=" + prodId)))
            {
                return JsonConvert.DeserializeObject<ProductInfo>(reader.ReadToEnd());
            }
        }

        /// <summary>
        /// Gets the places from an userId.
        /// </summary>
        /// <param name="userId">The user to get the places from</param>
        /// <returns>The places</returns>
        /// <seealso cref="Places"/>
        public static Places GetPlacesFrom(int userId)
        {
            WebClient client = new WebClient();
            using (StreamReader reader = new StreamReader(client.OpenRead("http://www.roblox.com/Contests/Handlers/Showcases.ashx?userId="+userId)))
            {
                return JsonConvert.DeserializeObject<Places>(reader.ReadToEnd());
            }
        }

        /// <summary>
        /// Checks if a user is friends with another person.
        /// </summary>
        /// <param name="firstUserId">The first user to check friend status.</param>
        /// <param name="secondUserId">The second user to check friend status.</param>
        /// <returns>Are the two users friends?</returns>
        public static bool IsFriendsWith(int firstUserId, int secondUserId)
        {
            WebClient client = new WebClient();
            using (StreamReader reader = new StreamReader(client.OpenRead("http://www.roblox.com/Game/LuaWebService/HandleSocialRequest.ashx?method=IsFriendsWith&playerId=" + firstUserId + "&userId=" + secondUserId)))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().TrimStart(' ');
                    if (line.ToLower().StartsWith("<value type=\"boolean\">") && line.ToLower().EndsWith("</value>"))
                    {
                        string rep = line.ToLower().Replace("<value type=\"boolean\">", "").Replace("</value>", "");
                        bool ret = false;
                        bool.TryParse(rep, out ret);
                        return ret;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if a user is a best friend with another person.
        /// </summary>
        /// <param name="firstUserId">The first user to check best friend status.</param>
        /// <param name="secondUserId">The second user to check best friend status.</param>
        /// <returns>Are the two users best friends?</returns>
        public static bool IsBestFriendsWith(int firstUserId, int secondUserId)
        {
            WebClient client = new WebClient();
            using (StreamReader reader = new StreamReader(client.OpenRead("http://www.roblox.com/Game/LuaWebService/HandleSocialRequest.ashx?method=IsBestFriendsWith&playerId="+firstUserId+"&userId="+secondUserId)))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().TrimStart(' ');
                    if (line.ToLower().StartsWith("<value type=\"boolean\">") && line.ToLower().EndsWith("</value>"))
                    {
                        string rep = line.ToLower().Replace("<value type=\"boolean\">", "").Replace("</value>", "");
                        bool ret = false;
                        bool.TryParse(rep, out ret);
                        return ret;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Get's the username by id.
        /// </summary>
        /// <param name="uId">The id</param>
        /// <returns>The username from the id</returns>
        public static string GetUsernameFromId(int uId)
        {
            WebClient client = new WebClient();
            using (StreamReader reader = new StreamReader(client.OpenRead(ROBLOXAPI_SITE+"users/"+uId)))
            {
                return JsonConvert.DeserializeObject<User>(reader.ReadToEnd()).Username;
            }
        }

        /// <summary>
        /// Get's the thumbnail image from a product info object.
        /// </summary>
        /// <param name="info">The object to get the image from.</param>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        /// <returns>The image from the object</returns>
        public static Image GetThumbnailImage(ProductInfo info, int width, int height)
        {
            WebClient c = new WebClient();
            Stream s = c.OpenRead("http://www.roblox.com/Thumbs/Asset.ashx?assetId=" + info.AssetId + "&width=" + width + "&height=" + height);
            return Image.FromStream(s);
        }

        /// <summary>
        /// Get's the user's blurb by user id.
        /// </summary>
        /// <param name="uId">The user id</param>
        /// <returns>The user's blurb.</returns>
        public static string GetUserBlurb(int uId)
        {
            WebClient c = new WebClient();
            using(StreamReader reader = new StreamReader(c.OpenRead("http://rbxapi.zmidnight.net/user/blurb/?UserId="+uId)))
            {
                string f = reader.ReadToEnd();
                if (f.StartsWith("R/xhtml1/DTD/xhtml1-transitional.dtd\">")) // Prevents a long arse return which is just part of the html.
                {
                    return "";
                }
                return f;
            }
        }

        /// <summary>
        /// Get's the user's thumbnail with default size.
        /// </summary>
        /// <param name="usr">The user to get the thumbnail for.</param>
        /// <returns>The user's thumbnail.</returns>
        public static Image GetUserThumbnail(User usr)
        {
            WebClient c = new WebClient();
            Stream s = c.OpenRead("http://www.roblox.com/Thumbs/Avatar.ashx?userId=" + usr.Id);
            return Image.FromStream(s);
        }

        /// <summary>
        /// Get's the user's thumbnail with default size.
        /// </summary>
        /// <param name="usr">The user to get the thumbnail for.</param>
        /// <returns>The user's thumbnail.</returns>
        public static Image GetUserThumbnail(int uId)
        {
            WebClient c = new WebClient();
            Stream s = c.OpenRead("http://www.roblox.com/Thumbs/Avatar.ashx?userId=" + uId);
            return Image.FromStream(s);
        }

        /// <summary>
        /// Get's the user's thumbnail with default size.
        /// </summary>
        /// <param name="usr">The user to get the thumbnail for.</param>
        /// <returns>The user's thumbnail.</returns>
        public static Image GetUserThumbnail(int uId, int width, int height)
        {
            WebClient c = new WebClient();
            Stream s = c.OpenRead("http://www.roblox.com/Thumbs/Avatar.ashx?userId=" + uId+"&width="+width+"&height="+height);
            return Image.FromStream(s);
        }

        /// <summary>
        /// Get's the user's thumbnail.
        /// </summary>
        /// <param name="usr">The user to get the thumbnail for</param>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        /// <returns>The user's thumbnail.</returns>
        public static Image GetUserThumbnail(User usr, int width, int height)
        {
            WebClient c = new WebClient();
            Stream s = c.OpenRead("http://www.roblox.com/Thumbs/Avatar.ashx?userId=" + usr.Id + "&width="+width+"&height="+height);
            return Image.FromStream(s);
        }

        /// <summary>
        /// Get a user object by username
        /// </summary>
        /// <param name="username">The username to get the object</param>
        /// <returns>The user object. Can be null if user doesn't exist or fails parsing.</returns>
        public static User GetUserByUsername(string username)
        {
            WebClient c = new WebClient();
            using(StreamReader reader = new StreamReader(c.OpenRead("http://rproxy.tk/rapi/GetIdByUsername/"+username)))
            {
                if (reader.ReadToEnd() == "-1")
                {
                    return null;
                }
                try
                {
                    int i = 0;
                    int.TryParse(reader.ReadToEnd(), out i);
                    return GetUserById(i);
                }
                catch { return null; }
            }
        }

        /// <summary>
        /// Gets an user object by id.
        /// </summary>
        /// <param name="uId">The id to get the user object.</param>
        /// <returns>The object of the user.</returns>
        public static User GetUserById(int uId)
        {
            try
            {
                WebClient client = new WebClient();
                using (StreamReader reader = new StreamReader(client.OpenRead(ROBLOXAPI_SITE + "users/" + uId)))
                {
                    return JsonConvert.DeserializeObject<User>(reader.ReadToEnd());
                }
            }
            catch { return null; }
        }

        #endregion

    }
}
