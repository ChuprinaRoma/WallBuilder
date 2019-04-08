
using System;
using System.Collections.Generic;
using WallBuilder.Model;

namespace WallBuilder.Layer
{
    public class Writer : IDataGet
    {
        public string GetWidthAndHeightWall()
        {
            string widthAndHeightWall = null;
            Console.WriteLine("Enter wall dimensions (Format: Width, Height)");
            widthAndHeightWall = Console.ReadLine().Trim();
            if(!widthAndHeightWall.Contains(" "))
            {
                Console.WriteLine("Dimensions not in correct format.");
                Console.ReadKey();
                Environment.Exit(0);
            }
            return widthAndHeightWall;
        }

        public List<int[]> GetWall(string widthAndHeightWall)
        {
            int width = Convert.ToInt32(widthAndHeightWall.Split(' ')[0]);
            int height = Convert.ToInt32(widthAndHeightWall.Split(' ')[1]);
            List<int[]> wall = new List<int[]>();
            for(int i = 0; i < height; i++)
            {
                wall.Add(new int[width]);
            }
            Console.WriteLine("Enter a wall in the form of a matrix where \"0\" is an empty space, and \"1\" is part of the kerpich");
            Console.WriteLine("For example:");
            Console.WriteLine("1010101011");
            Console.WriteLine("0011111101");
            Console.WriteLine("1111111111");
            Console.WriteLine("Drive part of the wall through Enter");
            for(int i = 0; i < wall.Count; i++)
            {
                for (int j = 0; j < wall[i].Length; j++)
                {
                    try
                    {
                        wall[i][j] = Convert.ToInt32(Console.ReadLine());
                        if(wall[i][j] != 0 && wall[i][j] != 1)
                        {
                            Console.WriteLine("You can enter the numbers 0 and 1");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("You can enter the numbers 0 and 1");
                        Console.ReadKey(); 
                        Environment.Exit(0);
                    }
                }
            }
            return wall;
        }

        public List<Brick> GetBricks()
        {
            List<Brick> bricks = null;
            int countBricks = 0;
            Console.WriteLine("Enter the number of bricks");
            try
            {
                countBricks = Convert.ToInt32(Console.ReadLine());
                bricks = new List<Brick>();
            }
            catch
            {
                Console.WriteLine("You can only enter a number");
                Console.ReadKey();
                Environment.Exit(0);
            }
            for (int i = 0; i < countBricks; i++)
            {
                Console.WriteLine("Enter the size of the bricks and the number of such bricks (Format water: width >=8, height >=8, number)");
                string bricksTemp = Console.ReadLine().Trim();
                try
                {
                    if (Convert.ToInt32(bricksTemp.Split(' ')[0]) >= 8 || Convert.ToInt32(bricksTemp.Split(' ')[1]) >= 8)
                    {
                        Console.WriteLine("Width m height of brick can not be more than 8");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    bricks.Add(new Brick(bricks.Count + 2, Convert.ToInt32(bricksTemp.Split(' ')[0]), Convert.ToInt32(bricksTemp.Split(' ')[1]), Convert.ToInt32(bricksTemp.Split(' ')[2])));
                    
                }
                catch
                {
                    Console.WriteLine("Not in correct format.");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }
            return bricks;
        }

        public void WriteData(string widthAndHeightWall, List<int[]> wall, List<Brick> bricks)
        {
            Console.WriteLine("You entered:");
            Console.WriteLine($"WidthWall: {widthAndHeightWall.Split(' ')[0]}");
            Console.WriteLine($"HeightWall: {widthAndHeightWall.Split(' ')[1]}");
            Console.WriteLine(widthAndHeightWall);
            Console.WriteLine("View of the wall in the image of the matrix");
            foreach (var wallH in wall)
            {
                Console.WriteLine();
                foreach (var wallW in wallH)
                {
                    Console.Write(wallW);
                }
            }
            Console.WriteLine($"Number of brick types:{bricks.Count}");
            foreach (var brick in bricks)
            {
                Console.WriteLine($"Brick: {brick.Width} {brick.Height} {brick.Count}");
            }

        }
    }
}
