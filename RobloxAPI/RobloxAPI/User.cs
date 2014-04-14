using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RobloxAPI
{
    public class User
    {

        public int Id { get; set; }
        public string Username { get; set; }

        public User[] Friends
        {
            get
            {
                return RobloxApi.GetUserFriends(Id);
            }
        }

        public string Blurb
        {
            get
            {
                string blurb = RobloxApi.GetUserBlurb(Id);
                return blurb;
            }
        }

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

    }
}
