using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
namespace SandySharp
{
    class Scene
    {
        public List<GameObject> gameObjects;
        List<Layer> graphicLayers;
        RenderWindow window;
        
        public Scene(RenderWindow _window)
        {
            gameObjects = new List<GameObject>();
            graphicLayers = new List<Layer>();
            window = _window;
        }
        public void AddLayer(Layer layer)//добавление нового слоя
        {
            graphicLayers.Add(layer);
            graphicLayers.OrderByDescending(lay => lay.order);  //сортировка списка слоев после добавления нового
            //TODO починить порядок слоев
        }

        public void AddObject(GameObject gameObject) 
        {
            gameObjects.Add(gameObject);
        }

        public void initialisation()
        {
            foreach(var obj in gameObjects)
            {
                obj.start();
            }
        }

        public void graphic_update()//отрисовка сцены
        {
            for(int i=0; i<graphicLayers.Count; i++)
            {
                window.Draw(graphicLayers[i].getSpriteToDraw());
                graphicLayers[i].picture.update_texture();
            }
        }


        public void update()
        {
            foreach(var obj in gameObjects)
            {
               if(obj.IsActive()) obj.update();
            }
        }


        //возвращает массив изображений, которые хранятся в слоях
        public Layer[] getLayers()
        {
            return graphicLayers.ToArray();
        }


        public GameObject Get_Gameobject_WithName(string _name)
        {
            foreach (var obj in gameObjects)
            {
                if (obj.name == _name) return obj;
            }
            return null;
        }



    }
}
