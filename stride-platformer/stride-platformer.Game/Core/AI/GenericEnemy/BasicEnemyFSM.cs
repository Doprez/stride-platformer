using System;
using Doprez.Stride.AI.FSMs;
using Stride.Core;
using Stride.Engine;
using StridePlatformer.States;
using Navigation;

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

	private AsyncPathfinder _pathfinder;

	//States
	private MoveToState _moveTo;
	private AttackState _attack;
	private IdleState _idle;


	public override void Start()
	{
		_pathfinder = Entity.Get<AsyncPathfinder>();

		if(_pathfinder == null)
			throw new InvalidOperationException("The Pathfinder component is not set");

		InitializeStates();

		SetCurrentState(_idle);
	}

	private void InitializeStates()
	{
		_moveTo = new MoveToState(this, _pathfinder, AnimationComponent);

		_attack = new AttackState(this, AttackTrigger, AnimationComponent);
		_attack.EntityToTryAndHit = Player;

		_idle = new IdleState(this, AnimationComponent, _moveTo);
		_idle.PlayerSeenTrigger = PlayerSeenTrigger;
	}

	public override void UpdateFSM()
	{
		
	}
}