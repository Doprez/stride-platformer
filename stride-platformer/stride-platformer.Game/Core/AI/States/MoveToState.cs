using System.Threading.Tasks;
using Doprez.Stride.AI.FSMs;
using Stride.Core.Mathematics;

public class MoveToState : FSMState
{
    public Vector3 Target;

    private Pathfinder _pathfinder;

    public MoveToState(Pathfinder pathfinder)
    {
        _pathfinder = pathfinder;
    }

    public override async Task EnterState()
    {
        await _pathfinder.SetWaypointAsync(Target);
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