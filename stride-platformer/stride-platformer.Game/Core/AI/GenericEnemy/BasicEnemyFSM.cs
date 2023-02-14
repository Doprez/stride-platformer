using System;
using System.Threading.Tasks;
using Doprez.Stride.AI.FSMs;
using Stride.Engine;

public class BasicEnemyFSM : FSM
{
    public Entity Player{get;set;}
    public PhysicsComponent AttackTrigger;

    private Pathfinder _pathfinder;
    private MoveToState _moveTo;
    private AttackState _attack;


    public override void Initialize()
    {
        _pathfinder = Entity.Get<Pathfinder>();

        if(_pathfinder == null)
            throw new InvalidOperationException("The Pathfinder component is not set");

        InitializeStates();

        SetCurrentState(_moveTo);
    }

    public override async Task AsyncUpdate()
    {
        await Task.Delay(100);

        if(Player.GetDistanceBetweenVectors(Entity.Transform.Position, _moveTo.Target) > 0.5f)
        {
            _moveTo.Target = Player.Transform.WorldMatrix.TranslationVector;

		    SetCurrentState(_moveTo);
        }
        else
        {
            SetCurrentState(_attack);
        }
    }

    private void InitializeStates()
    {
        _moveTo = new MoveToState(_pathfinder);
        _moveTo.Target = Player.Transform.WorldMatrix.TranslationVector;

        _attack = new AttackState(AttackTrigger, 500);
        _attack.EntityToTryAndHit = Player;
    }
}