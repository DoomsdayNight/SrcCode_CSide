using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001E6 RID: 486
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Sets the maximum acceleration of an agent as it follows a path, given in units / sec^2. Returns Success.")]
	public class SetAcceleration : Action
	{
		// Token: 0x06000AE4 RID: 2788 RVA: 0x000220D8 File Offset: 0x000202D8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00022118 File Offset: 0x00020318
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return TaskStatus.Failure;
			}
			this.navMeshAgent.acceleration = this.acceleration.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002214B File Offset: 0x0002034B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.acceleration = 0f;
		}

		// Token: 0x040006D1 RID: 1745
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006D2 RID: 1746
		[Tooltip("The NavMeshAgent acceleration")]
		public SharedFloat acceleration;

		// Token: 0x040006D3 RID: 1747
		private NavMeshAgent navMeshAgent;

		// Token: 0x040006D4 RID: 1748
		private GameObject prevGameObject;
	}
}
