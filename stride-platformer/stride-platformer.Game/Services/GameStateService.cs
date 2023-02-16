using Stride.Engine;

namespace StridePlatformer.Services;

public class GameStateService
{
	
	public Game RunningGameData { get; }

	public float PlayerHealth = 0;

	
	public GameStateService(Game game)
	{
		RunningGameData = game;
	}



}