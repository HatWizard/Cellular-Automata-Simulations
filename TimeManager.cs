using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
namespace SandySharp
{
    class TimeManager
    {
        private static TimeManager Instance;
        private Clock GlobalClock;
        public static TimeManager instance()
        {
            if (Instance == null) Instance = new TimeManager();
            return Instance;
        }
        public void start()
        {
            GlobalClock = new Clock();
            GlobalClock.Restart();
        }


        public void update(){}

        /// <summary>
        /// Возвращает время с начала работы текущей сцены.
        /// </summary>
        public Time getTime()
        {
            return GlobalClock.ElapsedTime;
        }

    }
}
