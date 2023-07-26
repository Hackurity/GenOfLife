using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
  public class Cell
    {
       private bool[] genom;// одномерный массив типа bool для представления генома клетки
       public  bool alive;  //живая клетка или мертвая 
       private const int sizeOfGenom = 9;
       private int x;//координаты клетки на поле    возможно сделать поля readonly
       private int y;
       private int countNeighbours; //клетка хранит кол-во соседей
       public List<int> favoritePositions;
       // ДОБАВИТЬ ВОЗМОЖНОСТЬ ЗАДАЧИ ЦВЕТА КЛЕТКИ В ЗАВИСИМОСТИ ОТ ГЕНОМА

       //свойства под поля, которые используются вне класса
       public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public int CountNeighbours
        {
            get
            {
                return countNeighbours;
            }
            set
            {
                countNeighbours = value;
            }
        }

        //можно ли распарсить геном в этом месте???
        private void ParserGenom()
        {
            favoritePositions = new List<int>();
            for(int tripletForPars=0 ;tripletForPars<sizeOfGenom;tripletForPars++)
            {
                if(genom[tripletForPars])
                {
                    favoritePositions.Add(tripletForPars);
                }
            }
            
        }
        public void Mutation()
        {
            Random mutation = new Random();
            int numberTripletMutation = mutation.Next() % sizeOfGenom;
            genom[numberTripletMutation] = !genom[numberTripletMutation];
        }
        public bool[] getGenom()
        {
            bool[] copy_genom = genom;
           
            return copy_genom;
        }

        public Cell() //конструктор по умолчанию для создания изначального поколения клеток
        {
            Random randomTriplet = new Random();
            genom = new bool[sizeOfGenom];
            for(int triplet=0;triplet<sizeOfGenom;triplet++)
            {
                genom[triplet] = Convert.ToBoolean(randomTriplet.Next()%2);// возможно поменять на randomTriplet.Next(0,2)
            }
            //ТЕСТ
            genom[3] = false;
            genom[4] = false;
            ParserGenom();
        }
        public Cell(Cell parent_1, Cell parent_2, Cell parent_3)
        {
            Random randomForCrossover = new Random();
            genom = new bool[sizeOfGenom];
            for (int triplet=0;triplet<sizeOfGenom;triplet++)
            {
                int selectTripletFromParent = randomForCrossover.Next() % 3;
                switch(selectTripletFromParent)
                {
                    case 0:
                        genom[triplet] = parent_1.genom[triplet];
                        break;
                    case 1:
                        genom[triplet] = parent_2.genom[triplet];   //случайным образом передаем в значение триплета
                        break;                                      //триплет одного из 3-ох родителей
                    case 2:
                        genom[triplet] = parent_3.genom[triplet];
                        break;

                }
            }
            ParserGenom();
        }
    }
}
