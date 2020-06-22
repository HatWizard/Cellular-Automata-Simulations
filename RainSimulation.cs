using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
namespace SandySharp
{
    class RainSimulation:Simulation
    {
        Image lightningField;
        Color waterColor;
        Color lightningColor;
        float lightningChance;
        float rainChance;
        int maxRainRoll;
        int maxLightningRoll;
        public RainSimulation(Color _waterColor, Color _lightningColor, Image _field, Image _lightningField, float _lightningChance=0.1f, float _rainChance=0.08f, string _name="nameless")
        {
            waterColor = _waterColor;
            lightningColor = _lightningColor;
            field = _field;
            lightningField = _lightningField;
            lightningChance = _lightningChance;
            rainChance = _rainChance;
            rand = new Random();

            maxRainRoll = (int)(100 / rainChance);
            maxLightningRoll= (int)(100 / lightningChance);
            name = _name;
        }

        public override void start()
        {
           
        }

        public override void update()
        {
            uint j = 2;
            for(uint i=2; i < field.Size.X; i++)
            {
                if(rand.Next(1, maxRainRoll) == 1)
                {
                    field.SetPixel(i, j, waterColor);
                }
                else
                if(rand.Next(1, maxLightningRoll) == 1)
                {
                    lightningField.SetPixel(i, j, lightningColor);
                }
            }
        }
    }
}
