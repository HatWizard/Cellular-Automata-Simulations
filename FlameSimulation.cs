using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
namespace SandySharp
{
    class FlameSimulation : Simulation
    {
        Color color;         //цвет пикселей пламени
        Color triggerColor;  //цвет пиксель рядом с котором генерируется пламя
        Image baseField;     //изображение(слой), на котором нужно искать пиксели-триггеры

        Color[] alphaGradient;
        Color temp;
        public FlameSimulation(Color _color, Color _triggerColor ,Image _baseField,  Image _field)
        {
            color = _color;
            baseField = _baseField;
            field = _field;
            triggerColor = _triggerColor;
            alphaGradient = new Color[17];
            for(int i= alphaGradient.Length-1; i>=0; i--)
            {
                alphaGradient[i] = color;
                alphaGradient[i].A =(byte)(i*15);
                Console.WriteLine(alphaGradient[i].A);
            }

        }

        public override void start(){
            
        }


    

        public override void update()
        {
            for (uint i = field.Size.X - 2; i > 1; i--)
                for (uint j = field.Size.Y - 2; j > 1; j--)
                {
                    if (baseField.GetPixel(i, j)==triggerColor)
                    {
                        if (rand.Next() % 2 == 1)
                        {
                            field.SetPixel(i, j + 1, color);
                        }
                    }
                    temp = field.GetPixel(i, j);

                    if (temp.G == color.G)
                    {
                        if(temp.A <= 0)
                        {
                            field.SetPixel(i, j, Color.Transparent);

                        }

                        else
                        {
                            int a = rand.Next() % 100;
                            if (a < 50) Pixy.MovePixel(i, j, up, field);
                            else
                            if (a < 60) Pixy.MovePixel(i, j, upL, field);
                            else
                            if (a < 70) Pixy.MovePixel(i, j, upR, field);

                            if (a>25)
                            {
                                field.SetPixel(i, j, alphaGradient[(temp.A / 15) - 1]);
                            }
                        }

                          
                            

                    }

                }
        }


        
    }
}
