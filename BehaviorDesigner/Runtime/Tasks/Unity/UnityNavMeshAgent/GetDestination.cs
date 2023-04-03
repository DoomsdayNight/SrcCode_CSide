using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001DE RID: 478
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Gets the destination of the agent in world-space units. Returns Success.")]
	public class GetDestination : Action
	{
		// Token: 0x06000AC4 RID: 2756 RVA: 0x00021C84 File Offset: 0x0001FE84
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00021CC4 File Offset: 0x0001FEC4
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.navMeshAgent.destination;
			return TaskStatus.Success;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00021CF7 File Offset: 0x0001FEF7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x040006B4 RID: 1716
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006B5 RID: 1717
		[SharedRequired]
		[Tooltip("The NavMeshAgent destination")]
		public SharedVector3 storeValue;

		// Token: 0x040006B6 RID: 1718
		private NavMeshAgent navMeshAgent;

		// Token: 0x040006B7 RID: 1719
		private GameObject prevGameObject;
	}
}
