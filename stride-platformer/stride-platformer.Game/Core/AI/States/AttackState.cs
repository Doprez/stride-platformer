
using System.Collections.Specialized;
using System.Threading.Tasks;
using Doprez.Stride.AI.FSMs;
using Stride.Core.Collections;
using Stride.Engine;
using Stride.Physics;

public class AttackState : FSMState
{

    public Entity EntityToTryAndHit;

    //constructor initiated
    private PhysicsComponent _attackTrigger;
    private int _attackDuration = 500;

    //general private vars
    private Entity _entitycollided;

    public AttackState(PhysicsComponent attackTrigger, int attackDuration)
    {
        _attackTrigger = attackTrigger;
        _attackDuration = attackDuration;
    }

    public override Task EnterState() 
    {
		_attackTrigger.Collisions.CollectionChanged += CollisionsChanged;
        return Task.CompletedTask;
    }

    public override Task ExitState()
    {
		_attackTrigger.Collisions.CollectionChanged -= CollisionsChanged;
        return Task.CompletedTask;
    }

    public override Task UpdateState() 
    {
		return Task.CompletedTask;
    }

	private void CollisionsChanged(object sender, TrackingCollectionChangedEventArgs args) 
    {
		// Cast the argument 'item' to a collision object
		var collision = (Collision)args.Item;

		// We need to make sure which collision object is not the Trigger collider
		// We perform a little check to find the ballCollider 
		var playerCollider = _attackTrigger == collision.ColliderA ? collision.ColliderB : collision.ColliderA;

		if (args.Action == NotifyCollectionChangedAction.Add) {
			// When a collision has been added to the collision collection, we know an object has 'entered' our trigger
			if (playerCollider.Entity.Name == "PlayerCharacter") {
				_entitycollided = playerCollider.Entity;
			}
		}
	}
    
}