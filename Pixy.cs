using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace SandySharp
{
    static class Pixy
    {

        
        public static void DrawPixel(Color color, Image image)
        {
            if (ViewSystem.Instance.MouseInBounds(image))
            {
                image.SetPixel((uint)ViewSystem.Instance.getImageMouseCoor(image).X, (uint)ViewSystem.Instance.getImageMouseCoor(image).Y, color);
            }
        }



        /// <summary>
        /// Передвинуть пиксель изображения в направлении вектора
        /// </summary>
        public static void MovePixel(uint x, uint y, Vector2i offset, Image image)
        {
            image.SetPixel(x + (uint)offset.X, y + (uint)offset.Y, image.GetPixel(x, y));
            image.SetPixel(x, y, Color.Transparent);

        }


        /// <summary>
        /// Передвинуть пиксель изображения в направлении вектора с учетом колизии. Если на пути движения есть пиксель, то передвижение будет отмененно
        /// </summary>
        public static void MovePixelWithCols(uint x, uint y, Vector2i offset, Image image)
        {
            if (image.GetPixel(x + (uint)offset.X, y + (uint)offset.Y) == Color.Transparent)
            {
                image.SetPixel(x + (uint)offset.X, y + (uint)offset.Y, image.GetPixel(x, y));
                image.SetPixel(x, y, Color.Transparent);
            }
        }

        public static void MovePixelWithCols(uint x, uint y, Vector2i offset, Image image, int range)
        {
            if (image.GetPixel(x + (uint)offset.X, y + (uint)offset.Y) == Color.Transparent)
            {
                range--;
                image.SetPixel(x + (uint)offset.X, y + (uint)offset.Y, image.GetPixel(x, y));
                image.SetPixel(x, y, Color.Transparent);
                if (range != 0) MovePixelWithCols(x + (uint)offset.X, y + (uint)offset.Y, offset, image, range);
            }
        }


        public static bool InImageBounds(int x, int y, Image image)
        {
            return x < image.Size.X && x > 0 && y < image.Size.Y && y > 0;
        }

        public static void drawCube(Color color, Image image, int length)
        {
            if (ViewSystem.Instance.MouseInBounds(image))
            {
                
                Vector2i mouse = ViewSystem.Instance.getImageMouseCoor(image);
                Console.WriteLine(image.Pixels[mouse.X + mouse.Y *image.Size.Y]);
                for (int i = mouse.X - length / 2; i < mouse.X + length / 2; i++)
                    for (int j = mouse.Y - length / 2; j < mouse.Y + length / 2; j++)
                    {
                        
                        if (Pixy.InImageBounds(i, j, image)) 
                        {
                            image.SetPixel((uint)i, (uint)j, color);
                            
                        } 
                    }
            }
           
        }

        public static Queue<Vector2i> getNeighbours(int x, int y, Image image)
        {
            Vector2i temp;
            Queue<Vector2i> pack = new Queue<Vector2i>();
            if (image.GetPixel((uint)x - 1, (uint)y) != Color.Transparent) pack.Enqueue(new Vector2i(x - 1, y));
            if (image.GetPixel((uint)x + 1, (uint)y) != Color.Transparent) pack.Enqueue(new Vector2i(x + 1, y));
            if (image.GetPixel((uint)x, (uint)y - 1) != Color.Transparent) pack.Enqueue(new Vector2i(x, y - 1));
            if (image.GetPixel((uint)x, (uint)y + 1) != Color.Transparent) pack.Enqueue(new Vector2i(x, y + 1));
            return pack;
        }



        public static bool CoordInBounds(uint x, uint y, Image image)
        {
            return x < image.Size.X - 2 && x > 2 && y < image.Size.Y - 2 && y > 2;
        }




        public static  Image drawBorder(Image image)
        {
            for(uint j=0; j<image.Size.Y; j++)
            for(uint i=0; i<image.Size.X; i++)
            {
                    image.SetPixel(i, 0,Color.White);
                    image.SetPixel(0, j, Color.White);
                    image.SetPixel(image.Size.X-1, j, Color.White);
                    image.SetPixel(i, image.Size.Y - 1, Color.White);
            }
            return image;
        }

    }
}
