using Stride.Engine;

namespace StridePlatformer
{
    class StridePlatformerApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Run();
            }
        }
    }
}
