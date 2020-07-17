using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

using System.Runtime.InteropServices;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using TGUI;
using Box2DX.Dynamics;
using Box2DX.Common;
using Box2DX.Collision;
namespace SandySharp
{
    class Program
    {
        
        static RenderWindow window;
        


        static public void Main(string[] args)
        {
            //


            //Font font = new Font("C:/Users/Пользователь/source/repos/SandySharp/SandySharp/EightBitDragon-anqx.ttf");
            
            //WINDOW
            window = new RenderWindow(new SFML.Window.VideoMode(640, 480), "SandySharp");    
            window.SetVerticalSyncEnabled(true);
            window.Closed += Window_Closed;
            window.SetFramerateLimit(60);

            SceneManager.instance().start(window);
            SceneCreator.instance().start(window);
            //////////SCENE CREATION//////////////

            //.instance().loadScene1();
            SceneManager.instance().loadScene(SceneCreator.instance().createScene());

            Stopwatch stopwatch = new Stopwatch();
            //////////////////////////////////////


            while (window.IsOpen)
            {


                stopwatch.Restart();

                //SYSTEMS UPDATES
                SceneManager.instance().GlobalUpdate();
                TimeManager.instance().update();
                ViewSystem.Instance.update();
                BrushManager.instance().update();
                stopwatch.Stop();

                UI_SYSTEM.instance().update(stopwatch.ElapsedMilliseconds); //передаем gui данные для обновления



                window.DispatchEvents();
                window.Clear(SFML.Graphics.Color.Black);

                //DrawHere
                SceneManager.instance().DrawScene();
                UI_SYSTEM.instance().Draw();

                window.Display();
                
                

            }
        }

        private static void Window_Closed(object sender, EventArgs e)
        {
            window.Close();
        }
    }


}
