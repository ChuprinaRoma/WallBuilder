using System;
using System.Collections.Generic;
using System.IO;
using WallBuilder.Model;

namespace WallBuilder.Layer
{
    public class Txtriter : IDataGet
    {
        private string[] data;
        private int heithCount = 0;
        public Txtriter(string path)
        {
            try
            {
                data = File.ReadAllLines(path);
            }
            catch
            {
                Console.WriteLine("Path not in correct format.");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        public List<Brick> GetBricks()
        {
            List<Brick> bricks = null;
            int countBricks = 0;
            try
            {
                countBricks = Convert.ToInt32(data[heithCount+1]);
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
                string bricksTemp = data[(heithCount + 2) + i].Trim();
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

        public List<int[]> GetWall(string widthAndHeightWall)
        {
            int width = Convert.ToInt32(widthAndHeightWall.Split(' ')[0]);
            int height = Convert.ToInt32(widthAndHeightWall.Split(' ')[1]);
            List<int[]> wall = new List<int[]>();
            for (int i = 0; i < height; i++)
            {
                wall.Add(new int[width]);
            }
            for (int i = 0; i < wall.Count; i++)
            {
                for (int j = 0; j < wall[i].Length; j++)
                {
                    try
                    {
                        wall[i][j] = Convert.ToInt32(data[i+1][j].ToString());
                        if (wall[i][j] != 0 && wall[i][j] != 1)
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
                heithCount = i + 1;
            }
            return wall;
        }

        public string GetWidthAndHeightWall()
        {
            string widthAndHeightWall = data[0];
            if (!widthAndHeightWall.Contains(" "))
            {
                Console.WriteLine("Dimensions not in correct format.");
                Console.ReadKey();
                Environment.Exit(0);
            }
            return widthAndHeightWall;
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
            Console.WriteLine();
            Console.WriteLine($"Number of brick types:{bricks.Count}");
            foreach (var brick in bricks)
            {
                Console.WriteLine($"Brick: {brick.Width} {brick.Height} {brick.Count}");
            }
        }
    }
}
