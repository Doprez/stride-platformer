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

    public MoveToState(Pathfinder pathfinder, AnimationComponent animationComponent)
    {
        _pathfinder = pathfinder;
        _animationComponent = animationComponent;
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