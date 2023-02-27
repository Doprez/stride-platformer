using Stride.Engine;
using StridePlatformer.Services;
using UI.Views;

namespace StridePlatformer.Data;

public class PlayerData : StartupScript
{

	private float _playerHealth { get; set; } = 100;
	private GameStateService _gameState;
	private MainView _mainView;
	
	public override void Start() 
	{
		base.Start();

		_gameState = Services.GetService<GameStateService>();
		_gameState.Player = Entity;

		Services.ServiceAdded += ServiceAdded;
	}

	private void ServiceAdded(object sender, Stride.Core.ServiceEventArgs e)
	{
		if (e.ServiceType == typeof(MainView))
		{
			_mainView = Game.Services.GetService<MainView>();
		}
	}

	public void RemovePlayerHealth(float damage)
	{
		_playerHealth -= damage;
		//need to change game state when health hits 0
		_mainView.HealthBar.Value = _playerHealth;
	}

	public void AddPlayerHealth(float health)
	{
		_playerHealth += health;
		_mainView.HealthBar.Value = _playerHealth;
	}
}