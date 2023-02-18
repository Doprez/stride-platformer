using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Doprez.Stride.AI.FSMs;
using Stride.Core.Collections;
using Stride.Engine;
using Stride.Physics;
using StridePlatformer.Data;

public class BasicEnemyFSM : FSM
{
	public Entity Player{get;set;}
	public PhysicsComponent AttackTrigger;

	private Pathfinder _pathfinder;
	private MoveToState _moveTo;
	private AttackState _attack;


	public override void Initialize()
	{
		_pathfinder = Entity.Get<Pathfinder>();

		if(_pathfinder == null)
			throw new InvalidOperationException("The Pathfinder component is not set");

		InitializeStates();
		
		AttackTrigger.Collisions.CollectionChanged += CollisionsChanged;

		SetCurrentState(_moveTo);
	}

	public override async Task AsyncUpdate()
	{
		await Task.Delay(100);

		if(Player.GetDistanceBetweenVectors(Entity.Transform.Position, Player.Transform.Position) > 0.5f)
		{
			_moveTo.Target = Player.Transform.WorldMatrix.TranslationVector;

			SetCurrentState(_moveTo);
		}
		else
		{
			SetCurrentState(_attack);
		}
	}

	private void InitializeStates()
	{
		_moveTo = new MoveToState(_pathfinder);
		_moveTo.Target = Player.Transform.WorldMatrix.TranslationVector;

		_attack = new AttackState(AttackTrigger, 500);
		_attack.EntityToTryAndHit = Player;
	}

	private void CollisionsChanged(object sender, TrackingCollectionChangedEventArgs args) 
	{
		// Cast the argument 'item' to a collision object
		var collision = (Collision)args.Item;

		// We need to make sure which collision object is not the Trigger collider
		// We perform a little check to find the ballCollider 
		var playerCollider = AttackTrigger == collision.ColliderA ? collision.ColliderB : collision.ColliderA;

		if (args.Action == NotifyCollectionChangedAction.Add) 
		{
			// When a collision has been added to the collision collection, we know an object has 'entered' our trigger
			if (playerCollider.Entity.Name == "PlayerCharacter") 
			{
				var entitycollided = playerCollider.Entity;
				entitycollided.Get<PlayerData>().PlayerHealth -= 10;
			}
		}
	}
	
}