using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
namespace SandySharp
{
    class Counter : Simulation
    {
        //симуляция считает количество активных пикселей на поле. 
        Image image;
        public int countPixels=0;


        public Counter(Image _image)
        {
            image = _image;
        }
        public override void start()
        {
            
        }

        public override void update()
        {
                
                for (uint i = image.Size.X - 2; i > 1; i--)
                    for (uint j = image.Size.Y - 2; j > 1; j--)
                    {
                        if (image.GetPixel(i, j) != Color.Transparent) countPixels++;
                    }

        }
    }
}
