using System;
using WallBuilder.Layer;

namespace WallBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            new ManagerBuilder().Worker();
            Console.ReadKey();
        }
    }
}
