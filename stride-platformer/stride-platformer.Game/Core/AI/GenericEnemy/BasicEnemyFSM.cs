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

	public override Task AsyncUpdate()
	{
		return Task.CompletedTask;
	}

	private void InitializeStates()
	{
		_moveTo = new MoveToState(this, _pathfinder, AnimationComponent);

		_attack = new AttackState(this, AttackTrigger, AnimationComponent);
		_attack.EntityToTryAndHit = Player;

		_idle = new IdleState(this, AnimationComponent, _moveTo);
		_idle.PlayerSeenTrigger = PlayerSeenTrigger;
	}
	
}