using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using TGUI;
using System.Globalization;
namespace SandySharp
{
    class SceneCreator
    {
        RenderWindow window;
        Scene scene;
        //Лист, содержащий все добавленные пользователем симуляции
        List<Simulation> simulations;
        //LAYERS
        Image image;
        Image flame_field;
        Image fire_field;

        //UI
        Panel originPanel;
        Panel mainPanel;
        Panel sandSimulationPanel;
        Panel liquidSimulationPanel;
        Panel fireSimulationPanel;
        Panel flameSimulationPanel;
        Panel lightningSimulationPanel;
        Panel rainSimulationPanel;

        Panel currentToolPanel;

        static SceneCreator Instance;
        #region Singleton
        public static SceneCreator instance()
        {
            if (Instance == null) Instance = new SceneCreator();
            return Instance;
        }
        #endregion
        public void start(RenderWindow _window)
        {
            window = _window;

            mainPanel = new Panel();
            originPanel = new Panel();



            image = new Image(160, 120, Color.Transparent);
            fire_field = new Image(160, 120, Color.Transparent);
            flame_field = new Image(160, 120, Color.Transparent);

            simulations = new List<Simulation>();
        }


       public Scene createScene()
        {
            bool sceneCreated = false;

            //GUI
            Gui SC_gui = new Gui(window);
            
            mainPanel.Renderer.BackgroundColor = Color.Black;
            mainPanel.Renderer.BorderColor = Color.White;
            mainPanel.PositionLayout = new Layout2d("2.5%", "5%");
            mainPanel.SizeLayout = new Layout2d("95%", "90%");
            mainPanel.Renderer.Borders = new Outline(5, 5, 5, 5);

            Button createButton = new Button("CREATE");
            createButton.Clicked += (e, a) => { sceneCreated = true; };
            createButton.PositionLayout = new Layout2d("80%", "90%");

            mainPanel.Add(createButton);
            SC_gui.Add(mainPanel);


            //SIMULATIONS GUI
            Panel simulationsPanel = new Panel();

            simulationsPanel.Renderer.BackgroundColor = Color.Black;
            simulationsPanel.Renderer.BorderColor = Color.White;
            simulationsPanel.PositionLayout = new Layout2d("3%", "5%");
            simulationsPanel.SizeLayout = new Layout2d("25%", "80%");
            simulationsPanel.Renderer.Borders = new Outline(2, 2, 2, 2);

            Panel AddedSimuationsPanel = new Panel(simulationsPanel);
            AddedSimuationsPanel.PositionLayout = new Layout2d("73%", "5%");


            originPanel.Renderer.BackgroundColor = Color.Black;
            originPanel.Renderer.BorderColor = Color.White;
            originPanel.PositionLayout = new Layout2d("30.5%", "5%");
            originPanel.SizeLayout = new Layout2d("40%", "80%");
            originPanel.Renderer.Borders = new Outline(2, 2, 2, 2);



            mainPanel.Add(simulationsPanel);
            mainPanel.Add(AddedSimuationsPanel);


            /////LAYER CREATING/////////
            image = Pixy.drawBorder(image);

            RenderObject picture = new RenderObject(image);
            RenderObject flame_picture = new RenderObject(flame_field);
            RenderObject fire_picture = new RenderObject(fire_field);

            Layer main_layer = new Layer(1, "main");
            Layer Fire_layer = new Layer(2, "fire_layer");
            Layer flame_layer = new Layer(3, "flame_layer");


            scene = new Scene(window);
            main_layer.setLayerRenderObject(picture);
            Fire_layer.setLayerRenderObject(fire_picture);
            flame_layer.setLayerRenderObject(flame_picture);
            scene.AddLayer(main_layer);
            scene.AddLayer(Fire_layer);
            scene.AddLayer(flame_layer);




            //SIMULATION CREATING PANELS
            SandSimulationPanelInit();
            LiquidSimulationPanelInit();
            FireSimulationPanelInit();
            flameSimulationPanelInit();
            lightningSimulationPanelInit();
            rainSimulationPanelInit();

            ListBox SimulationsList = new ListBox();
            SimulationsList.AddItem("Sand Simulation", "Sand Simulation");
            SimulationsList.AddItem("Liquid Simulation", "Liquid Simulation");
            SimulationsList.AddItem("Fire Simulation", "Fire Simulation");
            SimulationsList.AddItem("Flame Simulation", "Flame Simulation");
            SimulationsList.AddItem("Lightning Simulation", "Lightning Simulation");
            SimulationsList.AddItem("Rain Simulation", "Rain Simulation");
            SimulationsList.ItemSelected += (e, a) =>
            {
                switch (SimulationsList.GetSelectedItemId())
                {
                    case ("Sand Simulation"):
                        currentToolPanel.Visible = false;
                        currentToolPanel = sandSimulationPanel;
                        currentToolPanel.Visible = true;
                        break;

                    case ("Liquid Simulation"):
                        currentToolPanel.Visible = false;
                        currentToolPanel = liquidSimulationPanel;
                        currentToolPanel.Visible = true;
                        break;
                    case ("Fire Simulation"):
                        currentToolPanel.Visible = false;
                        currentToolPanel = fireSimulationPanel;
                        currentToolPanel.Visible = true;
                        break;
                    case ("Flame Simulation"):
                        currentToolPanel.Visible = false;
                        currentToolPanel = flameSimulationPanel;
                        currentToolPanel.Visible = true;
                        break;
                    case ("Lightning Simulation"):
                        currentToolPanel.Visible = false;
                        currentToolPanel = lightningSimulationPanel;
                        currentToolPanel.Visible = true;
                        break;
                    case ("Rain Simulation"):
                        currentToolPanel.Visible = false;
                        currentToolPanel = rainSimulationPanel;
                        currentToolPanel.Visible = true;
                        break;
                }

            };
            simulationsPanel.Add(SimulationsList);

            while (window.IsOpen && !sceneCreated)
            {


                

                window.DispatchEvents();
                window.Clear(SFML.Graphics.Color.Black);
                //Draw here
                SC_gui.Draw();
                window.Display();



            }

            //Scene created!
            foreach(var obj in simulations)
            {
                scene.AddObject(obj);
            }
            SC_gui.RemoveAllWidgets();
            return scene;
        }


        

