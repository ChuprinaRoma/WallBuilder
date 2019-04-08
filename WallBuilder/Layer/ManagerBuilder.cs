using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WallBuilder.Model;

namespace WallBuilder.Layer
{
    public class ManagerBuilder
    {
        IDataGet dataGet = null;

        public void Worker()
        {
            bool isBuilt_By = false;
            Console.WriteLine("Select method load data: 1 - console enterd, 2 - txt");
            dataGet = GetDataGet(Console.ReadLine());
            string widthAndHeightWall = dataGet.GetWidthAndHeightWall();
            List<int[]> wall = dataGet.GetWall(widthAndHeightWall);
            List<Brick> bricks = dataGet.GetBricks();
            dataGet.WriteData(widthAndHeightWall, wall, bricks);
            bricks.Sort((c1, c2) => c2.Width.CompareTo(c1.Width));
            for(int i = 0; i < bricks.Count; i++)
            {
                if (i != 0)
                {
                    var buf = bricks[0];
                    bricks[0] = bricks[i];
                    bricks[i] = buf;
                }
                UsedCountToZero(bricks);
                List<int[]> newWall = GoBuilder(Convert.ToInt32(widthAndHeightWall.Split(' ')[0]), Convert.ToInt32(widthAndHeightWall.Split(' ')[1]), wall, bricks);
                if (newWall.FirstOrDefault(w => w.FirstOrDefault(arr => arr == 1) == 1) == null)
                {
                    Console.WriteLine("YES");
                    isBuilt_By = true;
                    return;
                }
            }
            if (!isBuilt_By)
            {
                Console.WriteLine("NO");
            }
        }


        private void UsedCountToZero(List<Brick> bricks)
        {
            for (int i = 0; i < bricks.Count; i++)
            {
                bricks[i].CountUsed = 0;
            }
        }

        private IDataGet GetDataGet(string select)
        {
            IDataGet dataGet = null;
            switch (select)
            {
                case "1":
                    {
                        dataGet = new Writer();
                        break;
                    }
                case "2":
                    {
                        Console.WriteLine("Enter the path of the data file For example: Data.txt");
                        dataGet = new Txtriter(Console.ReadLine());
                        break;
                    }
                default:
                    {
                        Console.WriteLine("not in correct format.");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;
                    }
            }
            return dataGet;
        }

        public List<int[]> GoBuilder(int widthWall, int heightWall, List<int[]> wall, List<Brick> bricks)
        {
            for (int i = 0; i < wall.Count; i++)
            {
                for (int j = 0; j < wall[i].Length; j++)
                {
                    if (wall[i][j] == 1)
                    {
                        for (int l = 0; l < bricks.Count; l++)
                        {
                            for (int y = 0; y < bricks[l].Count; y++)
                            {
                                if (bricks[l].CountUsed != bricks[l].Count)
                                {
                                    int tempj = j;
                                    int tempi = i;
                                    List<int[]> tempWall = CloneWall(wall);
                                    tempWall = GetNewAndCheckWall(tempWall, tempi, tempj, bricks[l].Width, bricks[l].Height, bricks[l].ID);
                                    if (tempWall == null)
                                    {
                                        if (bricks[l].Width != bricks[l].Height)
                                        {
                                            tempWall = CloneWall(wall);
                                            tempWall = GetNewAndCheckWall(tempWall, tempi, tempj, bricks[l].Height, bricks[l].Width, bricks[l].ID);
                                            if (tempWall != null)
                                            {
                                                if (CheckSupportBrick(wall, tempi, tempj, bricks[l].Width, bricks[l].Height))
                                                {
                                                    bricks[l].CountUsed++;
                                                    wall = tempWall;
                                                    if (tempj + bricks[l].Width <= wall.Count)
                                                    {
                                                        tempj += bricks[l].Height;
                                                    }
                                                    else
                                                    {
                                                        tempj = 0;
                                                        tempi++;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    else if (tempWall != null)
                                    {
                                        if (CheckSupportBrick(tempWall, tempi, tempj, bricks[l].Width, bricks[l].Height))
                                        {
                                            bricks[l].CountUsed++;
                                            wall = tempWall;
                                            if (tempj + bricks[l].Width <= wall[tempi].Length)
                                            {
                                                tempj += bricks[l].Width;
                                            }
                                            else
                                            {
                                                tempj = 0;
                                                tempi++;
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return wall;
        }

        private List<int[]> CloneWall(List<int[]> wall)
        {
            string jsonWall = JsonConvert.SerializeObject(wall);
            return JsonConvert.DeserializeObject<List<int[]>>(jsonWall);
        }


        public List<int[]> GetNewAndCheckWall(List<int[]> wall, int i, int j, int width, int height, int id)
         {
            int saveI = i;
            int saveJ = j;
             if ((height <= wall.Count && width <= wall[i].Length) && (height + i <= wall.Count && width + j <= wall[i].Length))
             {
                for (; i < height + saveI; i++)
                {
                    for (j = saveJ; j < width + saveJ; j++)
                    {
                        if (wall[i][j] == 1)
                        {
                            wall[i][j] = id;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            else
            {
                return null;
            }
             return wall;
        }


        public bool CheckSupportBrick(List<int[]> wall, int i, int j, int width, int height)
        {
            int saveJ = j;
            if (wall.Count == i + height)
            {
                return true;
            }
            for (; j < width + saveJ; j++)
            {
                if (wall.Count > height && wall[i+height][j] == 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}