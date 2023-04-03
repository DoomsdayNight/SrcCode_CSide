using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001E8 RID: 488
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Sets the destination of the agent in world-space units. Returns Success if the destination is valid.")]
	public class SetDestination : Action
	{
		// Token: 0x06000AEC RID: 2796 RVA: 0x00022200 File Offset: 0x00020400
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00022240 File Offset: 0x00020440
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return TaskStatus.Failure;
			}
			if (!this.navMeshAgent.SetDestination(this.destination.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00022277 File Offset: 0x00020477
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.destination = Vector3.zero;
		}

		// Token: 0x040006D9 RID: 1753
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006DA RID: 1754
		[SharedRequired]
		[Tooltip("The NavMeshAgent destination")]
		public SharedVector3 destination;

		// Token: 0x040006DB RID: 1755
		private NavMeshAgent navMeshAgent;

		// Token: 0x040006DC RID: 1756
		private GameObject prevGameObject;
	}
}
