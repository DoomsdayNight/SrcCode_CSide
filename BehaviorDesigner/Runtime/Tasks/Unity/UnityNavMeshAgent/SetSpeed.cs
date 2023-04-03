using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001EA RID: 490
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Sets the maximum movement speed when following a path. Returns Success.")]
	public class SetSpeed : Action
	{
		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002231C File Offset: 0x0002051C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002235C File Offset: 0x0002055C
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return TaskStatus.Failure;
			}
			this.navMeshAgent.speed = this.speed.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002238F File Offset: 0x0002058F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.speed = 0f;
		}

		// Token: 0x040006E1 RID: 1761
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006E2 RID: 1762
		[Tooltip("The NavMeshAgent speed")]
		public SharedFloat speed;

		// Token: 0x040006E3 RID: 1763
		private NavMeshAgent navMeshAgent;

		// Token: 0x040006E4 RID: 1764
		private GameObject prevGameObject;
	}
}
