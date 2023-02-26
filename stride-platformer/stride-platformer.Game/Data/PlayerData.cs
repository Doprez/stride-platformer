

using Stride.Engine;
using StridePlatformer.Services;

namespace StridePlatformer.Data;

public class PlayerData : StartupScript
{

	public float PlayerHealth { get; set; } = 100;

	private GameStateService _gameState;
	
	public override void Start() 
	{
		base.Start();

		_gameState = Services.GetService<GameStateService>();
		_gameState.Player = Entity;
	}
}