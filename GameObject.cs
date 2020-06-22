using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
namespace SandySharp
{
   abstract class GameObject
    {
        private bool active=true;
        public string name;
        public abstract void update();
        public abstract void start();

        public bool IsActive() => active;
        public void setActive(bool _active) => active = _active;

        public void toggleActive() => active = !active;

        public void turnToggle() => active = !active;

    }

   


    
}