        private void SandSimulationPanelInit()
        {
            sandSimulationPanel = new Panel(originPanel);


            int r = 255, g = 255, b = 255;
            Panel colorPick = new Panel();
            colorPick.SizeLayout = new Layout2d("10%", "7%");
            colorPick.PositionLayout = new Layout2d("80%", "5%");

            EditBox red = new EditBox();
            red.PositionLayout = new Layout2d("5%", "5%");
            red.MaximumCharacters = 3;

            red.SizeLayout = new Layout2d("20%", "7%");
            red.DefaultText = "255";

            EditBox green = new EditBox(red);
            green.PositionLayout = new Layout2d("30%", "5%");
            EditBox blue = new EditBox(red);
            blue.PositionLayout = new Layout2d("55%", "5%");

            EditBox Name = new EditBox();
            Name.PositionLayout = new Layout2d("5%%", "18%");
            Name.SizeLayout = new Layout2d("60%", "7%");
            Name.Text = "SandSim";
            Name.SetSelectedText(0, 0);
            Name.MaximumCharacters = 10;
            sandSimulationPanel.Add(Name);

            red.TextChanged += (e, a) => {
                int.TryParse(red.Text, out r);
                int.TryParse(blue.Text, out b);
                int.TryParse(green.Text, out g);
                colorPick.Renderer.BackgroundColor = new Color((byte)r, (byte)g, (byte)b);
            };

            green.TextChanged += (e, a) => {
                int.TryParse(red.Text, out r);
                int.TryParse(blue.Text, out b);
                int.TryParse(green.Text, out g);
                colorPick.Renderer.BackgroundColor = new Color((byte)r, (byte)g, (byte)b);
            };

            blue.TextChanged += (e, a) => {
                int.TryParse(red.Text, out r);
                int.TryParse(blue.Text, out b);
                int.TryParse(green.Text, out g);
                colorPick.Renderer.BackgroundColor = new Color((byte)r, (byte)g, (byte)b);
            };


            sandSimulationPanel.Add(red);
            sandSimulationPanel.Add(green);
            sandSimulationPanel.Add(blue);
            sandSimulationPanel.Add(colorPick);

            Button AddSandSim = new Button("Add");
            AddSandSim.PositionLayout = new Layout2d("70%", "90%");
            AddSandSim.Clicked += (e, a) => { Color sandColor = new Color((byte)r, (byte)g, (byte)b); Console.WriteLine(sandColor); simulations.Add(new SandSimulation(sandColor, image, Name.Text)); scene.AddColor(Name.Text, sandColor); };
            sandSimulationPanel.Add(AddSandSim);

            mainPanel.Add(sandSimulationPanel);
            sandSimulationPanel.Visible = false;
            currentToolPanel = sandSimulationPanel;
        }

