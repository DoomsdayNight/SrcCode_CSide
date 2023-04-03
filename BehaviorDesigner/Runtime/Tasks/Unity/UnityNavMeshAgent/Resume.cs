using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001E5 RID: 485
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Resumes the movement along the current path after a pause. Returns Success.")]
	public class Resume : Action
	{
		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002205C File Offset: 0x0002025C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0002209C File Offset: 0x0002029C
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return TaskStatus.Failure;
			}
			this.navMeshAgent.isStopped = false;
			return TaskStatus.Success;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x000220C5 File Offset: 0x000202C5
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040006CE RID: 1742
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006CF RID: 1743
		private NavMeshAgent navMeshAgent;

		// Token: 0x040006D0 RID: 1744
		private GameObject prevGameObject;
	}
}
