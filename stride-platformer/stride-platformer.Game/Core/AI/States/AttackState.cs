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
	private readonly PhysicsComponent _attackCollider;

	//general private vars
	private readonly AnimationComponent _animationComponent;
	private readonly GameStateService _gameState;
	private PlayingAnimation _playingClip;
	private readonly float _enemyBaseDamage = 10;

	private const string _attackAnimName = "Attack";

	public AttackState(FSM fsm, PhysicsComponent attackCollider, AnimationComponent animationComponent)
	{
		FiniteStateMachine = fsm;
		_attackCollider = attackCollider;
		_animationComponent = animationComponent;

		FiniteStateMachine.States.Add((int)EnemyStates.Attack01, this);

		_gameState = FiniteStateMachine.Services.GetService<GameStateService>();
	}

	public override void EnterState()
	{
		_attackCollider.Enabled = true;
		_playingClip = _animationComponent.Play(_attackAnimName);
		_playingClip.RepeatMode = AnimationRepeatMode.PlayOnce;

		_attackCollider.Collisions.CollectionChanged += CollisionsChanged;
	}

	public override void ExitState()
	{
		//make sure we dont update the collision event when not in the attack state
		_attackCollider.Collisions.CollectionChanged -= CollisionsChanged;
		_attackCollider.Enabled = false;
	}

	public override void UpdateState()
	{
		//once the animation "Attack" has finished playing go back to the move state and chase player
		if (!_animationComponent.IsPlaying(_attackAnimName))
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
		var collidedEntity = _attackCollider == collision.ColliderA ? collision.ColliderB : collision.ColliderA;

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