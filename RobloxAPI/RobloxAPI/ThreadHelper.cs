using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RobloxAPI
{
    public static class ThreadHelper
    {

        public static ThreadState StartThread(this Thread t, object obj)
        {
            t.Start(obj);
            return t.ThreadState;
        }

        public static ThreadState StartThread(this Thread t)
        {
            t.Start();
            return t.ThreadState;
        }

    }
}
