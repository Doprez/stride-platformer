
using System.Collections.Specialized;
using System.Threading.Tasks;
using Doprez.Stride.AI.FSMs;
using Stride.Core.Collections;
using Stride.Engine;
using Stride.Physics;
using StridePlatformer.Data;

public class AttackState : FSMState
{

		public Entity EntityToTryAndHit;

		//constructor initiated
		private PhysicsComponent _attackTrigger;

		//general private vars
		private Entity _entitycollided;
		private PlayerData _playerData;

		public AttackState(PhysicsComponent attackTrigger)
		{
			_attackTrigger = attackTrigger;
		}

		public override async Task EnterState() 
		{
			await Task.Delay(100);
		}

		public override async Task ExitState() 
		{
			await Task.Delay(100);
		}

		public override async Task UpdateState() 
		{
			await Task.Delay(100);
		}
		
}