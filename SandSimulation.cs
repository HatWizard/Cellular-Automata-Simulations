using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
namespace SandySharp
{
    class SandSimulation : Simulation
    {

        Color sand;
        Color temp;
        public SandSimulation(Color _sand_color, Image _field, string _name="nameless")
        {
            sand = _sand_color;
            field = _field;
            name = _name;
        }

        public override void start()
        {
            
        }

        public override void update()
        {
           
            for (uint i = field.Size.X - 2; i > 1; i--)
                for (uint j = field.Size.Y - 2; j > 1; j--)
                {
                        //Console.WriteLine(field.GetPixel(i, j));
                        if (field.GetPixel(i, j) == sand)
                        {
                             
                            if (field.GetPixel(i, j + 1) == Color.Transparent)
                            {
                                Pixy.MovePixel(i,j,down,field);
                            }
                            else
                                if (field.GetPixel(i - 1, j + 1) == Color.Transparent)
                            {
                                Pixy.MovePixel(i, j, downL, field);
                            }
                            else
                                if (field.GetPixel(i + 1, j + 1) == Color.Transparent)
                            {
                                Pixy.MovePixel(i, j, downR, field);
                            }
                        }
                   
                }
        }
    }
}
