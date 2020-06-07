using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace SandySharp
{
    class Layer
    {
        public String name;
        public int order;
        public RenderObject picture { get; set; }
        private bool active;

        public bool IsActive() => active;
        public void setActive(bool _active) => active = _active;

        public Layer( int _order ,String _name)
        {
            order = _order;
            name = _name;
        }

        public void setLayerRenderObject(RenderObject _picture)
        {
            picture = _picture;
        }

        public void Clear()
        {
            picture.Dispose();   
        }

        public Sprite getSpriteToDraw()
        {
            return picture.sprite;
        }

    }

 
}
