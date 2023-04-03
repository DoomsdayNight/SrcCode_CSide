using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001E2 RID: 482
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Is the agent stopped?")]
	public class IsStopped : Conditional
	{
		// Token: 0x06000AD4 RID: 2772 RVA: 0x00021ECC File Offset: 0x000200CC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00021F0C File Offset: 0x0002010C
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return TaskStatus.Failure;
			}
			if (!this.navMeshAgent.isStopped)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00021F38 File Offset: 0x00020138
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040006C4 RID: 1732
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006C5 RID: 1733
		private NavMeshAgent navMeshAgent;

		// Token: 0x040006C6 RID: 1734
		private GameObject prevGameObject;
	}
}
