using System.Collections.Specialized;
using Doprez.Stride.AI.FSMs;
using Stride.Core.Collections;
using Stride.Engine;
using Stride.Physics;
using StridePlatformer.Data;
using StridePlatformer.Services;
using StridePlatformer.Player;
using Stride.Animations;

namespace StridePlatformer.States;

public class AttackState : FSMState
{

	public Entity EntityToTryAndHit;

	//constructor initiated
	private PhysicsComponent _attackTrigger;

	//general private vars
	private Entity _entitycollided;
	private AnimationComponent _animationComponent;
	private PlayerData _playerData;
	private GameStateService _gameState;
	private PlayingAnimation _playingClip;
	private float _enemyBaseDamage = 10;

	public AttackState(FSM fsm, PhysicsComponent attackTrigger, AnimationComponent animationComponent)
	{
		FiniteStateMachine = fsm;
		_attackTrigger = attackTrigger;
		_animationComponent = animationComponent;

		FiniteStateMachine.States.Add((int)EnemyStates.Attack01, this);

		_gameState = FiniteStateMachine.Services.GetService<GameStateService>();
	}

	public override void EnterState()
	{
		_playingClip = _animationComponent.Play("Attack");
		_playingClip.RepeatMode = AnimationRepeatMode.PlayOnce;

		_attackTrigger.Collisions.CollectionChanged += CollisionsChanged;
	}

	public override void ExitState()
	{
		_attackTrigger.Collisions.CollectionChanged -= CollisionsChanged;
	}

	public override void UpdateState()
	{
		if (!_animationComponent.IsPlaying("Attack"))
		{
			FiniteStateMachine.SetCurrentState(FiniteStateMachine.GetState((int)EnemyStates.Walk));
		}
	}

	private void CollisionsChanged(object sender, TrackingCollectionChangedEventArgs args)
	{
		// Cast the argument 'Item' to a collision object
		var collision = (Collision)args.Item;

		// We need to make sure which collision object is not the Trigger collider
		// We perform a little check to find the ballCollider 
		var collidedEntity = _attackTrigger == collision.ColliderA ? collision.ColliderB : collision.ColliderA;

		if (args.Action == NotifyCollectionChangedAction.Add)
		{
			// When a collision has been added to the collision collection, we know an object has 'entered' our trigger
			var playerCollided = collidedEntity.Entity.Get<PlayerController>();
			if (playerCollided != null)
			{
				_gameState.DamagePlayerHealth(_enemyBaseDamage);
				FiniteStateMachine.SetCurrentState(FiniteStateMachine.GetState((int)EnemyStates.Walk));
			}
		}
	}
		
}