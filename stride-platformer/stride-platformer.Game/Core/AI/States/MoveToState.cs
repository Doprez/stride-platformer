using Doprez.Stride.AI.FSMs;
using Stride.Core.Mathematics;
using Stride.Engine;
using Navigation;

namespace StridePlatformer.States;

public class MoveToState : FSMState
{
    public Entity Target;

    private readonly AsyncPathfinder _pathfinder;
	private readonly AnimationComponent _animationComponent;

    private Vector3 _originalTargetPoint;

    public MoveToState(FSM fsm, AsyncPathfinder pathfinder, AnimationComponent animationComponent)
    {
        FiniteStateMachine = fsm;
        _pathfinder = pathfinder;
        _animationComponent = animationComponent;

		FiniteStateMachine.States.Add((int)EnemyStates.Walk, this);
    }

    public override void EnterState()
    {
		_animationComponent.Play("Walk");
		_originalTargetPoint = Target.WorldPosition();
        _pathfinder.SetWaypoint(_originalTargetPoint);
    }

    public override void ExitState()
	{
		//reset forces the pathfinder to stop moving the unit
		_pathfinder.Reset();
    }

    public override void UpdateState()
    {
        if(!_pathfinder.HasPath || _originalTargetPoint != Target.WorldPosition())
        {
            _originalTargetPoint = Target.WorldPosition();
            _pathfinder.SetWaypoint(_originalTargetPoint);
        }

        if(_pathfinder.GetCurrentPathDistance < .7f)
        {
            FiniteStateMachine.SetCurrentState(FiniteStateMachine.GetState((int)EnemyStates.Attack01));
        }

    }
}