using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace SandySharp
{
    class Diagnostic_GLManager
    {
        Stopwatch stopwatch;
        protected static Diagnostic_GLManager Instance;
        List<float> frameTimes;
        public static Diagnostic_GLManager instance()
        {
            if (Instance == null)
            {
                Instance = new Diagnostic_GLManager();
            }
            return Instance;
        }

        public void start()
        {
            stopwatch = new Stopwatch();
            frameTimes = new List<float>();
        }

        public void update()
        {
            stopwatch.Stop();
            frameTimes.Add(stopwatch.ElapsedMilliseconds);
            if (frameTimes.Count > 10)
            {
                frameTimes.RemoveAt(1);
            }
        }

        public void IterationUpdate()
        {
            stopwatch.Restart();
        }
    
        public float getFrameTime()//возвращает среднее время обработки кадра (производительность)
        {
            return frameTimes.Average();
        }
    }
}
