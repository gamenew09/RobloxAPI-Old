using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Net;

namespace RobloxAPI
{
    /// <summary>
    /// The most used images that are used in programs.
    /// </summary>
    public static class RobloxPictureAssets
    {

        private static Image GetImage(string url)
        {
            return new Bitmap(new WebClient().OpenRead(url));
        }

        public static Image PlayerPointsImage { get { return GetImage("http://images.rbxcdn.com/f85c7f5a9dc95d986a6da12102cd9a3a.png"); }}

        public static Image RebuseReportImage { get { return GetImage("http://web.roblox.com/images/abuse.PNG"); } }

        public static Image OBCImage { get { return GetImage("http://web.roblox.com/images/icons/overlay_obcOnly.png"); } }
        public static Image TBCImage { get { return GetImage("http://web.roblox.com/images/icons/overlay_tbcOnly.png"); } }
        public static Image BCImage { get { return GetImage("http://web.roblox.com/images/icons/overlay_bcOnly.png"); } }

    }
}