        private void LiquidSimulationPanelInit()
        {
            liquidSimulationPanel = new Panel(originPanel);

            int r = 48, g = 158, b = 252;
            Panel colorPick = new Panel();
            colorPick.SizeLayout = new Layout2d("10%", "7%");
            colorPick.PositionLayout = new Layout2d("80%", "5%");

            EditBox red = new EditBox();
            red.PositionLayout = new Layout2d("5%", "5%");
            red.MaximumCharacters = 3;

            red.SizeLayout = new Layout2d("20%", "7%");
            red.DefaultText = "255";

            EditBox green = new EditBox(red);
            green.PositionLayout = new Layout2d("30%", "5%");
            EditBox blue = new EditBox(red);
            blue.PositionLayout = new Layout2d("55%", "5%");

            EditBox Name = new EditBox();
            Name.PositionLayout = new Layout2d("5%%", "18%");
            Name.SizeLayout = new Layout2d("60%", "7%");
            Name.Text = "LiqSim";
            Name.SetSelectedText(0, 0);
            Name.MaximumCharacters = 10;
            liquidSimulationPanel.Add(Name);

            red.TextChanged += (e, a) => {
                int.TryParse(red.Text, out r);
                int.TryParse(blue.Text, out b);
                int.TryParse(green.Text, out g);
                colorPick.Renderer.BackgroundColor = new Color((byte)r, (byte)g, (byte)b);
            };

            green.TextChanged += (e, a) => {
                int.TryParse(red.Text, out r);
                int.TryParse(blue.Text, out b);
                int.TryParse(green.Text, out g);
                colorPick.Renderer.BackgroundColor = new Color((byte)r, (byte)g, (byte)b);
            };

            blue.TextChanged += (e, a) => {
                int.TryParse(red.Text, out r);
                int.TryParse(blue.Text, out b);
                int.TryParse(green.Text, out g);
                colorPick.Renderer.BackgroundColor = new Color((byte)r, (byte)g, (byte)b);
            };


            liquidSimulationPanel.Add(red);
            liquidSimulationPanel.Add(green);
            liquidSimulationPanel.Add(blue);
            liquidSimulationPanel.Add(colorPick);

            Button AddliquidSim = new Button("Add");
            AddliquidSim.PositionLayout = new Layout2d("70%", "90%");
            AddliquidSim.Clicked += (e, a) => { Color liquidColor = new Color((byte)r, (byte)g, (byte)b); Console.WriteLine("Simulation added with color: "+liquidColor); simulations.Add(new LiquidSimulation(liquidColor, image, Name.Text)); scene.AddColor(Name.Text, liquidColor); };
            liquidSimulationPanel.Add(AddliquidSim);

            mainPanel.Add(liquidSimulationPanel);
            liquidSimulationPanel.Visible = false;
        }


