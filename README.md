                                                                                # GenOfLife
                                                      ---Игра Жизнь Джона Конвея с применением генетического алгоритма---
                            ::: В этой вариации генетический алгоритм применяется для выработки генома самых приспособленных к правилам жизни (заданными условиями классической игры в жизнь) :::
                          ---Геном представляет собой битовый массив из 8 бит, каждый из которых отвечает за желание клетки размещатся в пустой клетке, у которой имеется n соседей,
                          где n - номер активного бита (равного 1) в геноме.
                              --При нескольких активных битах выбирается один случайным образом
                              --Мутация генома возникает с вероятностью 10%
                            Стоит напомнить условия клеточного автомата Джона Конвея:
                              --Клетка жива, если с ней рядом две живые клетки
                              --Клетка зарождается, если рядом с ней три живые клетки
                              --Клетка умирает, если у нее меньше 2 соседей или больше 3