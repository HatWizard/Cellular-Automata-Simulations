using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace SandySharp
{
    class FireSimulation : Simulation
    {
        Color color;
        byte shift;
        Color woodColor;
        byte woodshift;
        Image field;
        Image baseField;
        Color activeFire;
        Color passiveFire;
        Color temPix;
        Color water;
        Queue<Vector2i> temp;
        bool fireable = true;
        public FireSimulation(Color _color, byte _shift, Color _woodColor, byte _woodshift, Image _field, Image _baseField, Color _water, string _name="nameless")
        {
            
            color = _color;
            shift = _shift;
            woodColor = _woodColor;
            woodshift = _woodshift;
            field = _field;
            baseField = _baseField;
            activeFire = new Color(color.R, (byte)(color.G + shift), color.B, color.A);
            passiveFire = new Color(color.R, (byte)(color.G + shift*0.9f), color.B, color.A);
            rand = new Random();
            water = _water;
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
                    
                    
                    //Console.WriteLine("there");
                    if (InGradientFire(field.GetPixel(i, j), shift))
                    {

                 
                        temPix = field.GetPixel(i, j);
                        // Console.WriteLine(temPix + " " + activeFire);
                        if (baseField.GetPixel(i, j) == water) { field.SetPixel(i, j, Color.Transparent); }
                        else
                        if (temPix == activeFire)
                        {
                           
                           fireable = true;
                           foreach(var pix in Pixy.getNeighbours((int)i, (int)j, baseField))
                            {
                                
                                if (InGradient(baseField.GetPixel((uint)pix.X, (uint)pix.Y)) == true)
                                {
                                   
                                    fireable = false;
                                    if (rand.Next() % 50 == 1)
                                    {
                                        field.SetPixel((uint)pix.X, (uint)pix.Y, activeFire);
                                        baseField.SetPixel((uint)pix.X, (uint)pix.Y, Color.Transparent);
                                    }
                                }
                            }

                            if (fireable) { field.SetPixel(i, j, passiveFire); }


                        }
                        else if(temPix == color)
                        {
                            if (rand.Next()%70==1)
                            {
                                field.SetPixel(i, j, Color.Transparent);
                            }
                        }
                        else
                        {
                            if (rand.Next() % 30==1)
                            {
                                field.SetPixel(i, j, color);
                            }
                        }

                    }
                }
        }



        //смещение цвета дерева
        public bool InGradient(Color pix) => woodColor.R <= pix.R && pix.R <= woodColor.R + woodshift;

        //смещение цвета огня
        public bool InGradientFire(Color pix, int Greensh)
        {
            return color.G <= pix.G && pix.G <= color.G + Greensh;
        }

        public Color getActiveFire()
        {
            return activeFire;
        }
    }
}