        private void FireSimulationPanelInit()
        {
            fireSimulationPanel = new Panel(originPanel);

            ////MAIN COLOR////
            int r = 255, g = 123, b = 15;
            Panel colorPick = new Panel();
            colorPick.SizeLayout = new Layout2d("10%", "7%");
            colorPick.PositionLayout = new Layout2d("80%", "5%");

            EditBox red = new EditBox();
            red.PositionLayout = new Layout2d("5%", "5%");
            red.MaximumCharacters = 3;

            red.SizeLayout = new Layout2d("20%", "7%");
            red.DefaultText = "255";

            EditBox green = new EditBox(red);
            green.PositionLayout = new Layout2d("30%", "5%");
            EditBox blue = new EditBox(red);
            blue.PositionLayout = new Layout2d("55%", "5%");
            ///////////////////

            red.TextChanged += (e, a) => {
                int.TryParse(red.Text, out r);
                int.TryParse(blue.Text, out b);
                int.TryParse(green.Text, out g);
                colorPick.Renderer.BackgroundColor = new Color((byte)r, (byte)g, (byte)b);
            };

            green.TextChanged += (e, a) => {
                int.TryParse(red.Text, out r);
                int.TryParse(blue.Text, out b);
                int.TryParse(green.Text, out g);
                colorPick.Renderer.BackgroundColor = new Color((byte)r, (byte)g, (byte)b);
            };

            blue.TextChanged += (e, a) => {
                int.TryParse(red.Text, out r);
                int.TryParse(blue.Text, out b);
                int.TryParse(green.Text, out g);
                colorPick.Renderer.BackgroundColor = new Color((byte)r, (byte)g, (byte)b);
            };

            ////WOOD COLOR/////
            int r1 = 92, g1 = 35, b1 = 7;
            Panel colorPick1 = new Panel();
            colorPick1.SizeLayout = new Layout2d("10%", "7%");
            colorPick1.PositionLayout = new Layout2d("80%", "15%");

            EditBox red1 = new EditBox();
            red1.PositionLayout = new Layout2d("5%", "15%");
            red1.MaximumCharacters = 3;

            red1.SizeLayout = new Layout2d("20%", "7%");
            red1.DefaultText = "255";

            EditBox green1 = new EditBox(red1);
            green1.PositionLayout = new Layout2d("30%", "15%");
            EditBox blue1 = new EditBox(red1);
            blue1.PositionLayout = new Layout2d("55%", "15%");
            ///////////////////

            red1.TextChanged += (e, a) => {
                int.TryParse(red1.Text, out r1);
                int.TryParse(blue1.Text, out b1);
                int.TryParse(green1.Text, out g1);
                colorPick1.Renderer.BackgroundColor = new Color((byte)r1, (byte)g1, (byte)b1);
            };

            green1.TextChanged += (e, a) => {
                int.TryParse(red1.Text, out r1);
                int.TryParse(blue1.Text, out b1);
                int.TryParse(green1.Text, out g1);
                colorPick1.Renderer.BackgroundColor = new Color((byte)r1, (byte)g1, (byte)b1);
            };

            blue1.TextChanged += (e, a) => {
                int.TryParse(red1.Text, out r1);
                int.TryParse(blue1.Text, out b1);
                int.TryParse(green1.Text, out g1);
                colorPick1.Renderer.BackgroundColor = new Color((byte)r1, (byte)g1, (byte)b1);
            };

            ////WATER COLOR////
            int r2 = 50, g2 = 50, b2 = 255;
            Panel colorPick2 = new Panel();
            colorPick2.SizeLayout = new Layout2d("10%", "7%");
            colorPick2.PositionLayout = new Layout2d("80%", "30%");

            EditBox red2 = new EditBox();
            red2.PositionLayout = new Layout2d("5%", "30%");
            red2.MaximumCharacters = 3;

            red2.SizeLayout = new Layout2d("20%", "7%");
            red2.DefaultText = "255";

            EditBox green2 = new EditBox(red2);
            green2.PositionLayout = new Layout2d("30%", "30%");
            EditBox blue2 = new EditBox(red2);
            blue2.PositionLayout = new Layout2d("55%", "30%");
            ///////////////////
            
            red2.TextChanged += (e, a) => {
                int.TryParse(red2.Text, out r2);
                int.TryParse(blue2.Text, out b2);
                int.TryParse(green2.Text, out g2);
                colorPick2.Renderer.BackgroundColor = new Color((byte)r2, (byte)g2, (byte)b2);
            };

            green2.TextChanged += (e, a) => {
                int.TryParse(red2.Text, out r2);
                int.TryParse(blue2.Text, out b2);
                int.TryParse(green2.Text, out g2);
                colorPick2.Renderer.BackgroundColor = new Color((byte)r2, (byte)g2, (byte)b2);
            };

            blue2.TextChanged += (e, a) => {
                int.TryParse(red2.Text, out r2);
                int.TryParse(blue2.Text, out b2);
                int.TryParse(green2.Text, out g2);
                colorPick2.Renderer.BackgroundColor = new Color((byte)r2, (byte)g2, (byte)b2);
            };

            /////////////////////

            EditBox Name = new EditBox();
            Name.PositionLayout = new Layout2d("5%%", "50%");
            Name.SizeLayout = new Layout2d("60%", "7%");
            Name.Text = "FireSim";
            Name.SetSelectedText(0, 0);
            Name.MaximumCharacters = 10;
            fireSimulationPanel.Add(Name);


            fireSimulationPanel.Add(red);
            fireSimulationPanel.Add(green);
            fireSimulationPanel.Add(blue);
            fireSimulationPanel.Add(colorPick);

            fireSimulationPanel.Add(red1);
            fireSimulationPanel.Add(green1);
            fireSimulationPanel.Add(blue1);
            fireSimulationPanel.Add(colorPick1);

            fireSimulationPanel.Add(red2);
            fireSimulationPanel.Add(green2);
            fireSimulationPanel.Add(blue2);
            fireSimulationPanel.Add(colorPick2);


            Button AddfireSim = new Button("Add");
            AddfireSim.PositionLayout = new Layout2d("70%", "90%");
            AddfireSim.Clicked += (e, a) =>
            {
                Color fireColor = new Color((byte)r, (byte)g, (byte)b);
                Color woodColor = new Color((byte)r1, (byte)g1, (byte)b1);
                Color waterColor = new Color((byte)r2, (byte)g2, (byte)b2);
                Console.WriteLine("Simulation added with color: " + fireColor);
                simulations.Add(new FireSimulation(fireColor,40, woodColor,40, fire_field, image, waterColor,Name.Text));
                scene.AddColor(Name.Text, fireColor);
                scene.AddColor("Wood", woodColor);
            };
            fireSimulationPanel.Add(AddfireSim);

            mainPanel.Add(fireSimulationPanel);
            fireSimulationPanel.Visible = false;


        }


