
using System.Threading.Tasks;
using Doprez.Stride.AI.FSMs;
using Stride.Engine;

public class AttackState : FSMState
{

    public Entity EntityToTryAndHit;

    //constructor initiated
    private PhysicsComponent _attackTrigger;
    private int _attackDuration = 500;

    //general private vars
    private Entity _entitycollided;

    public AttackState(PhysicsComponent attackTrigger, int attackDuration)
    {
        _attackTrigger = attackTrigger;
        _attackDuration = attackDuration;
    }

    public override async Task EnterState()
    {
        _entitycollided = await _attackTrigger.GetCollidedEntity();
    }

    public override Task ExitState()
    {
        return Task.CompletedTask;
    }

    public override async Task UpdateState()
    {
        if(_entitycollided != null && EntityToTryAndHit == _entitycollided)
        {
            //HurtPlayer
        }
    }
}