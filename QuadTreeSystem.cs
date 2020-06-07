using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
namespace SandySharp
{

    public struct QuadTree
    {
        public QuadTree[] childQuads;
        public int startX, startY; //точка начала квадрата (верхний левый угол)
        public int endX, endY; //точка конца квадрата (нижний правый угол)
        public int depth;

        //возвращает ширину квадрата
        public int wigth
        {
            get { return endX - startX; }
        }

        //возвращает высоту квадрата
        public int height
        {
            get { return endY - startY; }
        }

        public QuadTree(int _startX, int _startY, int _endX, int _endY, int _depth)
        {
            startX = _startX;
            startY = _startY;
            endX = _endX;
            endY = _endY;
            depth = _depth;
            childQuads = new QuadTree[4];
        }

    }




    class QuadTreeSystem
    {
        List<QuadTree> quadsToUpdate;

      
        public void start()
        {
            quadsToUpdate = new List<QuadTree>();
        }


        //Проходим по всем пикселям в квадранте, если хотя бы  один из требует обновления - квадрант грязный.
        public bool IsDirty(QuadTree quad, Image image)
        {
            for(uint i = (uint)quad.startX; i<quad.endX; i++)
                for(uint j= (uint)quad.startY; j<quad.endY; j++)
                {
                    if (image.GetPixel(i, j) != Color.Transparent) return true;
                }
            return false;
        }


        public Queue<QuadTree> QuadTreeAlghorithm(QuadTree quad, Image field)
        {
            Queue<QuadTree> QuadsHeap = new Queue<QuadTree>();
            Queue<QuadTree> DirtyQuads = new Queue<QuadTree>();

            QuadsHeap.Enqueue(quad);
            while (QuadsHeap.Count!=0)
            {
                QuadTree currentQuad = QuadsHeap.Dequeue();
                if (currentQuad.depth < 3)
                {

                    if(IsDirty(quad, field))
                    {
                        foreach (var childQuad in cutQuad(currentQuad).childQuads)
                        {
                            QuadsHeap.Enqueue(childQuad);
                        }
                    }

                }
                else
                {
                    if(IsDirty(currentQuad, field)) DirtyQuads.Enqueue(currentQuad);
                }

            }

            return DirtyQuads;
        }




        //делит полученный quad на 4 новых quads и задаем им параметры.
        QuadTree cutQuad(QuadTree quad)
        {
            int tempWidth = quad.wigth / 2;
            int tempHeight = quad.height / 2;


            quad.childQuads[0] = new QuadTree(quad.startX,             quad.startY,              quad.startX + tempWidth,     quad.startY + tempHeight,     quad.depth+1);
            quad.childQuads[1] = new QuadTree(quad.startX+tempWidth, quad.startY,              quad.startX + quad.wigth,    quad.startY + tempHeight,     quad.depth+1);
            quad.childQuads[2] = new QuadTree(quad.startX,             quad.startY+tempHeight, quad.startX + tempWidth,     quad.startY + quad.height,    quad.depth+1);
            quad.childQuads[3] = new QuadTree(quad.startX+tempWidth, quad.startY+tempHeight, quad.startX + quad.wigth,    quad.startY + quad.wigth,     quad.depth+1);

            return quad;
        }


        public static void fillQuad(QuadTree quad, Image image)
        {
            for (uint i = (uint)quad.startX; i < quad.endX; i++)
                for (uint j = (uint)quad.startY; j < quad.endY; j++)
                {
                    image.SetPixel(i, j, Color.Cyan);
                }
        }


    }
}
