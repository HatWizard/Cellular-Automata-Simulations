using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace SandySharp
{
    class LiquidSimulation : Simulation
    {
        Color liquid;
        bool DirectionSwap = false;
        Func<uint, bool> CycleDir;
        public LiquidSimulation(Color liquid_color, Image _field, string _name = "nameless")
        {

            liquid = liquid_color;
            field = _field;
            rand = new Random();
            name = _name;
        }
        public override void start()
        {
            
        }



        //TODO создать симмуляцию, генерирующую капли дождя
        public override void update()
        {

            liq_simulation();
        }


        private void liq_simulation()
        {


            //эта часть алгоритма отвечает за покадровую смену направления прохождения по массиву пикселей
            uint i;

            int iterator;

            if (DirectionSwap)
            {
                iterator = 1;
                CycleDir = DirLeft;
                i = 0;
            }
            else
            {
                iterator = -1;
                CycleDir = DirRight;
                i = field.Size.X - 2;
            }


            for (; CycleDir.Invoke(i); i += (uint)iterator)
                for (uint j = field.Size.Y-2; j > 0; j--)
                {

                    if (field.GetPixel(i, j) == liquid) //если пиксель отвечает за воду
                    {

                        //проверяем возможность движения вниз
                        if (field.GetPixel(i, j + 1) == Color.Transparent)
                            if (rand.Next() % 40 == 1)
                            {
                                if (field.GetPixel(i - 1, j) == Color.Transparent) Pixy.MovePixel(i, j, Left, field);
                                else
                                    if (field.GetPixel(i + 1, j) == Color.Transparent)
                                    Pixy.MovePixel(i, j, Right, field);
                            }
                            else
                            {
                                Pixy.MovePixelWithCols(i, j, down, field, 2);
                            }


                        else//проверяем возможность движения вниз-влево и вниз-вправо
                            if (field.GetPixel(i - 1, j + 1) == Color.Transparent)
                            Pixy.MovePixel(i, j, downL, field);
                        else
                            if (field.GetPixel(i + 1, j + 1) == Color.Transparent)
                            Pixy.MovePixel(i, j, downR, field);
                        else
                        {
                            //проверяем возможность движения влево и вправо (расползание)
                            if (field.GetPixel(i + 1, j) == Color.Transparent || field.GetPixel(i - 1, j) == Color.Transparent)
                            {
                                Vector2i dir = FindWaterBalanceDirection(i, j);
                                if (field.GetPixel(i - 1, j) == Color.Transparent && (dir == Left))
                                    Pixy.MovePixelWithCols(i, j, dir, field, rand.Next(1, 5));
                                else
                                    if (field.GetPixel(i + 1, j) == Color.Transparent && (dir == Right))
                                        Pixy.MovePixelWithCols(i, j, dir, field, rand.Next(1, 5));
                            }


                        }
                    }
                }
            //изменить направление прохождения по массиву
            DirectionSwap = !DirectionSwap;
        }


        Vector2i FindWaterBalanceDirection(uint x, uint y)//находит направление "расползания" жидкости
        {

            uint xR = x, xL = x;  //копируем значени координаты x
            bool RB = false;
            bool LB = false;

            //проверяем всю пиксели справа и слева от данного и ищем "провал", место где уровень жидкости будет меньше текущего
            do
            {
                xR++; //итераторы для правого и левого направления
                xL--;

                //если слеваа свободно, направление не было заблокировано и не вышли за границы - проверяем, есть ли там провал (пиксель на уровень ниже свободен)
                if (field.GetPixel(xL, y) == Color.Transparent && !LB && xL > 0)
                {
                    if (field.GetPixel(xL, y + 1) == Color.Transparent)
                    {
                       
                        return Left;

                    }   //если провал найден, возвращаем нужное направление
                }
                //если хотя бы одно из условий не выполняется - блокируем направление, чтобы не проверять его в дальнейших итерациях

                else LB = true;
                //аналогично справа
                if (field.GetPixel(xR, y) == Color.Transparent && !RB && xR < field.Size.X - 2)
                {
                    if (field.GetPixel(xR, y + 1) == Color.Transparent) 
                    {
                        
                        return Right;
                    }
                }

                else RB = true;
                //если оба направления заблокированы и провал не найден - останавливаем цикл.
            } while (!RB || !LB);
            //return zero;
            //случайное движение в одном из направлений
            if (rand.Next(0, 2) == 0)
            {

                return Left;
            }

            else
            {

                return Right;
            }


        }


        //вспомогательные методы для делегации
        bool DirLeft(uint i) { return i < field.Size.X - 2; }
        bool DirRight(uint i) { return i > 0; }

    }
}
