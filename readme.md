Minefield Game
===============

A robot’s position and location is represented by a combination of x and y co-ordinates and a letter representing one of the four cardinal compass points. 
The minefield is divided up into a grid to simplify navigation. An example position might be 0, 0, N, which means the rover is in the bottom left corner and facing North. 

In order to control a robot, user sends a simple string of letters as instructions. 
The possible letters are 'L', 'R' and 'M'.  'L' and 'R' makes the robot turn 90 degrees left or right respectively,
without moving from its current spot.  'M' means move forward one grid point, while maintaining the same heading direction.  

Assume that the square directly North from (x, y) is (x, y+1), the square directly East from (x, y) is (x+1, y) 


INPUTS: 
-------

The first line of input informs the robot is the upper-right coordinates of the minefield. The lower-left coordinates are assumed to be 0, 0.   

The rest of the input is instructions to robots. Each robot has two lines of input. The first line gives the robot's position, 
and the second line is a series of instructions telling the robot how to explore the minefield. 


The position is made up of two integers and a letter separated by spaces, corresponding to the x and y co-ordinates and the robot's orientation. 
Each robot will be finished sequentially, which means that the second robot won't start to move until the first one has finished moving. 


OUTPUT: 
-------

The output for each robot should be its final co-ordinates and heading direction. 

Test Input: 

5 5 

1 2 N 

LMLMLMLMM 

3 3 E 

MMRMMRMRRM 

 
Expected Output: 

1 3 N 

5 1 E 