using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
namespace SandySharp
{
    class RenderObject //класс-прослойка между тремя типами изображений для их удобного использования
    {
        public Texture texture;
        public Sprite sprite;
        public Image image;

        public RenderObject(Image _image)
        {
            image = _image;
            texture = new Texture(image);
            
            sprite = new Sprite(texture);
        }


        public void update_texture()
        {
            texture.Update(image);
        }

        public void Dispose()
        {
            image.Dispose();
            texture.Dispose();
            sprite.Dispose();
        }
    }
}