        void flameSimulationPanelInit()
        {
            flameSimulationPanel = new Panel(originPanel);

            ////MAIN COLOR////
            int r = 230, g = 100, b = 10;
            Panel colorPick = new Panel();
            colorPick.SizeLayout = new Layout2d("10%", "7%");
            colorPick.PositionLayout = new Layout2d("80%", "5%");

            EditBox red = new EditBox();
            red.PositionLayout = new Layout2d("5%", "5%");
            red.MaximumCharacters = 3;

            red.SizeLayout = new Layout2d("20%", "7%");
            red.DefaultText = "255";

            EditBox green = new EditBox(red);
            green.PositionLayout = new Layout2d("30%", "5%");
            EditBox blue = new EditBox(red);
            blue.PositionLayout = new Layout2d("55%", "5%");
            ///////////////////

            red.TextChanged += (e, a) => {
                int.TryParse(red.Text, out r);
                int.TryParse(blue.Text, out b);
                int.TryParse(green.Text, out g);
                colorPick.Renderer.BackgroundColor = new Color((byte)r, (byte)g, (byte)b);
            };

            green.TextChanged += (e, a) => {
                int.TryParse(red.Text, out r);
                int.TryParse(blue.Text, out b);
                int.TryParse(green.Text, out g);
                colorPick.Renderer.BackgroundColor = new Color((byte)r, (byte)g, (byte)b);
            };

            blue.TextChanged += (e, a) => {
                int.TryParse(red.Text, out r);
                int.TryParse(blue.Text, out b);
                int.TryParse(green.Text, out g);
                colorPick.Renderer.BackgroundColor = new Color((byte)r, (byte)g, (byte)b);
            };

            ////TRIGGER COLOR/////
            int r1 = 255, g1 = 123, b1 = 15;
            Panel colorPick1 = new Panel();
            colorPick1.SizeLayout = new Layout2d("10%", "7%");
            colorPick1.PositionLayout = new Layout2d("80%", "15%");

            EditBox red1 = new EditBox();
            red1.PositionLayout = new Layout2d("5%", "15%");
            red1.MaximumCharacters = 3;

            red1.SizeLayout = new Layout2d("20%", "7%");
            red1.DefaultText = "255";

            EditBox green1 = new EditBox(red1);
            green1.PositionLayout = new Layout2d("30%", "15%");
            EditBox blue1 = new EditBox(red1);
            blue1.PositionLayout = new Layout2d("55%", "15%");
            ///////////////////

            red1.TextChanged += (e, a) => {
                int.TryParse(red1.Text, out r1);
                int.TryParse(blue1.Text, out b1);
                int.TryParse(green1.Text, out g1);
                colorPick1.Renderer.BackgroundColor = new Color((byte)r1, (byte)g1, (byte)b1);
            };

            green1.TextChanged += (e, a) => {
                int.TryParse(red1.Text, out r1);
                int.TryParse(blue1.Text, out b1);
                int.TryParse(green1.Text, out g1);
                colorPick1.Renderer.BackgroundColor = new Color((byte)r1, (byte)g1, (byte)b1);
            };

            blue1.TextChanged += (e, a) => {
                int.TryParse(red1.Text, out r1);
                int.TryParse(blue1.Text, out b1);
                int.TryParse(green1.Text, out g1);
                colorPick1.Renderer.BackgroundColor = new Color((byte)r1, (byte)g1, (byte)b1);
            };

            EditBox Name = new EditBox();
            Name.PositionLayout = new Layout2d("5%%", "50%");
            Name.SizeLayout = new Layout2d("60%", "7%");
            Name.Text = "FlameSim";
            Name.SetSelectedText(0, 0);
            Name.MaximumCharacters = 10;
            flameSimulationPanel.Add(Name);


            flameSimulationPanel.Add(red);
            flameSimulationPanel.Add(green);
            flameSimulationPanel.Add(blue);
            flameSimulationPanel.Add(colorPick);

            flameSimulationPanel.Add(red1);
            flameSimulationPanel.Add(green1);
            flameSimulationPanel.Add(blue1);
            flameSimulationPanel.Add(colorPick1);




            Button AddflameSim = new Button("Add");
            AddflameSim.PositionLayout = new Layout2d("70%", "90%");
            AddflameSim.Clicked += (e, a) =>
            {
                Color flameColor = new Color((byte)r, (byte)g, (byte)b);
                Color triggerColor = new Color((byte)r1, (byte)g1, (byte)b1);
                Console.WriteLine("Simulation added with color: " + flameColor);
                simulations.Add(new FlameSimulation(flameColor, triggerColor, fire_field, flame_field, Name.Text));
            };
            flameSimulationPanel.Add(AddflameSim);

            mainPanel.Add(flameSimulationPanel);
            flameSimulationPanel.Visible = false;

        }


