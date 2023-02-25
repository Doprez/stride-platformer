using System.Net.Http.Headers;
using System.Collections.Specialized;
using Stride.Core.Collections;
using Stride.Engine;
using Stride.Physics;
using StridePlatformer.Player;
using StridePlatformer.Services;

namespace Controllers;

public class CoinController : StartupScript
{

	public PhysicsComponent Trigger;

	private GameStateService _gameState;

	public override void Start() 
	{
		base.Start();
		if(Trigger == null)
		{
			Trigger = Entity.Get<PhysicsComponent>();
		}

		Trigger.Collisions.CollectionChanged += CollisionsChanged;

		_gameState = Game.Services.GetService<GameStateService>();
		_gameState.AddCoinFromScene(Entity);
	}
	
	private void CollisionsChanged(object sender, TrackingCollectionChangedEventArgs args)
	{
		// Cast the argument 'item' to a collision object
		var collision = (Collision)args.Item;

		// We need to make sure which collision object is not the Trigger collider
		// We perform a little check to find the ballCollider 
		var collidedEntity = Trigger == collision.ColliderA ? collision.ColliderB : collision.ColliderA;

		if (args.Action == NotifyCollectionChangedAction.Add) 
		{
			// When a collision has been added to the collision collection, we know an object has 'entered' our trigger
			var playerCollided = collidedEntity.Entity.Get<PlayerController>();
			if(playerCollided != null)
			{
				_gameState.CollectCoin(Entity);
			}
		}
	}
}