using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace SandySharp
{
    class SceneManager
    {
        //TODO организовать смену сцен.
        static SceneManager Instance;
        #region Singleton
        public static SceneManager instance()
        {
            if (Instance == null) Instance = new SceneManager();
            return Instance;
        }
        #endregion
        public Scene currentScene { get; set; }
        public RenderWindow window;
        //TEST things
        
        Texture testBoxTexture;
        Sprite testBox;

        int currentSceneNumber=0;

        public void start(RenderWindow _window)
        {
            window = _window;
        }

        public void setCurrentScene(Scene _scene)
        {
            currentScene = _scene;
        }

        /// <summary>
        /// Обновляет текущую сцену, а также некоторые глобальные системы (вызов методов с задержкой)
        /// </summary>
        public void GlobalUpdate()
        {
            currentScene.update();
        }

        public void DrawScene()
        {
            currentScene.graphic_update();
        }

        public void switchScene( int increment)
        {
            currentSceneNumber+=increment;
            switch (currentSceneNumber)
            {
                case 0:
                    loadScene1();
                    break;

                case 1:
                    loadScene2();
                    break;
            }

            Console.WriteLine("Scene" + currentSceneNumber + " loaded!");
        }


        



        #region scene1Loading
        public void loadScene1()
        {
            //COLORS

            Color sand1 = new Color(252, 235, 145);
            Color emptyP = new Color(0, 0, 0);
            Color waterP = new Color(101, 175, 182, 150);
            Color lightning = new Color(108, 233, 236);
            Color fire = new Color(191, 106, 2);
            byte fireoffset = 42;
            Color fireActive = new Color(191, 148, 2);
            Color wood = new Color(53, 33, 21);
            byte woodoffset = 29;

            //MAIN__FIELD
            //Image image = new Image("C:/Users/Пользователь/Desktop/SandySharp/SandySharp/FirstScene.png");
            Image image = new Image(160, 120, Color.Transparent);
            Image flame_field = new Image(160, 120, Color.Transparent);
            Image fire_field = new Image(160, 120, Color.Transparent);

            image = Pixy.drawBorder(image);

            RenderObject picture = new RenderObject(image);
            RenderObject flame_picture = new RenderObject(flame_field);
            RenderObject fire_picture = new RenderObject(fire_field);

            //starts



            //SCENES AND LAYERS
            Scene scene = new Scene(window);
            Layer main_layer = new Layer(1, "main");
            Layer Fire_layer = new Layer(2, "fire_layer");
            Layer flame_layer = new Layer(3, "flame_layer");



            //SIMULATIONS
            SandSimulation sandSim = new SandSimulation(sand1, image, "sand_sim");
            LiquidSimulation liqSim = new LiquidSimulation(waterP, image, "liquid_sim");
            FireSimulation fireSim = new FireSimulation(fire, fireoffset, wood, woodoffset, fire_field, image, waterP, "fire_sim");
            FlameSimulation flameSim = new FlameSimulation(fire, fireSim.getActiveFire(), fire_field, flame_field, "flame_sim");
            LightningSimulation lightningSim = new LightningSimulation(lightning, fireSim.getActiveFire(), fire_field, image, "lightning_sim");
            RainSimulation rainSim = new RainSimulation(waterP, lightning, image, fire_field, 0.1f, 0.08f, "rain_sim");


            scene.AddLayer(main_layer);
            scene.AddLayer(Fire_layer);
            scene.AddLayer(flame_layer);


            scene.AddObject(fireSim);
            scene.AddObject(flameSim);
            scene.AddObject(liqSim);
            scene.AddObject(sandSim);
            //scene.AddObject(lightningSim);
            //scene.AddObject(rainSim);

            main_layer.setLayerRenderObject(picture);
            Fire_layer.setLayerRenderObject(fire_picture);
            flame_layer.setLayerRenderObject(flame_picture);
            scene.initialisation();







            //SYSTEMS INICIALIZATION
            SceneManager.instance().setCurrentScene(scene);
            TimeManager.instance().start();
            ViewSystem.instance().start(picture.sprite);


            BrushManager.instance().start(image);
            BrushManager.instance().AddColor(waterP, "water");
            BrushManager.instance().AddColor(fireSim.getActiveFire(), "fire");
            BrushManager.instance().AddColor(sand1, "sand");
            BrushManager.instance().AddColor(wood, "wood");

            UI_SYSTEM.instance().start();

            //testPHYSICS




        }
        #endregion

        #region scene2Loading
        void loadScene2()
        {
            Color sand1 = new Color(252, 235, 145);
            Color emptyP = new Color(0, 0, 0);
            Color waterP = new Color(101, 175, 182, 150);
            Color lightning = new Color(108, 233, 236);
            Color fire = new Color(191, 106, 2);
            byte fireoffset = 42;
            Color fireActive = new Color(191, 148, 2);
            Color wood = new Color(53, 33, 21);
            byte woodoffset = 29;

            //MAIN__FIELD
            Image image = new Image("C:/Users/Пользователь/Desktop/SandySharp/SandySharp/FirstScene.png");

            Image flame_field = new Image(160, 120, Color.Transparent);
            Image fire_field = new Image(160, 120, Color.Transparent);

            image = Pixy.drawBorder(image);

            RenderObject picture = new RenderObject(image);
            RenderObject flame_picture = new RenderObject(flame_field);
            RenderObject fire_picture = new RenderObject(fire_field);

            //starts



            //SCENES AND LAYERS
            Scene scene = new Scene(window);
            Layer main_layer = new Layer(1, "main");
            Layer Fire_layer = new Layer(2, "fire_layer");
            Layer flame_layer = new Layer(3, "flame_layer");



            //SIMULATIONS
            SandSimulation sandSim = new SandSimulation(sand1, image, "sand_sim");
            LiquidSimulation liqSim = new LiquidSimulation(waterP, image, "liquid_sim");
            FireSimulation fireSim = new FireSimulation(fire, fireoffset, wood, woodoffset, fire_field, image, waterP, "fire_sim");
            FlameSimulation flameSim = new FlameSimulation(fire, fireSim.getActiveFire(), fire_field, flame_field, "flame_sim");
            LightningSimulation lightningSim = new LightningSimulation(lightning, fireSim.getActiveFire(), fire_field, image, "lightning_sim");
            RainSimulation rainSim = new RainSimulation(waterP, lightning, image, fire_field, 0.1f, 0.08f, "rain_sim");


            scene.AddLayer(main_layer);
            scene.AddLayer(Fire_layer);
            scene.AddLayer(flame_layer);


            scene.AddObject(fireSim);
            scene.AddObject(flameSim);
            scene.AddObject(liqSim);
            scene.AddObject(sandSim);
            scene.AddObject(lightningSim);
            scene.AddObject(rainSim);

            main_layer.setLayerRenderObject(picture);
            Fire_layer.setLayerRenderObject(fire_picture);
            flame_layer.setLayerRenderObject(flame_picture);
            scene.initialisation();







            //SYSTEMS INICIALIZATION
            SceneManager.instance().setCurrentScene(scene);
            TimeManager.instance().start();
            ViewSystem.instance().start(picture.sprite);


            BrushManager.instance().start(image);
            BrushManager.instance().AddColor(waterP, "water");
            BrushManager.instance().AddColor(fireSim.getActiveFire(), "fire");
            BrushManager.instance().AddColor(sand1, "sand");
            BrushManager.instance().AddColor(wood, "wood");
            BrushManager.instance().AddColor(lightning, "lightning");

            UI_SYSTEM.instance().start();
        }
        #endregion


        //получить все обьекты текущей сцены
        public GameObject[] GetGameObjects()
        {
            if (currentScene != null)
                return currentScene.gameObjects.ToArray();
            return null;
        }


        public void loadScene(Scene scene)
        {
            scene.initialisation();
            SceneManager.instance().setCurrentScene(scene);
            TimeManager.instance().start();
            ViewSystem.instance().start(scene.getLayers()[0].getSpriteToDraw());


            BrushManager.instance().start(scene.getLayers()[0].picture.image);

            foreach(var key in scene.colors.Keys)
            {
                BrushManager.instance().AddColor(scene.colors[key], key);
            }
          
            UI_SYSTEM.instance().start();
        }


    }


}