        void lightningSimulationPanelInit()
        {
            lightningSimulationPanel = new Panel(originPanel);

            ////MAIN COLOR////
            int r = 15, g = 247, b = 255;
            Panel colorPick = new Panel();
            colorPick.SizeLayout = new Layout2d("10%", "7%");
            colorPick.PositionLayout = new Layout2d("80%", "5%");

            EditBox red = new EditBox();
            red.PositionLayout = new Layout2d("5%", "5%");
            red.MaximumCharacters = 3;

            red.SizeLayout = new Layout2d("20%", "7%");
            red.DefaultText = "255";

            EditBox green = new EditBox(red);
            green.PositionLayout = new Layout2d("30%", "5%");
            EditBox blue = new EditBox(red);
            blue.PositionLayout = new Layout2d("55%", "5%");
            ///////////////////

            red.TextChanged += (e, a) => {
                int.TryParse(red.Text, out r);
                int.TryParse(blue.Text, out b);
                int.TryParse(green.Text, out g);
                colorPick.Renderer.BackgroundColor = new Color((byte)r, (byte)g, (byte)b);
            };

            green.TextChanged += (e, a) => {
                int.TryParse(red.Text, out r);
                int.TryParse(blue.Text, out b);
                int.TryParse(green.Text, out g);
                colorPick.Renderer.BackgroundColor = new Color((byte)r, (byte)g, (byte)b);
            };

            blue.TextChanged += (e, a) => {
                int.TryParse(red.Text, out r);
                int.TryParse(blue.Text, out b);
                int.TryParse(green.Text, out g);
                colorPick.Renderer.BackgroundColor = new Color((byte)r, (byte)g, (byte)b);
            };

            ////TRIGGER COLOR/////
            int r1 = 255, g1 = 123, b1 = 15;
            Panel colorPick1 = new Panel();
            colorPick1.SizeLayout = new Layout2d("10%", "7%");
            colorPick1.PositionLayout = new Layout2d("80%", "15%");

            EditBox red1 = new EditBox();
            red1.PositionLayout = new Layout2d("5%", "15%");
            red1.MaximumCharacters = 3;

            red1.SizeLayout = new Layout2d("20%", "7%");
            red1.DefaultText = "255";

            EditBox green1 = new EditBox(red1);
            green1.PositionLayout = new Layout2d("30%", "15%");
            EditBox blue1 = new EditBox(red1);
            blue1.PositionLayout = new Layout2d("55%", "15%");
            ///////////////////

            red1.TextChanged += (e, a) => {
                int.TryParse(red1.Text, out r1);
                int.TryParse(blue1.Text, out b1);
                int.TryParse(green1.Text, out g1);
                colorPick1.Renderer.BackgroundColor = new Color((byte)r1, (byte)g1, (byte)b1);
            };

            green1.TextChanged += (e, a) => {
                int.TryParse(red1.Text, out r1);
                int.TryParse(blue1.Text, out b1);
                int.TryParse(green1.Text, out g1);
                colorPick1.Renderer.BackgroundColor = new Color((byte)r1, (byte)g1, (byte)b1);
            };

            blue1.TextChanged += (e, a) => {
                int.TryParse(red1.Text, out r1);
                int.TryParse(blue1.Text, out b1);
                int.TryParse(green1.Text, out g1);
                colorPick1.Renderer.BackgroundColor = new Color((byte)r1, (byte)g1, (byte)b1);
            };

            EditBox Name = new EditBox();
            Name.PositionLayout = new Layout2d("5%%", "50%");
            Name.SizeLayout = new Layout2d("60%", "7%");
            Name.Text = "LightSim";
            Name.SetSelectedText(0, 0);
            Name.MaximumCharacters = 10;
            lightningSimulationPanel.Add(Name);


            lightningSimulationPanel.Add(red);
            lightningSimulationPanel.Add(green);
            lightningSimulationPanel.Add(blue);
            flameSimulationPanel.Add(colorPick);

            lightningSimulationPanel.Add(red1);
            lightningSimulationPanel.Add(green1);
            lightningSimulationPanel.Add(blue1);
            lightningSimulationPanel.Add(colorPick1);




            Button AddlightningSim = new Button("Add");
            AddlightningSim.PositionLayout = new Layout2d("70%", "90%");
            AddlightningSim.Clicked += (e, a) =>
            {
                Color lightningColor = new Color((byte)r, (byte)g, (byte)b);
                Color triggerColor = new Color((byte)r1, (byte)g1, (byte)b1);
                Console.WriteLine("Simulation added with color: " + triggerColor);
                simulations.Add(new LightningSimulation(lightningColor, triggerColor, fire_field, image, Name.Text));
                scene.AddColor(Name.Text, lightningColor);
            };
            lightningSimulationPanel.Add(AddlightningSim);

            mainPanel.Add(lightningSimulationPanel);
            lightningSimulationPanel.Visible = false;

        }

