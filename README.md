                                                                                # GenOfLife
                                       ::: The game of life of John Conway with the use of a genetic algorithm. :::
              In this variation, the genetic algorithm is used to generate the genomes of the most adapted to the rules of life (given by the conditions of the classical game of life).
                              The genome is a bit array of 8 bits, each of which corresponds to the desire of the cell to settle in an empty cell that has n neighbors,
                                              where n is the number of active bits (equal to 1) in the genome.
                                      --When several active bits are selected, one is selected randomly 
                                      --Mutation of the genome occurs with a probability of 10%. 
                              It is worth recalling the conditions of John Conway’s cellular automaton: 
                              --A cell is alive if there are two living cells next to it 
                              --A cell arises if there are three living cells nearby 
                              --A cell dies if it has less than 2 neighbors or more than 3 neighbors.

<img align="center" src="https://Hackurity/GenOfLife/blob/main/work_example/example.png" />

