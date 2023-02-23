
using System.Collections.Specialized;
using System.Threading.Tasks;
using Doprez.Stride.AI.FSMs;
using Stride.Core.Collections;
using Stride.Engine;
using Stride.Physics;
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

		//FiniteStateMachine.States.Add((int)EnemyStates.Attack01, this);
	}

	public override async Task EnterState() 
	{
		await Task.Delay(100);
	}

	public override async Task ExitState() 
	{
		await Task.Delay(100);
	}

	public override async Task UpdateState() 
	{
		await Task.Delay(100);
	}
		
}