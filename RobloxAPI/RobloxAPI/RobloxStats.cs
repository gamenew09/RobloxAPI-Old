using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace RobloxAPI
{
    /// <summary>
    /// Small class for ROBLOX Statistics.
    /// </summary>
    public static class RobloxStats
    {

        /// <summary>
        /// Get's how many players are playing Roblox when this is called. It's rare when the player count is zero...
        /// </summary>
        /// <see cref="http://robloxwebapi.weebly.com/healthmonitor.html"/>
        /// <returns>How many players playing ROBLOX.</returns>
        public static int GetPlayerCount() 
        {
            WebClient c = new WebClient();
            try {
                using (StreamReader reader = new StreamReader(c.OpenRead(new Uri("http://www.roblox.com/HealthMonitor/PlayerCount.ashx"))))
                {
                    return int.Parse(reader.ReadToEnd());
                }
            } catch { return 0; }
        }

        /// <summary>
        /// Checks if ROBLOX's Databases are up.
        /// </summary>
        /// <returns>If Roblox's Databases are up</returns>
        public static bool IsRobloxDBUp()
        {
            WebClient c = new WebClient();
            try {
                using (StreamReader reader = new StreamReader(c.OpenRead(new Uri("http://www.roblox.com/HealthMonitor/DBServerDiagnostics.ashx"))))
                {
                    return (reader.ReadToEnd() == "OK") ? true : false;
                }
            } catch { return false; }
        }

    }
}
