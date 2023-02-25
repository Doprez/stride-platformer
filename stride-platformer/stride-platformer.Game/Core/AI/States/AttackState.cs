
using Doprez.Stride.AI.FSMs;
using Stride.Engine;
using StridePlatformer.Data;

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

	public AttackState(FSM fsm, PhysicsComponent attackTrigger, AnimationComponent animationComponent)
	{
		FiniteStateMachine = fsm;
		_attackTrigger = attackTrigger;
		_animationComponent = animationComponent;

		FiniteStateMachine.States.Add((int)EnemyStates.Attack01, this);
	}

	public override void EnterState() 
	{
		
	}

	public override void ExitState() 
	{
		
	}

	public override void UpdateState() 
	{
		
	}
		
}