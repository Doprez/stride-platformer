using System.Threading.Tasks;
using Doprez.Stride.AI.FSMs;
using Stride.Core.Mathematics;
using Stride.Engine;

namespace StridePlatformer.States;

public class MoveToState : FSMState
{
    public Vector3 Target;

    private readonly Pathfinder _pathfinder;
	private readonly AnimationComponent _animationComponent;

    public MoveToState(FSM fsm, Pathfinder pathfinder, AnimationComponent animationComponent)
    {
        FiniteStateMachine = fsm;
        _pathfinder = pathfinder;
        _animationComponent = animationComponent;

		FiniteStateMachine.States.Add((int)EnemyStates.Walk, this);
    }

    public override async Task EnterState()
    {
        await _pathfinder.SetWaypointAsync(Target);
		_animationComponent.Play("Walk");
    }

    public override Task ExitState()
    {
        return Task.CompletedTask;
    }

    public override async Task UpdateState()
    {
        if(!_pathfinder.HasPath)
        {
            await _pathfinder.SetWaypointAsync(Target);
        }
    }
}