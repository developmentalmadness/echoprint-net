using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Microsoft.Xna.Framework;

namespace SongIdentifier
{
    class XnaAsyncDispatcher
    {
        Timer timer;

        public XnaAsyncDispatcher()
        {
            timer = new Timer(50);
            timer.Elapsed += timer_Elapsed;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            FrameworkDispatcher.Update();
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
    }
}
