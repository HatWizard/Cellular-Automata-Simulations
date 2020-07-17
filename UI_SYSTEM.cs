using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using TGUI;
namespace SandySharp
{
    class UI_SYSTEM
    {
        private static UI_SYSTEM Instance;
        public Gui gui;
        private Panel MainPanel;
        private Panel BrushesPanel;
        private Panel LayerPanel;
        private Panel performancePanel;
        private Panel objectsControlPanel;
        private TextBox frameDelay;
        private Font font;
        //слои 
        Dictionary<string, Image> layers;
        
        public static UI_SYSTEM instance()
        {
            if (Instance == null) {
                Instance = new UI_SYSTEM();
            } 
            return Instance;
        }

        public void start()
        {

            
            if(gui!=null) gui.RemoveAllWidgets();
            gui = new Gui(SceneManager.instance().window);
            font = new Font("EightBitDragon-anqx.ttf");
            
            gui.Font = font;
            
            
            //Создание главной панели
            MainPanel = new Panel();
            Layout2d layout2D = new Layout2d("20%", "100%");
            MainPanel.SizeLayout = layout2D;
            MainPanel.Renderer.BackgroundColor = Color.Black;
            MainPanel.Renderer.BorderColor = Color.White;
            MainPanel.Renderer.Borders = new Outline(5);
            
            gui.Add(MainPanel);
            

            font = new Font("EightBitDragon-anqx.ttf");
            //string fontPath = System.IO.Directory.GetCurrentDirectory();
            //Console.WriteLine(fontPath);
            //Создание панели с кистями.
            BrushesPanel = new Panel();
            BrushesPanel.Renderer.BackgroundColor = Color.Black;
            BrushesPanel.Renderer.BorderColor = Color.White;
            BrushesPanel.PositionLayout = new Layout2d("0%", "5%");
            BrushesPanel.SizeLayout = new Layout2d("100%", "25%");
            MainPanel.Add(BrushesPanel);

            //Кисти.
            RadioButtonGroup brushes = new RadioButtonGroup();
            
            Dictionary<string, Color> tempColors = BrushManager.instance().getColors();  //получаем список цветов от менеджера кистей
            int positionOffset = 0; //определяет смещение для кнопки
            
            foreach(var colorKey in tempColors.Keys)
            {
                positionOffset += 10; 
                RadioButton colorBrush_Btn = new RadioButton(colorKey); //создаем кнопку с именем
                colorBrush_Btn.Renderer.TextColor = Color.White;
                colorBrush_Btn.Renderer.TextColorHover= Color.Blue;
                colorBrush_Btn.PositionLayout = new Layout2d("0%", positionOffset + "%"); //производим смещение
                colorBrush_Btn.Toggled += (e, a) => { if (colorBrush_Btn.Checked) BrushManager.instance().changeCurrentColor(colorKey); };  //привязываем к кнопке метод изменяющий текущий цвет у менеджера кистей
                brushes.Add(colorBrush_Btn); //добавляем кнопку на панель
                
            }
            
            BrushesPanel.Add(brushes);

            //список слоев
            ListBox LayerList = new ListBox();
            LayerList.Renderer.TextColor = Color.White;
            layers = new Dictionary<string, Image>();
            Layer[] tempLayerList = SceneManager.instance().currentScene.getLayers();
            foreach(var lay in tempLayerList)
            {
                //добавляем слои в словарь. При выборе значения в списке - значение будет передано словарю в качестве ключа и будет получение изображение, которое будет передано менеджеру кисти
                //таким образом рисование будет происходить на выбранном пользователем слое
                layers.Add(lay.name, lay.picture.image);
                LayerList.AddItem(lay.name, lay.name);
            }

            LayerList.ItemSelected += (e, a) => { BrushManager.instance().changeCurrentImage(layers[LayerList.GetSelectedItemId()]);};

            LayerList.Renderer.BackgroundColor = Color.Transparent;
            LayerPanel = new Panel();
            LayerPanel.Renderer.BackgroundColor = Color.Black;
            LayerPanel.Renderer.BorderColor = Color.White;
            LayerPanel.PositionLayout = new Layout2d("0%", "30%");
            LayerPanel.SizeLayout = new Layout2d("100%", "30%");
            LayerPanel.Renderer.Borders = new Outline(0,5,0,0);
            LayerPanel.Add(LayerList);
            MainPanel.Add(LayerPanel);

            //Данные производительности
            performancePanel = new Panel();
            performancePanel.Renderer.BackgroundColor = Color.Black;
            performancePanel.Renderer.BorderColor = Color.White;
            performancePanel.Renderer.Borders = new Outline(0, 5, 0, 5);
            performancePanel.PositionLayout = new Layout2d("0%", "50%");
            performancePanel.SizeLayout = new Layout2d("100%", "10%");

            frameDelay = new TextBox();
            frameDelay.Renderer.BackgroundColor = Color.Transparent;
            frameDelay.Renderer.TextColor = Color.White;
            performancePanel.Add(frameDelay);
            MainPanel.Add(performancePanel);


            //Контроль симуляций (включение/выключение)
            
            objectsControlPanel = new Panel();
            GameObject[] Scene_objects = SceneManager.instance().GetGameObjects();
            positionOffset = 0;
            foreach (var obj in Scene_objects)
            {
                CheckBox gameObjectSwitcher = new CheckBox();
                gameObjectSwitcher.Checked = true;
                gameObjectSwitcher.Text = obj.name;
                gameObjectSwitcher.Toggled += (e, a) => { obj.toggleActive(); };
                gameObjectSwitcher.PositionLayout = new Layout2d("0%", positionOffset + "%");
                gameObjectSwitcher.Renderer.TextColor = Color.White;
                gameObjectSwitcher.Renderer.TextColorHover = Color.Green;
                gameObjectSwitcher.Renderer.Font = font;
                positionOffset += 13;
                objectsControlPanel.Add(gameObjectSwitcher);
            }



            objectsControlPanel.Renderer.BackgroundColor = Color.Black;
            objectsControlPanel.Renderer.BorderColor = Color.White;
            objectsControlPanel.Renderer.Borders = new Outline(0, 5, 0, 5);
            objectsControlPanel.PositionLayout = new Layout2d("0%", "60%");
            objectsControlPanel.SizeLayout = new Layout2d("100%", "30%");
            
            MainPanel.Add(objectsControlPanel);

            //кнопки смены сцены
            //Button sceneNext = new Button("next>");
            //sceneNext.Clicked += (e, r) => { SceneManager.instance().switchScene(1); };
            //sceneNext.PositionLayout = new Layout2d("50%", "90%");
            //MainPanel.Add(sceneNext);

            //Button scenePrev = new Button("<prev");
            //scenePrev.Clicked += (e, r) => { SceneManager.instance().switchScene(-1); };
            //scenePrev.PositionLayout = new Layout2d("0%", "90%");
            //MainPanel.Add(scenePrev);

        }


        

        public void update(float timeDelayData)
        {
            frameDelay.Text = "Frame delay: " + timeDelayData;
        }

        public void Draw()
        {
            gui.Draw();
        }




    }





}
