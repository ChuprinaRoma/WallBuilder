

namespace WallBuilder.Model
{
    public class Brick
    {
        public int ID { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Count { get; private set; }
        public int CountUsed { get; set; }
        public bool IsAreUsed { get; set; }

        public Brick(int id, int width, int height, int count)
        {
            ID = id;
            Width = width;
            Height = height;
            Count = count;
            CountUsed = 0;
            IsAreUsed = true;
        }
    }
}
