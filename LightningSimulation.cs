using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
namespace SandySharp
{
    class LightningSimulation : Simulation
    {
        public Color color;
        public Color fireColor;
        private Image field;
        private Image baseField;
        private uint x, y;
        private Color temp;
        private bool stopTime=true;

        //test
        
        public LightningSimulation(Color _color, Color _fireColor ,Image _field, Image _baseField, string _name = "nameless")
        {
            field = _field;
            baseField = _baseField;
            color = _color;
            fireColor = _fireColor;
            rand = new Random();
            name = _name;
        }
        public override void start()
        {
            
        }

        
        public override void update()
        {
            if (stopTime)
            {

                for(uint j =2; j<baseField.Size.Y-2; j++)
                    for(uint i=2; i<baseField.Size.X-2; i++)
                    {
                        
                            if (field.GetPixel(i,j) == color)
                            {
                                x = i; y = j;
                                int randDir = rand.Next(0, 5);
                                if (randDir < 3)
                                {
                                    y++;
                                   
                                }
                                else if (randDir == 3)
                                {
                                    x++;
                                    y++;
                                    
                                }
                                else if (randDir == 4)
                                {
                                    x--;
                                    y++;
                                    
                                }
                                temp = baseField.GetPixel(x, y);

                                if (temp==Color.Transparent)
                                {
                                    field.SetPixel(x, y, color);
                                }
                                else if(temp!=color)
                                {
                                    field.SetPixel(x, y, fireColor);
                                    
                                }

                            }

                    }

                stopTime = false;
                Task.Delay(50).ContinueWith(t=>eraseLightnings());

            }
           

        }



        public void eraseLightnings()
        {
                for (uint j = 1; j < baseField.Size.Y - 1; j++)
                    for (uint i = 1; i < baseField.Size.X - 1; i++)
                        if (field.GetPixel(i, j) == color)
                        {
                            field.SetPixel(i, j, Color.Transparent);
                        }
                stopTime = true;
        }
    }
}
