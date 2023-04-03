using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001DF RID: 479
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Gets the stop status. Returns Success.")]
	public class GetIsStopped : Action
	{
		// Token: 0x06000AC8 RID: 2760 RVA: 0x00021D18 File Offset: 0x0001FF18
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00021D58 File Offset: 0x0001FF58
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.navMeshAgent.isStopped;
			return TaskStatus.Success;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00021D8B File Offset: 0x0001FF8B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = null;
		}

		// Token: 0x040006B8 RID: 1720
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006B9 RID: 1721
		[SharedRequired]
		[Tooltip("The stop status")]
		public SharedBool storeValue;

		// Token: 0x040006BA RID: 1722
		private NavMeshAgent navMeshAgent;

		// Token: 0x040006BB RID: 1723
		private GameObject prevGameObject;
	}
}
