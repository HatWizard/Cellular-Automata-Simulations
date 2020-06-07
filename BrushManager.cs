using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
namespace SandySharp
{

    //отвечает за рисование пикселей.
    //будет создаваться один раз, но при переходе в новую сцену получает в качестве аргументов новое изображение для рисования.
    class BrushManager 
    {
          
        private Dictionary<string, Color> colors { get; set; }  //набор цветов
        private Image image;          //целевое изображение

        private static BrushManager Instance;
        private string currentColor="undefined";  //цвет, которым будет рисовать кисть при нажатии мышки
        public static BrushManager instance()
        {
            if (Instance == null)
            {
                Instance = new BrushManager();
            }
            return Instance;
        }
        public void start(Image _image)
        {
            image = _image;
            colors = new Dictionary<string, Color>();

        }


        
        public void update()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left)) {
                paint(currentColor);
            }
        }


        //рисование
        private void paint(string colorName, int length=5)
        {
            if (colors.ContainsKey(colorName))
            {
                Pixy.drawCube(colors[colorName], image, length);
            }
            
        }


        //получить текущий цвет кисти.
        public Color getCurrentBrushColor()
        {
            if (colors.ContainsKey(currentColor))
            {
                return colors[currentColor];
            }
            return Color.Black;
        }

        //добавить новый цвет в список цветов.
        public void AddColor(Color color, string key)
        {
            colors.Add(key, color);
        }


        public void changeCurrentColor(String color)
        {
            if (colors.ContainsKey(color))
            {
                currentColor = color;
                Console.WriteLine(color);
            }
            else
            {
                Console.WriteLine("BRUSHMANAGER: ERROR: COLOR WITH NAME " + color + " DOESNT EXIST");
            }

        }


        public void changeCurrentImage(Image _image)
        {
            image = _image;
        }


        public Dictionary<string, Color> getColors()
        {
            return colors;
        }


    }
}
