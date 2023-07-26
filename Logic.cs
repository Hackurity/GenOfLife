using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GameOfLife
{
  public class Logic
    {
        public int countGeneration = 0;
        private Cell[,] population;
        private readonly int rows;
        private readonly int cols;
        private Random random = new Random();
        //string text = "log.txt";

        /*...............................*/
        
        //конструктор класса Logic
        public Logic(int rows, int cols, int density)
        {
            this.rows = rows;
            this.cols = cols;
            population = new Cell[cols, rows];

            //using (File.Create(text))
           

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    Cell someCell = new Cell();
                    someCell.X = x;
                    someCell.Y = y;
                    population[x, y] = someCell;
                    someCell.CountNeighbours = 0;
                    population[x,y].alive = random.Next((int)density) == 0;
                    //логика следующая: клетка становится живой если выполняется логическая операция
                    //random.Next((int)density) == 0;
                    /*if (population[x, y].alive)
                         File.AppendAllText(text, $"1");
                    else
                        File.AppendAllText(text, $"0");*/


                }
                //File.AppendAllText(text, $"\n");

            }
            // в конструкторе класса также нужно посчитать кол-во соседей для каждой клетки
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    population[x, y].CountNeighbours = CountNeighbours(x, y);
                    
                }
            }
        }
        //метод, отвечающий за переход клеток

        public void CellTransition()
        { 

            bool findedTransition = false;
        
            for(int x = 0; x < cols; x++)
            {
                for(int y = 0;  y < rows; y++)
                {
                    for (int i = -1; i < 2; i++)
                    {

                        if (findedTransition)
                        {
                            break;
                        }
                        for (int j = -1; j < 2; j++)
                        {
                            int col = (x + i + cols) % cols;
                            int row = (y + j + rows) % rows;

                            bool isSelfChecking = col == x && row == y;
                            bool abilityGo = population[col, row].alive == false;
                            if (abilityGo && !isSelfChecking)
                            {
                                var countNeighbours = population[col, row].CountNeighbours;
                                foreach (var value in population[col,row].favoritePositions)
                                {
                                    if (countNeighbours==value && population[col, row].alive == false)
                                    {
                                        //population[someCell.X, someCell.Y].alive = false;
                                        population[x,y].X = population[col, row].X;
                                        population[x,y].Y = population[col, row].Y;

                                        findedTransition = true;
                                        break;

                                    }
                                }
                            }
                        }

                    }
                }
            }
            
        }
/*.............................................*/


        public void NextGeneration()
        {
            //File.AppendAllText(text, "Generation \n _______________________ \n");
            //СЮДА НЕОБХОДИМО ДОБАВИТЬ ЛОГИКУ ГЕНЕТИЧЕСКОГО АЛГОРИТМА
            //ЗДЕСЬ ЖЕ РАСПАРСИТЬ ГЕНОМ НА ПОВЕДЕНИЕ КЛЕТКИ
            var newPopulation = new Cell[cols, rows];
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    Cell someCell = new Cell();
                    someCell.X = x;
                    someCell.Y = y;
                    someCell.CountNeighbours = CountNeighbours(x, y);

                    //var neighboursCount = CountNeighbours(x, y);
                    var hasLife = population[x, y].alive;
                    if (!hasLife && someCell.CountNeighbours == 3) // зарождение
                    {
                        List<Cell> parents = new List<Cell>();
                        for (int i = -1; i < 2; i++)
                        {
                            for (int j = -1; j < 2; j++)
                            {
                                int col = (x + i + cols) % cols;
                                int row = (y + j + rows) % rows;

                                bool isSelfChecking = col == x && row == y;
                                bool hasParent = population[col, row].alive;
                                if (hasParent && !isSelfChecking)
                                {
                                    parents.Add(population[col, row]);
                                }
                            }
                        }
                        /*File.AppendAllText(text, $"{x} {y} {population[x, y].alive} \n");

                        foreach(var p in parents)
                            File.AppendAllText(text, $"Parents {p.X} {p.Y} {p.alive} \n");
                        File.AppendAllText(text, $"\n \n");*/


                        someCell = new Cell(parents[0], parents[1], parents[2]);//создали клетку с геном от родителей
                        someCell.X = x; //присвоили поля x и y
                        someCell.Y = y;
                        someCell.alive = true;
                        someCell.CountNeighbours = population[x, y].CountNeighbours;

                        //здесь должно происходить скрещивание
                        //Сюда же добавляем наш метод мутаций, который будет срабатывать с определенным шансом
                        //К примеру 10%
                        Random varioty = new Random();
                        if (varioty.Next() % 10 == 0)
                        {
                            someCell.Mutation();
                        }
                         newPopulation[x, y] = someCell;

                    }
                    else if (hasLife && (someCell.CountNeighbours > 3 || someCell.CountNeighbours < 2))//условие смерти
                    {
                        someCell = population[x, y];
                        someCell.X = x;
                        someCell.Y = y;
                        someCell.alive = false;
                        newPopulation[x, y] = someCell;
                        //population[x, y].alive = false;
                    }

                    else
                    {
                        newPopulation[x, y] = population[x, y];
                    }
                    
                    //if(hasLife&&population[x,y].CountNeighbours==2) //условие остаться на месте
                    


                }
            }


            population = newPopulation;
        }


        public Cell[,] GetCurrentGeneration()
        {
            var result = new Cell[cols, rows];
            for(int x=0;x<cols;x++)
            {
                for(int y=0;y<rows;y++)
                {
                    result[x, y] = population[x, y];
                }
            }

            return result;
        }
        private int CountNeighbours(int x, int y)
        {
            int count = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int col = (x + i + cols) % cols;
                    int row = (y + j + rows) % rows;

                    bool isSelfChecking = col == x && row == y;
                    bool hasLife = population[col, row].alive;
                    if (hasLife && !isSelfChecking)
                    {
                        count++;
                    }
                }
            }
            
            return count;
        }

    }
}
