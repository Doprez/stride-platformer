using System.Collections.Specialized;
using System.Threading.Tasks;
using Doprez.Stride.AI.FSMs;
using Stride.Core.Collections;
using Stride.Engine;
using Stride.Physics;

namespace StridePlatformer.States;

public class IdleState : FSMState
{

	public PhysicsComponent PlayerSeenTrigger;

	private AnimationComponent _animationComponent;
	private MoveToState _moveTo;

	public IdleState(FSM fsm, AnimationComponent animationComponent, MoveToState moveTo)
	{
		FiniteStateMachine = fsm;
		_animationComponent = animationComponent;
		_moveTo = moveTo;

		FiniteStateMachine.States.Add((int)EnemyStates.Idle, this);
	}

	public override Task EnterState()
	{
		_animationComponent.Play("Idle");
		PlayerSeenTrigger.Collisions.CollectionChanged += CollisionsChanged;
		return Task.CompletedTask;
	}

	public override Task ExitState()
	{
		PlayerSeenTrigger.Collisions.CollectionChanged -= CollisionsChanged;
		return Task.CompletedTask;
	}

	public override async Task UpdateState()
	{
		//Need to have an awaitable method or else the program gets stuck here and wont render.
		await Task.Delay(100);
	}

	private void CollisionsChanged(object sender, TrackingCollectionChangedEventArgs args)
	{
		// Cast the argument 'Item' to a collision object
		var collision = (Collision)args.Item;

		// We need to make sure which collision object is not the Trigger collider
		// We perform a little check to find the ballCollider 
		var playerCollider = PlayerSeenTrigger == collision.ColliderA ? collision.ColliderB : collision.ColliderA;

		if (args.Action == NotifyCollectionChangedAction.Add)
		{
			// When a collision has been added to the collision collection, we know an object has 'entered' our trigger
			if (playerCollider.Entity.Name == "PlayerCharacter")
			{
				var entitycollided = playerCollider.Entity;
				_moveTo.Target = entitycollided.Transform.WorldMatrix.TranslationVector;
				FiniteStateMachine.SetCurrentState(_moveTo);
			}
		}
	}
	
}