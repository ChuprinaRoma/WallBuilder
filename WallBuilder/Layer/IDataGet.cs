using System.Collections.Generic;
using WallBuilder.Model;

namespace WallBuilder.Layer
{
    public interface IDataGet
    {
        string GetWidthAndHeightWall();
        List<int[]> GetWall(string widthAndHeightWall);
        List<Brick> GetBricks();
        void WriteData(string widthAndHeightWall, List<int[]> wall, List<Brick> bricks);
    }
}
