using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace SandySharp
{
    abstract class Simulation : GameObject
    {
        protected Image field;
        protected static Random rand;//генератор случайных чисел

        public static Vector2i down = new Vector2i(0, 1);
        public static Vector2i downL = new Vector2i(-1, 1);
        public static Vector2i downR = new Vector2i(1, 1);
        public static Vector2i Right = new Vector2i(1, 0);
        public static Vector2i Left = new Vector2i(-1, 0);
        public static Vector2i zero = new Vector2i(0, 0);
        public static Vector2i up = new Vector2i(0, -1);
        public static Vector2i upL = new Vector2i(-1, -1);
        public static Vector2i upR = new Vector2i(1, -1);
    }
}
