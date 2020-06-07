
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System;
namespace SandySharp
{
    class ViewSystem
    {
        public static ViewSystem Instance;
        private View view;
        private RenderWindow window;
        public static ViewSystem instance()
        {
            if (Instance == null) Instance = new ViewSystem();
            return Instance;
        }

        public void start(Sprite sprite)
        {
            FloatRect tempRect = sprite.GetGlobalBounds();
            FloatRect viewRect = new FloatRect(tempRect.Left-50, tempRect.Top-15, tempRect.Width+50, tempRect.Height+50);


            view = new View(viewRect);
            window = SceneManager.instance().window;
        }

        public void update()
        {
            window.SetView(view);
            View_InputHandlers();
        }


        private void View_InputHandlers()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Subtract))
            {
                ZoomOut();
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Add))
            {
                ZoomIn();
            }

            moving();
        }


        private void ZoomIn()
        {
            view.Zoom(1.1f);

        }


        private void ZoomOut()
        {
            view.Zoom(0.9f);
            
        }

        public RenderWindow GetWindow()
        {
            return window;
        }


        private void moving()
        {
            Vector2f temp = view.Center;
            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) temp.Y--;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) temp.Y++;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) temp.X++;
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) temp.X--;
            view.Center = temp;
        }

        
        public Vector2i getImageMouseCoor(Image image)//возвращает координаты внутри в массиве пикселей изображения
        {
            /*
            float x = Mouse.GetPosition(window).X * (float)((float)image.Size.X / (float)window.Size.X);
            float y = Mouse.GetPosition(window).Y * ((float)image.Size.Y / (float)window.Size.Y);
            */


            //return new Vector2i((int)x, (int)y);
            return (Vector2i)window.MapPixelToCoords(Mouse.GetPosition(window));
        }

        public bool MouseInBounds(Image image)
        {
            return getImageMouseCoor(image).X < image.Size.X && getImageMouseCoor(image).X > 0 && getImageMouseCoor(image).Y < image.Size.Y && getImageMouseCoor(image).Y > 0;
        }

        public View GetView()
        {
            return view;
        }



    }
}