        void rainSimulationPanelInit()
        {
            rainSimulationPanel = new Panel(originPanel);

            ////MAIN COLOR////
            int r = 48, g = 158, b = 252;
            Panel colorPick = new Panel();
            colorPick.SizeLayout = new Layout2d("10%", "7%");
            colorPick.PositionLayout = new Layout2d("80%", "5%");

            EditBox red = new EditBox();
            red.PositionLayout = new Layout2d("5%", "5%");
            red.MaximumCharacters = 3;

            red.SizeLayout = new Layout2d("20%", "7%");
            red.DefaultText = "255";

            EditBox green = new EditBox(red);
            green.PositionLayout = new Layout2d("30%", "5%");
            EditBox blue = new EditBox(red);
            blue.PositionLayout = new Layout2d("55%", "5%");
            ///////////////////

            red.TextChanged += (e, a) => {
                int.TryParse(red.Text, out r);
                int.TryParse(blue.Text, out b);
                int.TryParse(green.Text, out g);
                colorPick.Renderer.BackgroundColor = new Color((byte)r, (byte)g, (byte)b);
            };

            green.TextChanged += (e, a) => {
                int.TryParse(red.Text, out r);
                int.TryParse(blue.Text, out b);
                int.TryParse(green.Text, out g);
                colorPick.Renderer.BackgroundColor = new Color((byte)r, (byte)g, (byte)b);
            };

            blue.TextChanged += (e, a) => {
                int.TryParse(red.Text, out r);
                int.TryParse(blue.Text, out b);
                int.TryParse(green.Text, out g);
                colorPick.Renderer.BackgroundColor = new Color((byte)r, (byte)g, (byte)b);
            };

            ////TRIGGER COLOR/////
            int r1 = 255, g1 = 123, b1 = 15;
            Panel colorPick1 = new Panel();
            colorPick1.SizeLayout = new Layout2d("10%", "7%");
            colorPick1.PositionLayout = new Layout2d("80%", "15%");

            EditBox red1 = new EditBox();
            red1.PositionLayout = new Layout2d("5%", "15%");
            red1.MaximumCharacters = 3;

            red1.SizeLayout = new Layout2d("20%", "7%");
            red1.DefaultText = "255";

            EditBox green1 = new EditBox(red1);
            green1.PositionLayout = new Layout2d("30%", "15%");
            EditBox blue1 = new EditBox(red1);
            blue1.PositionLayout = new Layout2d("55%", "15%");
            ///////////////////

            red1.TextChanged += (e, a) => {
                int.TryParse(red1.Text, out r1);
                int.TryParse(blue1.Text, out b1);
                int.TryParse(green1.Text, out g1);
                colorPick1.Renderer.BackgroundColor = new Color((byte)r1, (byte)g1, (byte)b1);
            };

            green1.TextChanged += (e, a) => {
                int.TryParse(red1.Text, out r1);
                int.TryParse(blue1.Text, out b1);
                int.TryParse(green1.Text, out g1);
                colorPick1.Renderer.BackgroundColor = new Color((byte)r1, (byte)g1, (byte)b1);
            };

            blue1.TextChanged += (e, a) => {
                int.TryParse(red1.Text, out r1);
                int.TryParse(blue1.Text, out b1);
                int.TryParse(green1.Text, out g1);
                colorPick1.Renderer.BackgroundColor = new Color((byte)r1, (byte)g1, (byte)b1);
            };

            EditBox Name = new EditBox();
            Name.PositionLayout = new Layout2d("5%%", "50%");
            Name.SizeLayout = new Layout2d("60%", "7%");
            Name.Text = "FlameSim";
            Name.SetSelectedText(0, 0);
            Name.MaximumCharacters = 10;
            rainSimulationPanel.Add(Name);


            rainSimulationPanel.Add(red);
            rainSimulationPanel.Add(green);
            rainSimulationPanel.Add(blue);
            rainSimulationPanel.Add(colorPick);

            rainSimulationPanel.Add(red1);
            rainSimulationPanel.Add(green1);
            rainSimulationPanel.Add(blue1);
            rainSimulationPanel.Add(colorPick1);


            double watChan=0.1f, lightChance=0.08f;
            EditBox waterChance = new EditBox(red);
            waterChance.PositionLayout = new Layout2d("5%", "40%");
            waterChance.Text = "0.1";
            waterChance.SetSelectedText(0, 0);
            waterChance.MaximumCharacters = 6;
            EditBox lightningChance = new EditBox(waterChance);
            lightningChance.PositionLayout = new Layout2d("35%", "40%");
            lightningChance.Text = "0.08";
            lightningChance.SetSelectedText(0, 0);
            rainSimulationPanel.Add(waterChance);
            rainSimulationPanel.Add(lightningChance);



            Button AddrainSim = new Button("Add");
            AddrainSim.PositionLayout = new Layout2d("70%", "90%");
            AddrainSim.Clicked += (e, a) =>
            {
                Color rainColor = new Color((byte)r, (byte)g, (byte)b);
                Color lightningColor = new Color((byte)r1, (byte)g1, (byte)b1);
                Console.WriteLine("Simulation added with color: " + rainColor);
                double.TryParse(waterChance.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out watChan);
                double.TryParse(lightningChance.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out lightChance);
                simulations.Add(new RainSimulation(rainColor, lightningColor, image, fire_field, (float)watChan, (float)lightChance, Name.Text));
                Console.WriteLine(watChan);
                Console.WriteLine(lightChance);
            };
            rainSimulationPanel.Add(AddrainSim);

            mainPanel.Add(rainSimulationPanel);
            rainSimulationPanel.Visible = false;

        }


    }

}
