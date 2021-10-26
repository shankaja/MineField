using System;
using System.Collections;

namespace MineFieldGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Game Start");
            Console.WriteLine("MINEFIELD");
            Console.WriteLine("---------");
            ArrayList s = readInputFromConsole();
            if (!validateInput(s))
            {
                Console.WriteLine("Invalid input size ...");
                return;
            }

            int sizeX, sizeY;
            if (!defineMineField(s, out sizeX, out sizeY))
            {
                return;
            }

            Console.WriteLine("OUTPUT");
            Console.WriteLine("--------------------");
            int line = 1;
            Robot robot = null;
            foreach (var strLine in s)
            {
                if (line % 2 == 1)
                {
                    String[] position = strLine.ToString().Split(" ");
                    int xPos, yPos;
                    if (validateRobotCoordinates(position, out xPos, out yPos))
                    {
                        robot = new Robot(xPos, yPos, position[2], sizeX, sizeY);
                    }
                    else
                    {
                        return;
                    }
                }
                else if (robot != null)
                {
                    robot.setMoveString(strLine + "");
                    Console.WriteLine(robot.moveAndReturnPos());
                }
                line++;
            }
            moveEmptyMovingStrRobot(robot); // if last robot moving string is empty, final pos will be displayed
        }

        private static void moveEmptyMovingStrRobot(Robot robot)
        {
            if (robot != null && !robot.isMoved())
            {
                Console.WriteLine(robot.moveAndReturnPos()); // empty movingf string present robots will be handled
                robot.setMoved(true);
            }
        }


        /// <summary>
        /// Mine field size will be extracted from the user inputs
        /// </summary>
        /// <returns> validity of mine field size </returns>
        private static Boolean defineMineField(ArrayList s, out int sizeX, out int sizeY)
        {
            sizeX = 0;
            sizeY = 0;
            var v = s[0];
            if (v is String)
            {
                String[] xy = v.ToString().Split(" ");
                if (xy.Length != 2)
                {
                    Console.WriteLine("Mine field size defenition is invalid!");
                    return false;
                }
                sizeX = Int16.Parse(xy[0]);
                sizeY = Int16.Parse(xy[1]);
            }
            s.RemoveAt(0);
            return true;
        }


        /// <returns> validity of coordinates </returns>
        private static Boolean validateRobotCoordinates(string[] position, out int xPos, out int yPos)
        {
            xPos = 0;
            yPos = 0;
            try
            {
                xPos = Int16.Parse(position[0]);
                yPos = Int16.Parse(position[1]);
                if( position.Length != 3 )
                {
                    Console.WriteLine(" Coordinates are not complete with the direction !");
                    return false;
                }
                if(!"NESW".Contains(position[2]))
                {
                    Console.WriteLine(" Invalid direction found !");
                    return false;
                }
                return true;
            }
            catch (FormatException)
            {
                Console.WriteLine(" Coordinates are invalid for robot positioning !");
            }
            return false;
           
        }


        /// <summary>
        /// This function returns the user inputs extracted from the console
        /// </summary>
        /// <returns> input as a array list</returns>
        private static ArrayList readInputFromConsole()
        {
            var array = new ArrayList();
            Console.WriteLine("Please enter the minefield and robot coordinates");
            String line = Console.ReadLine().Trim();
            while (!"".Equals(line))
            {
                array.Add(line);
                line = Console.ReadLine().Trim();
            }
            return array;
        }

        /// <summary>
        /// This validates the input
        /// </summary>
        /// <param name="arrayList"></param>
        /// <returns></returns>
        private static Boolean validateInput(ArrayList arrayList)
        {
            return !(arrayList.Count <= 1);
        }
    }

    class Robot
    {
        private int x, y, sizeX, sizeY;
        private String movingString;
        private String direction;
        private Boolean moved;

        public Robot(int x, int y, String direction, int sizeX, int sizeY)
        {
            this.x = x;
            this.y = y;
            this.direction = direction;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            moved = false;
        }

        public void setMoveString(String moveStr)
        {
            this.movingString = moveStr;
        }


        /// <summary>
        /// Handling the robot moving operation
        /// and returning the final coordinates with the direction
        /// </summary>
        public String moveAndReturnPos()
        {
            if (movingString != null)
            { 
                foreach (Char c in movingString)
                {
                    if ('M'.Equals(c))
                    {
                        move();
                    }
                    else
                    {
                        this.direction = getDirection(c + "");
                    }
                }
            }
            moved = true;
            return x + " " + y + " " + direction;
        }

        /// <summary>
        /// Advancing the direction of Robot
        /// L - anti clockwise
        /// R - clockwise
        /// </summary>
        private String getDirection(String side)
        {
            switch (direction)
            {
                case "N":
                    if ("L".Equals(side))
                    {
                        return "W";
                    }
                    else
                    {
                        return "E";
                    }
                case "E":
                    if ("L".Equals(side))
                    {
                        return "N";
                    }
                    else
                    {
                        return "S";
                    }
                case "S":
                    if ("L".Equals(side))
                    {
                        return "E";
                    }
                    else
                    {
                        return "W";
                    }
                default:
                    if ("L".Equals(side))
                    {
                        return "S";
                    }
                    else
                    {
                        return "N";
                    }
            }
        }

        /// <summary>
        /// Moving the robot from one column
        /// If the moving axis end found, the robot will come from the reflective side
        /// </summary>
        private void move()
        {
            switch (direction)
            {
                case "N":
                    if(this.y==this.sizeY)
                    {
                        this.y = 1;
                    }
                    else
                    {
                        this.y++;
                    }
                    break;
                case "E":
                    if (this.x == this.sizeX)
                    {
                        this.x = 1;
                    }
                    else
                    {
                        this.x++;
                    }
                    break;
                case "S":
                    if (this.y == 1)
                    {
                        this.y = this.sizeY;
                    }
                    else
                    {
                        this.y--;
                    }
                    break;
                default:
                    if (this.x == 1)
                    {
                        this.x = this.sizeX;
                    }
                    else
                    {
                        this.x--;
                    }
                    break;
            }
                
        }

        public Boolean isMoved()
        {
            return this.moved;
        }

        public void setMoved(Boolean moved)
        {
            this.moved = moved;
        }
    }
}
