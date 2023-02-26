using System.Threading.Tasks;
using Doprez.Stride.AI.FSMs;
using Stride.Core.Mathematics;
using Stride.Engine;

namespace StridePlatformer.States;

public class MoveToState : FSMState
{
    public Entity Target;

    private readonly Pathfinder _pathfinder;
	private readonly AnimationComponent _animationComponent;

    private Vector3 _originalTargetPoint;

    public MoveToState(FSM fsm, Pathfinder pathfinder, AnimationComponent animationComponent)
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
        
    }

    public override void UpdateState()
    {
        if(!_pathfinder.HasPath || _originalTargetPoint != Target.WorldPosition())
        {
            _originalTargetPoint = Target.WorldPosition();
            _pathfinder.SetWaypoint(_originalTargetPoint);
        }

        if(Vector3.Distance(FiniteStateMachine.Entity.WorldPosition(), Target.WorldPosition()) < 0.5f)
        {
            FiniteStateMachine.SetCurrentState(FiniteStateMachine.GetState((int)EnemyStates.Attack01));
        }

    }
}