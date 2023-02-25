using System.Collections.Generic;
using Stride.Engine;

namespace StridePlatformer.Services;

public class GameStateService
{
	
	public Game RunningGameData { get; }

	public Entity Player { get; set; }
	
	//Coins registered in a scene
	public List<Entity> CoinsInScene { get; private set; } = new();

	
	public GameStateService(Game game)
	{
		RunningGameData = game;
	}

	public void AddCoinFromScene(Entity coin)
	{
		CoinsInScene.Add(coin);
	}

	public int GetCoinsRemaining()
	{
		return CoinsInScene.Count;
	}

	public void CollectCoin(Entity coin)
	{
		coin.FindRoot().DestroyEntity();
	}

}