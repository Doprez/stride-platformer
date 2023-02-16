using Stride.Engine;
using StridePlatformer.Services;

namespace StridePlatformer
{
    class StridePlatformerApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Services.AddService(new GameStateService(game));
                game.Run();
            }
        }
    }
}
