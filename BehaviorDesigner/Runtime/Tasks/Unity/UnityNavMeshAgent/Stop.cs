using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001EB RID: 491
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Stop movement of this agent along its current path. Returns Success.")]
	public class Stop : Action
	{
		// Token: 0x06000AF8 RID: 2808 RVA: 0x000223B0 File Offset: 0x000205B0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x000223F0 File Offset: 0x000205F0
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return TaskStatus.Failure;
			}
			this.navMeshAgent.isStopped = true;
			return TaskStatus.Success;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00022419 File Offset: 0x00020619
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040006E5 RID: 1765
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006E6 RID: 1766
		private NavMeshAgent navMeshAgent;

		// Token: 0x040006E7 RID: 1767
		private GameObject prevGameObject;
	}
}
