using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Doprez.Stride.AI.FSMs;
using Stride.Animations;
using Stride.Core;
using Stride.Core.Collections;
using Stride.Engine;
using Stride.Physics;
using StridePlatformer.Data;
using StridePlatformer.States;

public class BasicEnemyFSM : FSM
{
	[Display("Player Entity")]
	[DataMember(0)]
	public Entity Player{get;set;}
	[Display("Attack Trigger")]
	[DataMember(1)]
	public PhysicsComponent AttackTrigger;
	[Display("Player Seen Trigger")]
	[DataMember(2)]
	public PhysicsComponent PlayerSeenTrigger;

	[Display("Animation Component")]
	[DataMember(20)]
	public AnimationComponent AnimationComponent { get; set; }

	private Pathfinder _pathfinder;

	//States
	private MoveToState _moveTo;
	private AttackState _attack;
	private IdleState _idle;


	public override void Initialize()
	{
		_pathfinder = Entity.Get<Pathfinder>();

		if(_pathfinder == null)
			throw new InvalidOperationException("The Pathfinder component is not set");

		InitializeStates();

		SetCurrentState(_idle);
	}

	public override async Task AsyncUpdate()
	{
		await Task.Delay(100);

		/*if(Player.GetDistanceBetweenVectors(Entity.Transform.Position, Player.Transform.Position) > 0.5f)
		{
			_moveTo.Target = Player.Transform.WorldMatrix.TranslationVector;

			SetCurrentState(_moveTo);
		}
		else
		{
			SetCurrentState(_attack);
		}*/
	}

	private void InitializeStates()
	{
		_moveTo = new MoveToState(_pathfinder, AnimationComponent);
		//_moveTo.Target = Player.Transform.WorldMatrix.TranslationVector;
		_moveTo.FiniteStateMachine = this;

		_attack = new AttackState(AttackTrigger, AnimationComponent);
		_attack.EntityToTryAndHit = Player;
		_attack.FiniteStateMachine = this;

		_idle = new IdleState(AnimationComponent, _moveTo);
		_idle.PlayerSeenTrigger = PlayerSeenTrigger;
		_idle.FiniteStateMachine = this;
	}
	
}